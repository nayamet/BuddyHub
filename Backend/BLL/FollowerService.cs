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
    public class FollowerService
    {
        public static void Add(FollowerDto fd)
        {
            DataAccessFactory.FollowerDataAccess().Add(Mapper.Map<Follower>(fd));
        }

        public static IEnumerable<FollowerDto> GetFollowersByUserId(int id)
        {
            var fd = new List<FollowerDto>();
            var followers = DataAccessFactory.FollowerDataAccess().Get();
            var userfoll = (from f in followers
                            where f.FR_Following_Users_Id == id
                            select f);
            var usf = userfoll.Select(Mapper.Map<Follower, FollowerDto>);

            foreach (var f in usf)
            {
                var follower = UserService.GetUserById(f.FR_Follower_Users_Id);
                var following = UserService.GetUserById(f.FR_Following_Users_Id);
                f.FollowerName = follower.Name;
                //f.FollowingName = following.Name;
                fd.Add(f);
            }
            return fd;
        }

        public static IEnumerable<FollowerDto> GetFollowingByUserId(int id)
        {
            var fd = new List<FollowerDto>();
            var followings = DataAccessFactory.FollowerDataAccess().Get();
            var userfoll = (from f in followings
                            where f.FR_Follower_Users_Id == id
                            select f);
            var usf = userfoll.Select(Mapper.Map<Follower, FollowerDto>);

            foreach (var f in usf)
            {
                var follower = UserService.GetUserById(f.FR_Follower_Users_Id);
                var following = UserService.GetUserById(f.FR_Following_Users_Id);
                //f.FollowerName = follower.Name;
                f.FollowingName = following.Name;
                fd.Add(f);
            }
            return fd;
        }

        public static bool DeleteFollowing(int id)
        {
            var foll = DataAccessFactory.FollowerDataAccess().Get(id);

            if (foll == null) { return false; }
            else
            {
                DataAccessFactory.FollowerDataAccess().Delete(id);
                return true;
            }
        }
    }
}
