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
    }
}
