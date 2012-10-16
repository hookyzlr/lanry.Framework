using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 连接配置类
    /// </summary>
    public class ConnectionConfig :ElementInfomation
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ElementProperty("name")]
        public string Name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [ElementProperty("provider")]
        public string Provider { set; get; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [ElementProperty("connString")]
        public string ConnString { set; get; }
    }
}
