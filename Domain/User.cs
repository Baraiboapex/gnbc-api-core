using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

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
        public bool IsAdmin {get; set;} = false;
        public bool CanBlog {get; set;} = false;
        public bool CanRecieveNotifications {get; set;} = false;
        public virtual ICollection<BlogPost> BlogPosts {get; set;}
        public virtual ICollection<BlogComment> BlogComments {get; set;}
        public virtual UserFavorite UserFavorite {get; set;}
    }
}