using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BlogPostCategories
{
    public class RemoveBlogCategory
    {
        public class DeleteBlogCategory : IRequest
        {
            public Guid BlogCategoryId { get; set; }
        }

        public class DeleteBlogCategoryHandler : IRequestHandler<DeleteBlogCategory>
        {
            private readonly GNBCContext _context;
            public DeleteBlogCategoryHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteBlogCategory request, CancellationToken cancellationToken)
            {
                var currentBlogPostCategory = await _context.BlogPostCategories.FindAsync(request.BlogCategoryId);
                bool blogPostCategoryExists = currentBlogPostCategory != null;

                if(blogPostCategoryExists)
                {
                    _context.Remove(currentBlogPostCategory);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404,"Blog post category does not exist");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}