using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace NursingCommon
{
    /// <summary>
    /// 全局配置类
    /// </summary>
    public static class SiteConfig
    {
        public static IConfigurationSection _appSection = null;

        public static string OracleConn = null;

        public static bool StandardAPI { get; set; }
        //数据库
        public static List<DataConnect> DataConnectList = null;
        //登录缓存信息
        public static UserLogin UserInfo = null;

        //Token的加密串
        public static string Secret = "3DA828DDEACD4610B70FF011EAF66428";

        /// <summary>
        /// api配置信息
        /// </summary>
        public static string AppSetting(string key)
        {
            string str = string.Empty;
            if (_appSection.GetSection(key) != null)
            {
                str = _appSection.GetSection(key).Value;
            }
            return str;
        }

        public static void SetAppSetting(IConfigurationSection section)
        {
            _appSection = section;
        }

        public static void SetConnectObj(List<DataConnect> section)
        {
            DataConnectList = section;
        }

     
        /// <summary>
        /// 设置oracle连接串
        /// </summary>
        /// <param name="section"></param>
        public static void SetOracleConn(string section)
        {
            OracleConn = section;
        }

        public static string GetSite(string apiName)
        {
            return AppSetting(apiName);
        }
        //Token的加密串
        public static void SetSecretSetting(string section)
        {
            Secret = section;
        }

        public static void SetUserLoginInfo(UserLogin user)
        {
            UserInfo = user;
        }
    }

    public class UserLogin
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string ChineseName { get; set; }
    }
}
