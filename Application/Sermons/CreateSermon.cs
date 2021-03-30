using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Persistence;

namespace Application.Sermons
{
    public class CreateSermon
    {
        public class NewSermon : IRequest
        {
             //These will need to be inbound DTO's in the future!!!!
            public SermonDTO AddSermon { get; set; }
        }

        public class NewSermonHandler : IRequestHandler<NewSermon>
        {
            private readonly GNBCContext _context;
            public NewSermonHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(NewSermon request, CancellationToken cancellationToken)
            {
                bool sermonDoesNotExist = (await _context.Sermons.FindAsync(request.AddSermon.SermonName)) == null;

                if(sermonDoesNotExist)
                {
                    var sermon = new Sermon();

                    sermon.SermonName = request.AddSermon.SermonName;
                    sermon.SermonDescription = request.AddSermon.SermonDescription;
                    sermon.SermonVideoLink = request.AddSermon.SermonVideoLink;
                    
                     _context.Sermons.Add(sermon);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400, "Sermon already exists.");
                    
                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}