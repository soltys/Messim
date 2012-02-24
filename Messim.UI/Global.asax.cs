using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Messim.UI.Models;

namespace Messim.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("channel.html");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.MapRoute(
                "Best",
                "Best",
                new { controller = "Home", action = "Best" });
            routes.MapRoute(
                "New",
                "New",
                new { controller = "Home", action = "New" });
            routes.MapRoute(
                "SingleMessage",
                "Message/{MessageID}",
                new { controller = "Message", action = "Details" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{UserName}", // URL with parameters
                new { controller = "Home", action = "Index", UserName = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MessimContext>());

        }
    }
}