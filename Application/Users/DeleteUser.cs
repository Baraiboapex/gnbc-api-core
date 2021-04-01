using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class DeleteUser
    {
        public class RemoveUser : IRequest
        {
            public Guid UserId { get; set; }
        }

         public class RemoveUserHandler : IRequestHandler<RemoveUser>
        {
            private readonly GNBCContext _context;
            public RemoveUserHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveUser request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.UserId);
                bool userExists = user != null;

                if(userExists)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(404, "User does not exist.");
                    
                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}