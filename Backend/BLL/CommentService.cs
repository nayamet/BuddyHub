using AutoMapper;
using BOL;
using BOL.Dto;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CommentService
    {
        public static void AddComment(CommentDto comment)
        {
            //comment.CreatedAt = DateTime.Now;   
            DataAccessFactory.CommentDataAccess().Add(Mapper.Map<Comment>(comment));
            int notifierId = comment.FK_Users_Id;
            //string notifierName = UserService.GetUserById(notifierId).Name;
            int userId = PostService.GetPostById(comment.FK_Posts_Id).FK_Users_Id;
            string msg = "commented on your post";
            string link = "";
            NotificationService.AddNotification(userId, notifierId, msg, link);
        }

        public static CommentDto GetCommentById(int id)
        {
            var comments = DataAccessFactory.CommentDataAccess().Get().Select(Mapper.Map<Comment, CommentDto>);

            var data = (from c in comments
                        where c.Id == id
                        select c).FirstOrDefault();
            return data;
        }

        public static IEnumerable<CommentDto> GetCommentByPostId(int id)
        {
            var comments = DataAccessFactory.CommentDataAccess().Get().Where(c=>c.FK_Posts_Id == id).Select(Mapper.Map<Comment, CommentDto>);

            var com = new List<CommentDto>();

            foreach (var d in comments)
            {
                d.Username = UserService.GetUserById(d.FK_Users_Id).Username;
                com.Add(d);
            }
            return com;
        }

        public static bool EditComment(CommentDto comment)
        {
            var _comment = DataAccessFactory.CommentDataAccess().Get(comment.Id);

            if (_comment == null) { return false; }
            else
            {
                DataAccessFactory.CommentDataAccess().Edit(Mapper.Map<CommentDto, Comment>(comment));
                return true;
            }
        }

        public static bool DeleteComment(int id)
        {
            var _comment = DataAccessFactory.CommentDataAccess().Get(id);

            if (_comment == null) { return false; }
            else
            {
                DataAccessFactory.CommentDataAccess().Delete(id);
                return true;
            }
        }
    }
}
