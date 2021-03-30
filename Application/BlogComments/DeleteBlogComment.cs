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
    public class DeleteBlogComment
    {
        public class RemoveComment : IRequest
        {
            public Guid BlogCommentId { get; set; }
        }

        public class RemoveCommentHandler : IRequestHandler<RemoveComment>
        {
            private readonly GNBCContext _context;
            public  RemoveCommentHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveComment request, CancellationToken cancellationToken)
            {
                var currentBlogComment = await _context.BlogComments.FindAsync(request.BlogCommentId);
                bool blogCommentExists = currentBlogComment != null;

                if(blogCommentExists)
                {
                    _context.BlogComments.Remove(currentBlogComment);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404,"Blog comment does not exist.");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}