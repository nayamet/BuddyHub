using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class RecoveryPasswordDto
    {
        public int Id { get; set; }
        public string QuestionOne { get; set; }
        public string QuestionOneAnswer { get; set; }
        public string QuestionTwo { get; set; }
        public string QuestionTwoAnswer { get; set; }
        public int FK_Users_Id { get; set; }
    }
}
