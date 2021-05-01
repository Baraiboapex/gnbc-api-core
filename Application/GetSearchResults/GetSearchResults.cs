using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.InDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.GetSearchResults
{
    public class GetSearchResults
    {
        public class FindSearchResult : IRequest
        {
            public string SearchVal { get; set; }
        }

        public class FindSearchResultHandler : IRequestHandler<FindSearchResult>
        {
            private readonly GNBCContext _context;
            public FindSearchResultHandler(GNBCContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(FindSearchResult request, CancellationToken cancellationToken)
            {
                return Unit.Value;
            }
        }
    }
}