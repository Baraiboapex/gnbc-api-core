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

namespace Application.BlogComments
{
    public class ListBlogComments
    {
        public class GetBlogComments : IRequest<List<Hashtable>> 
        {
            public Guid BlogPostId {get; set;}
        }

        public class GetBlogCommentsHandler : IRequestHandler<GetBlogComments, List<Hashtable>>
        {
            private readonly GNBCContext _context;
            public GetBlogCommentsHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<List<Hashtable>> Handle(GetBlogComments request, CancellationToken cancellationToken)
            {
                var blogPost = await _context.BlogPosts.Include(bc => bc.BlogPostComments).ThenInclude(bc => bc.User).SingleOrDefaultAsync(bp => bp.Id == request.BlogPostId);
                var blogPostComments = blogPost.BlogPostComments;

                if(blogPostComments.Count > 0)
                {
                    var blogCommentsAttach = new List<Hashtable>();

                    foreach(var blogComment in blogPostComments)
                    {
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "BlogCommentId", Value = blogComment.Id });
                        outboundItemData.AddField(new DictionaryEntry {Key = "CommentContent", Value = blogComment.CommentContent});

                        var outboundSecondChildItem = new OutboundDTO();

                        outboundSecondChildItem.AddField(new DictionaryEntry { Key = "UserId", Value = blogComment.User.Id });
                        outboundSecondChildItem.AddField(new DictionaryEntry { Key = "Email", Value = blogComment.User.Email });

                        outboundItemData.AddField(new DictionaryEntry { Key = "User", Value = outboundSecondChildItem.GetPayload() });

                        blogCommentsAttach.Add(outboundItemData.GetPayload());
                    }

                    return blogCommentsAttach;
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404,"No blog comments were found");

                    throw newError;
                }
            }
        }
    }
}