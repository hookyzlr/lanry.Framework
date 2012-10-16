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
        public static DataList<HM_Category> GetCatergory()
        {
            SqlTable table = new SqlTable("HM_Category");
            table.InnerJoin("HM_Article", "CategoryID", "CategoryID");
            
            SqlSelect select = new SqlSelect();
            select.OrderBy("LastModifyDate", SqlSortType.ASC);
            return SqlQueryCache.Localhost.SelectList<HM_Category>(table, select);
        }
    }
}
