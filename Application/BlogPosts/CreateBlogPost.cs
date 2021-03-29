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

                bool blogPostDoesNotExist = (await  _context.BlogPosts.FindAsync(request.PostToAdd.BlogPostTitle)) == null;

                if(blogPostDoesNotExist)
                {
                    var blogPost = new BlogPost();

                    blogPost.BlogPostTitle = request.PostToAdd.BlogPostTitle;
                    blogPost.BlogPostContent = request.PostToAdd.BlogPostContent;
                    blogPost.BlogPostImage = request.PostToAdd.BlogPostImage;

                    _context.BlogPosts.Add(blogPost);
                    await _context.SaveChangesAsync();
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