
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NursingCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using NursingModel.TaskModels;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace NursingServices
{
    public class ActionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private readonly string Key = "_thisWebApiOnActionMonitorLog_";
        private string logpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log/runtime/log" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        private string errpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log/error/log" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");


        public ActionFilter()
        {
        }
        /// <summary>
        /// api执行之前
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            actionContext.HttpContext.Request.EnableBuffering();
            ApiLog MonLog = new ApiLog();
            MonLog.StartTime = DateTime.Now;
            var controllerActionDescriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;

            //获取Action 参数
            MonLog.ActionParams = null;// actionContext.ActionArguments;
            MonLog.Header = actionContext.HttpContext.Request.Headers.ToString();
            MonLog.RequestType = actionContext.HttpContext.Request.Method;
            MonLog.ClientIP = actionContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            byte[] arr = ObjectToBytes(MonLog);

            actionContext.HttpContext.Session.Set(Key, arr);
        }
        /// <summary>
        /// api执行完毕
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {

            base.OnActionExecuted(actionExecutedContext);
            LogHelper.logpath = logpath;
            LogHelper.errpath = errpath;
            byte[] vs;
            if (!actionExecutedContext.HttpContext.Session.TryGetValue(Key, out vs))
            {
                LogHelper.WriteErrorLog(null, "尝试获取值失败");
                return;
            }
            ApiLog MonLog = BytesToObject(vs) as ApiLog;
            actionExecutedContext.HttpContext.Session.Clear();
            MonLog.EndTime = DateTime.Now;
            MonLog.TotalTime = ((MonLog.EndTime - MonLog.StartTime).TotalSeconds * 1000).ToString() + "毫秒";
            MonLog.Method = actionExecutedContext.RouteData.Values["Action"].ToString();
            MonLog.Controller_Log = actionExecutedContext.RouteData.Values["Controller"].ToString();
            LogHelper.WriteLog(null, JsonConvert.SerializeObject(MonLog));
        }



        /// <summary>
        /// 统一返回
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
            if (!SiteConfig.StandardAPI)
                return;
            //对接BH平台
            if (context.HttpContext.Request.Path.Value.Contains("/bh/"))
            {
                return;
            }
            //对接PDA，格式待确定
            else if (context.HttpContext.Request.Path.Value.Contains("/pda/"))
            {
                return;
            }
            if (context.Result is JsonResult)
            {
                var objectResult = context.Result as JsonResult;
                if (objectResult.Value == null)
                {
                    context.Result = new ObjectResult(new { code = 404, sub_msg = "未找到资源", msg = "" });
                }
                else
                {
                    context.Result = new ObjectResult(new { Success = true, Msg = "", Data = objectResult.Value });
                }
            }
            else if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult.Value == null)
                {
                    context.Result = new ObjectResult(new { code = 404, sub_msg = "未找到资源", msg = "" });
                }
                else
                {
                    context.Result = new ObjectResult(new { Success = true, Msg = "", Data = objectResult.Value });
                    //电子白板返回专用（电子白板需要特定返回格式）
                    if (context.HttpContext.Request.Path.Value.Contains("GetElectronicWhiteBoard"))
                    {
                        context.Result = new ObjectResult(objectResult.Value);
                    }
                }
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(new { code = 404, sub_msg = "未找到资源", msg = "" });
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new ObjectResult(new { code = 200, msg = "", result = (context.Result as ContentResult).Content });
            }
            else if (context.Result is StatusCodeResult)
            {
                context.Result = new ObjectResult(new { code = (context.Result as StatusCodeResult).StatusCode, sub_msg = "", msg = "" });
            }
        }

        /// <summary>
        /// 对象转byte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /// <summary>
        /// byte转对象
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// 错误日志记录
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            LogHelper.WriteErrorLog(context.Exception, context.Exception.Message);
        }
    }
}