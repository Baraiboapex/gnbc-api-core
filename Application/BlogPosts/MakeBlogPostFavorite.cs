using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BlogPosts
{
    public class MakeBlogPostFavorite
    {
        public class AddToFavorites : IRequest
        {
            public Guid UserId {get; set;}
            public Guid BlogPostId {get; set;}
        }

         public class AddToFavoritesHandler : IRequestHandler<AddToFavorites>
        {
            private readonly GNBCContext _context;

            public AddToFavoritesHandler(GNBCContext context)
            {
                _context = context;
            }  

            public async Task<Unit> Handle(AddToFavorites request, CancellationToken cancellationToken)
            {
                var currentUserFavorite = await _context.UserFavorites.Include(uf => uf.BlogPosts).SingleOrDefaultAsync(u => u.UserId == request.UserId);
                var currentBlogPost = await _context.BlogPosts.Include(bp => bp.UserFavorites).SingleOrDefaultAsync(bp => bp.Id == request.BlogPostId);

                bool userExists = currentUserFavorite != null;
                bool blogPostExists = currentBlogPost != null;

                if(userExists)
                {
                    if(blogPostExists)
                    {
                        currentUserFavorite.BlogPosts.Add(currentBlogPost);
                        currentBlogPost.UserFavorites.Add(currentUserFavorite);

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404, "Blog post does not exist");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "User does not exist");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}