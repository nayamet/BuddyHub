using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentRepo : IRepository<Comment, int>
    {
        readonly BuddyDbContext db;
        public CommentRepo(BuddyDbContext db) => this.db = db;

        public void Add(Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var c = db.Comments.Find(id);
            db.Comments.Remove(c);
            db.SaveChanges();

        }

        public void Edit(Comment comment)
        {
            var c = db.Comments.Find(comment.Id);
            db.Entry(c).CurrentValues.SetValues(comment);
            db.SaveChanges();
        }

        public Comment Get(int id) => db.Comments.Find(id);

        public IEnumerable<Comment> Get() => db.Comments;
    }
}
