using BuddyHub.Models.VirtualModel;
using BuddyHub.Models.VM;
using BuddyHub.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Controllers
{
    public class FollowerController : Controller
    {
        // GET: Follower
        [Authorize]
        public ActionResult DoFollow(int id)
        {
            int UserId = (int)Session["UserId"];
            FollowerData f = FollowerRepository.CheckFollowing(UserId, id);
            if(f == null)
            {
                FollowerRepository.DoFollow(UserId, id);
                return RedirectToAction("Index", "Home");
            }
            FollowerRepository.UnDoFollow(f);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult ShowFollowers()
        {
            int UserId = (int)Session["UserId"];
            List<ProfileData> pd = FollowerRepository.ShowFollowers(UserId);
            return View(pd);
        }
        [Authorize]
        public ActionResult ShowFollowing()
        {
            int UserId = (int)Session["UserId"];
            List<ProfileData> pd = FollowerRepository.ShowFollowing(UserId);
            return View(pd);
        }
    }
}