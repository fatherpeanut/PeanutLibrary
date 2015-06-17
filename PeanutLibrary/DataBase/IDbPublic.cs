using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.DataBase
{
    public interface IDbPublic
    {
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        bool ColumnExists(string tableName, string columnName);

        /// <summary>
        /// 获取对应字段的当前最大值+1(Int)
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="TableName">表名</param>
        /// <returns>最大值+1</returns>
        int GetMaxID(string FieldName, string TableName);

        /// <summary>
        /// 判断数据库语句是否能返回有效值
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <returns>是否返回有效值</returns>
        bool Exists(string sentence);

        /// <summary>
        /// 判断数据库语句是否能返回有效值
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>是否返回有效值</returns>
        bool Exists(string sentence, params object[] cmdParms);

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>是否存在</returns>
        bool TabExists(string TableName);
    }
}
