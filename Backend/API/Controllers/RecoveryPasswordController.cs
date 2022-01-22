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
    public class RecoveryPasswordController : ApiController
    {
        [Route("Api/RecoveryPassword")]
        [HttpPost]
        public IHttpActionResult CreateRecoveryPassword(RecoveryPasswordDto rp)
        {
            if (ModelState.IsValid)
            {
                RecoveryPasswordService.Add(rp);
                return Ok("RecoveryPassword Saved Successfully");
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }
        }

        [Route("Api/RecoveryPassword/{id}")]
        [HttpGet]
        public IHttpActionResult GetRecoveryPasswordByUserId(int id)
        {
            var data = RecoveryPasswordService.GetRecoveryPasswordByUserId(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [Route("Api/RecoveryPassword")]
        [HttpPut]
        public IHttpActionResult UpdateRecoveryPassword(RecoveryPasswordDto rp)
        {
            if (ModelState.IsValid)
            {
                var status = RecoveryPasswordService.EditRecoveryPassword(rp);
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
