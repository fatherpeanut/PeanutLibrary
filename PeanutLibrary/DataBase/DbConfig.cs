using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.DataBase
{
    public class DbConfig
    {
        private string _configPath;
        private string _configKey;
        private EnumType.ConfigType _configType;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string configPath
        {
            get { return _configPath; }
            set { _configPath = value; }
        }

        /// <summary>
        /// 子节点名称
        /// </summary>
        public string configKey
        {
            get { return _configKey; }
            set { _configKey = value; }
        }

        /// <summary>
        /// 配置文件节点(AppSetting,connectionStrings)
        /// </summary>
        public EnumType.ConfigType configType
        {
            get { return _configType; }
            set { _configType = value; }
        }

        /// <summary>
        /// 构造函数(默认配置文件ConnectionStrings.ConnectionString)
        /// </summary>
        public DbConfig()
        {
            configKey = "connectionString";
            configType = EnumType.ConfigType.Connection;
        }

        /// <summary>
        /// 构造函数(自定义)
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        /// <param name="configKey">子节点名称</param>
        /// <param name="type">构造函数(默认配置文件ConnectionStrings.ConnectionString)</param>
        public DbConfig(string configPath, string configKey, EnumType.ConfigType type)
        {
            _configPath = configPath;
            _configKey = configKey;
            _configType = type;
        }
    }
}
