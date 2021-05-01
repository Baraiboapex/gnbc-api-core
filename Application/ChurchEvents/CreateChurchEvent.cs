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
    public class CreateChurchEvent
    {
        public class AddChurchEvent : IRequest
        {
            public ChurchEventDTO NewChurchEvent { get; set; }
        }

        public class AddChurchEventHandler : IRequestHandler<AddChurchEvent>
        {
            private readonly GNBCContext _context;
            public AddChurchEventHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddChurchEvent request, CancellationToken cancellationToken)
            {
                var currentChurchEvent = await _context.ChurchEvents.SingleOrDefaultAsync(ce => ce.ChurchEventName == request.NewChurchEvent.ChurchEventName);
                bool churchEventDoesNotExist = currentChurchEvent == null;

                if(churchEventDoesNotExist)
                {
                    var churchEvent = new ChurchEvent();

                    churchEvent.ChurchEventName = request.NewChurchEvent.ChurchEventName;
                    churchEvent.ChurchEventDescription = request.NewChurchEvent.ChurchEventDescription;
                    churchEvent.ChurchEventImage = request.NewChurchEvent.ChurchEventImage;
                    churchEvent.ChurchEventDate = request.NewChurchEvent.ChurchEventDate;
                    churchEvent.ChurchEventFacebookLink = request.NewChurchEvent.ChurchEventFacebookLink;

                    _context.ChurchEvents.Add(churchEvent);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400, "Church event already exists.");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}