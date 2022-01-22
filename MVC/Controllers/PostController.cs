using BuddyHub.Auth;
using BuddyHub.Models.VirtualModel;
using BuddyHub.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult View(int id)
        {
            return View(PostRepository.GetPostDataById(id));
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreatePost(PostData pd)
        {
            int UserId = (int)Session["UserId"];
            PostRepository.CreatePost(pd, UserId);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditPost(int id) 
        {
            //System.Diagnostics.Debug.WriteLine(PoId);

            string UserId = (string)Session["Username"];
            var post = PostRepository.GetPostDataById(id);
            if (post.Username.Replace(" ","") != UserId)
            {
                return RedirectToAction("Index", "Home");

            }
            return View(post);

        }

        [Authorize]
        [HttpPost]
        public ActionResult EditPost(PostData pd, int id)
        {
            var b = PostRepository.EditPost(pd, id);
            if(b)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(pd);
        }

        [Authorize]
        public ActionResult RemovePost(int id)
        {
            int UserId = (int)Session["UserId"];
            bool b = PostRepository.RemovePost(id, UserId);         
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult RemoveComment(int id)
        {
            int UserId = (int)Session["UserId"];
            bool b = PostRepository.RemoveComment(id, UserId);
            return RedirectToAction("Index", "Home");
        }
        [AdminAccess]
        public ActionResult AdminRemovePost(int id)
        {
            int UserId = (int)Session["UserId"];
            bool b = PostRepository.RemovePost(id, UserId);
            return RedirectToAction("AllPost", "Admin");
        }
        [Authorize]
        public ActionResult LikeOnPost(string username, int postId)
        {
            PostRepository.CreateLike(username, postId);
            return Redirect("/Home/Index");
        }
        [Authorize]
        public ActionResult CommentOnPost(string username, int postId, string ctext)
        {
            PostRepository.CreateComment(username, postId, ctext);
            return Redirect("/Post/View/"+postId);
        }
        [AdminAccess]
        public ActionResult ChangeStatus(int PostId)
        {
            PostRepository.ChangeStatus(PostId);
            return Redirect("/Admin/AllPost");
        }
        [Authorize]
        public ActionResult MyPost()
        {
            int UserId = (int)Session["UserId"];
            List<PostData> pd = PostRepository.GetMyPost(UserId);
            return View(pd);
        }
    }
}