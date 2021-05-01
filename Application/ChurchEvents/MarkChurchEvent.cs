using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ChurchEvents
{
    public class MarkChurchEvent
    {
        public class MarkEvent : IRequest
        {
            public Guid UserId { get; set; }
            public Guid ChurchEventId { get; set; }
        }

        public class MarkEventHandler : IRequestHandler<MarkEvent>
        {
            private readonly GNBCContext _context;
            public MarkEventHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(MarkEvent request, CancellationToken cancellationToken)
            {
                var currentEvent = await _context.ChurchEvents.Include(ce => ce.UserFavorites).ThenInclude(uf => uf.ChurchEvents).SingleOrDefaultAsync(ce => ce.Id == request.ChurchEventId);
                var currentUserFavorite = await _context.UserFavorites.Include(uf => uf.ChurchEvents).SingleOrDefaultAsync(uf => uf.UserId == request.UserId);

                bool userFavoriteExists = currentUserFavorite != null;
                bool eventExists = currentEvent != null;

                if(userFavoriteExists)
                {
                    if(eventExists)
                    {
                        currentUserFavorite.ChurchEvents.Add(currentEvent);
                        currentEvent.UserFavorites.Add(currentUserFavorite);

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404, "Event does not exist");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "User does not exist");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}