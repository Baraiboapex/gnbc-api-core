using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.BibleStudies
{
    public class DeleteBibleStudy
    {
        public class RemoveBibleStudy : IRequest
        {
            public Guid BibleStudyId { get; set; }
        }

        public class RemoveBibleStudyHandler : IRequestHandler<RemoveBibleStudy>
        {
            private readonly GNBCContext _context;
            public RemoveBibleStudyHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveBibleStudy request, CancellationToken cancellationToken)
            {
                var currentBibleStudy = await _context.Sermons.FindAsync(request.BibleStudyId);
                bool bibleStudyDoesNotExist = currentBibleStudy == null;
                
                if(bibleStudyDoesNotExist)
                {
                    _context.Sermons.Remove(currentBibleStudy);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("sermon Does Not Exist");
                }

                return Unit.Value;
            }
        }
    }
}