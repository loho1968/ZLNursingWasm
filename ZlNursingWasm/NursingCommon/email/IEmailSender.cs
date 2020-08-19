using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NursingCommon
{
    /// <summary>
    /// 邮件接口
    /// </summary>
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
