using NursingCommon;using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NursingBLL;
using NursingDAL;
using NursingBaseFunc;
using ZlNursingCommon;
using ZlNursingServices.Controllers;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 管道任务
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    [Route("pda/[controller]")]
    public class TubeTaskController : ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<TubeTaskController>();
            return Json(servicesStructures);
        }

        #region  管道任务
        /// <summary>
        ///置管后产生任务
        ///1、所有导管置管后产生填写脱落风险评估表任务，立即执行
        ///2、中心静脉导管（PICC、CVC）置管后产生导管观察任务，每天一次
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="patSupdevId">病人附着物ID</param>
        /// <param name="supdevTypeId">附着物类型ID</param>
        /// <param name="time">执行时间</param>
        /// <param name="executor">执行人</param>
        /// <returns></returns>
        [HttpGet("AddTubeTask")]
        [DisplayName(name = "导管任务处理")]
        [Note(name = "导管任务处理")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddTubeTask(string pid, string pvid, string wardId, string patSupdevId, string supdevTypeId, DateTime time, string executor)
        {
            TubeTaskBLL tubeTaskBLL = new TubeTaskBLL();
            TubeTaskDAL tubeTaskDAL = new TubeTaskDAL();
            string supdevName = tubeTaskDAL.GetSupdevName(patSupdevId);
            string executeDescript = "(新插管：" + supdevName + ")";
            try
            {
                tubeTaskBLL.AddTubeTask(pid, pvid, wardId, patSupdevId, supdevTypeId, time, executor, executeDescript);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("任务处理失败");
            }
        }
        /// <summary>
        ///拔管时调用
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="patSupdevId">病人附着物ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="time">执行时间</param>
        /// <param name="executor">执行人</param>
        /// <returns></returns>
        [HttpGet("TubeDrawingTask")]
        [DisplayName(name = "拔管任务处理")]
        [Note(name = "拔管任务处理")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult TubeDrawingTask(string pid, string pvid, string patSupdevId, string wardId, DateTime time, string executor)
        {
            TubeTaskBLL tubeTaskBLL = new TubeTaskBLL();
            TubeTaskDAL tubeTaskDAL = new TubeTaskDAL();
            string supdevName = tubeTaskDAL.GetSupdevName(patSupdevId);
            string executeDescript = "(拔管：" + supdevName + ")";
            try
            {
                tubeTaskBLL.TubeDrawingTask(pid, pvid, patSupdevId, wardId, time, executor, executeDescript);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("任务处理失败");
            }
        }
        /// <summary>
        /// 撤消拔管任务处理
        /// 撤消拔管时需重新评估风险
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="patSupdevId">病人附着物ID</param>
        /// <param name="supdevTypeId">附着物类型ID</param>
        /// <param name="time">操作时间</param>
        /// <param name="executor">操作人</param>
        /// <returns></returns>
        [HttpGet("TubeDrawingUndoTask")]
        [DisplayName(name = "撤消拔管任务处理")]
        [Note(name = "撤消拔管任务处理")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult TubeDrawingUndoTask(string pid, string pvid, string wardId, string patSupdevId, string supdevTypeId, DateTime time, string executor)
        {
            TubeTaskBLL tubeTaskBLL = new TubeTaskBLL();
            try
            {
                tubeTaskBLL.TubeDrawingUndoTask(pid, pvid, wardId, patSupdevId, supdevTypeId, time, executor);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("任务处理失败");
            }
        }

        /// <summary>
        ///执行导管观察记录任务。
        ///填写导管观察记录后调用，继续生成下次任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="patSupdevId">病人附着物ID</param>
        /// <param name="time">执行时间</param>
        /// <param name="executor">执行人</param>
        /// <returns></returns>
        [HttpGet("ExecuteTubeObservTask")]
        [DisplayName(name = "执行导管观察记录任务处理")]
        [Note(name = "执行导管观察记录任务处理")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ExecuteTubeObservTask(int taskId,string pid, string pvid, string patSupdevId, DateTime time, string executor)
        {
            TubeTaskBLL tubeTaskBLL = new TubeTaskBLL();
            try
            {
                tubeTaskBLL.ExecuteTubeObservTask(taskId, pid, pvid, patSupdevId, time, executor);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("任务处理失败");
            }
        }

        /// <summary>
        /// 执行管道脱落风险评估表任务
        /// 填写表单后调用，完成当前任务，并根据分值判断是否生成新的任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="time">执行时间</param>
        /// <param name="executor">执行人</param>
        /// <param name="oldScore">修改前分值，新增时传空</param>
        /// <param name="newScore">修改后分值，或新增的分值</param>
        /// <param name="isNew">是否新增，1-新增，2-修改</param>
        /// <returns></returns>
        [HttpGet("ExecuteTubeRiskTask")]
        [DisplayName(name = "执行管道脱落风险评估任务处理")]
        [Note(name = "执行管道脱落风险评估任务处理")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ExecuteTubeRiskTask(int taskId, string pid, string pvid, string wardId, DateTime time, string executor, decimal oldScore, decimal newScore, int isNew)
        {
            TubeTaskBLL tubeTaskBLL = new TubeTaskBLL();
            //try
            //{
                if (isNew == 1)
                {
                    tubeTaskBLL.ExecuteTubeRiskTask(taskId, pid, pvid, wardId, time, executor, newScore);
                    return Ok();
                }
                else
                {
                    tubeTaskBLL.ModifyTubeRiskTask(taskId, pid, pvid, wardId, time, executor, oldScore, newScore);
                    return Ok();
                }
            //}
            //catch (Exception)
            //{
            //    throw new Exception("任务处理失败");
            //}
        }

        /// <summary>
        /// 作废导管任务处理
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pvid"></param>
        /// <param name="wardId"></param>
        /// <param name="patSupdevId"></param>
        /// <param name="time"></param>
        /// <param name="executor"></param>
        /// <returns></returns>
        [HttpGet("AddTubeCancleTask")]
        [DisplayName(name = "作废导管任务处理")]
        [Note(name = "作废导管任务处理")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddTubeCancleTask(string pid,string pvid, string wardId, string patSupdevId,DateTime time, string executor)
        {
            TubeTaskBLL tubeTaskBLL = new TubeTaskBLL();
            try
            {
                tubeTaskBLL.AddTubeCancleTask(pid, pvid, wardId, patSupdevId, time, executor);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("任务处理失败");
            }
        }

        /// <summary>
        /// 撤消作废导管任务处理
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pvid"></param>
        /// <param name="wardId"></param>
        /// <param name="patSupdevId"></param>
        /// <param name="supdevTypeId"></param>
        /// <param name="time"></param>
        /// <param name="executor"></param>
        /// <returns></returns>
        [HttpGet("AddTubeCancleUndoTask")]
        [DisplayName(name = "撤消作废导管任务处理")]
        [Note(name = "撤消作废导管任务处理")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddTubeCancleUndoTask(string pid, string pvid, string wardId, string patSupdevId, string supdevTypeId, DateTime time,string executor)
        {
            TubeTaskBLL tubeTaskBLL = new TubeTaskBLL();
            try
            {
                tubeTaskBLL.AddTubeCancleUndoTask(pid, pvid, wardId, patSupdevId, supdevTypeId, time, executor);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("任务处理失败");
            }
        }


        #endregion
    } 
}