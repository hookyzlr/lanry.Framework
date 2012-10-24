using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Lanry.Utility;

namespace Lanry.DataAccess
{
    /// <summary>
    /// Sql语句接口
    /// </summary>
    public interface ISqlQuery
    {
        #region Select
        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <param name="action"></param>
        void Select(SqlTable table, SqlSelect select, Action<IDataReader> action);

        /// <summary>
        /// T 查询数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        T Select<T>(SqlTable table, SqlSelect select);

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        DataList<T> SelectList<T>(SqlTable table, SqlSelect select, Action<IDataReader> action);

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        DataList<T> SelectList<T>(SqlTable table, SqlSelect select);
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="beginTranscation"></param>
        /// <returns></returns>
        int Delete(SqlTable table, SqlWhere where,bool beginTranscation = false);
        #endregion

        #region Insert
        /// <summary>
        /// Insert 手动输入需要插入的字段值
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        int Insert(SqlTable table, IDictionary<string, object> fields);

        /// <summary>
        /// Insert 根据实体及其属性自动反射出插入Sql，并执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert<T>(T entity);
        #endregion

        #region Update
        /// <summary>
        /// Update：手动输入需要更新的字段，执行更新语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int Update(SqlTable table, IDictionary<string, object> fields, SqlWhere where);

        /// <summary>
        /// Update：根据实体反射出更新Sql，并执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int Update<T>(T entity, SqlWhere where);
        #endregion

        #region Execute Procedure
        /// <summary>
        /// 执行指定的存储过程，返回一个对应实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        T Execute<T>(string procedureName, SqlParam param);

        /// <summary>
        /// 执行指定的存储过程，返回一个实体集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        DataList<T> ExecuteList<T>(string procedureName, SqlParam param);
        #endregion

        #region Execute Sql

        /// <summary>
        /// 执行指定的Sql语句 Insert/Update
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="beginTranscation"></param>
        /// <returns></returns>
        int ExecuteSql(string sql, SqlParam param,bool beginTranscation = false);

        /// <summary>
        /// 执行指定的Sql并执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="action"></param>
        void ExecuteSql<T>(string sql, SqlParam param, Action<IDataReader> action);
        #endregion
    }
}
