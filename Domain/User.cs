using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Domain
{
    public class User : BaseEntity
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        [Required]
        public string Email{get; set;}
         [Required]
        public string Password {get; set;}
        public bool CanBlog {get; set;}
        public virtual ICollection<BlogPost> BlogPosts {get; set;}
        public virtual UserFavorite UserFavorite {get; set;}
    }
}