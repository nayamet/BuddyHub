using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class OAuthDto
    {
        public int Id { get; set; }
        [Required]
        public string OriginId { get; set; }
        [Required]
        public string OriginName { get; set; }

        //From the User table
        [Required]
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }

        //From the Profile table
        public string Contact { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ProfileImage { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Religion { get; set; }
        public string Relationship { get; set; }
       

    }
}
