using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain.InDTOs;

namespace Application.BlogPostCategories
{
    public class EditBlogCategory
    {
        public class ModifyBlogCategory : IRequest
        {
            public BlogPostCategoryDTO BlogPostCategoryToEdit { get; set; }
        }

        public class ModifyBlogCategoryHandler : IRequestHandler<ModifyBlogCategory>
        {
            private readonly GNBCContext _context;
            public ModifyBlogCategoryHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ModifyBlogCategory request, CancellationToken cancellationToken)
            {
                var currentBlogCategory = await _context.BlogPostCategories.FindAsync(request.BlogPostCategoryToEdit.Id);
                bool blogCategoryExists = currentBlogCategory != null;

                if(blogCategoryExists)
                {
                    currentBlogCategory.BlogPostCategoryName = request.BlogPostCategoryToEdit.BlogPostCategoryName;

                    _context.Attach(currentBlogCategory);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404,"Blog category does not exist!");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}