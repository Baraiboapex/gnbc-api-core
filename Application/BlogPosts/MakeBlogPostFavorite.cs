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
                var currentUser = await _context.Users.FindAsync(request.UserId);
                bool userDoesNotExist = currentUser == null;

                if(!userDoesNotExist)
                {   
                    var currentUserFavorite = await _context.UserFavorites.FindAsync(request.UserId);
                    bool userFavoriteDoesNotExist = currentUserFavorite == null;
                    
                    if(!userFavoriteDoesNotExist)
                    {
                        var currentBlogPost = await _context.BlogPosts.FindAsync(request.BlogPostId);
                        bool blogPostDoesNotExist = currentBlogPost == null;
                    
                        if(!blogPostDoesNotExist)
                        {
                            currentUser.UserFavorite.BlogPosts.Add(currentBlogPost);
                            currentBlogPost.UserFavorites.Add(currentUser.UserFavorite);

                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newError = new NewError();

                            newError.AddValue(404, "Blog post does not exist.");

                            throw newError;
                        }
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404, "User favorite does not exist.");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "User does not exist.");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}