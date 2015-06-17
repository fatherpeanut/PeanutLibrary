using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace PeanutLibrary.DataBase
{
    public interface IDbTransaction
    {
        /// <summary>
        /// 执行多条数据库语句，实现数据库事务。
        /// </summary>
        /// <param name="sentenceList">多条数据库语句</param>
        int ExecuteSentenceTran(List<String> sentenceList);

        /// <summary>
        /// 执行多条数据库语句，实现数据库事务。
        /// </summary>
        /// <param name="sentenceList">数据库语句的哈希表（key为数据库语句，value是该语句的SqlParameter[]）</param>
        bool ExecuteSentenceTran(Hashtable sentenceList);
    }
}
