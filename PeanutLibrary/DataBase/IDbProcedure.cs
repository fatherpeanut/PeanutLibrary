using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PeanutLibrary.DataBase
{
    public interface IDbProcedure
    {
        /// <summary>
        /// 执行存储过程，返回相应数据库DataReader ( 注意：调用该方法后，一定要对相应数据库DataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>相应数据库DataReader</returns>
        object RunProcedure(string storedProcName, IDataParameter[] parameters);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <param name="Times">响应时间</param>
        /// <returns>DataSet</returns>
        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times);

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">out影响的行数</param>
        /// <returns>ReturnValue</returns>
        int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected);
    }
}
