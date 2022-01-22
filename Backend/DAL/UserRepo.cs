using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepo : IRepository<User,int>
    {
        readonly BuddyDbContext db;
        public UserRepo(BuddyDbContext db) => this.db = db;

        public void Add(User user)
        {
                db.Users.Add(user);
                db.SaveChanges();
        }

        public void Delete(int id)
        {
            var _user = db.Users.FirstOrDefault(u => u.Id == id);
            db.Users.Remove(_user);
            db.SaveChanges();
           
        }

        public void Edit(User user)
        {
            var _user = db.Users.Find(user.Id);
            db.Entry(_user).CurrentValues.SetValues(user);
            db.SaveChanges();
        }

        public User Get(int id) => db.Users.FirstOrDefault(u => u.Id == id);

        public IEnumerable<User> Get() => db.Users;
       

    }
}
