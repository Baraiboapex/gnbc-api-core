using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;


namespace Application.Users
{
    public class CreateUser
    {
        public class AddUser : IRequest
        {
            public User UserToAdd { get; set; }
        }

        public class AddUserHandler : IRequestHandler<AddUser>
        {
            private readonly GNBCContext _context;
            public AddUserHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddUser request, CancellationToken cancellationToken)
            {
                bool userDoesNotExist = (await _context.Users.FindAsync(request.UserToAdd.Email)) == null;

                if(userDoesNotExist)
                {
                    _context.Users.Add(request.UserToAdd);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newError = new NewError();

                    newError.AddValue(400, "User already exists.");
                    
                    throw newError;
                }

                return Unit.Value;
            }
        }
    }
}