using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NursingCommon
{
    public class LogHelper
    {
        public static string logpath = Directory.GetCurrentDirectory();
        public static string errpath = Directory.GetCurrentDirectory();

        /// <summary>
        /// 运行日志记录
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public static void WriteLog(Exception ex, string message)
        {
            try
            {
                //FileOperate.SaveStringToFile("------------------------------------开始记录-----------------------------------------", logpath, false, false);
                FileOperate.SaveStringToFile(","+message, logpath);
            }
            catch (Exception e)
            {
            }
            finally
            {
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public static void WriteErrorLog(Exception ex, string message)
        {
            try
            {
                //FileOperate.SaveStringToFile("------------------------------------开始记录-----------------------------------------", logpath, false, false);

                ErrLog errLog = new ErrLog();
                errLog.ErrTime = DateTime.Now;
                errLog.ErrInfo = RemoveJsonKeyWords(message);
                if (ex != null)
                {
                    errLog.CallFunc = RemoveJsonKeyWords(ex.TargetSite.ToString());
                    errLog.ErrObject = RemoveJsonKeyWords(ex.Source);
                    errLog.CallStack = RemoveJsonKeyWords(ex.StackTrace);
                }
                FileOperate.SaveStringToFile(","+JsonConvert.SerializeObject(errLog), errpath);
            }
            catch (Exception e)
            {
            }
            finally
            {
            }
        }

        private static string RemoveJsonKeyWords(string str)
        {
            return str.Replace("{", "【").Replace("}", "】").Replace("[", "【").Replace("]", "】").Replace("\"", "“");
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="v"></param>
        public static void Debug(string v)
        {
            try
            {
                
            }
            catch (Exception e)
            {
               
            }
            finally
            {
               
            }
        }
    }
}
