using Jint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NursingBLL;
using NursingCommon;
using NursingModel.TaskModels;
using NursingModel.TaskModels;
using Quartz;
using ScheduledTasks;

namespace NursingServices.Controllers
{
    /// <summary>
    /// EF core示例，后续可删除
    /// </summary>
    public class EventTimeController : ApiController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //1、返回值统一使用IActionResult
        //2、此层不做过多的业务逻辑处理
        //3、Post类型可定义为dynamic

        /// <summary>
        /// Get-获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {

            IMemoryCache memoryCache = DIMemoryCache.GetMemory();
            memoryCache.Set("test","111");
            string item = memoryCache.Get("test") as string;
            
            EventTimeBLL eventTimeBLL = new EventTimeBLL();
            return Json(eventTimeBLL.GetAll());
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetByID(string id)
        {
            EventTimeBLL eventTimeBLL = new EventTimeBLL();
            return Json(eventTimeBLL.GetByID(id));
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Insert()
        {
            EventTimeBLL eventTimeBLL = new EventTimeBLL();
            EventTime eventTime = new EventTime();
            eventTime.Code = "111";
            eventTime.Name = "仙人板板";
            eventTime.Type = 1;
            return Json(eventTimeBLL.Create(eventTime));
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            EventTimeBLL eventTimeBLL = new EventTimeBLL();
            return Json(eventTimeBLL.Delete(id));

        }

    }
}