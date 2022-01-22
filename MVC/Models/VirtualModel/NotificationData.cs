using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VirtualModel
{
    public class NotificationData
    {
        public string FK_Username { get; set; }
        public string FK_Notifier_Username { get; set; }
        public string FK_Notifier_Name { get; set; }

        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string Message { get; set; }
        public string GotoLink { get; set; }
    }
}