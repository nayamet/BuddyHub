using BOL;
using BOL.Dto;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NotificationService
    {
        public static void AddNotification(int userId, int notifierId, string msg, string link)
        {
            var noti = new Notification()
            {
                FK_Users_Id = userId,
                FK_Notifier_Users_Id = notifierId,
                Message = msg,
                CreatedAt = DateTime.Now,
                GotoLink = link
            };
            DataAccessFactory.NotificationDataAccess().Add(noti);
        }
        public static IEnumerable<NotificationDto> GetNotificationByUserId(int id)
        {
            var _notif = DataAccessFactory.NotificationDataAccess().Get().Where(x => x.FK_Users_Id == id);
            var _notifById = new List<NotificationDto>();
            foreach(Notification n in _notif)
            {
                _notifById.Add(new NotificationDto()
                {
                    CreatedAt = n.CreatedAt,
                    FK_Notifier_Users_Id = n.FK_Notifier_Users_Id ?? 0,
                    FK_Notifier_Users_Name = UserService.GetUserById(n.FK_Notifier_Users_Id ?? 0).Name,
                    FK_Users_Id = n.FK_Users_Id ?? 0,
                    FK_Users_Name = UserService.GetUserById(n.FK_Users_Id ?? 0).Name,
                    Message = n.Message,
                    GotoLink = n.GotoLink

                }) ;
            }
            return _notifById.OrderBy(x => x.CreatedAt);
        }
    }
}
