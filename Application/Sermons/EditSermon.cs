using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.InDTOs;
using MediatR;
using Persistence;

namespace Application.Sermons
{
    public class EditSermon
    {
        public class ModifySermon : IRequest
        {
            //These will need to be inbound DTO's in the future!!!!
            public SermonDTO SermonToEdit { get; set; }
        }

        public class ModifySermonHandler : IRequestHandler<ModifySermon>
        {
            private readonly GNBCContext _context;
            public ModifySermonHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ModifySermon request, CancellationToken cancellationToken)
            {
                 var currentSermon = await _context.Sermons.FindAsync(request.SermonToEdit.Id);
                 bool sermonDoesNotExist = currentSermon == null;

                 if(!sermonDoesNotExist)
                 {
                     currentSermon.SermonName = request.SermonToEdit.SermonName;
                    currentSermon.SermonDescription = request.SermonToEdit.SermonDescription;
                    currentSermon.SermonVideoLink = request.SermonToEdit.SermonVideoLink;

                    _context.Sermons.Attach(currentSermon);
                    await _context.SaveChangesAsync();
                 }
                 else
                 {
                     
                    var newError = new NewError();

                    newError.AddValue(404, "Sermon does not exist.");
                    
                    throw newError;
                 }
                
                return Unit.Value;
            }
        }
    }
}