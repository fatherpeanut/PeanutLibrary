using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Exception
{
    public class StringException : LibraryException
    {
        public StringException()
        { }

        public StringException(string message)
            : base(message)
        { }

        public StringException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
