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
    public class SaveController : ApiController
    {
        [Route("Api/Save")]
        [HttpPost]
        public IHttpActionResult CreateSave(SaveDto save)
        {
            if (ModelState.IsValid)
            {
                SaveService.Add(save);
                return Ok("Save Saved Successfully");
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }
        }

        [Route("Api/Save/{id}")]
        [HttpGet]
        public IHttpActionResult GetSaveByUserId(int id)
        {
            var data = SaveService.GetSaveByUserId(id);
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
        [Route("Api/Save/{id}")]
        public IHttpActionResult DeleteSave(int id)
        {
            var status = SaveService.DeleteSave(id);
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
