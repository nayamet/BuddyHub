using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmailLogRepo : IRepository<EmailLog, int>
    {
        readonly BuddyDbContext db;
        public EmailLogRepo(BuddyDbContext db) => this.db = db;

        public void Add(EmailLog entity)
        {
            db.EmailLogs.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.EmailLogs.Remove(db.EmailLogs.Find(id));
            db.SaveChanges();
        }

        public void Edit(EmailLog entity)
        {
            var emailLog = db.EmailLogs.Find(entity.Id);
            db.Entry(emailLog).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public IEnumerable<EmailLog> Get()
        {
            return db.EmailLogs;
        }

        public EmailLog Get(int id)
        {
            return db.EmailLogs.Find(id);
        }
    }
}
