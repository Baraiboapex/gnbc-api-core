using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class BlogPostCategory : BaseEntity
    {
        private ICollection<BlogPost> _BlogPosts = new List<BlogPost>();

        [Required]
        public string BlogPostCategoryName {get; set;}
        public virtual ICollection<BlogPost> BlogPosts { get { return _BlogPosts; } set { _BlogPosts = value; } }
    }
}