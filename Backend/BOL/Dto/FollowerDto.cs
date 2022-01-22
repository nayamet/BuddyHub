using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class FollowerDto
    {
        public int Id { get; set; }
        public int FR_Follower_Users_Id { get; set; }
        public string FollowerName { get; set; }
        public int FR_Following_Users_Id { get; set; }
        public string FollowingName { get; set; }
    }
}
