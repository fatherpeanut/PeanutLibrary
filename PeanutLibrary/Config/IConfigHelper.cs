using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.Config
{
    public interface IConfigHelper
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        string GetValue(string key);

        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        void SetValue(string key, string value);

        /// <summary>
        /// 添加新的Key ，Value键值对
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        void AddSection(string key, string value);

        /// <summary>
        /// 根据Key删除项
        /// </summary>
        /// <param name="key">Key</param>
        void RemoveSection(string key);
    }
}
