using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Persistence;


namespace Application.Users
{
    public class CreateUser
    {
        public class AddUser : IRequest
        {
            public UserDTO UserToAdd { get; set; }
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
                    var user = new User();

                    user.FirstName = request.UserToAdd.FirstName;
                    user.LastName = request.UserToAdd.LastName;
                    user.Email = request.UserToAdd.Email;
                    user.Password = request.UserToAdd.Password;

                    _context.Users.Add(user);
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