using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BibleStudies
{
    public class MakeBibleStudyFavorite
    {
        public class AddToFavorites : IRequest
        {
            public Guid BibleStudyId {get; set;}
            public Guid UserId {get; set;}
        }

        public class AddToFavoritesHandler : IRequestHandler<AddToFavorites>
        {
            private readonly GNBCContext _context;

            public AddToFavoritesHandler(GNBCContext context)
            {
                _context = context;
            }  

            public async Task<Unit> Handle(AddToFavorites request, CancellationToken cancellationToken)
            {
                var currentUserFavorite = await _context.UserFavorites.Include(uf => uf.BibleStudies).SingleOrDefaultAsync(uf => uf.UserId == request.UserId);
                var currentBibleStudy = await _context.BibleStudies.Include(b => b.UserFavorites).SingleOrDefaultAsync(b => b.Id == request.BibleStudyId);

                bool userExists = currentUserFavorite != null;
                bool bibleStudyExists = currentBibleStudy != null;

                if(userExists)
                {
                    if(bibleStudyExists)
                    {
                        currentUserFavorite.BibleStudies.Add(currentBibleStudy);
                        currentBibleStudy.UserFavorites.Add(currentUserFavorite);

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404, "Bible study does not exist");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "User does not exist");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}