using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Persistence;

namespace Application.SermonSeries
{
    public class ShowOneSermonSeries
    {
        public class GetOneSermon : IRequest<Hashtable>
        {
            public Guid SermonSeriesToGet { get; set; }
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
                var currentSermonSeries = await _context.SermonSeries.FindAsync(request.SermonSeriesToGet);
                bool sermonSeriesExists = currentSermonSeries != null;

                if (sermonSeriesExists)
                { 
                    var outboundItemData = new OutboundDTO();

                    outboundItemData.AddField(new DictionaryEntry { Key = "SermonSeriesId", Value = currentSermonSeries.Id });
                    outboundItemData.AddField(new DictionaryEntry { Key = "SeriesName", Value = currentSermonSeries.SeriesName });

                    return outboundItemData.GetPayload();
                }
                else
                { 
                    var newError = new NewError();

                    newError.AddValue(404,"Sermon series does not exist.");

                    throw newError;
                }
            }
        }
    }
}