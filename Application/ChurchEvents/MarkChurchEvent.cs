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

namespace Application.ChurchEvents
{
    public class MarkChurchEvent
    {
        public class MarkEvent : IRequest
        {
            public Guid UserId {get; set;}
            public Guid ChurchEventId {get; set;}
        }
    }
}