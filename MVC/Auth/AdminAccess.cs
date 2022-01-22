using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuddyHub.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.All)]
    public class AdminAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (isAuthorized)
            {
                var type = httpContext.User.Identity.Name.ToString();
                System.Diagnostics.Debug.WriteLine(type);

                if (type == "admin")
                {
                    return true;
                }
                else { return false; }
            }
            else
                return false;
        }
    }
}