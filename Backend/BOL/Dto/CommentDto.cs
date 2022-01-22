using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int FK_Posts_Id { get; set; }
        public int FK_Users_Id { get; set; }
        public string Username { get; set; }

        public System.DateTime CreatedAt { get; set; }
        public string Text { get; set; }
    }
}
