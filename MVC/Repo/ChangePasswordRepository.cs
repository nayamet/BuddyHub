using BuddyHub.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Repo
{
    public static class ChangePasswordRepository
    {
        static buddyhubEntities db;
        static ChangePasswordRepository()
        {
            db = new buddyhubEntities();
        }

        public static void ChangePassword(string username, string password)
        {
            User u = db.Users.Single(usr => usr.Username == username);
            u.Password = password;
            db.SaveChanges();
        }

        public static bool CheckingPassword(string username, string password)
        {
            var oldpass = (from op in db.Users
                           where op.Username == username
                           select op.Password).FirstOrDefault();
            if(oldpass.Trim() == password)
            {
                return true;
            }
            return false;
        }

    }
}