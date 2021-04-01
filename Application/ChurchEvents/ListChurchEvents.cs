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
    public class ListChurchEvents
    {
        public class GetChurchEvents : IRequest<List<Hashtable>> { }

        public class GetChurchEventsHandler : IRequestHandler<GetChurchEvents, List<Hashtable>>
        {
            private readonly GNBCContext _context;
            public GetChurchEventsHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<List<Hashtable>> Handle(GetChurchEvents request, CancellationToken cancellationToken)
            {
                var churchEvents = await _context.ChurchEvents.ToListAsync();
                var loadChurchEvents = new List<Hashtable>();
                
                if(churchEvents.Count > 0)
                {
                    foreach(var churchEvent in churchEvents)
                    {
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventId", Value = churchEvent.Id });
                        outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventName", Value = churchEvent.ChurchEventName });
                        outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventDescription", Value = churchEvent.ChurchEventDescription });
                        outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventFacebookLink", Value = churchEvent.ChurchEventFacebookLink });
                        outboundItemData.AddField(new DictionaryEntry { Key = "ChurchEventImage", Value = churchEvent.ChurchEventImage });

                        loadChurchEvents.Add(outboundItemData.GetPayload());
                    }

                    return loadChurchEvents;
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400, "No church events were found.");
                    
                    throw newError;
                }
            }
        }
    }
}