using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 路由信息
    /// </summary>
    public class RouteConfig : ElementInfomation
    {
        /// <summary>
        /// 
        /// </summary>
        [ElementProperty("Name")]
        public string Name { set; get; }
    }
}
