using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OAuthRepo : IRepository<OAuth, int>
    {
        readonly BuddyDbContext db;
        public OAuthRepo(BuddyDbContext db) => this.db = db;

        public void Add(OAuth entity)
        {
            db.OAuths.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var _oAuth = db.OAuths.FirstOrDefault(u => u.FK_Users_Id == id);
            db.OAuths.Remove(_oAuth);
            db.SaveChanges();
        }

        public void Edit(OAuth entity)
        {

            var _oAuth = db.OAuths.Find(entity.FK_Users_Id);
            db.Entry(_oAuth).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public IEnumerable<OAuth> Get() => db.OAuths;

        public OAuth Get(int id) => db.OAuths.FirstOrDefault(u => u.FK_Users_Id == id);

    }
}
