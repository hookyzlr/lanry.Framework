using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Reflection;
using System.Data.SqlClient;


namespace Lanry.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class ORM
    {
        /// <summary>
        /// 数据表映射为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T TableToObject<T>(IDataReader reader)
        {
            Type type = typeof(T);
            PropertyInfo[] proInfo;
            T en;
            object[] attrs = null;

            PropertyAttibute propertyAttr;

            en = (T)Activator.CreateInstance(type);
            proInfo = en.GetType().GetProperties();

            foreach (PropertyInfo pinfo in proInfo)
            {
                attrs = pinfo.GetCustomAttributes(true);
                if (attrs != null && attrs.Length != 0)
                {
                    propertyAttr = (PropertyAttibute)attrs[0];
                    pinfo.SetValue(en, reader[propertyAttr.PropertyName], null);
                }
                else
                {
                    try
                    {
                        pinfo.SetValue(en, reader[pinfo.Name], null);
                    }
                    catch
                    { }
                }
            }
            return en;
        }

        /// <summary>
        /// xml映射为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T XmlToObject<T>(XmlNode node)
        {
            T en = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo[] propertyInfoArr = en.GetType().GetProperties();
            
            object[] attributes = null;
            ElementPropertyAttribute ela = null;
            XmlElement el = node as XmlElement;

            foreach (PropertyInfo pinfo in propertyInfoArr)
            {
                attributes = pinfo.GetCustomAttributes(false);

                if (attributes != null && attributes.Length > 0)
                {
                    ela = attributes[0] as ElementPropertyAttribute;

                    pinfo.SetValue(en, el.GetAttribute(ela.Name), null);
                }
            }

            return en;
        }

        /// <summary>
        /// 根据实体映射表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ReflectTableName<T>(T entity)
        {
            TableAttribute att = (TableAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));

            return att.TableName;
        }

        /// <summary>
        /// 根据实体映射字段/值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IDictionary<string,object> ReflectFields<T>(T entity)
        {
            PropertyInfo[] proInfos = entity.GetType().GetProperties();

            PropertyAttibute filedAttt = null;
            Dictionary<string, object> fields = new Dictionary<string, object>();

            foreach (PropertyInfo info in proInfos)
            {
                filedAttt = (PropertyAttibute)Attribute.GetCustomAttribute(info, typeof(PropertyAttibute));
                if (!filedAttt.IsIdentity)
                {
                    fields.Add(info.Name, info.GetValue(entity, null));
                }
            }

            return fields;
        }

        /// <summary>
        /// 根据实体映射SqlWhere条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReflectSqlWhere<T>(T entity,out object value)
        {
            string name = string.Empty;
            value = null;

            PropertyInfo[] proInfos = entity.GetType().GetProperties();

            PropertyAttibute filedAttt = null;

            foreach (PropertyInfo info in proInfos)
            {
                filedAttt = (PropertyAttibute)Attribute.GetCustomAttribute(info, typeof(PropertyAttibute));
                if (filedAttt.IsPrimaryKey)
                {
                   name = info.Name;
                   value = info.GetValue(entity, null);
                }
            }

            return name;
        }
    }
}
