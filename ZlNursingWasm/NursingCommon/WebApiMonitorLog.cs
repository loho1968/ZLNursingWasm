using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NursingCommon
{
    /// <summary>
    /// webapi日志监控
    /// </summary>
    [Serializable]
    public class WebApiMonitorLog
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public DateTime ExcuteStartTime { get; set; }

        public DateTime ExcuteEndTime { get; set; }

        /// <summary>
        /// 请求的Action 参数
        /// </summary>
        public IDictionary<string, object> ActionParams
        {
            get;
            set;
        }
        /// <summary>
        /// Http请求头
        /// </summary>
        public string HttpRequestHeaders
        {
            get;
            set;
        }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod
        {
            get;
            set;
        }
        /// <summary>
        /// 请求的IP地址
        /// </summary>
        public string IP
        {
            get;
            set;
        }
        /// <summary>
        /// 获取监控指标日志
        /// </summary>
        /// <param name="mtype"></param>
        /// <returns></returns>
        //public string GetLoginfo()
        //{
        //    string Msg = @"
        //    Action执行时间监控：
        //    控制器：{0}Controller
        //    方法:{1}
        //    开始时间：{2}
        //    结束时间：{3}
        //    总 时 间：{4}秒
        //    Http请求头:{5}
        //    客户端IP：{6},
        //    请求方法：{7}
        //            ";

        //    return string.Format(Msg,
        //        ControllerName,
        //        ActionName,
        //        ExcuteStartTime,
        //        ExcuteEndTime,
        //        (ExcuteEndTime - ExcuteStartTime).TotalSeconds,
        //         HttpRequestHeaders,
        //        IP,
        //        HttpMethod);
        //}
        /// <summary>
        /// 获取Action 参数
        /// </summary>
        /// <param name="Collections"></param>
        /// <returns></returns>
        public string GetCollections(Dictionary<string, object> Collections)
        {
            string Parameters = string.Empty;
            if (Collections == null || Collections.Count == 0)
            {
                return Parameters;
            }
            foreach (string key in Collections.Keys)
            {
                Parameters += string.Format("{0}={1}&", key, Collections[key]);
            }
            if (!string.IsNullOrWhiteSpace(Parameters) && Parameters.EndsWith("&"))
            {
                Parameters = Parameters.Substring(0, Parameters.Length - 1);
            }
            return Parameters;
        }


    }
}