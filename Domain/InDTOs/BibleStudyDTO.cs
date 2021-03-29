using System;

namespace Domain.InDTOs
{
    public class BibleStudyDTO
    {
        public Guid Id {get; set;}
        public string BibleStudyName {get; set;}
        public string BibleStudyDescription {get; set;}
        public string BibleStudyVideoLink {get; set;}
    }
}