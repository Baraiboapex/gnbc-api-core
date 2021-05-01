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
    public class ShowOneBlogComment
    {
        public class GetOneBlogComment : IRequest<Hashtable>
        {
            public Guid BlogPostId { get; set; }
        }

        public class GetOneBlogCommentHandler : IRequestHandler<GetOneBlogComment, Hashtable>
        {
            private readonly GNBCContext _context;
            public GetOneBlogCommentHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Hashtable> Handle(GetOneBlogComment request, CancellationToken cancellationToken)
            {
                var currentBlogComment = await _context.BlogComments.FindAsync(request.BlogPostId);
                bool blogCommentExists = currentBlogComment != null;

                if(blogCommentExists)
                {
                    var outboundItemData = new OutboundDTO();

                    outboundItemData.AddField(new DictionaryEntry { Key = "BlogCommentId", Value = currentBlogComment.Id });
                    outboundItemData.AddField(new DictionaryEntry {Key = "CommentContent", Value = currentBlogComment.CommentContent});
                    
                    return outboundItemData.GetPayload();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404,"Blog comment does not exist.");

                    throw newError;
                }
            }
        }
    }
}