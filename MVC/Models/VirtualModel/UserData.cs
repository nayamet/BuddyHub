using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VirtualModel
{
    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
    }
}