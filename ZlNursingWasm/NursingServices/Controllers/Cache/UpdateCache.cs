using NursingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NursingBLL;
using NursingBLL.Cache;
using NursingModel.TaskModels;

namespace NursingServices.Controllers
{
    [Route("[controller]/[action]")]
    [Route("api/[controller]/[action]")]
    [Route("bh/[controller]/[action]")]
    [Route("pda/[controller]/[action]")]

    public class UpdateCacheController : ApiController
    {
        //todo:刷新缓存后，转到一个页面，刷新开始时间、结束时间、总时长、刷新的内容
        public IMemoryCache Cache { get; set; }
        public UpdateCacheController(IMemoryCache memoryCache)
        {
            Cache = memoryCache;

        }
        /// <summary>
        /// 更新event_time缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet("EventTime")]
        public IActionResult EventTime()
        {
            //更新缓存
            EventTimeBLL eventTime = new EventTimeBLL();
            List<EventTime> eventTimes = eventTime.GetAll().ToList();
            Cache.Set<List<EventTime>>("eventTime", eventTimes);
            //MemeryCacheHelper<List<EventTime>>.Update(eventTimes, "eventTime");
            return Ok();
        }
        [HttpGet("TaskFormList")]
        public IActionResult TaskFormList()
        {
            List<TaskFormList> formList = new List<TaskFormList>();
            return Ok();
        }
        /// <summary>
        /// 更新所有缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet("UpdateALL")]
        public IActionResult UpdateALL()
        {
            InitCache.Init();
            return Ok();
        }
    }
}
