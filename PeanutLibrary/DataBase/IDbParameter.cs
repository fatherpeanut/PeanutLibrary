using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PeanutLibrary.DataBase
{
    public interface IDbParameter
    {
        /// <summary>
        /// 执行数据库语句，返回影响的记录数
        /// </summary>
        /// <param name="sentence">数据库语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSql(string sentence, params object[] cmdParms);

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sentence">计算查询结果语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>查询结果（object）</returns>
        object GetSingle(string sentence, params object[] cmdParms);

        /// <summary>
        /// 执行查询语句，返回相应数据库DataReader ( 注意：调用该方法后，一定要对相应数据库DataReader进行Close )
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>相应数据库DataReader</returns>
        object ExecuteReader(string sentence, params object[] cmdParms);

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>DataSet</returns>
        DataSet Query(string sentence, params object[] cmdParms);
    }
}
