using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class BlogPostCategory : BaseEntity
    {
        [Required]
        public string BlogPostCategoryName {get; set;}
        public virtual ICollection<BlogPost> BlogPosts {get; set;}
    }
}