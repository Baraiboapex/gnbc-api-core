using System;

namespace Domain.InDTOs
{
    public class AddUserToFavoriteDTO
    {
        public Guid ItemId {get; set;}
        public Guid UserId {get; set;}
    }
}