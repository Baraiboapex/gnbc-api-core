using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Sermons;
using Domain.InDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;


namespace API.Controllers
{
    public class BlogCommentsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public BlogCommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<List<Hashtable>>> GetBlogComments()
        {
            try
            {
                 return await _mediator.Send(new ListBlogComments.GetComments());
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

        [HttpGet("id")]
        public async Task<ActionResult<Hashtable>> GetBlogComment()
        {
            try
            {
                 return await _mediator.Send(new ListBlogComments.GetOneComment());
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
        public async Task<ActionResult<Hashtable>> PostBlogComment()
        {
            try
            {
                 return await _mediator.Send(new CreateBlogComment.AddNewComment());
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
        public async Task<ActionResult<Hashtable>> DeleteBlogComment(Guid id)
        {
            try
            {
                 return await _mediator.Send(new DeleteBlogComment.RemoveComment());
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