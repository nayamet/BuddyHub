using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Repo
{
    public class NotificationRepository
    {
        static buddyhubEntities db;
        static NotificationRepository()
        {
            db = new buddyhubEntities();
        }
        public static void SetNotification(string fkUser, string fkNotifier, string fkNotifierName, string Message, string gotoLink)
        {
            Notification p = new Notification()
            {
                FK_Notifier_Users_Id = UserRepo.FindUser(fkNotifier).Id,
                Message = Message,
                CreatedAt = DateTime.Now,
                GotoLink = gotoLink,
                FK_Users_Id = UserRepo.FindUser(fkUser).Id
            };
            db.Notifications.Add(p);
            db.SaveChanges();
        }
        public static List<NotificationData> GetNotificationDataFor(int id)
        {
            var uname = UserRepo.FindUserById(id).Username;
            var notifications = (from u in db.Users
                         join p in db.Notifications on u.Id equals p.FK_Notifier_Users_Id
                         where p.FK_Users_Id == id
                         select new NotificationData()
                         {
                             FK_Username = uname,
                             FK_Notifier_Username = u.Username,
                             FK_Notifier_Name = u.Name,
                             Message = p.Message,
                             GotoLink=p.GotoLink,
                             CreatedAt = p.CreatedAt
                         }).OrderByDescending(p => p.CreatedAt).ToList();
            return notifications;
        }
    }
}