using System;

namespace Domain.InDTOs
{
    public class ChurchEventDTO
    {
        public Guid Id {get; set;}
        public string ChurchEventName {get; set;}
        public string ChurchEventDescription {get; set;}
        public string ChurchEventDate {get; set;}
        public string ChurchEventFacebookLink {get; set;}
        public string ChurchEventImage {get; set;}
    }
}