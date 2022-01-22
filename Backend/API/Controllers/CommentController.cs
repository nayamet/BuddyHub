using BLL;
using BOL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommentController : ApiController
    {
        [Route("Api/Comment")]
        [HttpPost]
        public IHttpActionResult CreateComment(CommentDto comment)
        {
            if (ModelState.IsValid)
            {
                CommentService.AddComment(comment);
                return Ok("Comment Saved Successfully");
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }
        }

        [Route("Api/Comment/{id}")]
        [HttpGet]
        public IHttpActionResult GetCommentById(int id)
        {
            var data = CommentService.GetCommentById(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [Route("Api/Comment/Post/{id}")]
        [HttpGet]
        public IHttpActionResult GetCommentByPostId(int id)
        {
            var data = CommentService.GetCommentByPostId(id);
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
        [Route("Api/Comment/{id}")]
        public IHttpActionResult DeleteComment(int id)
        {
            var status = CommentService.DeleteComment(id);
            if (status)
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest();
            }

        }

        [Route("Api/Comment")]
        [HttpPut]
        public IHttpActionResult UpdateComment(CommentDto comment)
        {
            if (ModelState.IsValid)
            {
                var status = CommentService.EditComment(comment);
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
    }
}
