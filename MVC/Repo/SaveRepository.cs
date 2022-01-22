using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Repo
{
    public static class SaveRepository
    {
        static buddyhubEntities db;
        static SaveRepository()
        {
            db = new buddyhubEntities();
        }

        public static void SavePost(int PoId, int UserId)
        {
            Save s = new Save()
            {
                FK_Posts_Id = PoId,
                FK_Users_Id = UserId
            };
            db.Saves.Add(s);
            db.SaveChanges();
        }

        public static SaveData CheckForPost(int PoId, int UserId)
        {
            var post = (from p in db.Saves
                        where p.FK_Posts_Id == PoId && p.FK_Users_Id == UserId
                        select p).FirstOrDefault();
            if(post != null)
            {
                SaveData sd = new SaveData()
                {
                    Id = post.Id,
                    FK_Posts_Id = post.FK_Posts_Id,
                    FK_Users_Id = post.FK_Users_Id
                };
                return sd;
            }
            return null;
        }

        public static void RemoveSavePost(SaveData sd)
        {
            int SaveId = sd.Id;
            var s = db.Saves.Find(SaveId);
            db.Saves.Remove(s);
            db.SaveChanges();
        }



        public static List<PostData> GetPostByUserId(int UserId)
        {
            List<PostData> pd = new List<PostData>();
            var postsId = (from p in db.Saves
                           where p.FK_Users_Id == UserId
                           select p.FK_Posts_Id).ToList();
            
            foreach(var Id in postsId)
            {
                var post = PostRepository.GetPostDataById((int)Id);
                pd.Add(post);
            }

            return pd;
        }
    }
}