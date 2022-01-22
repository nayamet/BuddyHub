using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class ProfileDto
    {

        public int FK_Users_Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public System.DateTime DOB { get; set; }
        public string Religion { get; set; }
        public string Relationship { get; set; }
    }
}
