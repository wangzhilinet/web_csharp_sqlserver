using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FisherCommunity.App.Utility
{
    /// <summary>
    /// 配置帮助类
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>
        /// 取得布尔类型设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBoolSetting(string key)
        {
            return GetBoolSetting(key, false);
        }
        /// <summary>
        /// 取得布尔类型设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetBoolSetting(string key, bool defaultValue)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings[key]);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 取得布尔类型设置
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBoolSetting(Configuration configuration, string key)
        {
            return GetBoolSetting(configuration, key, false);
        }
        /// <summary>
        /// 取得布尔类型设置
        /// </summary>
        /// <param name="configuration">配置对象</param>
        /// <param name="key">主键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔类型配置值。如果指定的键不存在或值不是整型，则返回默认值。</returns>
        public static bool GetBoolSetting(Configuration configuration, string key, bool defaultValue)
        {
            if (configuration.AppSettings.Settings[key] == null)
            {
                return defaultValue;
            }
            bool result;
            bool success = bool.TryParse(configuration.AppSettings.Settings[key].Value, out result);
            if (success)
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 取得布尔类型设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSetting(string key)
        {
            return GetSetting(key, string.Empty);
        }
        /// <summary>
        /// 取得布尔类型设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetSetting(string key, string defaultValue)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                return defaultValue;
            }
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        /// 取得配置
        /// </summary>
        /// <param name="configuration">配置对象</param>
        /// <param name="key">主键名称</param>
        /// <returns></returns>
        public static string GetSetting(Configuration configuration, string key)
        {
            return GetSetting(configuration, key, string.Empty);
        }
        /// <summary>
        /// 取得配置
        /// </summary>
        /// <param name="configuration">配置对象</param>
        /// <param name="key">主键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串类型配置值。如果指定的键不存在，则返回默认值。</returns>
        public static string GetSetting(Configuration configuration, string key, string defaultValue)
        {
            if (configuration.AppSettings.Settings[key] == null)
            {
                return defaultValue;
            }
            return configuration.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// 取得int类型设置
        /// </summary>
        /// <param name="configuration">配置对象</param>
        /// <param name="key">主键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回指定键的int类型值。如果指定的键不存在或值不是整型，则返回默认值。</returns>
        public static int GetIntSetting(Configuration configuration, string key, int defaultValue)
        {
            if (configuration.AppSettings.Settings[key] == null)
            {
                return defaultValue;
            }
            int result;
            bool success = int.TryParse(configuration.AppSettings.Settings[key].Value, out result);
            if (success)
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 取得整型类型设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetIntSetting(string key)
        {
            return GetIntSetting(key, 0);
        }
        /// <summary>
        /// 取得整型类型设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetIntSetting(string key, int defaultValue)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings[key]);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 取得double类型设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static double GetDoubleSetting(string key)
        {
            return GetDoubleSetting(key, 0);
        }
        /// <summary>
        /// 取得double类型设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDoubleSetting(string key, double defaultValue)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToDouble(ConfigurationManager.AppSettings[key]);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 取得double类型设置
        /// </summary>
        /// <param name="configuration">配置对象</param>
        /// <param name="key">主键名称</param>
        /// <returns>返回指定键的double类型值。</returns>
        public static double GetDoubleSetting(Configuration configuration, string key)
        {
            return GetDoubleSetting(configuration, key, 0);
        }
        /// <summary>
        /// 取得double类型设置
        /// </summary>
        /// <param name="configuration">配置对象</param>
        /// <param name="key">主键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回指定键的double类型值。如果指定的键不存在或值不是double类型，则返回默认值。</returns>
        public static double GetDoubleSetting(Configuration configuration, string key, double defaultValue)
        {
            if (configuration.AppSettings.Settings[key] == null)
            {
                return defaultValue;
            }
            double result;
            bool success = double.TryParse(configuration.AppSettings.Settings[key].Value, out result);
            if (success)
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 添加或编辑配置项
        /// </summary>
        /// <param name="configuration">配置对象</param>
        /// <param name="key">主键名称</param>
        /// <param name="value">值</param>
        public static void SetSetting(Configuration configuration, string key, string value)
        {
            if (configuration.AppSettings.Settings[key] == null)
            {
                configuration.AppSettings.Settings.Add(key, value);
            }
            else
            {
                configuration.AppSettings.Settings[key].Value = value;
            }
        }
    }
}
