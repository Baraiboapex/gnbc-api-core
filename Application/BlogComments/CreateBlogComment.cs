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
    public class CreateBlogComment
    {
        public class AddBlogComment : IRequest
        {
            public BlogCommentDTO NewBlogComment { get; set; }
        }

        public class AddBlogCommentHandler : IRequestHandler<AddBlogComment>
        {
            private readonly GNBCContext _context;
            public AddBlogCommentHandler(GNBCContext context)
            {
                _context = context;
            }

            //Do not forget to add code to attach a user and the comment's parent blopg object!!
            public async Task<Unit> Handle(AddBlogComment request, CancellationToken cancellationToken)
            {
                var blogComment = new BlogComment();

                blogComment.CommentContent = request.NewBlogComment.CommentContent;

                _context.BlogComments.Add(blogComment);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}