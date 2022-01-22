using BuddyHub.Models.VirtualModel;
using BuddyHub.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Controllers
{
    public class SaveController : Controller
    {
        // GET: Save
        [Authorize]
        public ActionResult CheckPost(int id)
        {
            int UserId = (int)Session["UserId"];
            var post = SaveRepository.CheckForPost(id, UserId);
            if(post == null)
            {
                SaveRepository.SavePost(id, UserId);
                return RedirectToAction("Index", "Home");
            }
            SaveRepository.RemoveSavePost(post);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult ShowSavePost()
        {
            int UserId = (int)Session["UserId"];
            List<PostData> pd = new List<PostData>();
            pd = SaveRepository.GetPostByUserId(UserId);
            return View(pd);
        }
        [Authorize]
        public ActionResult RemoveSavePost(int id)
        {
            int UserId = (int)Session["UserId"];
            var post = SaveRepository.CheckForPost(id, UserId);
            SaveRepository.RemoveSavePost(post);
            return RedirectToAction("ShowSavePost");
        }
    }
}