using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PeanutLibrary.DataBase
{
    public abstract class DbAbs
    {
        protected string connectionString = "数据库连接字符串";

        public DbAbs(string appKey)
        {
            connectionString = appKey;
        }
    }
}
