using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Lanry.Web;
using Lanry.Utility;
using System.Reflection;

namespace Lanry.Framework
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            
            //ConfigurationBroker<RouteConfig>.Load("Route.config");
            //ElementInfomationCollection<RouteConfig> RL = ConfigurationBroker<RouteConfig>.GetSection("controllers");
            
            //foreach (RouteConfig item in RL)
            //{
            //    Type type = Type.GetType(item.Name);
            //    RouteEngine<>.Instance.RegisterRouteMap();
            //}
            RouteEngine<HomeController>.Instance.RegisterRouteMap();
            RouteEngine<TestController>.Instance.RegisterRouteMap();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes();
        }
    }
}