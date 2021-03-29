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
using Application.BlogPostCategories;

namespace API.Controllers
{
    public class BlogCategoryController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BlogCategoryController(IMediator mediator)
        {
            _mediator = mediator;

        }

        public async Task<ActionResult<List<Hashtable>>> GetBlogCategories()
        {
            try
            {
                 return await _mediator.Send(new ListBlogCategories.GetCategories());
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
        public async Task<ActionResult<Unit>> PostBlogCategory(BlogPostCategoryDTO blogCategory)
        {
             try
            {
                 return await _mediator.Send(new  CreateBlogPostCategory.AddBlogPostCategory{NewBlogPostCategory = blogCategory});
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
        public async Task<ActionResult<Unit>> PutBlogCategory(BlogPostCategoryDTO blogCategory)
        {
            try
            {
                 return await _mediator.Send(new EditBlogCategory.ModifyBlogCategory{BlogPostCategoryToEdit = blogCategory});
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

        //For now, deleting a blog post category is not suitable for MVP
        //There could be a future use for a similar function though. Therefore, it might
        //be wise to hold on to this DELETE endpoint for possible future use.

        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Unit>> DeleteBlogCategory(Guid id)
        // {
        //     try
        //     {
        //          return await _mediator.Send(new RemoveBlogCategory.DeletCategory{CategoryId = id});
        //     }
        //     catch(NewError ex)
        //     {
        //         if((int)ex.GetError()["Code"] == 400)
        //         {
        //             return BadRequest(ex.Message);
        //         }
        //         else if((int)ex.GetError()["Code"] == 404)
        //         {
        //             return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        //         }
        //         else if((int)ex.GetError()["Code"] == 401)
        //         {
        //             return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        //         }
        //     }

        //     return StatusCode(StatusCodes.Status500InternalServerError, "You screwed up bad!");
        // }
    }
}