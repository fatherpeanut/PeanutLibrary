using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PeanutLibrary.Config
{
    public abstract class ConfigAbs
    {
        protected Configuration config;

        /// <summary>
        /// 抽象构造函数(默认配置文件)
        /// </summary>
        protected ConfigAbs()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 抽象构造函数
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        protected ConfigAbs(string configPath)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = configPath;
            config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public abstract string GetValue(string key);

        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public abstract void SetValue(string key, string value);

        /// <summary>
        /// 添加新的Key ，Value键值对
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public abstract void AddSection(string key, string value);

        /// <summary>
        /// 根据Key删除项
        /// </summary>
        /// <param name="key">Key</param>
        public abstract void RemoveSection(string key);
    }
}
