using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Sermons;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using Domain.InDTOs;

namespace API.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

    }
}