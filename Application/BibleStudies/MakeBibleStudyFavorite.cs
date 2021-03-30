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
                var currentUser = await _context.Users.FindAsync(request.UserId);
                bool userDoesNotExist = currentUser == null;

                if(!userDoesNotExist)
                {   
                    var currentUserFavorite = await _context.UserFavorites.FindAsync(request.UserId);
                    bool userFavoriteDoesNotExist = currentUserFavorite == null;

                    if(!userFavoriteDoesNotExist)
                    {
                        var currentBibleStudy = await _context.BibleStudies.FindAsync(request.BibleStudyId);
                        bool bibleDoesNotExist = currentBibleStudy == null;
                    
                        if(!bibleDoesNotExist)
                        {
                            currentUser.UserFavorite.BibleStudies.Add(currentBibleStudy);
                            currentBibleStudy.UserFavorites.Add(currentUser.UserFavorite);
                        }
                        else
                        {
                            var newError = new NewError();

                            newError.AddValue(404,"Bible study does not exist.");

                            throw newError;
                        }
                    }
                    else
                    {
                        var newError = new NewError();

                        newError.AddValue(404,"User favorite study does not exist.");

                        throw newError;
                    }
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404,"User does not exist.");

                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}