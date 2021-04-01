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
    public class ShowOneChurchEvent
    {
        public class GetChurchEvent : IRequest<Hashtable>
        {
            public Guid ChurchEventId { get; set; }
        }

        public class GetChurchEventHandler : IRequestHandler<GetChurchEvent, Hashtable>
        {
            private readonly GNBCContext _context;
            public GetChurchEventHandler(GNBCContext context)
            {
                _context = context;

            }

            public async Task<Hashtable> Handle(GetChurchEvent request, CancellationToken cancellationToken)
            {
                var currentChurchEvent = await _context.ChurchEvents.FindAsync(request.ChurchEventId);
                bool churchEventExists = currentChurchEvent != null;

                if(churchEventExists)
                {
                    var outboundItemData = new OutboundDTO();

                    outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventId", Value = currentChurchEvent.Id });
                    outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventName", Value = currentChurchEvent.ChurchEventName });
                    outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventDescription", Value = currentChurchEvent.ChurchEventDescription });
                    outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventFacebookLink", Value = currentChurchEvent.ChurchEventFacebookLink });
                    outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventImage", Value = currentChurchEvent.ChurchEventImage });

                    return outboundItemData.GetPayload();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "Church event does not exist.");

                    throw newError;
                }
            }
        }
    }
}