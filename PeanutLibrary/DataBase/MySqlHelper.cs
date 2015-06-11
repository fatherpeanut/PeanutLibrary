using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.DataBase
{
    public class MySqlHelper : DbAbs, IDbHelper
    {
        public MySqlHelper()
            : base("connectString")
        { 
        }

        public MySqlHelper(string appKey)
            : base(appKey)
        { 
        }
    }
}
