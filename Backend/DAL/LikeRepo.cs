using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LikeRepo : IRepository<Like, int>
    {
        readonly BuddyDbContext db;
        public LikeRepo(BuddyDbContext db) => this.db = db;

        public void Add(Like like)
        {
            db.Likes.Add(like);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var l = db.Likes.Find(id);
            db.Likes.Remove(l);
            db.SaveChanges();

        }

        // Like edit er kisu nai

        public void Edit(Like like)
        {
            var _like = db.Likes.Find(like.Id);
            db.Entry(_like).CurrentValues.SetValues(like);
            db.SaveChanges();
        }

        public Like Get(int id) => db.Likes.Find(id);

        public IEnumerable<Like> Get() => db.Likes;


    }
}
