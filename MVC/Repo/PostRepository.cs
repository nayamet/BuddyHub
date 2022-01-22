using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Repo
{
    public static class PostRepository
    {
        static buddyhubEntities db;
        static PostRepository()
        {
            db = new buddyhubEntities();
        }
        public static List<PostData> GetPostData()
        {
            var posts = (from u in db.Users
                         join p in db.Posts on u.Id equals p.FK_Users_Id
                         where p.Status == 1
                         select new PostData()
                         {
                             PostId = p.Id,
                             PostText = p.PostsText,
                             CreatedAt = (DateTime)p.CreatedAt,
                             Status = (int)p.Status,
                             Username = u.Username,
                             Likes = (from l in db.Likes where l.FK_Posts_Id==p.Id select l).ToList(),
                             Comments = (from c in db.Comments
                                         join u1 in db.Users on c.FK_Users_Id equals u1.Id
                                         where c.FK_Posts_Id == p.Id
                                         select new CommentData()
                                         {  
                                             Id = c.Id,
                                             FK_Posts_Id = p.Id,
                                             FK_Users_Id = u1.Id,
                                             CreatedAt = c.CreatedAt,
                                             FK_Username = u1.Username,
                                             Text = c.Text
                                         }).OrderByDescending(c => c.CreatedAt).ToList()
                         }).OrderByDescending(p => p.CreatedAt).ToList();
            return posts;
        }
        public static List<PostData> GetAllPostAdmin()
        {
            var posts = (from u in db.Users
                         join p in db.Posts on u.Id equals p.FK_Users_Id
                         select new PostData()
                         {
                             PostId = p.Id,
                             PostText = p.PostsText,
                             CreatedAt = (DateTime)p.CreatedAt,
                             Status = (int)p.Status,
                             Username = u.Username,
                             Likes = (from l in db.Likes where l.FK_Posts_Id == p.Id select l).ToList(),
                             Comments = (from c in db.Comments
                                         join u1 in db.Users on c.FK_Users_Id equals u1.Id
                                         where c.FK_Posts_Id == p.Id
                                         select new CommentData()
                                         {
                                             Id = c.Id,
                                             FK_Posts_Id = p.Id,
                                             FK_Users_Id = u1.Id,
                                             CreatedAt = c.CreatedAt,
                                             FK_Username = u1.Username,
                                             Text = c.Text
                                         }).OrderByDescending(c => c.CreatedAt).ToList()
                         }).OrderByDescending(p => p.CreatedAt).ToList();
            return posts;
        }
        public static List<PostData> GetPostBySearch(String text)
        {
            var posts = (from u in db.Users
                         join p in db.Posts on u.Id equals p.FK_Users_Id
                         where p.PostsText.Contains(text)
                         select new PostData()
                         {
                             PostId = p.Id,
                             PostText = p.PostsText,
                             CreatedAt = (DateTime)p.CreatedAt,
                             Status = (int)p.Status,
                             Username = u.Username,
                             Likes = (from l in db.Likes where l.FK_Posts_Id == p.Id select l).ToList(),
                             Comments = (from c in db.Comments
                                         join u1 in db.Users on c.FK_Users_Id equals u1.Id
                                         where c.FK_Posts_Id == p.Id
                                         select new CommentData()
                                         {
                                             Id = c.Id,
                                             FK_Posts_Id = p.Id,
                                             FK_Users_Id = u1.Id,
                                             CreatedAt = c.CreatedAt,
                                             FK_Username = u1.Username,
                                             Text = c.Text
                                         }).OrderByDescending(c => c.CreatedAt).ToList()
                         }).OrderByDescending(p => p.CreatedAt).ToList();
            return posts;
        }
        public static PostData GetPostDataById(int id)
        {
            var posts = (from u in db.Users
                         join p in db.Posts on u.Id equals p.FK_Users_Id 
                         where p.Id == id
                         select new PostData()
                         {
                             PostId = p.Id,
                             PostText = p.PostsText,
                             CreatedAt = (DateTime)p.CreatedAt,
                             Status = (int)p.Status,
                             Username = u.Username,
                             Likes = (from l in db.Likes where l.FK_Posts_Id == p.Id select l).ToList(),
                             Comments = (from c in db.Comments join u1 in db.Users on c.FK_Users_Id equals u1.Id  where c.FK_Posts_Id == p.Id select new CommentData()
                             {
                                 Id = c.Id,
                                 FK_Posts_Id = p.Id,
                                 FK_Users_Id = u1.Id,
                                 CreatedAt = c.CreatedAt,
                                 FK_Username = u1.Username,
                                 Text = c.Text
                             }).OrderByDescending(c => c.CreatedAt).ToList()
                         }).ToList().FirstOrDefault();
            return posts;
        }

        public static void ChangeStatus(int postId)
        {
          
            var tempPost = (from p in db.Posts.Where(p => p.Id == postId) select p).FirstOrDefault();

            if (tempPost.Status == 1)
            {
                tempPost.Status = 2;
                db.SaveChanges();
            }
            else
            {
                tempPost.Status = 1;
                db.SaveChanges();

            }
        }

        public static void CreatePost(PostData pd, int UserId)
        {
            Post p = new Post()
            {
                PostsText = pd.PostText,
                CreatedAt = DateTime.Now,
                Status = 1,
                FK_Users_Id = UserId
            };
            db.Posts.Add(p);
            db.SaveChanges();
        }

        public static void CreateComment(string username, int postId, string ctext)
        {
            var user = UserRepo.FindUser(username);
            System.Diagnostics.Debug.WriteLine("Post is Commented"+ ctext);

            db.Comments.Add(new Comment() { FK_Users_Id = user.Id, FK_Posts_Id = postId, CreatedAt = DateTime.Now, Text = ctext });
            NotificationRepository.SetNotification(UserRepo.FindUserByPostId(postId).Username, UserRepo.FindUserById(user.Id).Username, UserRepo.FindUserById(user.Id).Name, "commented on your post", "/Post/View/"+postId);
            db.SaveChanges();

           
        }
        public static List<CommentData> GetAllComment()
        {
            var Comments = (from c in db.Comments
                            join u1 in db.Users on c.FK_Users_Id equals u1.Id
                            select new CommentData()
                            {
                                Id = c.Id,
                                FK_Users_Id = u1.Id,
                                CreatedAt = c.CreatedAt,
                                FK_Username = u1.Username,
                                Text = c.Text
                            }).OrderByDescending(c => c.CreatedAt).ToList();
            return Comments;
        }

            public static bool EditPost(PostData pd, int PoId)
        {
            var post = db.Posts.Single(p => p.Id == PoId);
            post.PostsText = pd.PostText;
            db.SaveChanges();
            return true;
        }

        public static bool RemovePost(int PoId, int UserId)
        {
            var post = db.Posts.Find(PoId);
            if(post.FK_Users_Id == UserId)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                return true;
            }
            return false;
            
        }
        public static bool RemoveComment(int PoId, int UserId)
        {
            var comment = db.Comments.Find(PoId);
            if (comment.FK_Users_Id == UserId)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
                return true;
            }
            return false;

        }

        public static List<PostData> GetMyPost(int UserId)
        {
            List<PostData> pd = new List<PostData>();
            var PoId = (from p in db.Posts
                        where p.FK_Users_Id == UserId
                        select p.Id).ToList();
            foreach(var po in PoId)
            {
                PostData p = GetPostDataById(po);
                pd.Add(p);
            }
            return pd;
        }
        public static void CreateLike(string username,int postId)
        {
            var user = UserRepo.FindUser(username);
            if (!IsPostLikedByUser(user.Id, postId))
            {
                db.Likes.Add(new Like() { FK_Users_Id = user.Id, FK_Posts_Id = postId });
                var te = UserRepo.FindUserByPostId(postId).Username;
                var dg = UserRepo.FindUserById(user.Id).Username;
                NotificationRepository.SetNotification(UserRepo.FindUserByPostId(postId).Username, UserRepo.FindUserById(user.Id).Username, UserRepo.FindUserById(user.Id).Name, "liked on your post", "/Post/View/" + postId);
                db.SaveChanges();
                System.Diagnostics.Debug.WriteLine("Post is Liked");

            }
            else
            {
                var like = (from l in db.Likes where l.FK_Posts_Id == postId && l.FK_Users_Id == user.Id select l).FirstOrDefault();
                db.Likes.Remove(like);
                db.SaveChanges();
                System.Diagnostics.Debug.WriteLine("Post is Uniked");

            }

        }
        public static bool IsPostLikedByUser(int uid, int postId)
        {
            return db.Likes.Any(l => l.FK_Posts_Id == postId && l.FK_Users_Id==uid);
        }
    }
}