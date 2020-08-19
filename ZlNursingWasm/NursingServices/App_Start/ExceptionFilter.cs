using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NursingModel.TaskModels;
using NursingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NursingServices
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public ExceptionFilter() { }
        public override void OnException(ExceptionContext context)
        {
            //对接BH异常
            if (context.HttpContext.Request.Path.Value.Contains("/bh/"))
            {
                var str = context.Exception.GetBaseException().Message;
                var res = new Microsoft.AspNetCore.Mvc.JsonResult(str);//此处必须为JsonObject，BH定义的为json
                res.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = res;
                return;
            }
            ////对接pda产生异常，待定格式
            //else if (context.HttpContext.Request.Path.Value.Contains("/pda/"))
            //{
            //    var str = context.Exception.GetBaseException().Message;
            //    var res = new Microsoft.AspNetCore.Mvc.JsonResult(str);
            //    res.StatusCode = StatusCodes.Status500InternalServerError;
            //    context.Result = res;
            //    return;
            //}
            //对接常规API产生异常，待定格式
            else if (context.HttpContext.Request.Path.Value.Contains("/api/"))
            {
                var strFirstMsg = context.Exception.GetBaseException().Message;
                var result = JsonResult(null, strFirstMsg, StatusCodes.Status500InternalServerError, false);
                result.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = result;
                return;
            }
            //if (!SiteConfig.StandardAPI)
            //{
            //    var NoStar = context.Exception.GetBaseException().Message;
            //    var res = new Microsoft.AspNetCore.Mvc.JsonResult(NoStar, new JsonSerializerSettings
            //    {
            //        NullValueHandling = NullValueHandling.Ignore
            //    });
            //    res.StatusCode = StatusCodes.Status500InternalServerError;
            //    context.Result = res;
            //    return;
            //}

            context.Result = new RedirectToActionResult("Index", "Error", new { error = context.Exception.GetBaseException().Message });
        }

        public static Microsoft.AspNetCore.Mvc.JsonResult JsonResult(object objData, string strMessage, int code, bool blnSuccess = true)
        {
            object obj;
            if (blnSuccess)
                obj = new
                {
                    success = blnSuccess,
                    message = strMessage,
                    data = objData
                };
            else
                obj = new
                {
                    success = blnSuccess,
                    message = strMessage,
                    Data = JObject.Parse("{}"),
                    code = code
                };


            return new Microsoft.AspNetCore.Mvc.JsonResult(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

    }
}
