using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class NotificationDto
    {
        public string FK_Users_Name { get; set; }
        public int FK_Users_Id { get; set; }

        public string FK_Notifier_Users_Name { get; set; }
        public int FK_Notifier_Users_Id { get; set; }

        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string Message { get; set; }
        public string GotoLink { get; set; }
    }
}
