using BuddyHub.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        [Authorize]
        public ActionResult Index(int id)
        {
            return View(NotificationRepository.GetNotificationDataFor(id));
        }
    }
}