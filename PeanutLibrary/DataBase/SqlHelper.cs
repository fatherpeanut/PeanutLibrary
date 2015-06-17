using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace PeanutLibrary.DataBase
{
    public class SqlHelper : DbAbs, IDbHelper
    {
        #region 构造函数
        // <summary>
        /// 构造函数(读取默认配置文件的connectionStrings.connectionString)
        /// </summary>
        public SqlHelper()
            : base()
        { }

        /// <summary>
        /// 构造函数(读取默认配置文件的connectionString子节点)
        /// </summary>
        /// <param name="type">配置文件节点(AppSetting,connectionStrings)</param>
        public SqlHelper(EnumType.ConfigType type)
            : base(type)
        { }

        /// <summary>
        /// 构造函数(读取默认配置文件的connectionStrings节点)
        /// </summary>
        /// <param name="configKey">子节点名称</param>
        public SqlHelper(string configKey)
            : base(configKey)
        { }

        /// <summary>
        /// 构造函数(读取默认配置文件节点)
        /// </summary>
        /// <param name="configKey">子节点名称</param>
        /// <param name="type">配置文件节点(AppSetting,connectionStrings)</param>
        public SqlHelper(string configKey, EnumType.ConfigType type)
            : base(configKey, type)
        { }

        /// <summary>
        /// 构造函数(读取配置文件信息)
        /// </summary>
        /// <param name="dbConfig">PeanutLibrary.DataBase.DbConfig对象</param>
        public SqlHelper(DbConfig dbConfig)
            : base(dbConfig)
        { }
        #endregion

        #region 私有方法
        /// <summary>
        /// 构建数据库连接
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <param name="conn">SqlConnection对象</param>
        /// <param name="trans">SqlTransaction对象</param>
        /// <param name="cmdText">String对象</param>
        /// <param name="cmdParms">SqlParameter</param>
        protected override void PrepareCommand(object cmd, object conn, object trans, string cmdText, object[] cmdParms)
        {
            SqlCommand _cmd;
            SqlConnection _conn;
            SqlTransaction _trans;
            SqlParameter[] _cmdParms;
            try
            {
                _cmd = (SqlCommand)cmd;
                _conn = (SqlConnection)conn;
                _trans = (SqlTransaction)trans;
                _cmdParms = (SqlParameter[])cmdParms;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.PrepareCommand(object cmd, object conn, object trans, string cmdText, object[] cmdParms)发生参数类型错误.", ex);
            }
            try
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                _cmd.Connection = _conn;
                _cmd.CommandText = cmdText;
                if (trans != null)
                    _cmd.Transaction = _trans;
                _cmd.CommandType = CommandType.Text;//cmdType;
                if (_cmdParms != null)
                {
                    foreach (SqlParameter parameter in _cmdParms)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        _cmd.Parameters.Add(parameter);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.PrepareCommand(object cmd, object conn, object trans, string cmdText, object[] cmdParms)发生错误.", ex);
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        protected override object BuildQueryCommand(object connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection _connection;
            try
            {
                _connection = (SqlConnection)connection;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.BuildQueryCommand(object connection, string storedProcName, IDataParameter[] parameters)发生参数类型错误.", ex);
            }
            try
            {
                SqlCommand command = new SqlCommand(storedProcName, _connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
                return command;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.BuildQueryCommand(object connection, string storedProcName, IDataParameter[] parameters)发生错误.", ex);
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        protected override object BuildIntCommand(object connection, string storedProcName, IDataParameter[] parameters)
        {
            try
            {
                SqlCommand command = (SqlCommand)BuildQueryCommand(connection, storedProcName, parameters);
                command.Parameters.Add(new SqlParameter("ReturnValue",
                    SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                    false, 0, 0, string.Empty, DataRowVersion.Default, null));
                return command;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.BuildIntCommand(object connection, string storedProcName, IDataParameter[] parameters)发生错误.", ex);
            }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public bool ColumnExists(string tableName, string columnName)
        {
            try
            {
                string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
                object res = GetSingle(sql);
                if (res == null)
                {
                    return false;
                }
                return Convert.ToInt32(res) > 0;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.ColumnExists(string tableName, string columnName)发生错误.", ex);
            }
        }

        /// <summary>
        /// 获取对应字段的当前最大值+1(Int)
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="TableName">表名</param>
        /// <returns>最大值+1</returns>
        public int GetMaxID(string FieldName, string TableName)
        {
            try
            {
                string strsql = "select max(" + FieldName + ")+1 from " + TableName;
                object obj = GetSingle(strsql);
                if (obj == null)
                {
                    return 1;
                }
                else
                {
                    return int.Parse(obj.ToString());
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.GetMaxID(string FieldName, string TableName)发生错误.", ex);
            }
        }

        /// <summary>
        /// 判断Sql语句是否能返回有效值
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <returns>是否返回有效值</returns>
        public bool Exists(string sentence)
        {
            try
            {
                object obj = GetSingle(sentence);
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = int.Parse(obj.ToString());
                }
                if (cmdresult == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.Exists(string sentence)发生错误.", ex);
            }
        }

        /// <summary>
        /// 判断Sql语句是否能返回有效值
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>是否返回有效值</returns>
        public bool Exists(string sentence, params object[] cmdParms)
        {
            try
            {
                object obj = GetSingle(sentence, cmdParms);
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = int.Parse(obj.ToString());
                }
                if (cmdresult == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.Exists(string sentence, params object[] cmdParms)发生错误.", ex);
            }
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>是否存在</returns>
        public bool TabExists(string TableName)
        {
            try
            {
                string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
                //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
                object obj = GetSingle(strsql);
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = int.Parse(obj.ToString());
                }
                if (cmdresult == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.TabExists(string TableName)发生错误.", ex);
            }
        }
        #endregion

        #region 简单语句
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSentence(string sentence)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sentence, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw new Exception.DbException("在SqlHelper.ExecuteSentence(string sentence)发生错误.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSentence(string sentence, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sentence, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception.DbException("在SqlHelper.ExecuteSentence(string sentence, string content)发生错误.", ex);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <param name="Times">响应时间</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSentenceByTime(string sentence, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sentence, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw new Exception.DbException("在SqlHelper.ExecuteSentenceByTime(string sentence, int Times)发生错误.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public object ExecuteSentenceGet(string sentence, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sentence, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception.DbException("在SqlHelper.ExecuteSentenceGet(string sentence, string content)发生错误.", ex);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSentenceInsertImg(string sentence, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sentence, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception.DbException("在SqlHelper.ExecuteSentenceInsertImg(string sentence, byte[] fs)发生错误.", ex);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sentence">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string sentence)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sentence, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw new Exception.DbException("在SqlHelper.GetSingle(string sentence)发生错误.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sentence">计算查询结果语句</param>
        /// <param name="Times">响应时间</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string sentence, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sentence, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw new Exception.DbException("在SqlHelper.GetSingle(string sentence, int Times)发生错误.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <returns>SqlDataReader(注意转换类型)</returns>
        public object ExecuteReader(string sentence)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sentence, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception.DbException("在SqlHelper.GetSingle(string sentence, int Times)发生错误.", ex);
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sentence)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sentence, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception.DbException("在SqlHelper.Query(string sentence)发生错误.", ex);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <param name="Times">响应时间</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sentence, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sentence, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception.DbException("在SqlHelper.Query(string sentence, int Times)发生错误.", ex);
                }
                return ds;
            }
        }
        #endregion

        #region 带参数语句
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sentence, params object[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sentence, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception.DbException("在SqlHelper.ExecuteSql(string sentence, params object[] cmdParms)发生错误.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sentence">计算查询结果语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string sentence, params object[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sentence, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception.DbException("在SqlHelper.GetSingle(string sentence, params object[] cmdParms)发生错误.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>SqlDataReader(注意类型转换)</returns>
        public object ExecuteReader(string sentence, params object[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, sentence, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception.DbException("在SqlHelper.ExecuteReader(string sentence, params object[] cmdParms)发生错误.", ex);
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sentence, params object[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sentence, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception.DbException("在SqlHelper.Query(string sentence, params object[] cmdParms)发生错误.", ex);
                    }
                    return ds;
                }
            }
        }
        #endregion

        #region 存储过程
        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader(注意类型转换)</returns>
        public object RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataReader returnReader;
                connection.Open();
                SqlCommand command = (SqlCommand)BuildQueryCommand(connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return returnReader;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.RunProcedure(string storedProcName, IDataParameter[] parameters)发生错误.", ex);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = (SqlCommand)BuildQueryCommand(connection, storedProcName, parameters);
                    sqlDA.Fill(dataSet, tableName);
                    connection.Close();
                    return dataSet;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)发生错误.", ex);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <param name="Times">响应时间</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = (SqlCommand)BuildQueryCommand(connection, storedProcName, parameters);
                    sqlDA.SelectCommand.CommandTimeout = Times;
                    sqlDA.Fill(dataSet, tableName);
                    connection.Close();
                    return dataSet;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)发生错误.", ex);
            }
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns>ReturnValue</returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int result;
                    connection.Open();
                    SqlCommand command = (SqlCommand)BuildIntCommand(connection, storedProcName, parameters);
                    rowsAffected = command.ExecuteNonQuery();
                    result = (int)command.Parameters["ReturnValue"].Value;
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DbException("在SqlHelper.RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)发生错误.", ex);
            }
        }
        #endregion

        #region 数据库事务
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sentenceList">多条SQL语句</param>
        public int ExecuteSentenceTran(List<string> sentenceList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < sentenceList.Count; n++)
                    {
                        string strsql = sentenceList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    tx.Rollback();
                    throw new Exception.DbException("在SqlHelper.ExecuteSentenceTran(List<string> sentenceList)发生错误.", ex);
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sentenceList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public bool ExecuteSentenceTran(Hashtable sentenceList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in sentenceList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        trans.Rollback();
                        throw new Exception.DbException("在SqlHelper.ExecuteSentenceTran(Hashtable sentenceList)发生错误.", ex);
                    }
                }
            }
        }
        #endregion
    }
}
