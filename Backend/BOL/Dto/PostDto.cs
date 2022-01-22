using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BOL.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public string PostsText { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public int FK_Users_Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int CommentCount{get; set;}
        public int LikeCount { get; set; }
        /*public virtual Comment Comment { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual User User { get; set; }*/
    }
}
