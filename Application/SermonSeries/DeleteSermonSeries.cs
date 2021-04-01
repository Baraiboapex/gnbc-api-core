using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Persistence;

namespace Application.SermonSeries
{
    public class DeleteSermonSeries
    {
        public class RemoveSermonSeries : IRequest
        {
            public Guid SermonSeriesToRemove { get; set; }
        }

        public class RemoveSermonSeriesHandler : IRequestHandler<RemoveSermonSeries>
        {
            private readonly GNBCContext _context;
            public RemoveSermonSeriesHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveSermonSeries request, CancellationToken cancellationToken)
            {
                var currentSermon = await _context.SermonSeries.FindAsync(request.SermonSeriesToRemove);
                bool sermonSeriesExists = currentSermon != null;

                if(sermonSeriesExists)
                {
                    _context.SermonSeries.Remove(currentSermon);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "Sermon series does not exist.");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}