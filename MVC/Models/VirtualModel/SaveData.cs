using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VirtualModel
{
    public class SaveData
    {
        public int Id { get; set; }
        public Nullable<int> FK_Posts_Id { get; set; }
        public Nullable<int> FK_Users_Id { get; set; }
    }
}