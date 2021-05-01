using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Users
{
    public class ListUsers
    {
        public class GetUsers : IRequest<List<Hashtable>> { }

        public class GetUsersHandler : IRequestHandler<GetUsers, List<Hashtable>>
        {
            private readonly GNBCContext _context;
            public GetUsersHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<List<Hashtable>> Handle(GetUsers request, CancellationToken cancellationToken)
            {
                var users = await _context.Users.Include(u => u.UserFavorite).ToListAsync();
                var usersAttach = new List<Hashtable>();

                if(users.Count > 0)
                {
                    foreach(var user in users)
                    {
                        var outboundItemData = new OutboundDTO();

                        outboundItemData.AddField(new DictionaryEntry { Key = "UserId", Value = user.Id });
                        outboundItemData.AddField(new DictionaryEntry { Key = "FirstName", Value = user.FirstName });
                        outboundItemData.AddField(new DictionaryEntry { Key = "LastName", Value = user.LastName });
                        outboundItemData.AddField(new DictionaryEntry { Key = "Email", Value = user.Email });
                        outboundItemData.AddField(new DictionaryEntry { Key = "UserCanBlog", Value = user.CanBlog });

                        usersAttach.Add(outboundItemData.GetPayload());
                    }
                
                    return usersAttach;
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "No users were found.");
                    
                    throw newError;
                }
            }
        }
    }
}