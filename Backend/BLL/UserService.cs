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
    public class UserService
    {
        public static IEnumerable<UserDto> GetAllUser()
        {
            return DataAccessFactory.UserDataAccess().Get().Select(Mapper.Map<User,UserDto>);
        }

        public static UserDto GetUserById(int id)
        {
            var _User = DataAccessFactory.UserDataAccess().Get(id);
            if (_User != null )
            {
                return Mapper.Map<User, UserDto>(_User);
            }
            else
            {
                return null;

            }
        }

   
        public static bool RegisterUser(UserDto user)
        {
            if(user == null || !IsUsernameAvailble(user.Username)) { return false; }
            else
            {
                DataAccessFactory.UserDataAccess().Add(Mapper.Map<UserDto,User>(user));
                return true;
            }
        }

        public static AuthenticUserDto GetAuthenticUserInfoByUsername(string username)
        {
            var _user = GetUserByUsername(username);
            var _profile = ProfileService.GetProfileById(_user.Id);
            var _token = LogService.GetTokenByUsername(username);
            return new AuthenticUserDto()
            {
                Id = _user.Id,
                Name = _user.Name,
                Username = _user.Username,
                Token = _token,
                Status = _user.Status,
                Type = _user.Type,
                ProfileImage = _profile.ProfileImage
            };
        }

        public static bool DeleteUserById(int id)
        {
            var _User = DataAccessFactory.UserDataAccess().Get(id);
            if (_User == null) { return false; }
            else
            {
                DataAccessFactory.UserDataAccess().Delete(id);
                return true;
            }
        }

        public static UserDto GetUserByUsername(string username)
        {
            var _User = DataAccessFactory.UserDataAccess().Get().Where(u => u.Username == username).FirstOrDefault();
            if (_User != null)
            {
                return Mapper.Map<User, UserDto>(_User);
            }
            else
            {
                return null;

            }
        }
        public static bool IsUsernameAvailble(string username)
        {
            return GetUserByUsername(username) == null ? true : false;
        }


        public static string IsAuthenticUser(LoginDto user)
        {
            var _user = GetUserByUsername(user.Username);
            if (_user == null) { return "notfound"; }
            else
            {
                if(_user.Password == user.Password)
                {
                    if(_user.Status == 1)
                    {
                        if (LogService.UpdateLogTimeAndStatus(_user.Id))
                        {
                            LogService.SetLog(new LogDto()
                            {
                                Country = "Bangladesh",
                                Ip = "192.168.50.1",
                                FK_Users_Id = _user.Id
                            });
                        }
                        return "authorized";
                    }
                    else
                    {
                        return "emailnotverified";
                    }
                   
                }
                else
                {
                    return "unauthorized";
                }
            }

        }

        public static bool EditUser(int id, UserDto user)
        {
            var _User = DataAccessFactory.UserDataAccess().Get(id);

            if (_User == null) { return false; }
            else
            {
                DataAccessFactory.UserDataAccess().Edit(Mapper.Map<UserDto, User>(user));
                return true;
            }
        }
    }
}
