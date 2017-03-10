using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Apps.Admins
{
    public class RouteConfig
    {
        //private static string[] namespaces = new string[1] { "Apps.Admins" };
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Globalization",
                "{lang}/{controller}/{action}/{id}",// 带有参数的 URL
                new { lang = "zh", controller = "Home", action = "Index", id = UrlParameter.Optional },// 参数默认值
                 new { lang = "^[a-zA-Z]{2}(-[a-zA-Z]{2})?$" }    //参数约束
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                 //namespaces: namespaces
            );
        }
    }
}
