using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VirtualModel
{
    public class FollowerData
    {
        public int Id { get; set; }
        public Nullable<int> FR_Follower_Users_Id { get; set; }
        public Nullable<int> FR_Following_Users_Id { get; set; }
    }
}