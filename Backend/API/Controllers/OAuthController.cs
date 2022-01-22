using BLL;
using BOL.Dto;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins:"*", headers: "*", methods: "*")]
    public class OAuthController : ApiController
    {
        [HttpGet]
        [Route("Api/OBC")]
        public IHttpActionResult OBC()
        {
            return Ok(Request.Headers);
        }
        [HttpGet]
        [Route("Api/OAuth")]
        public IHttpActionResult GetOAuth()
        {
            var _oAuth = OAuthService.GetAllOAuth();
            if (_oAuth == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_oAuth);
            }
        }
        [HttpPost]
        [Route("Api/CheckOAuth")]
        public IHttpActionResult CheckIfOAuthAvailable(OAuthDto oAuth)
        {
            
                if (OAuthService.IsOAuthExists(oAuth))
                {
                    return Ok("Yes");
                }
                else
                {
                    return Ok("No");
                }
        }
        [HttpPost]
        [Route("Api/OAuth/Register")]
        public IHttpActionResult CreateOAuth(OAuthDto oAuth)
        {
            if (ModelState.IsValid)
            {
                if (OAuthService.RegisterOAuth(oAuth))
                {
                    return Ok("User Registered Successfully");
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
        [HttpPost]
        [Route("Api/OAuth/Login")]
        public IHttpActionResult LoginOAuth(OAuthDto oAuth)
        {
            if (ModelState.IsValid)
            {
                var _user = OAuthService.LoginOAuth(oAuth);
                if (_user != null)
                {
                    return Ok(_user);
                }
                else
                {
                    return Ok("null");
                }
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }

        }


    }
}
