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
    public class RemoveChurchEvent
    {
        public class DeleteChurchEvent : IRequest
        {
            public Guid ChurchEventId { get; set; }
        }

        public class DeleteChurchEventHandler : IRequestHandler<DeleteChurchEvent>
        {
            private readonly GNBCContext _context;
            public DeleteChurchEventHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteChurchEvent request, CancellationToken cancellationToken)
            {
                var currentChurchEvent = await _context.ChurchEvents.FindAsync(request.ChurchEventId);
                bool churchEventExists = currentChurchEvent != null;

                if(churchEventExists)
                {
                    _context.ChurchEvents.Remove(currentChurchEvent);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "Church event does not exist.");

                    throw newError;
                }
                return Unit.Value;
            }
        }
    }
}