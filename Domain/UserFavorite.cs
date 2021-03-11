using System;
using System.Collections.Generic;

namespace Domain
{
    public class UserFavorite : BaseEntity
    {
        public int BibleStudyCount {get; set;}
        public int SermonCount {get; set;}
        public int BlogPostCount {get; set;}
        public Guid? UserId {get; set;}
        public virtual User User {get; set;}
        public virtual ICollection<BlogPost> BlogPosts {get; set;}
        public virtual ICollection<Sermon> Sermons {get; set;}
        public virtual ICollection<BibleStudy> BibleStudies {get; set;}
    }
}