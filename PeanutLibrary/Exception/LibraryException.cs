using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Exception
{
    public class LibraryException : ApplicationException
    {
        public LibraryException()
        { }

        public LibraryException(string message)
            : base(message)
        { }

        public LibraryException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
