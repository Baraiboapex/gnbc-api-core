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

namespace Application.BlogPostCategories
{
    public class AddBlogPostToBlogPostCategory
    {
        public class AttachBlogPostToBlogPostCategory : IRequest
        {
            public Guid BlogCategoryId { get; set; }

            public Guid BlogPostId { get; set; }
        }

        public class AttachBlogPostToBlogPostCategoryHandler : IRequestHandler<AttachBlogPostToBlogPostCategory>
        {
            private readonly GNBCContext _context;

            public AttachBlogPostToBlogPostCategoryHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AttachBlogPostToBlogPostCategory request, CancellationToken cancellationToken)
            {
                var currentBlogPostCategory = await _context.BlogPostCategories.SingleOrDefaultAsync(ss => ss.Id == request.BlogCategoryId);
                var currentBlogPost = await _context.BlogPosts.SingleOrDefaultAsync(s => s.Id == request.BlogPostId);

                bool blogCategoryExists = currentBlogPostCategory != null;
                bool blogPostExists = currentBlogPost != null;

                if(blogCategoryExists)
                {
                    if(blogPostExists)
                    {
                        currentBlogPostCategory.BlogPosts.Add(currentBlogPost);
                        _context.Entry(currentBlogPostCategory).State = EntityState.Modified;

                        _context.BlogPostCategories.Attach(currentBlogPostCategory);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404, "Blog post does not exist!");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "Blog category does not exist!");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}