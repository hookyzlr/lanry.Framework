using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanry.Utility;
using System.Reflection;

namespace Lanry.DataAccess
{
    /// <summary>
    /// 代理类
    /// </summary>
    internal class SqlQueryFactory
    {
        /// <summary>
        /// 创建数据库操作类
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static ISqlQuery CreateSqlQuery(string dbName)
        {
            ConfigurationBroker<ConnectionConfig>.Load("DataBase.config");
            ElementInfomationCollection<ConnectionConfig> ELC = ConfigurationBroker<ConnectionConfig>.GetSection("dbInfos");
            ConnectionConfig EL = (ConnectionConfig)ELC.GetByNameAndValue("Name", dbName);

            ElementInfomationCollection<DBMappingConfig> DBCL = ConnectionSettings.GetConnectionSettings<DBMappingConfig>("dbMapping");

            DBMappingConfig dbMapping = (DBMappingConfig)DBCL.Get();

            return (ISqlQuery)Activator.CreateInstance(Type.GetType(dbMapping.AssemblyName + "." + dbMapping.TypeName),  new object[] { EL.ConnString });
        }
    }
}
