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
    public class ListBibleStudies
    {
        public class GetBibleStudies : IRequest<List<Hashtable>>{}

        public class GetBibleStudiesHandler : IRequestHandler<GetBibleStudies, List<Hashtable>>
        {
            private readonly GNBCContext _context;
            public GetBibleStudiesHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<List<Hashtable>> Handle(GetBibleStudies request, CancellationToken cancellationToken)
            {
                var bibleStudies =  await _context.BibleStudies.ToListAsync();
                var bibleStudiesAttach = new List<Hashtable>();

                if(bibleStudies.Count > 0)
                {
                    foreach(var bibleStudy in bibleStudies)
                    {
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonId", Value = bibleStudy.Id });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonName", Value = bibleStudy.BibleStudyName });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonDescription", Value = bibleStudy.BibleStudyDescription });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonVideoLink", Value = bibleStudy.BibleStudyVideoLink });

                        bibleStudiesAttach.Add(outboundItemData.GetPayload());
                    }

                    return bibleStudiesAttach;
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400,"No bible studies found.");

                    throw newError;
                }
            }
        }
    }
}