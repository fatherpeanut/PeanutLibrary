using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Config
{
    public class AppSectionHelper : ConfigAbs, IConfigHelper
    {
        public AppSectionHelper()
            : base()
        { }

        public AppSectionHelper(string configPath)
            : base(configPath)
        { }

        public string GetValue(string key)
        {
            return config.AppSettings.Settings[key].Value.ToString().Trim();
        }

        public void SetValue(string key, string value)
        {
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }

        public void AddSection(string key, string value)
        {
            config.AppSettings.Settings.Add(key, value);
            config.Save();
        }

        public void RemoveSection(string key)
        {
            config.AppSettings.Settings.Remove(key);
            config.Save();
        }
    }
}
