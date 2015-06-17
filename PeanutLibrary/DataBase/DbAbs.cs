using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PeanutLibrary.DataBase
{
    public abstract class DbAbs
    {
        protected string connectionString = "数据库连接字符串";
        private Config.IConfigHelper config;

        /// <summary>
        /// 构造函数(读取默认配置文件的connectionStrings.connectionString)
        /// </summary>
        protected DbAbs()
        {
            config = new Config.ConnectionHelper();
            connectionString = config.GetValue("connectionString");
        }

        /// <summary>
        /// 构造函数(读取默认配置文件的connectionString子节点)
        /// </summary>
        /// <param name="type">配置文件节点(AppSetting,connectionStrings)</param>
        protected DbAbs(EnumType.ConfigType type)
        {
            if (type == EnumType.ConfigType.Connection)
                config = new Config.ConnectionHelper();
            else
                config = new Config.AppSectionHelper();
            connectionString = config.GetValue("connectionString");
        }

        /// <summary>
        /// 构造函数(读取默认配置文件的connectionStrings节点)
        /// </summary>
        /// <param name="configKey">子节点名称</param>
        protected DbAbs(string configKey)
        {
            config = new Config.ConnectionHelper();
            connectionString = config.GetValue(configKey);
        }

        /// <summary>
        /// 构造函数(读取默认配置文件节点)
        /// </summary>
        /// <param name="configKey">子节点名称</param>
        /// <param name="type">配置文件节点(AppSetting,connectionStrings)</param>
        protected DbAbs(string configKey, EnumType.ConfigType type)
        {
            if (type == EnumType.ConfigType.Connection)
                config = new Config.ConnectionHelper();
            else
                config = new Config.AppSectionHelper();
            connectionString = config.GetValue(configKey);
        }

        /// <summary>
        /// 构造函数(读取配置文件信息)
        /// </summary>
        /// <param name="dbConfig">PeanutLibrary.DataBase.DbConfig对象</param>
        protected DbAbs(DbConfig dbConfig)
        {
            if (dbConfig.configPath == null)
            {
                if (dbConfig.configType == EnumType.ConfigType.Connection)
                    config = new Config.ConnectionHelper();
                else
                    config = new Config.AppSectionHelper();
            }
            else
            {
                if (dbConfig.configType == EnumType.ConfigType.Connection)
                    config = new Config.ConnectionHelper(dbConfig.configPath);
                else
                    config = new Config.AppSectionHelper(dbConfig.configPath);
            }
            connectionString = config.GetValue(dbConfig.configKey);
        }

        /// <summary>
        /// 构建数据库连接
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        protected abstract void PrepareCommand(object cmd, object conn, object trans, string cmdText, object[] cmdParms);

        /// <summary>
        /// 构建数据库Command对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>数据库Command对象实例</returns>
        protected abstract object BuildQueryCommand(object connection, string storedProcName, IDataParameter[] parameters);

        /// <summary>
        /// 创建数据库Command对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>数据库Command对象实例</returns>
        protected abstract object BuildIntCommand(object connection, string storedProcName, IDataParameter[] parameters);
    }
}
