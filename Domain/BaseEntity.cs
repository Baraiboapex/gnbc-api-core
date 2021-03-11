using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class BaseEntity
    {
        
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get; set;}
        public DateTime? DateCreated {get; set;}
        public DateTime? DateModified {get; set;}

        public BaseEntity()
        {
            DateTime currentTime = DateTime.UtcNow;
            DateTime localTime = currentTime.ToLocalTime();
            DateCreated = localTime;
        }
    }
}