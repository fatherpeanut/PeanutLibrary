using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Config
{
    public class AppSettingHelper : ConfigAbs
    {
        public AppSettingHelper()
            : base()
        { }

        public AppSettingHelper(string configPath)
            : base(configPath)
        { }

        public override string GetValue(string key)
        {
            return config.AppSettings.Settings[key].Value.ToString().Trim();
        }

        public override void SetValue(string key, string value)
        {
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }

        public override void AddSection(string key, string value)
        {
            config.AppSettings.Settings.Add(key, value);
            config.Save();
        }

        public override void RemoveSection(string key)
        {
            config.AppSettings.Settings.Remove(key);
            config.Save();
        }
    }
}
