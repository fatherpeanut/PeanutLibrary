using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Exception
{
    public class EnumException : LibraryException
    {
        public EnumException()
        { }

        public EnumException(string message)
            : base(message)
        { }

        public EnumException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
