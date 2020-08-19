using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    public class LoggerHelper
    {
        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository Repository { get; set; }

        private static ILog _log;
        private static ILog Logger
        {
            get
            {
                if (_log == null)
                {
                    Configure();
                }
                return _log;
            }
        }

        private static void Configure()
        {
            _log = LogManager.GetLogger(Repository.Name, "");
        }


        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">message</param>
        public static void Debug(string message)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(message);
            }
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="message">message</param>
        public static void Info(object message)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info(message);
            }
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">message</param>
        public static void Warn(object message)
        {
            if (Logger.IsWarnEnabled)
            {
                Logger.Warn(message);
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">message</param>
        public static void Error(object message)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(message);
            }
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="message">message</param>
        public static void Fatal(object message)
        {
            if (Logger.IsFatalEnabled)
            {
                Logger.Fatal(message);
            }
        }
        /* Log a message object and exception */

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Debug(object message, Exception exception)
        {
            Logger.Debug(message, exception);
        }
        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Info(object message, Exception exception)
        {
            Logger.Info(message, exception);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Warn(object message, Exception exception)
        {
            Logger.Warn(message, exception);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Error(object message, Exception exception)
        {
            Logger.Error(message, exception);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Fatal(object message, Exception exception)
        {
            Logger.Fatal(message, exception);
        }
    }
}
