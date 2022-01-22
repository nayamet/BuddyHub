using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using BuddyHub.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Repo
{
    public static class ProfileRepository
    {
        static buddyhubEntities db;
        static ProfileRepository()
        {
            db = new buddyhubEntities();
        }

       
   
        public static void UpdateProfile(string Username, ProfileData pd)
        {
            var UserId = UserRepo.FindUser(Username).Id;
            var profile = db.Profiles.First(p => p.FK_Users_Id == UserId);
            profile.Contact = pd.Contact;
            profile.Email = pd.Email;
            profile.Address = pd.Address;
            profile.Gender = pd.Gender;
            profile.DOB = pd.DOB;
            profile.Religion = pd.Religion;
            profile.Relationship = pd.Relationship;
            db.SaveChanges();

            var user = db.Users.First(u => u.Username == Username);
            user.Name = pd.Name;

            db.SaveChanges();

        }

        public static void AddSocialProfile(SocialLink sl)
        {

            SocialLink social = new SocialLink()
            {
                SocialName = sl.SocialName,
                Link = sl.Link,
                FK_Users_Id = sl.FK_Users_Id
            };

            db.SocialLinks.Add(social);
            db.SaveChanges();

        }

        public static List<ProfileData> GetAllProfileData()
        {

            var AllProfile = (from u in db.Users
                        join u2 in db.Profiles on u.Id equals u2.FK_Users_Id
                        select new ProfileData()
                        {
                            Name = u.Name,
                            Type = u.Type,
                            Email = u2.Email,
                            Contact = u2.Contact,
                            PImage = u2.ProfileImage,
                            Address = u2.Address,
                            Gender = u2.Gender,
                            DOB = (DateTime)u2.DOB,
                            Status = u.Status,
                            Religion = u2.Religion,
                            FK_Users_Id = u2.FK_Users_Id,
                            ProfileImage = u2.ProfileImage,
                            Relationship = u2.Relationship,
                            Username = u.Username,
                            WorkProfiles = (from w in db.WorksProfiles
                                            where w.FK_Users_Id == u.Id
                                            select new WorkProfileData()
                                            {
                                                Id = w.Id,
                                                Institution = w.Institution,
                                                StartYear = w.StartYear,
                                                EndYear = w.EndYear,
                                                Position = w.Position,
                                                FK_Users_Id = w.FK_Users_Id
                                            }).ToList(),
                            
                        }).ToList();
            return AllProfile;
        }

        internal static void RemoveSocialProfile(int id)
        {
            var ss = db.SocialLinks.Find(id);
            db.SocialLinks.Remove(ss);
            db.SaveChanges();
        }

        public static ProfileData GetProfileData(string UserName)
        {
            
            var test = (from u in db.Users
                        join u2 in db.Profiles on u.Id equals u2.FK_Users_Id
                        where u.Username == UserName
                        select new ProfileData()
                        {
                            Name = u.Name,
                            Type = u.Type,
                            Email = u2.Email,
                            Contact = u2.Contact,
                            PImage = u2.ProfileImage,
                            Address = u2.Address,
                            Gender = u2.Gender,
                            Status = u.Status,
                            DOB = (DateTime)u2.DOB,
                            Religion = u2.Religion,
                            ProfileImage = u2.ProfileImage,
                            Relationship = u2.Relationship,
                            Username = u.Username,
                            WorkProfiles = (from w in db.WorksProfiles where w.FK_Users_Id==u.Id select new WorkProfileData()
                            {
                                Id = w.Id,
                                Institution = w.Institution,
                                StartYear = w.StartYear,
                                EndYear = w.EndYear,
                                Position = w.Position,
                                FK_Users_Id = w.FK_Users_Id
                            }).ToList(),
                            FK_Users_Id = u2.FK_Users_Id,
                            Posts = (from p in db.Posts
                                     where p.FK_Users_Id == u.Id
                                     select new PostData()
                                     {
                                         PostId = p.Id,
                                         PostText = p.PostsText,
                                         CreatedAt = (DateTime)p.CreatedAt,
                                         Status = (int)p.Status,
                                         Username = u.Username,
                                         FK_Users_Id = u2.FK_Users_Id,
                                         Likes = (from l in db.Likes where l.FK_Posts_Id == p.Id select l).ToList(),
                                         Comments = (from c in db.Comments
                                                     join u1 in db.Users on c.FK_Users_Id equals u1.Id
                                                     where c.FK_Posts_Id == p.Id
                                                     select new CommentData()
                                                     {
                                                         FK_Posts_Id = p.Id,
                                                         FK_Users_Id = u1.Id,
                                                         CreatedAt = c.CreatedAt,
                                                         FK_Username = u1.Username,
                                                         Text = c.Text
                                                     }).OrderByDescending(c => c.CreatedAt).ToList()
                                     }).ToList(),
                            Followers = (from f in db.Followers
                                         where f.FR_Following_Users_Id == u.Id
                                         select new FollowerData()
                                         {
                                             FR_Follower_Users_Id = f.FR_Follower_Users_Id,
                                             FR_Following_Users_Id = f.FR_Following_Users_Id
                                         }).ToList(),
                            Socials = (from s in db.SocialLinks where s.FK_Users_Id == u.Id select s).ToList(),
                        }).FirstOrDefault();
            return test;
        }
        public static ProfileData GetProfileData(int UserId)
        {
            var TempUser = UserRepo.FindUserById(UserId);
            return ProfileRepository.GetProfileData(TempUser.Username); 
        }



    }
}