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
    public class EditChurchEvent
    {
        public class ModifyChurchEvent : IRequest
        {
            public ChurchEventDTO ChurchEventToEdit { get; set; }
        }

        public class ModifyChurchEventHandler : IRequestHandler<ModifyChurchEvent>
        {
            private readonly GNBCContext _context;
            public ModifyChurchEventHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ModifyChurchEvent request, CancellationToken cancellationToken)
            {
                var currentChurchEvent = await _context.ChurchEvents.FindAsync(request.ChurchEventToEdit.Id);
                bool churchEventExists = currentChurchEvent != null;

                if(churchEventExists)
                {
                    currentChurchEvent.ChurchEventName = request.ChurchEventToEdit.ChurchEventName;
                    currentChurchEvent.ChurchEventDescription = request.ChurchEventToEdit.ChurchEventDescription;
                    currentChurchEvent.ChurchEventImage = request.ChurchEventToEdit.ChurchEventImage;
                    currentChurchEvent.ChurchEventDate = request.ChurchEventToEdit.ChurchEventDate;
                    currentChurchEvent.ChurchEventFacebookLink = request.ChurchEventToEdit.ChurchEventFacebookLink;

                   _context.ChurchEvents.Attach(currentChurchEvent);
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