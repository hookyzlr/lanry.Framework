using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

using Lanry.Utility;

namespace Lanry.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class AnonymousType
    {
        /// <summary>
        ///名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 参数集合
        /// </summary>
        public Dictionary<string, string> Params { get; set; }

        /// <summary>
        /// 重写ToString 方法 产生需要动态代码段
        /// </summary> 
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public class {0}", Name);
            sb.Append("{");
            foreach (var item in Params)
            {
                sb.AppendFormat("public string {0}", item.Key);
                sb.Append("{get{return @\"");
                sb.Append(item.Value);
                sb.Append("\";}} ");
            }

            sb.Append("}");
            return sb.ToString();
        } 
    }
    /// <summary>
    /// 路由规则 用于反射生成匿名对象
    /// </summary>
    public class RouteInfo : AnonymousType
    {
        /// <summary>
        /// 路由路径
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 路由表生成类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RouteBaseEngine<T>
    {
        #region 属性字段
        
        /// <summary>
        /// 路由参数
        /// </summary>
        public Dictionary<string, string> dict { set; get; }
        
        /// <summary>
        /// 路由参数验证表达式
        /// </summary>
        public Dictionary<string, string> paramRegExp { set; get; }

        /// <summary>
        /// 函数参数列表
        /// </summary>
        private List<ParameterInfo> paramList { set; get; }

        #endregion

        /// <summary>
        /// 根据Controller类生成路由表
        /// </summary>
        public void RegisterRouteMap()
        {
            string controller;
            string action;
            Type type;
            dict = new Dictionary<string, string>();
            StringBuilder paramBuilder;
            paramRegExp = new Dictionary<string, string>();

            #region 构建路由信息
            type = typeof(T);

            controller = type.Name;
            controller = controller.Replace("Controller", "");

            MethodInfo[] methodInfoArr = type.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            
            foreach (MethodInfo methodInfo in methodInfoArr)
            {
                NonActionAttribute[] actionAttr = (NonActionAttribute[])methodInfo.GetCustomAttributes(typeof(NonActionAttribute), true);

                if (actionAttr != null && actionAttr.Length > 0)
                    continue;

                paramBuilder = new StringBuilder();
                dict.Add("Controller", controller);
                
                //设置映射的方法名称
                action = methodInfo.Name;

                dict.Add("Action", action);

                //设置参数
                paramList = methodInfo.GetParameters().ToList<ParameterInfo>();
                Lanry.Utility.ParameterAttribute pAttr;

                foreach (ParameterInfo param in paramList)
                {
                    paramBuilder.Append("{").Append(param.Name).Append("}").Append("/");
                    //设置参数默认值
                    dict.Add(param.Name, (param.DefaultValue == null ? "" : param.DefaultValue.ToString()));

                    //设置参数验证表达式
                    object[] attrs = param.GetCustomAttributes(typeof(Lanry.Utility.ParameterAttribute),false);
                    
                    if (attrs.Length != 0)
                    {
                        pAttr =  (Lanry.Utility.ParameterAttribute)attrs[0];
                        if (pAttr.RegExp != null && !string.IsNullOrEmpty(pAttr.RegExp))
                        {
                            paramRegExp.Add(param.Name, pAttr.RegExp);
                        }
                    }
                }

                dict.Add("Param", paramBuilder.ToString().TrimEnd('/'));

                CreateRoute();

                dict.Clear();
                paramRegExp.Clear();
            }
            #endregion
        }

        /// <summary>
        /// 创建路由信息
        /// </summary>
        public virtual void CreateRoute()
        { }

        /// <summary>
        /// 从string动态创建类对象
        /// </summary>
        /// <param name="codeString"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public object CreateObjectFormString(string codeString, string className)
        {
            CSharpCodeProvider ccp = new CSharpCodeProvider();
            CompilerParameters param = new CompilerParameters(new string[] { "System.dll" });
            CompilerResults cr = ccp.CompileAssemblyFromSource(param, codeString);
            Type type = cr.CompiledAssembly.GetType(className);
            return type.GetConstructor(System.Type.EmptyTypes).Invoke(null);
        }

        #region 添加路由信息到路由表
        /// <summary>
        /// 添加路由信息
        /// </summary>
        /// <param name="routeName"></param>
        /// <param name="url"></param>
        public virtual void CreateRoutes(string routeName, string url)
        {
            RouteTable.Routes.MapRoute(routeName,url);
        }

        /// <summary>
        /// 添加路由信息
        /// </summary>
        /// <param name="routeName"></param>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        public virtual void CreateRoutes(string routeName, string url, object defaults)
        {
            RouteValueDictionary d = new RouteValueDictionary(defaults);
            Route item = new Route(url, d, null, null, null);
            if (RouteTable.Routes.Contains(item))
                return;
            else
            {
                RouteTable.Routes.MapRoute(routeName, url, defaults);
            }
        }

        /// <summary>
        /// 添加路由信息
        /// </summary>
        /// <param name="routeName"></param>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="constraints"></param>
        public virtual void CreateRoutes(string routeName, string url, object defaults, object constraints)
        {
            RouteValueDictionary d = (RouteValueDictionary)defaults;
            RouteValueDictionary c = (RouteValueDictionary)constraints;
            Route item = new Route(url, d, c, null, null);
            if (RouteTable.Routes.Contains(item))
                return;
            else
            {
                RouteTable.Routes.MapRoute(routeName, url, defaults, constraints);
            }
        }

        /// <summary>
        /// 添加路由信息
        /// </summary>
        /// <param name="routeName"></param>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="constraints"></param>
        /// <param name="namespaces"></param>
        public virtual void CreateRoutes(string routeName, string url, object defaults, object constraints, string[] namespaces)
        {
            RouteTable.Routes.MapRoute(routeName, url, defaults, constraints, namespaces);
        }
        #endregion
    }
}
