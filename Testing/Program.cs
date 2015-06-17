using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeanutLibrary;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            PeanutLibrary.DataBase.IDbHelper db = new PeanutLibrary.DataBase.SqlHelper();
        }
    }
}
