using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SaveRepo : IRepository<Save, int>
    {
        readonly BuddyDbContext db;
        public SaveRepo(BuddyDbContext db) => this.db = db;

        public void Add(Save s)
        {
            db.Saves.Add(s);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var save = db.Saves.Find(id);
            db.Saves.Remove(save);
            db.SaveChanges();
        }

        public void Edit(Save entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Save> Get()
        {
            return db.Saves;
        }

        public Save Get(int id)
        {
            return db.Saves.Find(id);
        }
    }
}
