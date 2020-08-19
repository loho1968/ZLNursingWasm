using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 专用于处理错误
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(string error)
        {
            ViewBag.error = error;
            return View();
        }
    }
}