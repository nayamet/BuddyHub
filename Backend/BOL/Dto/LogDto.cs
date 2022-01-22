using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class LogDto
    {
        public int Id { get; set; }

        public int FK_Users_Id { get; set; }

        public string Token { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime EndAt { get; set; }
        public int Status { get; set; }
  
        public string Ip { get; set; }
        public string Country { get; set; }
        public string Username { get; set; }

    }
}
