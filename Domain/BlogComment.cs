using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class BlogComment : BaseEntity
    {
        [Required]
        [MinLength(20)]
        public string CommentContent {get; set;}
        public virtual BlogPost BlogPost {get; set;}
        public virtual User User {get; set;}
    }
}