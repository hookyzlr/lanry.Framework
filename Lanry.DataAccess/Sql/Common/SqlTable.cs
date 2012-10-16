using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.DataAccess
{
    /// <summary>
    /// sqlTable
    /// </summary>
    public class SqlTable
    {
        /// <summary>
        /// 
        /// </summary>
        private bool _leftTableNoLock =true;
        /// <summary>
        /// 
        /// </summary>
        private bool _rightTableNolock = true;
        /// <summary>
        /// 
        /// </summary>
        private StringBuilder _tableName;
        /// <summary>
        /// 
        /// </summary>
        private bool _hasJoin;

        #region Properties

        /// <summary>
        /// 组合后的表结构
        /// </summary>
        public StringBuilder TableName
        {
            //get 
            //{
            //    if (!string.IsNullOrEmpty(RightTableName))
            //        return string.Format(" [{0}] l {1} [{2}] r on l.{3}=r.{4} ",
            //            _leftTableName, _joinType, _rightTableName, _leftKey, _rightKey);
            //    else
            //        return _leftTableName;
            //}
            get
            {
                if (_tableName == null)
                {
                    _tableName = new StringBuilder();
                }
                return _tableName;
            }
        }
        /// <summary>
        /// 是否给左表加锁
        /// </summary>
        public bool LeftTableNoLock
        {
            private set { _leftTableNoLock = value; }
            get { return _leftTableNoLock; }
        }

        /// <summary>
        /// 是否给右表加锁
        /// </summary>
        public bool RightTableNolock
        {
            private set { _rightTableNolock = value; }
            get { return _rightTableNolock; }
        }

        /// <summary>
        /// 是否存在连接
        /// </summary>
        public bool HasJoin
        {
            set { _hasJoin = value; }
            get { return _hasJoin; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftName"></param>
        public SqlTable(string leftName)
        {
            this.TableName.Append(leftName)
                .Append(" L with(nolock)");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftName"></param>
        /// <param name="leftNoLock"></param>
        /// <param name="rightNoLock"></param>
        public SqlTable(string leftName, bool leftNoLock, bool rightNoLock)
        {
            _leftTableNoLock = leftNoLock;
            _rightTableNolock = rightNoLock;

            this.TableName.Append(leftName)
                .Append(leftNoLock ? " L with(nolock)" : " ");
        }
        #endregion

        #region Join Table
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rightTableName"></param>
        /// <param name="leftKey"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        public SqlTable LeftJoin(string rightTableName,string leftKey,string rightKey)
        {
            this.TableName.Append(" left join ")
                .Append(rightTableName)
                .Append(RightTableNolock ? " R with(nolock)" : " R ")
                .Append(" on L.")
                .Append(leftKey)
                .Append("= R.")
                .Append(rightKey);
            _hasJoin = true;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rightTableName"></param>
        /// <param name="leftKey"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        public SqlTable RightJoin(string rightTableName, string leftKey, string rightKey)
        {
            this.TableName.Append(" right join ")
                .Append(rightTableName)
                .Append(RightTableNolock ? " R with(nolock)" : " R ")
                .Append(" on L.")
                .Append(leftKey)
                .Append("= R.")
                .Append(rightKey);
            _hasJoin = true;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rightTableName"></param>
        /// <param name="leftKey"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        public SqlTable CrossJoin(string rightTableName, string leftKey, string rightKey)
        {
            this.TableName.Append(" cross join ")
                .Append(rightTableName)
                .Append(RightTableNolock ? " R with(nolock)" : " R ")
                .Append(" on L.")
                .Append(leftKey)
                .Append("= R.")
                .Append(rightKey);
            _hasJoin = true;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rightTableName"></param>
        /// <param name="leftKey"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        public SqlTable InnerJoin(string rightTableName, string leftKey, string rightKey)
        {
            this.TableName.Append(" inner join ")
                .Append(rightTableName)
                .Append(RightTableNolock ? " R with(nolock)" : "R")
                .Append(" on ")
                .Append(" L.")
                .Append(leftKey)
                .Append("= R.")
                .Append(rightKey);
            _hasJoin = true;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rightTableName"></param>
        /// <param name="leftKey"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        public SqlTable OutJoin(string rightTableName, string leftKey, string rightKey)
        {
            this.TableName.Append(" out join ")
                .Append(rightTableName)
                .Append(RightTableNolock ? " R with(nolock)" : " R ")
                .Append(" on L.")
                .Append(leftKey)
                .Append("= R.")
                .Append(rightKey);
            _hasJoin = true;
            return this;
        }
        #endregion
    }
}
