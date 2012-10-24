using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 配置文件代理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ConfigurationBroker<T> where T : ElementInfomation,new()
    {
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void Load(string fileName)
        {
            ConfigToDataObject<T>.Load(AppDomain.CurrentDomain.BaseDirectory + @"Config\" +fileName);
        }

        /// <summary>
        /// 获取对应节
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static ElementInfomationCollection<T> GetSection(string sectionName)
        {
            return ConfigToDataObject<T>.GetElementCollection("configuration/" + sectionName);
        }

        /// <summary>
        /// 获取对应节
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static ElementInfomationCollection<T> GetSection(string fileName, string sectionName)
        {
            ConfigToDataObject<T>.Load(AppDomain.CurrentDomain.BaseDirectory + @"Config\" + fileName);
            return ConfigToDataObject<T>.GetElementCollection("configuration/" + sectionName);
        }
    }
}
