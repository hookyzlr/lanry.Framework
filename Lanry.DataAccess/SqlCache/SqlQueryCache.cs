using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public static class SqlQueryCache
    {
        public static ISqlQuery Localhost = SqlQueryFactory.CreateSqlQuery("Localhost");
    }
}
