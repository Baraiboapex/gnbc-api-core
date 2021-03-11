using System;
using System.Collections.Generic;

namespace Domain
{
    public class BibleStudy : BaseEntity
    {
        public string BibleStudyName {get; set;}
        public string BibleStudyDescription {get; set;}
        public string BibleStudyVideoLink {get; set;}
        public virtual ICollection<UserFavorite> UserFavorites {get; set;}
    }
}