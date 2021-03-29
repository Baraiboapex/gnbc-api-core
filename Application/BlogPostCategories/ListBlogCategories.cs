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
    public class ListBlogCategories
    {
        public class GetCategories : IRequest<List<Hashtable>> { }

        public class GetCategoriesHandler : IRequestHandler<GetCategories, List<Hashtable>>
        {
            private readonly GNBCContext _context;
            public GetCategoriesHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<List<Hashtable>> Handle(GetCategories request, CancellationToken cancellationToken)
            {
                var blogCategories = await _context.BlogPostCategories.ToListAsync();
                var blogCategoriesAttach = new List<Hashtable>();
                
                if(blogCategories.Count < 0)
                {
                    foreach(var category in blogCategories)
                    {
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "BlogCategoryId", Value = category.Id });
                        outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostCategoryName", Value = category.BlogPostCategoryName });
                        outboundItemData.AddField(new DictionaryEntry { Key = "BlogPosts", Value = category.BlogPosts });

                        blogCategoriesAttach.Add(outboundItemData.GetPayload());
                    }

                    return blogCategoriesAttach;
                }
                else
                {
                    throw new Exception("No blog posts were found");
                }
            }
        }
    }
}