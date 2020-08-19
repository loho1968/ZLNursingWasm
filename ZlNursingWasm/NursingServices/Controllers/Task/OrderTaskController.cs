using NursingCommon;using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NursingBaseFunc;
using NursingBLL;
using ZlNursingCommon;
using ZlNursingServices.Controllers;



namespace NursingServices.Controllers
{
    /// <summary>
    /// 任务医嘱
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    public class OrderTaskController : ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<OrderTaskController>();
            return Json(servicesStructures);
        }

        /// <summary>
        /// 获取任务系统医嘱
        /// </summary>
        /// <param name="Pid"></param>
        /// <param name="Pvid"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="NbSno"></param>
        /// <returns></returns>
        [HttpGet("GetTaskOrder")]
        [DisplayName(name = "获取任务系统医嘱")]
        [Note(name = "获取任务系统医嘱")]
        [ParaOutName(name = "TaskOrderContent")]
        [SchemaContent(name = "TaskOrderContent")]
        public IActionResult GetTaskOrder(string Pid, string Pvid, DateTime BeginTime, DateTime EndTime,int NbSno)
        {
            OrderTaskBLL orderTaskBLL=new OrderTaskBLL();
            return Ok(orderTaskBLL.GetTaskOrder(Pid, Pvid,NbSno, BeginTime, EndTime));
        }

        /// <summary>
        /// 获取分管床位病人任务清单
        /// </summary>
        /// <param name="NurseId"></param>
        /// <param name="WardId"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="NbSno"></param>
        /// <returns></returns>
        [HttpGet("GetPatsTaskOrder")]
        [DisplayName(name = "获取分管床位病人任务清单")]
        [Note(name = "获取分管床位病人任务清单")]
        [ParaOutName(name = "TaskOrderContent")]
        [SchemaContent(name = "TaskOrderContent")]
        public IActionResult GetPatsTaskOrder(string NurseId, Int32 WardId, DateTime BeginTime, DateTime EndTime,int NbSno)
        {
            OrderTaskBLL orderTaskBLL = new OrderTaskBLL();
            return Ok(orderTaskBLL.GetPatsTaskOrder(NurseId,WardId,BeginTime,EndTime,NbSno));
        }
    }
}
