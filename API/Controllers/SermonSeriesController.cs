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
using Application.SermonSeries;

namespace API.Controllers
{
    public class SermonSeriesController : BaseApiController
    {
        private readonly IMediator _mediator;
        public SermonSeriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<List<Hashtable>>> GetSermonSeries()
        {
            try
            {
                return await _mediator.Send(new ListSermonSeries.GetSermonSeries());
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
        public async Task<ActionResult<Hashtable>> GetSermonSeries(Guid id)
        {
            try
            {
                return await _mediator.Send(new ShowOneSermonSeries.GetOneSermonSeries{SermonSeriesToGet = id});
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
        public async Task<ActionResult<Unit>> PostSermonSeries([FromForm] SermonSeriesDTO sermonSeries)
        {
            try
            {
                return await _mediator.Send(new CreateSermonSeries.AddNewSermonSeries{SermonSeriesToAdd = sermonSeries});
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

        [HttpPut]
        public async Task<ActionResult<Unit>> PutSermonSeries([FromForm] SermonSeriesDTO sermon)
        {
            try
            {
                return await _mediator.Send(new EditSermonSeries.ModifySermonSeries{SermonSeriesToEdit = sermon});
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
        public async Task<ActionResult<Unit>> DeleteSermonSeries(Guid id)
        {
            try
            {
                return await _mediator.Send(new DeleteSermonSeries.RemoveSermonSeries{SermonSeriesToRemove = id});
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