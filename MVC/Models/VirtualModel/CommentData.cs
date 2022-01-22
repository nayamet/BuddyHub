using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VirtualModel
{
    public class CommentData
    {
        public int Id { get; set; }
        public Nullable<int> FK_Posts_Id { get; set; }
        public Nullable<int> FK_Users_Id { get; set; }
        public string FK_Username { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string Text { get; set; }

       
    }
}