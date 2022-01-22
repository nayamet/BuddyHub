using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class FollowerRepo : IRepository<Follower, int>
    {
        readonly BuddyDbContext db;

        public FollowerRepo(BuddyDbContext db)
        {
            this.db = db;
        }

        public void Add(Follower entity)
        {
            db.Followers.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var p = db.Followers.Find(id);
            db.Followers.Remove(p);
            db.SaveChanges();
        }

        public void Edit(Follower entity)
        {
            var p = db.Followers.Find(entity.Id);
            db.Entry(p).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public IEnumerable<Follower> Get()
        {
            return db.Followers;
        }

        public Follower Get(int id)
        {
            return db.Followers.Find(id);
        }
    }
}
