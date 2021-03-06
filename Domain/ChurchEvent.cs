using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Domain
{
    public class ChurchEvent : BaseEntity
    {
        [Required]
        public string ChurchEventName {get; set;}
        [Required]
        public string ChurchEventDescription {get; set;}
        [Required]
        public string ChurchEventDate {get; set;}
        public string ChurchEventFacebookLink {get; set;}
        public string ChurchEventImage {get; set;}
        public virtual ICollection<UserFavorite> UserFavorites {get; set;}
    }
}