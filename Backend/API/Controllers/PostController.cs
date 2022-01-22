using BOL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostController : ApiController
    {
        [Route("Api/Post")]
        [HttpPost]
        public IHttpActionResult CreatePost(PostDto post)
        {
            if (ModelState.IsValid)
            {
                PostService.Add(post);
                return Ok("Post Saved Successfully");
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }
        }

        [Route("Api/Post")]
        [HttpGet]
        public IHttpActionResult GetPost()
        {
            var data = PostService.GetAllPost();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [Route("Api/Post/{id}")]
        [HttpGet]
        public IHttpActionResult GetPostId(int id)
        {
            var data = PostService.GetPostById(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [Route("Api/Post/UserId/{id}")]
        [HttpGet]
        public IHttpActionResult GetPostByUserId(int id)
        {
            var data = PostService.GetPostByUserId(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpDelete]
        [Route("Api/Post/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            var status = PostService.DeletePost(id);
            if (status)
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest();
            }

        }
        [Route("Api/Post")]
        [HttpPut]
        public IHttpActionResult UpdatePost(PostDto post)
        {
            if (ModelState.IsValid)
            {
                var status = PostService.EditPost(post);
                if (status)
                {
                    return Ok("Updated");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }

        }
        [Route("Api/Stats")]
        [HttpGet]
        public IHttpActionResult GetAllStats()
        {
            return Ok(PostService.GetAllStats());

        }
    }
}