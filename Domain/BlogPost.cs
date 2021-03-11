using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class BlogPost : BaseEntity
    {
        [Required]
        public string BlogPostTitle {get; set;}
        [Required]
        [MinLength(20)]
        public string BlogPostContent {get; set;}
        public string BlogPostImage {get; set;}
        public virtual ICollection<BlogComment> BlogPostComments {get; set;}
        public virtual ICollection<BlogPostCategory> BlogCategories {get; set;}
        public virtual ICollection<UserFavorite> UserFavorites {get; set;}
        public virtual User User {get; set;}
    }
}