using AutoMapper;
using BOL;
using BOL.Dto;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ChangePasswordService
    {
        public static string ChangePassword(ChangePasswordDto cp)
        {
            var _User = DataAccessFactory.UserDataAccess().Get().Where(u => u.Username == cp.Username).FirstOrDefault();
            if (_User != null)
            {
                var user = Mapper.Map<User, UserDto>(_User);
                if(user.Password == cp.OldPassword)
                {
                    user.Password = cp.NewPassword;
                    var u = Mapper.Map<UserDto, User>(user);
                    DataAccessFactory.UserDataAccess().Edit(u);
                    return "Password changed successfully";
                }
                else
                {
                    return "Wrong password";
                }
            }
            else
            {
                return "Username not found";

            }
        }
    }
}
