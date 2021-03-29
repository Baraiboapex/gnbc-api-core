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
                var users = await _context.Users.ToListAsync();
                var usersAttach = new List<Hashtable>();

                if(users.Count > 0)
                {
                    Task<List<Hashtable>> createSermonList = Task<List<Hashtable>>.Factory.StartNew(()=>{
                        foreach(var user in users)
                        {
                            var outboundItemData = new OutboundDTO();

                             usersAttach.Add(outboundItemData.GetPayload());
                        }
                    
                        return usersAttach;
                    });

                    return await createSermonList;
                }
                else
                {
                    throw new Exception("No users were found");
                }
            }
        }
    }
}