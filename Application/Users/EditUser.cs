using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class EditUser
    {
        public class ModifyUser : IRequest
        {
            public User UserToEdit { get; set; }
        }

        public class ModifyUserHandler : IRequestHandler<ModifyUser>
        {
            private readonly GNBCContext _context;
            public ModifyUserHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ModifyUser request, CancellationToken cancellationToken)
            {

                var currentUser = await _context.Users.FindAsync(request.UserToEdit.Id);
                bool userDoesNotExist = currentUser == null;

                if(!userDoesNotExist)
                {
                    currentUser.FirstName = request.UserToEdit.FirstName;
                    currentUser.LastName = request.UserToEdit.LastName;
                    currentUser.Email = request.UserToEdit.Email;
                    currentUser.Password = request.UserToEdit.Password;

                    _context.Users.Attach(currentUser);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("User does not exist");
                }
                
                return Unit.Value;
            }
        }
    }
}