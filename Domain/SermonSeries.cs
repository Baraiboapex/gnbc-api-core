using System.Collections.Generic;

namespace Domain
{
    public class SermonSeries : BaseEntity
    {
        public string SeriesName {get; set;}
        public virtual ICollection<Sermon> Sermons {get; set;}
    }
}