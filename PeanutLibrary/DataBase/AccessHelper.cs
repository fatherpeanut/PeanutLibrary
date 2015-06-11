using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.DataBase
{
    public class AccessHelper : DbAbs, IDbHelper
    {
        public AccessHelper()
            : base("connectString")
        { 
        }

        public AccessHelper(string appKey)
            : base(appKey)
        { 
        }
    }
}
