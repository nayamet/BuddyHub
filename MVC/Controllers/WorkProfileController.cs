using BuddyHub.Models.VirtualModel;
using BuddyHub.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Controllers
{
    public class WorkProfileController : Controller
    {
        // GET: WorkProfile

        [HttpGet]
        [Authorize]

        public ActionResult AddWorkProfile()
        {
            return View();
        }

        [HttpPost]
        [Authorize]

        public ActionResult AddWorkProfile(WorkProfileData wpd)
        {
            int UserId = (int)Session["UserId"];
            if (ModelState.IsValid)
            {
                WorkProfileRepository.AddWorkProfile(UserId, wpd);
                return RedirectToAction("ShowWorkProfile");
            }
            return View(wpd);
        }

        [HttpGet]
        [Authorize]

        public ActionResult EditWorkProfile(int id)
        {
            WorkProfileData wpd = WorkProfileRepository.FindWorkProfileById(id);
            return View(wpd);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditWorkProfile(WorkProfileData wpd)
        {
            int UserId = (int)Session["UserId"];
            if (ModelState.IsValid)
            {
                WorkProfileRepository.EditWorkProfile(wpd);
                return RedirectToAction("ShowWorkProfile");
            }
            return View(wpd);
        }
    }
}