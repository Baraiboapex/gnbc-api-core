using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BibleStudies
{
    public class ShowOneBibleStudy
    {
        public class GetOneBibleStudy : IRequest<Hashtable>
        {
            public Guid BibleStudyId {get; set;}
        }

         public class GetOneBibleStudyHandler : IRequestHandler<GetOneBibleStudy, Hashtable>
        {
            private readonly GNBCContext _context;
            public GetOneBibleStudyHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Hashtable> Handle(GetOneBibleStudy request, CancellationToken cancellationToken)
            {
                var sermon = await _context.Sermons.FindAsync(request.BibleStudyId);
                bool bibleDoesNotExist = sermon == null;
                
                if(!bibleDoesNotExist)
                {
                    Task<Hashtable> createBibleStudy = Task<Hashtable>.Factory.StartNew(()=>{
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonId", Value = sermon.Id });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonName", Value = sermon.SermonName });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonDescription", Value = sermon.SermonDescription });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonVideoLink", Value = sermon.SermonVideoLink });

                        return outboundItemData.GetPayload();
                    });

                    return await createBibleStudy;
                }
                else
                {
                    throw new Exception("Sermon does not exist!");
                }
                
            }
        }
    }
}