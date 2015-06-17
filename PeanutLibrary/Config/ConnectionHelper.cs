using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Config
{
    public class ConnectionHelper : ConfigAbs, IConfigHelper
    {
        public ConnectionHelper()
            : base()
        { }

        public ConnectionHelper(string configPath)
            : base(configPath)
        { }

        public string GetValue(string key)
        {
            return config.ConnectionStrings.ConnectionStrings[key].ConnectionString.ToString().Trim();
        }

        public void SetValue(string key, string value)
        {
            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            config.Save();
        }

        public void AddSection(string key, string value)
        {
            config.ConnectionStrings.ConnectionStrings.Add(new System.Configuration.ConnectionStringSettings(key, value));
            config.Save();
        }

        public void RemoveSection(string key)
        {
            config.ConnectionStrings.ConnectionStrings.Remove(key);
            config.Save();
        }
    }
}
