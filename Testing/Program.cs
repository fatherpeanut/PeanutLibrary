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
            PeanutLibrary.Config.ConnectionHelper config = new PeanutLibrary.Config.ConnectionHelper();
            Console.WriteLine(config.GetValue("abc"));
            PeanutLibrary.Config.ConnectionHelper configUser = new PeanutLibrary.Config.ConnectionHelper("Config\\User.config");
            Console.WriteLine(configUser.GetValue("abc"));
        }
    }
}
