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
using Application.BibleStudies;
using Application.Users;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<ActionResult<List<Hashtable>>> GetUsers()
        {
            try
            {
                 return await _mediator.Send(new ListUsers.GetUsers());
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hashtable>> GetOneUser(Guid id)
        {
            try
            {
                 return await _mediator.Send(new ShowOneUser.GetOneUser{UserId = id});
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> PostUser(UserDTO user)
        {
            try
            {
                 return await _mediator.Send(new CreateUser.AddUser{UserToAdd = user});
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> PutUser(UserDTO user)
        {
            try
            {
                 return await _mediator.Send(new EditUser.ModifyUser{UserToEdit = user});
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeletUser(Guid id)
        {
            try
            {
                 return await _mediator.Send(new DeleteUser.RemoveUser{UserId= id});
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }
    }
}