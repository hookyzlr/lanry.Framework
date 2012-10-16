using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.ComponentModel;
using Lanry.Framework.Controllers;
using Lanry.Web;

namespace Lanry.Framework
{
    public class RouteEngine<T> : RouteBaseEngine<T>
    {
        private static RouteEngine<T> _instance;
        private static object _lockObj = new object();

        /// <summary>
        /// 单例实例
        /// </summary>
        public static RouteEngine<T> Instance
        {
            set
            {
                _instance = value;
            }
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new RouteEngine<T>();
                        }
                    }
                }

                return _instance;
            }
        }

        public RouteEngine()
        {
            
        }

        public override void CreateRoutePara()
        {
            string param = string.Empty;
            
            RouteTable.Routes.MapRoute(
                dict["Controller"], // 路由名称
                "{controller}/{action}/" + dict["Param"],
                new { controller = dict["Controller"], action = dict["Action"]}  // 带有参数的 URL
                // 参数默认值
            );

            //RouteTable.Routes.MapRoute(
            //    "Default", // 路由名称
            //    "{controller}/{action}",
            //    new { controller = "List", action = "GetList" }  // 带有参数的 URL
            //    // 参数默认值
            //);
        }

    }
}