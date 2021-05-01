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
    public class SearchController : BaseApiController
    {
        [HttpGet("{searchItem}")]
        public async Task<ActionResult<Hashtable>> GetSearchResults(Guid searchItem)
        {
            try
            {
                 //return await _mediator.Send(new ShowOneChurchEvent.GetChurchEvent{ChurchEventId = id});
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