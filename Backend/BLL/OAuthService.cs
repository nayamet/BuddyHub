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
    public class OAuthService
    {
        public static IEnumerable<OAuthDto> GetAllOAuth()
        {
            var t = DataAccessFactory.OAuthDataAccess().Get();
            var u = new List<OAuthDto>();

            foreach(OAuth i in t)
            {
                u.Add(new OAuthDto()
                {
                    OriginId = i.OriginId,
                    OriginName = i.OriginName,
                    Name = i.User.Name,
                    Username = i.User.Username,
                    Password = i.User.Password,
                    Type = i.User.Type,
                    Status = i.User.Status,
                    Contact = i.User.Profile.Contact,
                    Email = i.User.Profile.Email,
                    ProfileImage = i.User.Profile.ProfileImage,
                    Address = i.User.Profile.Address,
                    Gender =  i.User.Profile.Gender,
                    DOB = i.User.Profile.DOB,
                    Relationship = i.User.Profile.Relationship,
                    Religion = i.User.Profile.Religion

                });
            }
            
            return u;
        }

        public static bool IsOAuthExists(OAuthDto oAuth)
        {
            if(oAuth != null)
            {
                var _OAuth = DataAccessFactory.OAuthDataAccess().Get().Where(o => o.OriginId == oAuth.OriginId && o.OriginName == oAuth.OriginName).FirstOrDefault();
                if(_OAuth == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static OAuthDto GetOAuthById(int id)
        {
            var _OAuth = DataAccessFactory.OAuthDataAccess().Get(id);
            var _User = DataAccessFactory.UserDataAccess().Get(id);
            if (_OAuth != null )
            {
                var temp = Mapper.Map<OAuthDto>(_OAuth);
                return Mapper.Map<OAuth, OAuthDto>(_OAuth);
            }
            else
            {
                return null;

            }
        }

        public static AuthenticUserDto LoginOAuth(OAuthDto oAuth)
        {
            if (IsOAuthExists(oAuth))
            {
                var _oauth = GetAllOAuth().FirstOrDefault(x => x.OriginId == oAuth.OriginId && x.OriginName == oAuth.OriginName);
                var _user = UserService.GetUserByUsername(_oauth.Username);
                if (LogService.UpdateLogTimeAndStatus(_user.Id))
                {
                    LogService.SetLog(new LogDto()
                    {
                        Country = "Bangladesh",
                        Ip = "192.168.50.1",
                        FK_Users_Id = _user.Id
                    });
                }
                return UserService.GetAuthenticUserInfoByUsername(_oauth.Username);
            }
            else
            {
                return null;
            }
        }

        public static bool RegisterOAuth(OAuthDto OAuth)
        {
            if (!IsOAuthExists(OAuth))
            {
                bool isCreated = UserService.RegisterUser(new UserDto()
                {
                    Name = OAuth.Name,
                    Username = OAuth.Username,
                    Type = "general",
                    Status = 1,
                    Password = OAuth.Password
                });
                if (isCreated)
                {
                    var fk_uid = UserService.GetUserByUsername(OAuth.Username).Id;
                    var _oAuth = new OAuth()
                    {
                        FK_Users_Id = fk_uid,
                        OriginId = OAuth.OriginId,
                        OriginName = OAuth.OriginName
                    };
                    DataAccessFactory.OAuthDataAccess().Add(_oAuth);
                    var _profile = new BOL.Profile()
                    {
                        FK_Users_Id = fk_uid,
                        Contact = "Not Set",
                        Email = OAuth.Email,
                        ProfileImage = OAuth.ProfileImage,
                        Address = "Not Set",
                        Gender = "Not Set",
                        DOB = new DateTime(2000, 01, 01, 01, 00, 00),
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
            else
            {
                return false;
            }
        }

    }
}
