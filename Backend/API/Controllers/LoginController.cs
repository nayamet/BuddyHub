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
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Api/Login")]
        public IHttpActionResult DoLogin(LoginDto _user)
        {
            if (ModelState.IsValid)
            {
                var res = UserService.IsAuthenticUser(_user);
                if (res == "authorized")
                {
                    return Ok(UserService.GetAuthenticUserInfoByUsername(_user.Username));
                }
                else if(res == "emailnotverified") 
                {
                    return Ok("emailnotverified");
                }
                else
                {
                    return Ok("unauthorized");
                }
            }
            else
            {
                return BadRequest("validationerror");
            }
        }
    }
}
