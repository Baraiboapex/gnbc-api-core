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
    public class ShowOneUser
    {
        public class GetOneUser : IRequest<Hashtable>
        {
            public Guid UserId { get; set; }
        }

         public class GetOneUserHandler : IRequestHandler<GetOneUser, Hashtable>
        {
            private readonly GNBCContext _context;
            public GetOneUserHandler(GNBCContext context)
            {
                _context = context;

            }

            public async Task<Hashtable> Handle(GetOneUser request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.UserId);

                Task<Hashtable> createUser = Task<Hashtable>.Factory.StartNew(() =>
                {
                    var outboundItemData = new OutboundDTO();

                    outboundItemData.AddField(new DictionaryEntry { Key = "UserId", Value = user.Id });
                    outboundItemData.AddField(new DictionaryEntry { Key = "FirstName", Value = user.FirstName });
                    outboundItemData.AddField(new DictionaryEntry { Key = "LastName", Value = user.LastName });
                    outboundItemData.AddField(new DictionaryEntry { Key = "Email", Value = user.Email });
                    outboundItemData.AddField(new DictionaryEntry { Key = "UserCanBlog", Value = user.CanBlog });

                    return outboundItemData.GetPayload();
                });

                return await createUser;
            }
        }
    }
}