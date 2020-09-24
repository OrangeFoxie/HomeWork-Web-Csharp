using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LAB_01
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Hello",
                url: "{controler}/{action}/{name}/{id}",            //https://localhost:44312/HelloWorld/Wellcome/Scott/3
                defaults: new { controller = "HelloWorld", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
