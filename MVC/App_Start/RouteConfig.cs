using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BuddyHub
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // routes.MapRoute(
            //     name: "Login",
            //     url: "Login",
            //     defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            // );
            // routes.MapRoute(
            //    name: "Registration",
            //    url: "Registration",
            //    defaults: new { controller = "User", action = "Registration", id = UrlParameter.Optional }
            //);
            //Post / LikeOnPost / fahim / 10
            routes.MapRoute(
                name: "ChangePostStatus",
                url: "Post/ChangeStatus/{PostId}",
                defaults: new { controller = "Post", action = "ChangeStatus", PostId = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ChangeProfileStatus",
                url: "User/ChangeStatus/{Username}",
                defaults: new { controller = "User", action = "ChangeStatus", Username = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Profile",
                url: "Profile/{Username}",
                defaults: new { controller = "Profile", action = "ViewProfile", Username = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EditProfile",
                url: "Profile/{action}/{Username}/{OpId}",
                defaults: new { controller = "Profile", action = "Edit", Username = UrlParameter.Optional, OpId = UrlParameter.Optional }
            );
            /// Profile / DeleteWorkProfile / @Model.Username.Replace(" ", "") / @item.Id
            routes.MapRoute(
                name: "Like",
                url: "Post/LikeOnPost/{Username}/{PostId}",
                defaults: new { controller = "Post", action = "LikeOnPost", Username = UrlParameter.Optional, PostId = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Comment",
               url: "Post/CommentOnPost/{Username}/{PostId}",
               defaults: new { controller = "Post", action = "CommentOnPost", Username = UrlParameter.Optional, PostId = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
         );
        }
    }
}
