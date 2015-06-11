using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.DataBase
{
    public class OracleHelper : DbAbs, IDbHelper
    {
        public OracleHelper()
            : base("connectString")
        { 
        }

        public OracleHelper(string appKey)
            : base(appKey)
        { 
        }
    }
}
