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

            Console.ReadLine();
            
            
        }
    }
}
