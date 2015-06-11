using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.DataBase
{
    public class SqlHelper : DbAbs, IDbHelper
    {
        public SqlHelper()
            : base("connectString")
        { 
        }

        public SqlHelper(string appKey)
            : base(appKey)
        { 
        }
    }
}
