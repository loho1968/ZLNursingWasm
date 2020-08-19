using System;
using System.Collections.Generic;
using System.Text;

namespace NursingCommon
{
    /// <summary>
    /// Api日志帮助类
    /// </summary>
    [Serializable]
    public class ApiLog
    {
        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller_Log { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 总 时 间
        /// </summary>
        public string TotalTime { get; set; }
        /// <summary>
        /// Http请求头
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIP { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// 控制器参数
        /// </summary>
        public string ActionParams { get; set; }
    }
}
