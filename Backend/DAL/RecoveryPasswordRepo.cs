using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class RecoveryPasswordRepo : IRepository<RecoveryPassword, int>
    {
        readonly BuddyDbContext db;

        public RecoveryPasswordRepo(BuddyDbContext db)
        {
            this.db = db;
        }

        public void Add(RecoveryPassword entity)
        {
            db.RecoveryPasswords.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var p = db.RecoveryPasswords.Find(id);
            db.RecoveryPasswords.Remove(p);
            db.SaveChanges();
        }

        public void Edit(RecoveryPassword entity)
        {
            var r = db.RecoveryPasswords.Find(entity.Id);
            db.Entry(r).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public IEnumerable<RecoveryPassword> Get()
        {
            return db.RecoveryPasswords;
        }

        public RecoveryPassword Get(int id)
        {
            return db.RecoveryPasswords.Find(id);
        }
    }
}

