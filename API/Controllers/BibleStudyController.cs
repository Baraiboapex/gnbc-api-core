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
        public async Task<ActionResult<Unit>> PostBibleStudy(BibleStudyDTO bibleStudy)
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
        public async Task<ActionResult<Unit>> PutBibleStudy(BibleStudyDTO bibleStudy)
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

        [HttpPut]
        [Route("api/[controller]/makefarvorite")]
        public async Task<ActionResult<Unit>> MakeBibleStudyFavorite(AddUserToFavoriteDTO userFavorite)
        {
            try
            {
                return await _mediator.Send(new MakeBibleStudyFavorite.AddToFavorites{BibleStudyId = userFavorite.ItemId, UserId = userFavorite.UserId});
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