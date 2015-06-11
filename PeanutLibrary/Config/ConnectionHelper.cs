using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Config
{
    public class ConnectionHelper : ConfigAbs
    {
        public ConnectionHelper()
            : base()
        { }

        public ConnectionHelper(string configPath)
            : base(configPath)
        { }

        public override string GetValue(string key)
        {
            return config.ConnectionStrings.ConnectionStrings[key].ConnectionString.ToString().Trim();
        }

        public override void SetValue(string key, string value)
        {
            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            config.Save();
        }

        public override void AddSection(string key, string value)
        {
            config.ConnectionStrings.ConnectionStrings.Add(new System.Configuration.ConnectionStringSettings(key, value));
            config.Save();
        }

        public override void RemoveSection(string key)
        {
            config.ConnectionStrings.ConnectionStrings.Remove(key);
            config.Save();
        }
    }
}
