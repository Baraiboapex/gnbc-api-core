using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.InDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.BlogPosts
{
    public class EditBlogPost
    {
        public class ModifyBlogPost : IRequest
        {
            public BlogPostDTO BlogPostToEdit { get; set; }
        }

        public class ModifyBlogPostHandler : IRequestHandler<ModifyBlogPost>
        {
            private readonly GNBCContext _context;
            public ModifyBlogPostHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ModifyBlogPost request, CancellationToken cancellationToken)
            {
                var currentBlogPost = await _context.BlogPosts.FindAsync(request.BlogPostToEdit.Id);
                bool blogPostDoesNotExist = currentBlogPost == null;

                if(!blogPostDoesNotExist)
                {
                    currentBlogPost.BlogPostTitle = request.BlogPostToEdit.BlogPostTitle;
                    currentBlogPost.BlogPostContent = request.BlogPostToEdit.BlogPostContent;
                    currentBlogPost.BlogPostImage = request.BlogPostToEdit.BlogPostImage;

                    _context.BlogPosts.Attach(currentBlogPost);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400, "Blog post does not exist");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}