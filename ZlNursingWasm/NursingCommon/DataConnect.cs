using System;
namespace NursingCommon
{
    [Serializable]
    public class DataConnect
    {
        /// <summary>
        /// 连接串名称：ZLHIS|ZLLIS
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        public string HOST { get; set; }
        /// <summary>
        /// 连接名
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 连接密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string Service { get; set; }
    }
}
