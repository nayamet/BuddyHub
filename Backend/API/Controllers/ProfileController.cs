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
    public class ProfileController : ApiController
    {
        [Route("Api/Profile")]
        [HttpPost]
        public IHttpActionResult CreateProfile(ProfileDto prof)
        {
            if (ModelState.IsValid)
            {
                ProfileService.RegisterProfile(prof);
                return Ok("Profile Saved Successfully");
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }
        }

        [Route("Api/Profile")]
        [HttpGet]
        public IHttpActionResult GetAllProfile()
        {
            var data = ProfileService.GetAllProfile();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [Route("Api/Profile/{id}")]
        [HttpGet]
        public IHttpActionResult GetProfileById(int id)
        {
            var data = ProfileService.GetProfileById(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
        [Route("Api/Notification/{id}")]
        [HttpGet]
        public IHttpActionResult GetNotificationByUserId(int id)
        {
            var data = NotificationService.GetNotificationByUserId(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [Route("Api/Profile")]
        [HttpPut]
        public IHttpActionResult EditProfile(ProfileDto prof)
        {
            if (ModelState.IsValid)
            {
                var status = ProfileService.EditProfile(prof);
                if(status)
                {
                    return Ok("Profile edited Successfully");
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

        [HttpDelete]
        [Route("Api/Profile/{id}")]
        public IHttpActionResult DeleteProfile(int id)
        {
            var status = ProfileService.DeleteProfileById(id);
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
