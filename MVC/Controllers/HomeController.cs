using BuddyHub.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(PostRepository.GetPostData());
        }
        [Authorize]
        public ActionResult Search(string SearchText)
        {
            return View(PostRepository.GetPostBySearch(SearchText));
        }

    }
}