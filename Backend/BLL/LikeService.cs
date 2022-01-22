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
    public class LikeService
    {
        public static void AddLike(LikeDto like)
        {
            DataAccessFactory.LikeDataAccess().Add(Mapper.Map<Like>(like));
            int notifierId = like.FK_Users_Id;
            //string notifierName = UserService.GetUserById(notifierId).Name;
            int userId = PostService.GetPostById(like.FK_Posts_Id).FK_Users_Id;
            string msg = "liked your post";
            string link = "";
            NotificationService.AddNotification(userId, notifierId, msg, link);
        }

        public static LikeDto GetLikeById(int id)
        {
            var likes = DataAccessFactory.LikeDataAccess().Get().Select(Mapper.Map<Like, LikeDto>);

            var data = (from l in likes
                        where l.Id == id
                        select l).FirstOrDefault();
            return data;
        }

        public static IEnumerable<LikeDto> GetLikeByPostId(int id)
        {
            var likes = DataAccessFactory.LikeDataAccess().Get().Select(Mapper.Map<Like, LikeDto>);

            var data = (from l in likes
                        where l.FK_Posts_Id == id
                        select l);
            return data;
        }

        public static bool DeleteLike(int id)
        {
            var _like = DataAccessFactory.LikeDataAccess().Get(id);

            if (_like == null) { return false; }
            else
            {
                DataAccessFactory.LikeDataAccess().Delete(id);
                return true;
            }
        }
    }
}
