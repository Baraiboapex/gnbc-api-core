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
                var currentUserFavorite = await _context.UserFavorites.Include(uf => uf.Sermons).SingleOrDefaultAsync(uf => uf.UserId == request.UserId);
                var currentSermons = await _context.Sermons.Include(s => s.UserFavorites).SingleOrDefaultAsync(s => s.Id == request.SermonId);

                bool sermonExists = currentSermons != null;
                bool userExists = currentUserFavorite != null;

                if(userExists)
                {
                    if(sermonExists)
                    {
                        currentUserFavorite.Sermons.Add(currentSermons);
                        currentSermons.UserFavorites.Add(currentUserFavorite);

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404, "Sermon does not exist");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "Event does not exist");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}