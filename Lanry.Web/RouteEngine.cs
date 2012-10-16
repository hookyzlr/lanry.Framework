using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.Text.RegularExpressions;
using Lanry.Web;

namespace Lanry.Framework
{
    /// <summary>
    /// 路由生成引擎
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteEngine()
        {
            
        }

        /// <summary>
        /// 创建路由信息
        /// </summary>
        public override void CreateRoute()
        {
            string param = string.Empty;
            Regex reg = new Regex(@"^\w{1,}/\w{1,}/\w{0,}$");
            Dictionary<string, string> dictParams = new Dictionary<string, string>();
            string url = dict["Controller"]+"/"+dict["Action"]+"/" + dict["Param"];

            //判断是否和默认路由重复
            if (reg.IsMatch(url))
            {
                return;
            }
            dict.Remove("Param");
            RouteInfo routeInfo = new RouteInfo() { Name = dict["Controller"], Url = url, Params = dict };
            
            object obj = CreateObjectFormString(routeInfo.ToString(),routeInfo.Name);
            AnonymousType anonymoursType = new AnonymousType() { Name = "Reg", Params = paramRegExp };
            if (paramRegExp.Count != 0)
            {
                object regObj = CreateObjectFormString(anonymoursType.ToString(), anonymoursType.Name);

                CreateRoutes(string.Empty, routeInfo.Url, obj, regObj);
            }
            else
            {
                CreateRoutes(string.Empty, routeInfo.Url, obj);
            }
        }
    }
}