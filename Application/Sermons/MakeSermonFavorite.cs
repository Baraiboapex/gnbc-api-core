using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Sermons
{
    public class MakeSermonFavorite
    {
        public class AddToFavorites : IRequest
        {
            public Guid UserId {get; set;}
            public Guid SermonId {get; set;}
        }

         public class AddToFavoritesHandler : IRequestHandler<AddToFavorites>
        {
            private readonly GNBCContext _context;

            public AddToFavoritesHandler(GNBCContext context)
            {
                _context = context;
            }  

            public async Task<Unit> Handle(AddToFavorites request, CancellationToken cancellationToken)
            {
                var currentUser = await _context.Users.FindAsync(request.UserId);
                bool userDoesNotExist = currentUser == null;

                if(!userDoesNotExist)
                {   
                    var currentUserFavorite = await _context.UserFavorites.FindAsync(request.UserId);
                    bool userFavoriteDoesNotExist = currentUserFavorite == null;
                    
                    if(!userFavoriteDoesNotExist)
                    {
                        var currentSermon = await _context.Sermons.FindAsync(request.SermonId);
                        bool sermonDoesNotExist = currentSermon == null;
                    
                        if(!sermonDoesNotExist)
                        {
                            currentUser.UserFavorite.Sermons.Add(currentSermon);
                            currentSermon.UserFavorites.Add(currentUser.UserFavorite);

                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            throw new Exception("Blog post study does not exist");
                        }
                    }
                    else
                    {
                        throw new Exception("User favorite study does not exist");
                    }
                }
                else
                {
                    throw new Exception("User does not exist");
                }

                return Unit.Value;
            }
        }
    }
}