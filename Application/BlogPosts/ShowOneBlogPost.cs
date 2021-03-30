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
    public class ShowOneBlogPost
    {
        public class ListOneBlogPost : IRequest<Hashtable>
        {
            public Guid BlogPostId { get; set; }
        }

        public class ListOneBlogPostHandler : IRequestHandler<ListOneBlogPost, Hashtable>
        {
             private readonly GNBCContext _context;

            public ListOneBlogPostHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Hashtable> Handle(ListOneBlogPost request, CancellationToken cancellationToken)
            {
                var blogPost = await _context.BlogPosts.FindAsync(request.BlogPostId);

                bool blogPostDoesNotExist = blogPost == null;

                if(!blogPostDoesNotExist)
                {
                    OutboundDTO outboundItemData = new OutboundDTO();

                    outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostId", Value = blogPost.Id });
                    outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostTitle", Value = blogPost.BlogPostTitle });
                    outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostContent", Value = blogPost.BlogPostContent });
                    outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostImage", Value = blogPost.BlogPostImage });

                    return outboundItemData.GetPayload();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "Blog post does not exist.");
                    
                    throw newError;
                }
            }
        }
    }
}