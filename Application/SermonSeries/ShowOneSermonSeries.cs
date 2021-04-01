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
        public class GetOneSermonSeries : IRequest<Hashtable>
        {
            public Guid SermonSeriesToGet { get; set; }
        }

        public class GetOneSermonSeriesHandler : IRequestHandler<GetOneSermonSeries, Hashtable>
        {
            private readonly GNBCContext _context;
            public GetOneSermonSeriesHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Hashtable> Handle(GetOneSermonSeries request, CancellationToken cancellationToken)
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