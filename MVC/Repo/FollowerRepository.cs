using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using BuddyHub.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Repo
{
    public static class FollowerRepository
    {
        static buddyhubEntities db;
        static FollowerRepository()
        {
            db = new buddyhubEntities();
        }

        public static FollowerData CheckFollowing(int UserId, int FollowingUserId)
        {
            var fd = (from f in db.Followers
                      where f.FR_Follower_Users_Id == UserId && f.FR_Following_Users_Id == FollowingUserId
                      select f).FirstOrDefault();
            if(fd != null)
            {
                FollowerData f = new FollowerData()
                {
                    Id = fd.Id,
                    FR_Follower_Users_Id = fd.FR_Follower_Users_Id,
                    FR_Following_Users_Id = fd.FR_Following_Users_Id
                };

                return f;
            }
            return null;
        }

        public static void DoFollow(int UserId, int FollowingUserId)
        {
            Follower f = new Follower()
            {
                FR_Follower_Users_Id = UserId,
                FR_Following_Users_Id = FollowingUserId
            };
            db.Followers.Add(f);
            NotificationRepository.SetNotification(UserRepo.FindUserById(UserId).Username, UserRepo.FindUserById(FollowingUserId).Username, UserRepo.FindUserById(FollowingUserId).Name, "started following you", "/Follower/ShowFollowers");
            db.SaveChanges();
        }

        public static void UnDoFollow(FollowerData fd)
        {
            int FoId = fd.Id;
            Follower f = db.Followers.Find(FoId);
            db.Followers.Remove(f);
            db.SaveChanges();
        }

        public static List<ProfileData> ShowFollowers(int UserId)
        {
            List<ProfileData> pd = new List<ProfileData>();
            var FollowersId = (from f in db.Followers
                               where f.FR_Following_Users_Id == UserId
                               select f.FR_Follower_Users_Id).ToList();
            foreach(var f in FollowersId)
            {
                ProfileData profile = ProfileRepository.GetProfileData((int)f);
                pd.Add(profile);
            }
            return pd;
        }

        public static List<ProfileData> ShowFollowing(int UserId)
        {
            List<ProfileData> pd = new List<ProfileData>();
            var FollowingId = (from f in db.Followers
                               where f.FR_Follower_Users_Id == UserId
                               select f.FR_Following_Users_Id).ToList();
            foreach (var f in FollowingId)
            {
                ProfileData profile = ProfileRepository.GetProfileData((int)f);
                pd.Add(profile);
            }
            return pd;
        }
    }
}