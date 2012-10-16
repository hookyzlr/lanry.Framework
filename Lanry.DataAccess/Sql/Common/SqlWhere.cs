using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.DataAccess
{
    /// <summary>
    /// 条件语句
    /// </summary>
    public class SqlWhere
    {
        private List<WhereItem> _list = new List<WhereItem>();
        /// <summary>
        /// 条件列表
        /// </summary>
        public List<WhereItem> List { get { return _list; } }

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public SqlWhere()
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public SqlWhere(string name, object value)
        {
            _list.Add(new WhereItem(name, value));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="whereType"></param>
        /// <param name="value"></param>
        public SqlWhere(string name, SqlWhereType whereType, object value)
        {
            _list.Add(new WhereItem(name, whereType, value));
        }
        #endregion

        #region 组合条件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlWhere Where(string name, object value)
        {
            _list.Add(new WhereItem(name, value));
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="whereType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlWhere Where(string name, SqlWhereType whereType, object value)
        {
            _list.Add(new WhereItem(name, whereType, value));
            return this;
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class WhereItem
    {
        /// <summary>
        /// 
        /// </summary>
        private string _name;
        /// <summary>
        /// 
        /// </summary>
        private SqlWhereType _whereType;
        /// <summary>
        /// 
        /// </summary>
        private object _value;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public WhereItem(string name, object value)
        {
            _name = name;
            _whereType = SqlWhereType.Equal;
            _value = value;
            //_conditionName = string.Format("{0}{1}{2}", _name, _whereType, _value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="whereType"></param>
        /// <param name="value"></param>
        public WhereItem(string name, SqlWhereType whereType, object value)
        {
            _name = name;
            _whereType = whereType;
            _value = value;
            //_conditionName = string.Format("{0}{1}{2}", _name, _whereType, _value);
        }
        #endregion

        #region Properties
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 操作符
        /// </summary>
        public SqlWhereType WhereType
        {
            set { _whereType = value; }
            get { return _whereType; }
        }
        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            set { _value = value; }
            get { return _value; }
        }
        ///// <summary>
        ///// 组合条件类型
        ///// </summary>
        //public string ConditionName
        //{
        //    set { _conditionName = value; }
        //    get { return _conditionName; }
        //}
        #endregion
    }
}
