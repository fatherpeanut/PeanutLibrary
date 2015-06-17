using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Exception
{
    public class DbException : LibraryException
    {
        public DbException()
        { }

        public DbException(string message)
            : base(message)
        { }

        public DbException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
