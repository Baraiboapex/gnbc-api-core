using System;

namespace Domain.InDTOs
{
    public class BlogPostDTO
    {
        public Guid Id {get; set;}
        public Guid UserId {get; set;}
        public string BlogPostTitle {get; set;}
        public string BlogPostContent {get; set;}
        public string BlogPostImage {get; set;}
    }
}