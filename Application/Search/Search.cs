using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application
{
    public class Search
    {
        public class SearchApp : IRequest
        {
            public string SearchItem { get; set; }
        }

        public class SearchAppHandler : IRequestHandler<SearchApp>
        {
            private readonly GNBCContext _context;

            public SearchAppHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(SearchApp request, CancellationToken cancellationToken)
            {
                return Unit.Value;
            }
        }
    }
}