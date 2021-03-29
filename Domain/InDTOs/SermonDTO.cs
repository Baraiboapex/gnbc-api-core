using System;

namespace Domain.InDTOs
{
    public class SermonDTO
    {
        public Guid Id {get; set;}
        public string SermonName {get; set;}
        public string SermonDescription {get; set;}
        public string SermonVideoLink {get; set;}
    }
}