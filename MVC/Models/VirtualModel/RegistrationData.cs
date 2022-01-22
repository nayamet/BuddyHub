using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VirtualModel
{
    public class RegistrationData
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Must more than 3 character long.")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$",ErrorMessage = "Password must contain a small, a capital letter and digit")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}