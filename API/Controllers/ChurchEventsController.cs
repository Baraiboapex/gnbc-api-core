using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using Domain.InDTOs;

namespace API.Controllers
{
    public class ChurchEventsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public ChurchEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}