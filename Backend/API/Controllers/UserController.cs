using AutoMapper;
using BLL;
using BOL;
using BOL.Dto;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("Api/User")]
        public IHttpActionResult GetUser()
        {
            var _users = UserService.GetAllUser();
            if( _users == null)
            {
                return NotFound();
            }
            else
            {
                return Ok( _users );
            }
        }

        [Route("Api/User/{id}")]
        public IHttpActionResult GetUser(int id)
        {
            var _user = UserService.GetUserById(id);
            if(_user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_user);
            }
        }
        [Route("Api/User/Username/{username}")]
        public IHttpActionResult GetUserByUsename(string username)
        {
            var _user = UserService.GetUserByUsername(username);
            if (_user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_user);
            }
        }
        [HttpPost]
        [Route("Api/User")]
        public IHttpActionResult CreateUser(UserDto user)
        {
            if (ModelState.IsValid)
            {
                UserService.RegisterUser(user);
                return Ok("User Saved Successfully");
            }
            else
            {
                return BadRequest("Didn't Fullfill Validation");
            }

        }
        [HttpDelete]
        [Route("Api/User/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            var status = UserService.DeleteUserById(id);
            if (status)
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut]
        [Route("Api/User/{id}")]
        public IHttpActionResult UpdateUser(int id, UserDto user)
        {
            if (ModelState.IsValid)
            {
                var status = UserService.EditUser(id, user);
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
