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
    public class LogController : ApiController
    {
        [HttpGet]
        [Route("Api/Logs")]
        public IHttpActionResult GetAllLog()
        {
            var _log = LogService.GetAllLog();
            if (_log == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_log);
            }
        }
        [HttpPost]
        [Route("Api/Logs")]
        public IHttpActionResult SetLog(LogDto _log)
        {
            if (ModelState.IsValid)
            {
                if (LogService.SetLog(_log))
                {
                    return Ok("Log Registered Successfully");
                }
                else
                {
                    return BadRequest("Already Registered!!");
                }
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }
        }
    }
}
