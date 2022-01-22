using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuddyHub.Repo
{
    public static class WorkProfileRepository
    {
        static buddyhubEntities db;
        static WorkProfileRepository()
        {
            db = new buddyhubEntities();
        }

        public static List<WorkProfileData> ShowWorkProfile(int UserId)
        {
            List<WorkProfileData> wpd = new List<WorkProfileData>();
            var wp = (from w in db.WorksProfiles
                      where w.FK_Users_Id == UserId
                      select w).ToList();
            foreach(var w in wp)
            {
                WorkProfileData work = new WorkProfileData()
                {
                    Id = w.Id,
                    Institution = w.Institution,
                    StartYear = w.StartYear,
                    EndYear = w.EndYear,
                    Position = w.Position,
                    FK_Users_Id = w.FK_Users_Id,
                    Username = (from u in db.Users where u.Id == UserId select u.Username).ToString()
                };
                wpd.Add(work);
            }
            return wpd;
        }

        public static void DeleteWorkProfile(int opId)
        {
            var wp = db.WorksProfiles.Find(opId);
            db.WorksProfiles.Remove(wp);
            db.SaveChanges();
        }

        public static void AddWorkProfile(int UserId, WorkProfileData wpd)
        {
            WorksProfile wp = new WorksProfile()
            {
                Institution = wpd.Institution,
                StartYear = wpd.StartYear,
                EndYear = wpd.EndYear,
                Position = wpd.Position,
                FK_Users_Id = UserId
            };

            db.WorksProfiles.Add(wp);
            db.SaveChanges();
        }

        public static WorkProfileData FindWorkProfileById(int WpId)
        {
            var wpd = (from w in db.WorksProfiles
                       where w.Id == WpId
                       select w).FirstOrDefault();

            WorkProfileData wp = new WorkProfileData()
            {
                Institution = wpd.Institution,
                StartYear = wpd.StartYear,
                EndYear = wpd.EndYear,
                Position = wpd.Position,
                FK_Users_Id = wpd.FK_Users_Id
            };

            return wp;
        }

        public static void EditWorkProfile(WorkProfileData wpd)
        {
            WorksProfile wp = db.WorksProfiles.Single(w => w.Id == wpd.Id);
            wp.Institution = wpd.Institution;
            wp.StartYear = wpd.StartYear;
            wp.EndYear = wpd.EndYear;
            wp.Position = wpd.Position;
            db.SaveChanges();
        }
    }
}