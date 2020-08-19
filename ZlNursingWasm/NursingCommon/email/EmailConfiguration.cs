using System;
using System.Collections.Generic;
using System.Text;

namespace NursingCommon
{
    /// <summary>
    /// 邮件配置信息
    /// </summary>
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
