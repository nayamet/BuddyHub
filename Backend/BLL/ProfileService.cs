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
    public class ProfileService
    {
        public static IEnumerable<ProfileDto> GetAllProfile()
        {
            // IEnumerable er vitore IEnumerable er same instace create kora jai na
            var profs = new List<ProfileDto>();
            var profiles = DataAccessFactory.ProfileDataAccess().Get().Select(Mapper.Map<BOL.Profile, ProfileDto>);
            foreach(var pro in profiles)
            {
                var user = UserService.GetUserById(pro.FK_Users_Id);
                pro.Username = user.Username;
                pro.Name = user.Name;
                pro.Status = user.Status;
                pro.Type = user.Type;
                profs.Add(pro);
            }
            return profs;
        }

        public static ProfileDto GetProfileById(int id)
        {
            var _Profile = DataAccessFactory.ProfileDataAccess().Get(id);
            var user = UserService.GetUserById(id);
            if (_Profile != null )
            {
                var pro = Mapper.Map<BOL.Profile, ProfileDto>(_Profile);
                pro.Username = user.Username;
                pro.Name = user.Name;
                pro.Status = user.Status;
                pro.Type = user.Type;
                return pro;
            }
            else
            {
                return null;

            }
        }

        public static bool RegisterProfile(ProfileDto user)
        {
            if(user == null) { return false; }
            else
            {
                DataAccessFactory.ProfileDataAccess().Add(Mapper.Map<ProfileDto, BOL.Profile>(user));
                return true;
            }
        }

        public static bool DeleteProfileById(int id)
        {
            var _Profile = DataAccessFactory.ProfileDataAccess().Get(id);
            if (_Profile == null) { return false; }
            else
            {
                DataAccessFactory.ProfileDataAccess().Delete(id);
                return true;
            }
        }

        public static bool EditProfile(ProfileDto user)
        {
            var _Profile = DataAccessFactory.ProfileDataAccess().Get(user.FK_Users_Id);

            if (_Profile == null) { return false; }
            else
            {
                DataAccessFactory.ProfileDataAccess().Edit(Mapper.Map<ProfileDto, BOL.Profile>(user));
                return true;
            }
        }
    }
}
