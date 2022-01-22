using BuddyHub.Models.EntityFramework;
using BuddyHub.Models.VirtualModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Repo
{
    public class RecoveryPasswordRepository : Controller
    {
        static buddyhubEntities db;
        static RecoveryPasswordRepository()
        {
            db = new buddyhubEntities();
        }

        public static bool SetQuestionAnswer(RecoveryPasswordData rpd, int Id)
        {

            var user = FindUser(Id);
            if(user == null)
            {
                RecoveryPassword rp = new RecoveryPassword()
                {
                    QuestionOne = rpd.QuestionOne,
                    QuestionOneAnswer = rpd.QuestionOneAnswer,
                    QuestionTwo = rpd.QuestionTwo,
                    QuestionTwoAnswer = rpd.QuestionTwoAnswer,
                    FK_Users_Id = Id
                };
                db.RecoveryPasswords.Add(rp);
                /*db.RecoveryPasswords.Upda(rp);*/
                db.SaveChanges();
                return true;
            }

            else
            {
                RecoveryPassword rp = db.RecoveryPasswords.Single(usr => usr.FK_Users_Id == Id);
                rp.QuestionOne = rpd.QuestionOne;
                rp.QuestionOneAnswer = rpd.QuestionOneAnswer;
                rp.QuestionTwo = rpd.QuestionTwo;
                rp.QuestionTwoAnswer = rpd.QuestionTwoAnswer;
                db.SaveChanges();
                return true;
            }
        }

        public static RecoveryPasswordData FindUser(int Id)
        {
            var u = (from user in db.RecoveryPasswords
                     where user.FK_Users_Id == Id
                     select user).FirstOrDefault();
            if(u == null)
            {
                return null;
            }
            RecoveryPasswordData rpd = new RecoveryPasswordData()
            {
                Id = u.Id,
                QuestionOne = u.QuestionOne,
                QuestionOneAnswer = u.QuestionOneAnswer,
                QuestionTwo = u.QuestionTwo,
                QuestionTwoAnswer = u.QuestionTwoAnswer,
                FK_Users_Id = u.FK_Users_Id
            };
            return rpd;
        }
        
  /*      public static bool CheckQuestionAnswer(RecoveryPasswordData rpd)
        {
            RecoveryPasswordData 
        }*/
    }
}