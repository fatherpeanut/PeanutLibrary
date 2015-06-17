using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PeanutLibrary.DataBase
{
    public interface IDbSimple
    {
        /// <summary>
        /// 执行数据库语句，返回影响的记录数
        /// </summary>
        /// <param name="sentence">数据库语句</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSentence(string sentence);

        /// <summary>
        /// 执行带一个存储过程参数的的数据库语句。
        /// </summary>
        /// <param name="sentence">数据库语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSentence(string sentence, string content);

        /// <summary>
        /// 执行数据库语句，返回影响的记录数
        /// </summary>
        /// <param name="sentence">数据库语句</param>
        /// <param name="Times">响应时间</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSentenceByTime(string sentence, int Times);

        /// <summary>
        /// 执行带一个存储过程参数的的数据库语句。
        /// </summary>
        /// <param name="sentence">数据库语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        object ExecuteSentenceGet(string sentence, string content);

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="sentence">数据库语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSentenceInsertImg(string sentence, byte[] fs);

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sentence">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        object GetSingle(string sentence);

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sentence">计算查询结果语句</param>
        /// <param name="Times">响应时间</param>
        /// <returns>查询结果（object）</returns>
        object GetSingle(string sentence, int Times);

        /// <summary>
        /// 执行查询语句，返回相应数据库DataReader ( 注意：调用该方法后，一定要对相应数据库DataReader进行Close )
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <returns>相应数据库DataReader</returns>
        object ExecuteReader(string sentence);

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <returns>DataSet</returns>
        DataSet Query(string sentence);

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sentence">查询语句</param>
        /// <param name="Times">响应时间</param>
        /// <returns>DataSet</returns>
        DataSet Query(string sentence, int Times);
    }
}
