using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanry.DataAccess;

namespace Lanry.Repository
{
    public static class SqlQueryCache
    {
        public static ISqlQuery Localhost = SqlQueryFactory.CreateSqlQuery("Localhost");
    }
}
