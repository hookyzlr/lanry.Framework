using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using Lanry.Utility;
using System.Web.Caching;

namespace Lanry.Utility
{
    /// <summary>
    /// 配置文件映射操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ConfigToDataObject<T> where T : ElementInfomation,new ()
    {
        private static XmlDocument _doc;

        private static object lockEntity = new object();

        /// <summary>
        /// 单例实体
        /// </summary>
        public static XmlDocument Doc
        {
            get
            {
                if (_doc == null)
                {
                    lock (lockEntity)
                    {
                        if (_doc == null)
                        {
                            _doc = new XmlDocument();
                        }
                    }
                }

                return _doc;
            }
            set { _doc = value; }
        }

        /// <summary>
        /// 加载xml文档
        /// </summary>
        /// <param name="filePath"></param>
        public static void Load(string filePath)
        {
            ExceptionHelper.FalseThrow(File.Exists(filePath),"无法找到对应文件！");

            _doc = (XmlDocument)CacheHelper.Get(filePath);
            if (_doc == null)
            {
                _doc = new XmlDocument();
                _doc.Load(filePath);
                CacheHelper.Insert(filePath,_doc,new CacheDependency(filePath));
            }
        }

        /// <summary>
        /// 获取配置映射实体的集合
        /// </summary>
        /// <param name="sectionName">XPath,要查找的节点的位置信息 例如://configuration/connectionStrings</param>
        /// <returns></returns>
        public static ElementInfomationCollection<T> GetElementCollection(string sectionName)
        {
            ExceptionHelper.TrueThrow(_doc == null, "请先加载对应配置文件！");
            ElementInfomationCollection<T> collection = new ElementInfomationCollection<T>();

            T entity = null;

            try
            {
                XmlNode xmlNode = _doc.SelectSingleNode(sectionName);
                
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    entity = ORM.XmlToObject<T>(node);

                    collection.Add(entity);
                }

                return collection;
            }
            catch
            {
                throw new Exception("配置文件映射实体出错.");
            }
        }

        /// <summary>
        /// 获取某一个配置节对应的实体类
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static ElementInfomation GetElementInfomation(string sectionName)
        {
            ExceptionHelper.TrueThrow(_doc == null, "请先加载对应配置文件！");

            Type type = typeof(T);
            PropertyInfo[] propertyInfoArr = type.GetProperties(BindingFlags.Public);
            
            T entity = new T();

            try
            {
                XmlElement el = _doc.SelectSingleNode(sectionName) as XmlElement;
                entity = ORM.XmlToObject<T>(el);
                return entity;
            }
            catch
            {
                throw new Exception("配置文件映射实体出错.");
            }
        }
    }
}
