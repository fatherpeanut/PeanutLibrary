using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PeanutLibrary
{
    public class ConfigHelper
    {
        private static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        public static void SetConfigFile(string configPath)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = configPath;
            config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 设置配置文件(默认路径)
        /// </summary>
        public static void SetConfigFile()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static string GetValue(string key)
        {
            return config.AppSettings.Settings[key].Value.ToString().Trim();
        }

        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetValue(string key, string value)
        {
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }

        /// <summary>
        /// 添加新的Key ，Value键值对
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public static void AddSection(string key, string value)
        {
            config.AppSettings.Settings.Add(key, value);
            config.Save();
        }

        /// <summary>
        /// 根据Key删除项
        /// </summary>
        /// <param name="key">Key</param>
        public static void RemoveSection(string key)
        {
            config.AppSettings.Settings.Remove(key);
            config.Save();
        }
    }
}
