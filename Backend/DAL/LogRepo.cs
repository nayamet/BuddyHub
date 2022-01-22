using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LogRepo : IRepository<Log, int>
    {
        readonly BuddyDbContext db;
        public LogRepo(BuddyDbContext db) => this.db = db;
        public void Add(Log entity)
        {
            db.Logs.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var _log = db.Logs.FirstOrDefault(x => x.Id == id);
            db.Logs.Remove(_log);
            db.SaveChanges();
        }

        public void Edit(Log entity)
        {
            var _log = db.Logs.Find(entity.Id);
            db.Entry(_log).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public IEnumerable<Log> Get()
        {
            return db.Logs;
        }

        public Log Get(int id)
        {
            return db.Logs.FirstOrDefault(x => x.Id == id);
        }
    }
}
