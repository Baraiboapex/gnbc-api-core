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
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class BibleStudyController : BaseApiController
    {
        private readonly IMediator _mediator;
        
        public BibleStudyController(IMediator mediator)
        {
            _mediator = mediator;
        }

         public async Task<ActionResult<List<Hashtable>>> GetBibleStudy()
        {
            try
            {
                return await _mediator.Send(new ListBibleStudies.GetBibleStudies());
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
        public async Task<ActionResult<Hashtable>> GetBibleStudy(Guid id)
        {
            try
            {
                return await _mediator.Send(new ShowOneBibleStudy.GetOneBibleStudy{BibleStudyId = id});
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
        [Authorize]
        public async Task<ActionResult<Unit>> PostBibleStudy([FromForm] BibleStudyDTO bibleStudy)
        {
            try
            {
                return await _mediator.Send(new CreateBibleStudy.NewBibleStudy{AddBibleStudy = bibleStudy});
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
        [Authorize]
        public async Task<ActionResult<Unit>> PutBibleStudy([FromForm] BibleStudyDTO bibleStudy)
        {
            try
            {
                return await _mediator.Send(new EditBibleStudy.ModifyBibleStudy{BibleStudyToEdit = bibleStudy});
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

        [HttpPut("makefarvorite")]
        [Authorize]
        public async Task<ActionResult<Unit>> MakeBibleStudyFavorite([FromForm] AddUserToFavoriteDTO userFavorite)
        {
            try
            {
                return await _mediator.Send(new MakeBibleStudyFavorite.AddToFavorites{BibleStudyId = userFavorite.ItemId, UserId = userFavorite.ParentId});
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
        [Authorize]
        public async Task<ActionResult<Unit>> DeleteBibleStudy(Guid Id)
        {
            try
            {
                return await _mediator.Send(new MakeSermonFavorite.AddToFavorites{SermonId = Id});
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