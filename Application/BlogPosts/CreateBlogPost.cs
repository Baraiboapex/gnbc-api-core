using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BlogPosts
{
    public class CreateBlogPost
    {
        
        public class AddBlogPost : IRequest
        {
            public BlogPostDTO PostToAdd { get; set; }
        }

        public class AddBlogPostHandler : IRequestHandler<AddBlogPost>
        {
            private readonly GNBCContext _context;
            public AddBlogPostHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddBlogPost request, CancellationToken cancellationToken)
            {
                var currentBlog = await _context.BlogPosts.SingleOrDefaultAsync(bp => bp.BlogPostTitle == request.PostToAdd.BlogPostTitle);
                var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.PostToAdd.UserId);
                bool blogPostDoesNotExist = currentBlog == null;
                bool userExists = currentUser != null;

                if(blogPostDoesNotExist)
                {
                    var blogPost = new BlogPost();

                    blogPost.BlogPostTitle = request.PostToAdd.BlogPostTitle;
                    blogPost.BlogPostContent = request.PostToAdd.BlogPostContent;
                    blogPost.BlogPostImage = request.PostToAdd.BlogPostImage;

                    _context.BlogPosts.Add(blogPost);
                    await _context.SaveChangesAsync();

                    if(userExists)
                    {
                        currentUser.BlogPosts.Add(blogPost);
                        blogPost.User = currentUser;

                        _context.Entry(currentUser).State = EntityState.Modified;

                        _context.Users.Attach(currentUser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404, "User does not exist");

                        throw newError;
                    }
                }
                else
                {
                    
                    var newError = new NewError();

                    newError.AddValue(400, "Blog post already exists");

                    throw newError;
                }

                return Unit.Value;
            }
        }

    }
}