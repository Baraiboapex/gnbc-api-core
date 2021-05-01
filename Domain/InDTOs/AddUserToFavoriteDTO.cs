using System;

namespace Domain.InDTOs
{
    public class AddUserToFavoriteDTO
    {
        public Guid ItemId {get; set;}
        public Guid ParentId {get; set;}
    }
}