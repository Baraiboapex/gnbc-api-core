using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.InDTOs;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Helpers;

namespace Application.BibleStudies
{
    public class CreateBibleStudy
    {
        public class NewBibleStudy : IRequest
        {
             //These will need to be inbound DTO's in the future!!!!
            public BibleStudyDTO AddBibleStudy { get; set; }
        }

        public class NewBibleStudyHandler : IRequestHandler<NewBibleStudy>
        {
            private readonly GNBCContext _context;
            public NewBibleStudyHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(NewBibleStudy request, CancellationToken cancellationToken)
            {
                var currentBibleStudy = await _context.BibleStudies.SingleOrDefaultAsync(b => b.BibleStudyName == request.AddBibleStudy.BibleStudyName);
                bool bibleDoesNotExist = currentBibleStudy == null;

                if(bibleDoesNotExist)
                {
                    var bibleStudy = new BibleStudy();

                    bibleStudy.BibleStudyName = request.AddBibleStudy.BibleStudyName;
                    bibleStudy.BibleStudyDescription = request.AddBibleStudy.BibleStudyDescription;
                    bibleStudy.BibleStudyVideoLink = request.AddBibleStudy.BibleStudyVideoLink;

                    _context.BibleStudies.Add(bibleStudy);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400,"Sermon already exists!" );

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}