using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class AuthenticUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Type { get; set; }
        public string ProfileImage { get; set; }

        public int Status { get; set; }
    }
}
