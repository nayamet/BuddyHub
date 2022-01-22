using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NotificationRepo : IRepository<Notification, int>
    {
        readonly BuddyDbContext db;
        public NotificationRepo(BuddyDbContext db) => this.db = db;

        public void Add(Notification noti)
        {
            db.Notifications.Add(noti);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
           
        }

        public void Edit(Notification noti)
        {
           
        }

        public Notification Get(int id) { return null; }

        public IEnumerable<Notification> Get() => db.Notifications;
    }
}
