using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VM
{
    public class ProfileData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string PImage { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime DOB { get; set; }
        public string Relationship { get; set; }
        public string Religion { get; set; }
        public string Username { get; set; }
        public Nullable<int> FK_Users_Id { get; set; }
        public int Status { get; internal set; }
        public string ProfileImage { get; internal set; }
        public virtual ICollection<WorkProfileData> WorkProfiles { get; set; }
        public virtual ICollection<PostData> Posts { get; set; }
        public virtual ICollection<FollowerData> Followers { get; set; }
        public List<SocialLink> Socials { get; internal set; }
    }
}