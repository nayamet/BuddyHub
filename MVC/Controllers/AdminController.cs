using BuddyHub.Auth;
using BuddyHub.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [AdminAccess]
        public ActionResult Index()
        {
            ViewBag.TotalUser = ProfileRepository.GetAllProfileData().Count();
            ViewBag.TotalPost = PostRepository.GetPostData().Count();
            ViewBag.TotalComment = PostRepository.GetAllComment().Count();
            return View();
        }
        [AdminAccess]
        public ActionResult AllUser()
        {
            return View(ProfileRepository.GetAllProfileData());
        }
        [AdminAccess]
        public ActionResult AllPost()
        {
            return View(PostRepository.GetAllPostAdmin());
        }
    }
}