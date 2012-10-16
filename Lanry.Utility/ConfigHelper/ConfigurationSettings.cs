using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Lanry.Utility;

namespace Lanry.Utility
{
    /// <summary>
    /// 配置类
    /// </summary>
    public static class ConnectionSettings
    {
        /// <summary>
        /// 获取配置信息映射的实体类集合
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static ElementInfomationCollection<T> GetConnectionSettings<T>(string sectionName)
            where T : ElementInfomation,new()
        {
            ConfigurationBroker<T>.Load("DataBase.config");
            return ConfigurationBroker<T>.GetSection("configuration/" + sectionName);
        }
    }

}
