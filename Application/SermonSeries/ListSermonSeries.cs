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

namespace Application.SermonSeries
{
    public class ListSermonSeries
    {
        public class GetSermonSeries : IRequest<List<Hashtable>> { }

        public class GetSermonsHandler : IRequestHandler<GetSermonSeries, List<Hashtable>>
        {
            private readonly GNBCContext _context;
            public GetSermonsHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<List<Hashtable>> Handle(GetSermonSeries request, CancellationToken cancellationToken)
            {
                var sermonSeries = await _context.SermonSeries.ToListAsync();
                var loadSermonSeries = new List<Hashtable>();

                if(sermonSeries.Count > 0)
                {
                    foreach(var series in sermonSeries)
                    {
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "SermonSeriesId", Value = series.Id });
                        outboundItemData.AddField(new DictionaryEntry { Key = "SeriesName", Value = series.SeriesName });

                        loadSermonSeries.Add(outboundItemData.GetPayload());
                    }

                    return loadSermonSeries;
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "No sermon series were found");

                    throw newError;
                }
            }
        }
    }
}