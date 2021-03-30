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
    public class DeleteBlogPost
    {
        public class RemoveBlogPost : IRequest
        {
            public Guid BlogPostId { get; set; }
        }

        public class RemoveBlogPostHandler : IRequestHandler<RemoveBlogPost>
        {
            private readonly GNBCContext _context;
            public RemoveBlogPostHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveBlogPost request, CancellationToken cancellationToken)
            {
                var currentBlogPost = await _context.BlogPosts.FindAsync(request.BlogPostId);
                bool blogPostDoesNotExist = currentBlogPost == null;

                if(!blogPostDoesNotExist)
                {
                    _context.BlogPosts.Remove(currentBlogPost);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "Blog post does not exists.");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}