using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.SermonSeries
{
    public class AddSermonToSeries
    {
        public class AttachSermonToSeries : IRequest
        {
            public Guid SermonId { get; set; }

            public Guid SermonSeriesId { get; set; }
        }

        public class AttachSermonToSeriesHandler : IRequestHandler<AttachSermonToSeries>
        {
            private readonly GNBCContext _context;
            public AttachSermonToSeriesHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AttachSermonToSeries request, CancellationToken cancellationToken)
            {
                var currentSermonSeries = await _context.SermonSeries.SingleOrDefaultAsync(ss => ss.Id == request.SermonSeriesId);
                var currentSermon = await _context.Sermons.SingleOrDefaultAsync(s => s.Id == request.SermonId);

                bool sermonSeriesExists = currentSermonSeries != null;
                bool sermonExists = currentSermon != null;

                if(sermonSeriesExists)
                {
                    if(sermonExists)
                    {
                        currentSermonSeries.Sermons.Add(currentSermon);
                    
                        _context.Entry(currentSermonSeries).State = EntityState.Modified;

                        _context.SermonSeries.Attach(currentSermonSeries);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404, "Sermon does not exist!");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "Sermon series does not exist!");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}