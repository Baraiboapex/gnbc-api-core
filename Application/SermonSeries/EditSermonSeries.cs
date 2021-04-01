using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Persistence;

namespace Application.SermonSeries
{
    public class EditSermonSeries
    {
        public class ModifySermonSeries : IRequest
        {
            public SermonSeriesDTO SermonSeriesToEdit { get; set; }
        }

        public class ModifySermonSeriesHandler : IRequestHandler<ModifySermonSeries>
        {
            private readonly GNBCContext _context;
            public ModifySermonSeriesHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ModifySermonSeries request, CancellationToken cancellationToken)
            {
                var currentSermon = await _context.SermonSeries.FindAsync(request.SermonSeriesToEdit.Id);
                bool sermonSeriesExists = currentSermon != null;

                if(sermonSeriesExists)
                {
                    currentSermon.SeriesName = request.SermonSeriesToEdit.SeriesName;

                    _context.SermonSeries.Attach(currentSermon);
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