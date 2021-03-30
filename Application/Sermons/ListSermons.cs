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
    public class ListSermons
    {
        public class GetSermons : IRequest<List<Hashtable>>{}

        public class GetSermonsHandler : IRequestHandler<GetSermons, List<Hashtable>>
        {
            private readonly GNBCContext _context;
            public GetSermonsHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<List<Hashtable>> Handle(GetSermons request, CancellationToken cancellationToken)
            {
                var sermons =  await _context.Sermons.ToListAsync();
                var sermonsAttach = new List<Hashtable>();

                if(sermons.Count > 0)
                {
                    foreach(var sermon in sermons)
                    {
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonId", Value = sermon.Id });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonName", Value = sermon.SermonName });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonDescription", Value = sermon.SermonDescription });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonVideoLink", Value = sermon.SermonVideoLink });

                        sermonsAttach.Add(outboundItemData.GetPayload());
                    }
                
                    return sermonsAttach;
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400, "No sermons were found.");
                    
                    throw newError;
                }
            }
        }
    }
}