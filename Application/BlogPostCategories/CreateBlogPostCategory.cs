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
                bool categoryDoesNotExist = (await _context.BlogPostCategories.FindAsync(request.NewBlogPostCategory)) == null;

                if(categoryDoesNotExist)
                {
                    var blogCategory = new BlogPostCategory();

                    blogCategory.BlogPostCategoryName = request.NewBlogPostCategory.BlogPostCategoryName;

                    _context.BlogPostCategories.Add(blogCategory);
                    await _context.SaveChangesAsync();

                }else{
                    throw new Exception("Blog Category already exists!");
                }

                return Unit.Value;
            }
        }
    }
}