using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProfileRepo : IRepository<Profile, int>
    {
        readonly BuddyDbContext db;
        public ProfileRepo(BuddyDbContext db) => this.db = db;

        public void Add(Profile Profile)
        {
                db.Profiles.Add(Profile);
                db.SaveChanges();
        }

        public void Delete(int id)
        {
            var _Profile = db.Profiles.FirstOrDefault(u => u.FK_Users_Id == id);
            db.Profiles.Remove(_Profile);
            db.SaveChanges();
           
        }

        public void Edit(Profile profile)
        {
            var _Profile = db.Profiles.Find(profile.FK_Users_Id);
            db.Entry(_Profile).CurrentValues.SetValues(profile);
            db.SaveChanges();
        }

        public Profile Get(int id) => db.Profiles.FirstOrDefault(u => u.FK_Users_Id == id);

        public IEnumerable<Profile> Get() => db.Profiles;
       

    }
}
