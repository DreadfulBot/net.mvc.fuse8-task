using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace net.mvc.fuse8_task
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

//            routes.MapRoute(
//                name: "Default",
//                url: "{controller}/{action}/{id}",
//                defaults: new { controller = "Home", action = "ShowDefault", id = UrlParameter.Optional }
//            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "Interval",
                url: "{controller}/{action}/{startDate}&{endDate}",
                defaults: new { controller = "Home", action = "Index", startDate = UrlParameter.Optional, endDate = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ExportExcel",
                url: "{controller}/{action}",
                defaults: new { controller = "Report", action = "ExportExcel" }
            );

            routes.MapRoute(
                name: "SendEmail",
                url: "{controller}/{action}",
                defaults: new { controller = "Report", action = "SendEmail" }
            );
        }
    }
}
