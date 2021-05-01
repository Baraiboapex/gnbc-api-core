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

            //Do not forget to add code to attach a user and the comment's parent blog object!!
            public async Task<Unit> Handle(AddBlogComment request, CancellationToken cancellationToken)
            {
                var currentUser = await _context.Users.Include(u => u.BlogComments).SingleOrDefaultAsync(u => u.Id == request.NewBlogComment.UserId);
                var currentBlogPost = await _context.BlogPosts.Include(bp => bp.BlogPostComments).SingleOrDefaultAsync(bp => bp.Id == request.NewBlogComment.BlogPostId);
               
                bool blogPostExists = currentBlogPost != null;
                bool userExists = currentUser != null;
               
                if (blogPostExists)
                {
                    BlogComment blogComment = new BlogComment();

                    blogComment.CommentContent = request.NewBlogComment.CommentContent;

                    currentBlogPost.BlogPostComments.Add(blogComment);

                    _context.BlogComments.Add(blogComment);
                    await _context.SaveChangesAsync();

                    if (userExists)
                    {
                        currentUser.BlogComments.Add(blogComment);
                        blogComment.User = currentUser;

                        _context.Entry(currentUser).State = EntityState.Modified;

                        _context.Users.Attach(currentUser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(400, "User does not exist.");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400, "Blog post does not exist.");

                    throw newError;
                }
                return Unit.Value;
            }
        }
    }
}