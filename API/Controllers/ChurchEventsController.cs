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
using Application.ChurchEvents;

namespace API.Controllers
{
    public class ChurchEventsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public ChurchEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<List<Hashtable>>> GetChurchEvents()
        {
            try
            {
                 return await _mediator.Send(new ListChurchEvents.GetChurchEvents());
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.GetError()["Message"]);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hashtable>> GetOneChurchEvents(Guid id)
        {
            try
            {
                 return await _mediator.Send(new ShowOneChurchEvent.GetChurchEvent{ChurchEventId = id});
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.GetError()["Message"]);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> PostChurchEvents([FromForm] ChurchEventDTO churchEvent)
        {
            try
            {
                 return await _mediator.Send(new CreateChurchEvent.AddChurchEvent{NewChurchEvent = churchEvent});
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.Message);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.GetError()["Message"]);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> PutChurchEvents([FromForm] ChurchEventDTO churchEvent)
        {
            try
            {
                 return await _mediator.Send(new EditChurchEvent.ModifyChurchEvent{ChurchEventToEdit = churchEvent});
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.GetError()["Message"]);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteChurchEvent(Guid id)
        {
            try
            {
                 return await _mediator.Send(new RemoveChurchEvent.DeleteChurchEvent{ChurchEventId = id});
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.GetError()["Message"]);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }

        [HttpPut("markchurchevent")]
        public async Task<ActionResult<Unit>> MarkChurchEvent([FromForm] AddUserToFavoriteDTO favorite)
        {
            try
            {
                 return await _mediator.Send(new MarkChurchEvent.MarkEvent{UserId = favorite.ParentId, ChurchEventId = favorite.ItemId, });
            }
            catch(NewError ex)
            {
                if((int)ex.GetError()["Code"] == 400)
                {
                    return BadRequest(ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.GetError()["Message"]);
                }
                else if((int)ex.GetError()["Code"] == 401)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ex.GetError()["Message"]);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        }
    }
}