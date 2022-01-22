using AutoMapper;
using BOL;
using BOL.Dto;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;


namespace BLL
{
    public class RegistrationService
    {
       
        public static bool RegisterUser(RegistrationDto user)
        {
            if(user == null) { return false; }
            else
            {

                bool isCreated = UserService.RegisterUser(new UserDto()
                {
                    Name = user.Name,
                    Username = user.Username,
                    Type = "general",
                    Status = 0,
                    Password = user.Password
                });
                if (isCreated)
                {
                    EmailLogService.EntryEmailByUsername(user.Username);
                    var fk_uid = UserService.GetUserByUsername(user.Username).Id;
                    var _profile = new BOL.Profile()
                    {
                        FK_Users_Id = fk_uid,
                        Contact = "Not Set",
                        Email = user.Email,
                        ProfileImage = "user.png",
                        Address = "Not Set",
                        Gender = "Not Set",
                        DOB = new DateTime(2000,01,01,01,00,00),
                        Relationship = "Not Set",
                        Religion = "Not Set"
                    };
                    DataAccessFactory.ProfileDataAccess().Add(_profile);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static IEnumerable<RegistrationDto> GetAllUser()
        {
            return DataAccessFactory.UserDataAccess().Get().Select(Mapper.Map<User, RegistrationDto>);
        }
    }
}
