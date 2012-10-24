using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanry.DataAccess;
using Lanry.Model;
using Lanry.Utility;

namespace Lanry.Repository
{
    public static class CatergoryRepository
    {
        public static DataList<TB1> GetTestData()
        {
            SqlTable table = new SqlTable("TB1");
            
            SqlSelect select = new SqlSelect();
            return SqlQueryCache.Localhost.SelectList<TB1>(table, select);
        }

        public static int Save(TB1 en)
        {
            return SqlQueryCache.Localhost.Insert(en);
        }

        public static int Update(TB1 en)
        {
            SqlWhere where = new SqlWhere("ID", en.ID);
            return SqlQueryCache.Localhost.Update(en, where);
        }
    }
}
