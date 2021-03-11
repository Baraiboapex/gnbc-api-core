using System;
using System.Collections.Generic;

namespace Domain
{
    public class Sermon : BaseEntity
    {
        public string SermonName {get; set;}
        public string SermonDescription {get; set;}
        public string SermonVideoLink {get; set;}
        public virtual ICollection<UserFavorite> UserFavorites {get; set;} 
    }
}
