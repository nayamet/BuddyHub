using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class PostRepo : IRepository<Post, int>
    {
        readonly BuddyDbContext db;

        public PostRepo(BuddyDbContext db)
        {
            this.db = db;
        }

        public void Add(Post entity)
        {
            db.Posts.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var p = db.Posts.Find(id);
            db.Posts.Remove(p);
            db.SaveChanges();
        }

        public void Edit(Post entity)
        {
            var p = db.Posts.Find(entity.Id);
            db.Entry(p).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public IEnumerable<Post> Get()
        {
            return db.Posts;
        }

        public Post Get(int id)
        {
            return db.Posts.Find(id);
        }
    }
}