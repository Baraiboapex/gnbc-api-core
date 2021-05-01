using System;

namespace Domain.InDTOs
{
    public class BlogCommentDTO
    {
        public string CommentContent {get; set;}
        public Guid UserId {get; set;}
        public Guid BlogPostId {get; set;}
    }
}