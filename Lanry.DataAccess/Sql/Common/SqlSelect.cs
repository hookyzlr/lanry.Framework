using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.DataAccess
{
    /// <summary>
    /// 查询语句
    /// </summary>
    public class SqlSelect
    {
        private SqlTable _table;
        private string[] _fields;
        private SqlWhere _condition = new SqlWhere();

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public SqlTable Table
        {
            set { _table = value; }
            get { return _table; }
        }
        /// <summary>
        /// 
        /// </summary>
        public SqlWhere Condition
        {
            set { _condition = value; }
            get { return _condition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string[] Fields
        {
            set { _fields = value; }
            get { return _fields; }
        }


        internal int PageIndex { set; get; }
        internal int PageSize { set; get; }
        internal string GroupByString { get; private set; }
		internal string OrderByString { get; private set; }

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public SqlSelect()
        {
            this.Fields = new string[] { "L.*" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        public SqlSelect(string[] fields)
        {
            this.Fields = fields;
        }
        #endregion

        #region Where Condition

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Where(string name, object value)
        {
            Where(name, SqlWhereType.Equal, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="whereType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlSelect Where(string name, SqlWhereType whereType, object value)
        {
            if (Condition == null) Condition = new SqlWhere();
            this.Condition.List.Add(new WhereItem(name, whereType, value));
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public SqlSelect GroupBy(string[] fields)
        {
            GroupByString = string.Join(",", fields);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public SqlSelect OrderBy(string field, SqlSortType sort)
        {
            if (OrderByString == null) OrderByString = string.Format("{0} {1}", field, sort);
            else OrderByString += string.Format(",{0} {1}", field, sort);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public SqlSelect Page(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            return this;
        }
        #endregion

    }
}
