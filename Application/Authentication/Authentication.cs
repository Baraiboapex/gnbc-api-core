using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.InDTOs;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Helpers;
using System.Collections;

namespace Application.Authentication
{
    public class Authentication
    {
        public class AuthenticateUser : IRequest<Hashtable>
        {
            public AuthenticationDTO UserToAuthenticate { get; set; }
        }

        public class AuthenticateUserHandler : IRequestHandler<AuthenticateUser, Hashtable>
        {
            public AuthenticateUserHandler(GNBCContext context)
            {
                _context = context;
                _secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            }

            public async Task<Hashtable> Handle(AuthenticateUser request, CancellationToken cancellationToken)
            {
                var currentUser = _context.Users.SingleOrDefaultAsync(
                    u => u.Email == request.UserToAuthenticate.Email && 
                    u.Password == request.UserToAuthenticate.Password
                );

                var outboundItemData = new OutboundDTO();

                
            }
        }
    }
}