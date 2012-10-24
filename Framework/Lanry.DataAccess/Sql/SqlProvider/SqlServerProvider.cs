using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanry.Utility;
using System.Linq;

namespace Lanry.DataAccess
{
    /// <summary>
    /// SqlServer
    /// </summary>
    internal class SqlServerProvider : ISqlQuery
    {
        private string ConnectionString { set; get; }

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connName"></param>
        public SqlServerProvider(string connName)
        {
            ConnectionString = connName;
        }
        #endregion

        #region Select
        /// <summary>
        /// 执行一条Sql查询语句，无返回值
        /// </summary>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <param name="action"></param>
        public void Select(SqlTable table, SqlSelect select, Action<IDataReader> action)
        {
            SqlCommand cmd = CreateSelectCommand(table, select);
            Execute(cmd, action);
        }

        /// <summary>
        /// 执行一条Sql查询语句，返回一个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public T Select<T>(SqlTable table, SqlSelect select)
        {
            SqlCommand cmd = CreateSelectCommand(table,select);
            return Execute<T>(cmd);
        }

        /// <summary>
        /// 执行一条Sql查询语句，返回一个实体集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public DataList<T> SelectList<T>(SqlTable table, SqlSelect select, Action<IDataReader> action)
        {
            SqlCommand cmd = CreateSelectCommand(table, select);
            return ExecuteList<T>(cmd);
        }

        /// <summary>
        /// 查询数据集，返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public DataList<T> SelectList<T>(SqlTable table, SqlSelect select)
        {
            SqlCommand cmd = CreateSelectCommand(table,select);
            return ExecuteList<T>(cmd);
        }
        #endregion 

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="beginTranscation"></param>
        /// <returns></returns>
        public int Delete(SqlTable table, SqlWhere where,bool beginTranscation = false)
        {
            SqlCommand cmd = CreateDeleteCommand(table, where, beginTranscation);
            return ExecuteNonQuery(cmd,beginTranscation);
        }
        #endregion

        #region Insert
        /// <summary>
        /// Insert 手动输入需要插入的字段值
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public int Insert(SqlTable table, IDictionary<string, object> fields)
        {
            SqlCommand cmd = CreateInsertCommand(table,fields);
            return ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Insert 根据实体及其属性自动反射出插入Sql，并执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert<T>(T entity)
        {
            SqlCommand cmd = CreateInsertCommand(new SqlTable(ORM.ReflectTableName<T>(entity)),
                ORM.ReflectFields<T>(entity));
            return ExecuteNonQuery(cmd);
        }

        #endregion

        #region Update
        /// <summary>
        /// Update：手动输入需要更新的字段，执行更新语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Update(SqlTable table, IDictionary<string, object> fields,SqlWhere where)
        {
            SqlCommand cmd = CreateUpdateCommand(table, fields, where);
            return ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Update：根据实体反射出更新Sql，并执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Update<T>(T entity,SqlWhere where)
        {
            if (where == null)
            {
                object value = null;
                string name = ORM.ReflectSqlWhere<T>(entity, out value);
                where = new SqlWhere(name,value);
            }

            SqlCommand cmd = CreateUpdateCommand(new SqlTable(ORM.ReflectTableName<T>(entity)), 
                ORM.ReflectFields<T>(entity), where);
            return ExecuteNonQuery(cmd);
        }
        #endregion

        #region Execute Procedure
        /// <summary>
        /// 执行存储过程，返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataList<T> ExecuteList<T>(string procedureName, SqlParam param)
        {
            SqlCommand cmd = CreateProcedureCommand(procedureName, param);
            return ExecuteList<T>(cmd);
        }

        /// <summary>
        /// 执行存储过程，返回一条结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T Execute<T>(string procedureName, SqlParam param)
        {
            SqlCommand cmd = CreateProcedureCommand(procedureName, param);
            return Execute<T>(cmd);
        }

        #endregion

        #region Execute Sql

        /// <summary>
        /// 执行指定的Sql语句 Insert/Update
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="beginTranscation"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, SqlParam param, bool beginTranscation = false)
        {
            SqlCommand cmd = CreateCommand(CommandType.Text, beginTranscation);
            cmd.CommandText = sql;

            foreach (SqlParameterItem item in param.ParameterList)
            {
                if (item.Direction == ParameterDirection.Input)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
                else
                {
                    cmd.Parameters.Add(item.Name, item.DbType);
                }
            }
            return ExecuteNonQuery(cmd,beginTranscation);
        }

        /// <summary>
        /// 执行指定的Sql并执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="action"></param>
        public void ExecuteSql<T>(string sql, SqlParam param, Action<IDataReader> action)
        {
            SqlCommand cmd = CreateCommand(CommandType.Text);
            cmd.CommandText = sql;
            foreach (SqlParameterItem item in param.ParameterList)
            {
                if (item.Direction == ParameterDirection.Input)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
                else
                {
                    cmd.Parameters.Add(item.Name, item.DbType);
                }
            }
            Execute(cmd, action);
        }
        #endregion

        #region Private Execute
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="action"></param>
        private void Execute(SqlCommand cmd, Action<IDataReader> action)
        {
            using (cmd)
            {
                try
                {
                    cmd.Connection.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        action(reader);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Connection.Dispose();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private T Execute<T>(SqlCommand cmd)
        {
            using (cmd)
            {
                try
                {
                    cmd.Connection.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            T entity = ORM.TableToObject<T>(reader);
                            return entity;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Connection.Dispose();
                }
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="beginTranscation"></param>
        /// <returns></returns>
        private int ExecuteNonQuery(SqlCommand cmd, bool beginTranscation)
        {
            using (cmd)
            {
                try
                {
                    cmd.Connection.Open();

                    int result = cmd.ExecuteNonQuery();
                    if (beginTranscation)
                        cmd.Transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    cmd.Transaction.Rollback();
                    throw new Exception(ex.Message);
                    
                }
                finally
                {
                    cmd.Connection.Dispose();
                    if (beginTranscation)
                    {
                        cmd.Transaction.Dispose();
                        cmd.Transaction = null;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private int ExecuteNonQuery(SqlCommand cmd)
        {
            using (cmd)
            {
                try
                {
                    cmd.Connection.Open();

                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally
                {
                    cmd.Connection.Dispose();
                }
            }
        }

        private DataList<T> ExecuteList<T>(SqlCommand cmd)
        {
            DataList<T> list = new DataList<T>();
            T entity ;

            using (cmd)
            {
                try
                {
                    cmd.Connection.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entity = ORM.TableToObject<T>(reader);
                            list.Add(entity);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Connection.Dispose();
                }
            }
            return list;
        }

        #endregion

        #region CreateCommand
        /// <summary>
        /// 创建插入命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldsValues"></param>
        /// <param name="returnID"></param>
        /// <returns></returns>
        private SqlCommand CreateInsertCommand(SqlTable table, IDictionary<string, object> fieldsValues,bool returnID = false)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            SqlCommand cmd = CreateCommand(CommandType.Text);

            int i = 0;
            string[] fields = new string[fieldsValues.Count];
            string[] values = new string[fieldsValues.Count];

            foreach (var kvp in fieldsValues)
            {
                fields[i] = "[" + kvp.Key + "]";
                values[i++] = "@" + kvp.Key;
                cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
            }

            sqlBuilder.AppendFormat("INSERT INTO {0}({1}) VALUES({2})", table.TableName.Replace("L with(nolock)", ""), string.Join(",", fields), string.Join(",", values));
            if (returnID) sqlBuilder.Append(" SELECT  SCOPE_IDENTITY();");
            cmd.CommandText = sqlBuilder.ToString();
            
            return cmd;
        }

        /// <summary>
        /// 创建删除命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="beginTranscation"></param>
        /// <returns></returns>
        private SqlCommand CreateDeleteCommand(SqlTable table, SqlWhere where,bool beginTranscation = false)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            SqlCommand cmd = CreateCommand(CommandType.Text, beginTranscation);

            sqlBuilder.Append("DELETE FROM ").Append(table.TableName.Replace("L with(nolock)", ""));

            if (where != null && where.List.Count != 0)
            {
                sqlBuilder.Append(" WHERE ");
                foreach (WhereItem item in where.List)
                {
                    sqlBuilder.Append(BuildWhereCondition(item)).Append(" AND ");
                    cmd.Parameters.AddWithValue("@" + item.Name, item.Value);
                }

                sqlBuilder.Remove(sqlBuilder.Length - 4, 4);
            }
            cmd.CommandText = sqlBuilder.ToString();
            return cmd;
        }

        /// <summary>
        /// 创建更新命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldValues"></param>
        /// <param name="where"></param>
        /// <param name="beginTranscation"></param>
        /// <returns></returns>
        private SqlCommand CreateUpdateCommand(SqlTable table, IDictionary<string, object> fieldValues, SqlWhere where, bool beginTranscation = false)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            SqlCommand cmd = CreateCommand(CommandType.Text, beginTranscation);
            StringBuilder fieldsBuilder = new StringBuilder();

            foreach (var item in fieldValues)
            {
                fieldsBuilder.Append(item.Key).Append("=@").Append(item.Key).Append(", ");
                cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
            }
            fieldsBuilder.Remove(fieldsBuilder.Length - 2, 2);

            if (table.HasJoin)
            {
                sqlBuilder.Append("UPDATE L SET ").Append(fieldsBuilder)
                    .Append(table.TableName);
            }
            else
            {
                sqlBuilder.Append("UPDATE ").Append(table.TableName.Replace("L with(nolock)", "")).Append(" SET ").Append(fieldsBuilder);
            }
            if (where != null && where.List.Count != 0)
            {
                sqlBuilder.Append(" WHERE ");
                foreach (WhereItem item in where.List)
                {
                    sqlBuilder.Append(BuildWhereCondition(item)).Append(" AND ");
                    cmd.Parameters.AddWithValue("@" + item.Name, item.Value);
                }
                sqlBuilder.Remove(sqlBuilder.Length - 4, 4);
            }
            cmd.CommandText = sqlBuilder.ToString();
            return cmd;
        }

        /// <summary>
        /// 创建查找命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        private SqlCommand CreateSelectCommand(SqlTable table, SqlSelect select)
        {
            SqlCommand cmd = CreateCommand(CommandType.Text);
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append("SELECT ").Append(string.Join(",",select.Fields))
                .Append(" FROM ").Append(table.TableName);

            if (select.Condition != null && select.Condition.List.Count != 0)
            {
                sqlBuilder.Append(" WHERE ");
                foreach (WhereItem item in select.Condition.List)
                {
                    sqlBuilder.Append(BuildWhereCondition(item)).Append(" AND ");
                    cmd.Parameters.AddWithValue("@" + item.Name, item.Value);
                }
                sqlBuilder.Remove(sqlBuilder.Length - 4, 4);
            }
           
            cmd.CommandText = sqlBuilder.ToString();
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="beginTranscation"></param>
        /// <returns></returns>
        private SqlCommand CreateCommand(CommandType type,bool beginTranscation =false)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = type;
            cmd.CommandTimeout = 60;

            if (beginTranscation)
            {
                cmd.Connection = new SqlConnection(ConnectionString);
                cmd.Transaction = cmd.Connection.BeginTransaction();
            }
            else
            {
                cmd.Connection = new SqlConnection(ConnectionString);
            }

            return cmd;
        }

        /// <summary>
        /// 创建存储过程执行命令
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private SqlCommand CreateProcedureCommand(string procedureName,SqlParam param)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 60;

            cmd.Connection = new SqlConnection(ConnectionString);

            foreach (SqlParameterItem item in param.ParameterList)
            {
                if (item.Direction == ParameterDirection.Output)
                {
                    cmd.Parameters.Add(item.Name, item.DbType, item.Size);
                }
                else
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value ?? DBNull.Value);
                }
            }
            return cmd;
        }
        #endregion

        #region BuidlCondition
        /// <summary>
        /// 创建条件语句
        /// </summary>
        /// <param name="whereItem"></param>
        /// <returns></returns>
        public string BuildWhereCondition(WhereItem whereItem)
        {
            StringBuilder whereBuild = new StringBuilder();

            switch (whereItem.WhereType)
            {
                case SqlWhereType.Equal:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, "=@");
                    break;
                case SqlWhereType.ExplicitLike:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, " like @");
                    break;
                case SqlWhereType.GreaterAndEqual:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, ">=@");
                    break;
                case SqlWhereType.GreaterThan:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, ">@");
                    break;
                case SqlWhereType.In:
                    whereBuild.AppendFormat("{0} in {1}", whereItem.Name, whereItem.Value);
                    break;
                case SqlWhereType.LeftLike:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, " like %@").Append("%");
                    break;
                case SqlWhereType.LessAndEqual:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, "<=@");
                    break;
                case SqlWhereType.LessThan:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, "<@");
                    break;
                case SqlWhereType.Like:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, " like %@").Append("%");
                    break;
                case SqlWhereType.NotEqual:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, " <> @");
                    break;
                case SqlWhereType.RightLike:
                    whereBuild.AppendFormat("{0}{1}{0}", whereItem.Name, " like @").Append("%");
                    break;
            }

            return whereBuild.ToString();
        }
        #endregion
    }
}