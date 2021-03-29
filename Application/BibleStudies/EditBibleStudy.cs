using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.InDTOs;
using MediatR;
using Persistence;

namespace Application.BibleStudies
{
    public class EditBibleStudy
    {
        public class ModifyBibleStudy : IRequest
        {
            //These will need to be inbound DTO's in the future!!!!
            public BibleStudyDTO BibleStudyToEdit { get; set; }
        }

        public class ModifyBibleStudyHandler : IRequestHandler<ModifyBibleStudy>
        {
            private readonly GNBCContext _context;
            public ModifyBibleStudyHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ModifyBibleStudy request, CancellationToken cancellationToken)
            {

                var currentBibleStudy = await _context.BibleStudies.FindAsync(request.BibleStudyToEdit.Id);
                bool bibleStudyExists = currentBibleStudy != null;
                
                if(bibleStudyExists)
                {
                    currentBibleStudy.BibleStudyName = request.BibleStudyToEdit.BibleStudyName;
                    currentBibleStudy.BibleStudyDescription = request.BibleStudyToEdit.BibleStudyDescription;
                    currentBibleStudy.BibleStudyVideoLink = request.BibleStudyToEdit.BibleStudyVideoLink;

                    _context.BibleStudies.Attach(currentBibleStudy);
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