using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuddyHub.Models.VirtualModel
{
    public class RecoveryPasswordData
    {
        public int Id { get; set; }
        [Required]
        public string QuestionOne { get; set; }
        [Required]
        public string QuestionOneAnswer { get; set; }
        [Required]
        public string QuestionTwo { get; set; }
        [Required]
        public string QuestionTwoAnswer { get; set; }
        public Nullable<int> FK_Users_Id { get; set; }
    }
}