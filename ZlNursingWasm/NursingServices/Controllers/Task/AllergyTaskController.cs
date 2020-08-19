using NursingCommon;using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NursingBLL;
using NursingBaseFunc;
using ZlNursingCommon;
using ZlNursingServices.Controllers;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 皮试任务
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    [Route("pda/[controller]")]
    public class AllergyTaskController : ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<AllergyTaskController>();
            return Json(servicesStructures);
        }

        #region  皮试医嘱任务
        /// <summary>
        ///皮试医嘱执行后，产生皮试结果观察任务，本地任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="baby">婴儿序号</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="orderId">医嘱ID</param>
        /// <param name="itemId">诊疗项目ID</param>
        /// <param name="time">执行时间</param>
        /// <param name="executeDescript">执行说明</param>
        /// <returns></returns>
        [HttpGet("AddAllergyTask")]
        [DisplayName(name = "产生皮试结果观察任务")]
        [Note(name = "产生皮试结果观察任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddAllergyTask(string pid, string pvid, decimal baby, string wardId, string  orderId, int itemId, DateTime time, string executeDescript )
        {
            AllergyTaskBLL allergyTask = new AllergyTaskBLL();
            try
            {
                if (allergyTask.AddAllergyTask(pid, pvid, baby, wardId, orderId, itemId, time, executeDescript))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("添加任务失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///皮试结果登记后，完成皮试结果观察任务，本地任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="baby">婴儿序号</param>
        /// <param name="orderId">医嘱ID</param>
        /// <param name="time">执行时间</param>
        /// <param name="executor">执行人</param>
        /// <returns></returns>
        [HttpGet("FinishAllergyTask")]
        [DisplayName(name = "完成皮试结果观察任务")]
        [Note(name = "完成皮试结果观察任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult FinishAllergyTask(string taskId, string pid, string pvid, decimal baby, string orderId, DateTime time, string executor)
        {
            AllergyTaskBLL allergyTask = new AllergyTaskBLL();
            try
            {
                if (allergyTask.FinishAllergyTask(taskId, pid, pvid, baby, orderId, time, executor))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("完成任务失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}