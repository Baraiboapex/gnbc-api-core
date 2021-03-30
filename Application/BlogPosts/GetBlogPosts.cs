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
    public class GetBlogPosts
    {
        public class ListBlogPosts : IRequest<List<Hashtable>> { }

        public class ListOneBlogPost : IRequest<Hashtable>
        {
            public Guid BlogPostId { get; set; }
        }

        public class ListBlogPostsHandler : IRequestHandler<ListBlogPosts, List<Hashtable>>
        {
            private readonly GNBCContext _context;

            public ListBlogPostsHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<List<Hashtable>> Handle(ListBlogPosts request, CancellationToken cancellationToken)
            {
                var blogPosts = await _context.BlogPosts.ToListAsync();
                var currentBlogPostList = new List<Hashtable>();

                if(blogPosts.Count > 0)
                {
                    Task<List<Hashtable>> createBlogPostList = Task<List<Hashtable>>.Factory.StartNew(() =>
                    {
                        

                        foreach (var blogPost in blogPosts)
                        {
                            OutboundDTO outboundItemData = new OutboundDTO();

                            outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostId", Value = blogPost.Id });
                            outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostTitle", Value = blogPost.BlogPostTitle });
                            outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostContent", Value = blogPost.BlogPostContent });
                            outboundItemData.AddField(new DictionaryEntry { Key = "BlogPostImage", Value = blogPost.BlogPostImage });

                            currentBlogPostList.Add(outboundItemData.GetPayload());
                        }
                        return currentBlogPostList;
                    });

                    return await createBlogPostList;
                } 
                else 
                {
                    throw new Exception("No blog posts were found");
                }
            }
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

                    newError.AddValue(404, "No blog posts were found.");

                    throw newError;
                }
            }
        }
    }
}