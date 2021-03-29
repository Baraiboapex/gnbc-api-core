using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Sermons
{
    public class DeleteSermon
    {
        public class RemoveSermon : IRequest
        {
            public Guid SermonId { get; set; }
        }

        public class RemoveSermonHandler : IRequestHandler<RemoveSermon>
        {
            private readonly GNBCContext _context;
            public RemoveSermonHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveSermon request, CancellationToken cancellationToken)
            {
                var currentSermon = await _context.Sermons.FindAsync(request.SermonId);
                bool sermonDoesNotExist = currentSermon == null;

                if(!sermonDoesNotExist)
                {
                    _context.Sermons.Remove(currentSermon);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Sermon does not exist");
                }
                
                return Unit.Value;
            }
        }
    }
}