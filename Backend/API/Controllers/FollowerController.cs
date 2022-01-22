using BLL;
using BOL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class FollowerController : ApiController
    {
        [Route("Api/Follow")]
        [HttpPost]
        public IHttpActionResult CreateFollow(FollowerDto fo)
        {
            if (ModelState.IsValid)
            {
                FollowerService.Add(fo);
                return Ok("Follower Saved Successfully");
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }
        }

        [Route("Api/Followers/{id}")]
        [HttpGet]
        public IHttpActionResult GetFollowersByUserId(int id)
        {
            var data = FollowerService.GetFollowersByUserId(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [Route("Api/Following/{id}")]
        [HttpGet]
        public IHttpActionResult GetFollowingByUserId(int id)
        {
            var data = FollowerService.GetFollowingByUserId(id);
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
        [Route("Api/Follower/{id}")]
        public IHttpActionResult DeleteFollowing(int id)
        {
            var status = FollowerService.DeleteFollowing(id);
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
