using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Exception
{
    public class ConfigException : LibraryException
    {
        public ConfigException()
        { }

        public ConfigException(string message)
            : base(message)
        { }

        public ConfigException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
