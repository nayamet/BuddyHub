using BuddyHub.Auth;
using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using BuddyHub.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BuddyHub.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginData ld)
        {
            if (ModelState.IsValid)
            {
                var user = UserRepo.GetAuthenticateUser(ld);
                if (user != null)
                {
                    if (user.Status == 1)
                    {
                        FormsAuthentication.SetAuthCookie(user.Type.Replace(" ", "").ToString(), false);
                        System.Diagnostics.Debug.WriteLine("Auth cokies set");

                        Session["UserId"] = user.Id;
                        Session["Username"] = user.Username.Replace(" ", "");
                        Session["Name"] = user.Name;
                        Session["Type"] = user.Type.Replace(" ","");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Sorry You are Disable By Admin!!!");
                        return View(ld);
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Wrong Credentials!!! Try again");
                    return View(ld);
                }
            }
             return View(ld);

        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationData rd)
        {

            if (ModelState.IsValid)
            {
                if (UserRepo.IsUsernameUnique(rd.Username))
                {
                    UserRepo.RegisterUser(rd);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Username", "Username already taken, choose another username");
                } 
            }
            return View(rd);
        }
        [AdminAccess]
        public ActionResult ChangeStatus(string username)
        {
            UserRepo.ChangeStatus(username);
            return Redirect("/Admin/AllUser");
        }
        [AdminAccess]
        public ActionResult AdminUserRemove(int id)
        {
            UserRepo.RemoveUserAdmin(id);
            return Redirect("/Admin/AllUser");
        }
        [Authorize]
        public ActionResult AddSocialProfile()
        {
            return View();
        }
        [Authorize]
        public ActionResult RemoveSocialLink(int id)
        {
            ProfileRepository.RemoveSocialProfile(id);
            return Redirect("/Profile/SocialProfile/"+Session["Username"]);
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddSocialProfile(SocialLink sl)
        {
            ProfileRepository.AddSocialProfile(sl);
            return Redirect("/Profile/SocialProfile/" + Session["Username"]);
        }

    }
}