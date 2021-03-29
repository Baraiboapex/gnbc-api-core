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
    public class ShowOneSermon
    {
        public class GetOneSermon : IRequest<Hashtable>
        {
            public Guid SermonId {get; set;}
        }

         public class GetOneSermonHandler : IRequestHandler<GetOneSermon, Hashtable>
        {
            private readonly GNBCContext _context;
            public GetOneSermonHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Hashtable> Handle(GetOneSermon request, CancellationToken cancellationToken)
            {
                var sermon = await _context.Sermons.FindAsync(request.SermonId);
                bool sermonDoesNotExist = sermon == null;
                //You will need ti add a try/catch block here which returns an exception object if 
                //an exception is thrown.
                
                if(!sermonDoesNotExist)
                {
                    Task<Hashtable> createSermon = Task<Hashtable>.Factory.StartNew(()=>{
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonId", Value = sermon.Id });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonName", Value = sermon.SermonName });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonDescription", Value = sermon.SermonDescription });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonVideoLink", Value = sermon.SermonVideoLink });

                        return outboundItemData.GetPayload();
                    });

                    return await createSermon;
                }
                else
                {
                    throw new Exception("Sermon does not exist");
                }
            }
        }
    }
}