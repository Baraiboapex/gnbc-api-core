using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.InDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using Application.BlogPosts;

namespace API.Controllers
{
    public class BlogController : BaseApiController
    {
        private readonly IMediator _mediator;
        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<List<Hashtable>>> GetBlogPosts()
        {
            try
            {
                 return await _mediator.Send(new GetBlogPosts.ListBlogPosts());
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
        public async Task<ActionResult<Hashtable>> GetBlogPost(Guid id)
        {
            try
            {
                 return await _mediator.Send(new ShowOneBlogPost.ListOneBlogPost{BlogPostId = id});
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
        public async Task<ActionResult<Unit>> PostBlogPost(BlogPostDTO blogPost){
             try
            {
                 return await _mediator.Send(new CreateBlogPost.AddBlogPost{PostToAdd = blogPost});
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
        public async Task<ActionResult<Unit>> PutBlogPost(BlogPostDTO blogPost){
             try
            {
                 return await _mediator.Send(new EditBlogPost.ModifyBlogPost{BlogPostToEdit = blogPost});
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