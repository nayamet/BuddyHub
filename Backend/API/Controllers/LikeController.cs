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
    public class LikeController : ApiController
    {
        [Route("Api/Like")]
        [HttpPost]
        public IHttpActionResult CreateLike(LikeDto like)
        {
            if (ModelState.IsValid)
            {
                LikeService.AddLike(like);
                return Ok("Post Saved Successfully");
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }
        }

        [Route("Api/Like/{id}")]
        [HttpGet]
        public IHttpActionResult GetLikeById(int id)
        {
            var data = LikeService.GetLikeById(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [Route("Api/Like/Post/{id}")]
        [HttpGet]
        public IHttpActionResult GetLikeByPostId(int id)
        {
            var data = LikeService.GetLikeByPostId(id);
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
        [Route("Api/Like/{id}")]
        public IHttpActionResult DeleteLike(int id)
        {
            var status = LikeService.DeleteLike(id);
            if (status)
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
