using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Persistence;

namespace Application.SermonSeries
{
    public class CreateSermonSeries
    {
        public class AddNewSermonSeries : IRequest
        {
            public SermonSeriesDTO SermonSeriesToAdd { get; set; }
        }

        public class AddNewSermonSeriesHandler : IRequestHandler<AddNewSermonSeries>
        {
            private readonly GNBCContext _context;
            public AddNewSermonSeriesHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddNewSermonSeries request, CancellationToken cancellationToken)
            {
                var currentSermonSeries = await _context.SermonSeries.FindAsync(request.SermonSeriesToAdd.SeriesName);
                bool sermonSeriesDoesNotExist = currentSermonSeries == null;

                if(sermonSeriesDoesNotExist)
                {
                    var sermonSeries = new Domain.SermonSeries();
                    
                    sermonSeries.SeriesName = request.SermonSeriesToAdd.SeriesName;

                    _context.SermonSeries.Add(sermonSeries);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();
                }

                return Unit.Value;
            }
        }
    }
}