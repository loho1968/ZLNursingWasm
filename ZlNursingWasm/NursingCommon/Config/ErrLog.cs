using System;
using System.Collections.Generic;
using System.Text;

namespace NursingCommon
{
    /// <summary>
    /// 全局错误日志
    /// </summary>
    public class ErrLog
    {
        /// <summary>
        /// 报错时间
        /// </summary>
        public DateTime ErrTime { get; set; }
        /// <summary>
        /// 触发方法
        /// </summary>
        public string CallFunc { get; set; }
        /// <summary>
        /// 异常对象
        /// </summary>
        public string ErrObject { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrInfo { get; set; }
        /// <summary>
        /// 堆栈调用
        /// </summary>
        public string CallStack { get; set; }
    }
}
