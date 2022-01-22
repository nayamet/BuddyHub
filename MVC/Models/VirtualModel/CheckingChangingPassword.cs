using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VirtualModel
{
    public class CheckingChangingPassword
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}