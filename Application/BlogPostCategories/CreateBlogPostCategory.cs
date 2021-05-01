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
    public class CreateBlogPostCategory
    {
        public class AddBlogPostCategory : IRequest
        {
            public BlogPostCategoryDTO NewBlogPostCategory { get; set; }
        }

        public class AddBlogPostCategoryHandler : IRequestHandler<AddBlogPostCategory>
        {
            private readonly GNBCContext _context;
            public AddBlogPostCategoryHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddBlogPostCategory request, CancellationToken cancellationToken)
            {
                var currentCategory = await _context.BlogPostCategories.SingleOrDefaultAsync(bc => bc.BlogPostCategoryName == request.NewBlogPostCategory.BlogPostCategoryName);
                bool categoryDoesNotExist = currentCategory == null;

                if(categoryDoesNotExist)
                {
                    var blogCategory = new BlogPostCategory();

                    blogCategory.BlogPostCategoryName = request.NewBlogPostCategory.BlogPostCategoryName;

                    _context.BlogPostCategories.Add(blogCategory);
                    await _context.SaveChangesAsync();

                }else{
                    var newError = new NewError();

                    newError.AddValue(400,"Blog Category already exists!");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}