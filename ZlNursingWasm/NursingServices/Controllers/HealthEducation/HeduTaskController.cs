using NursingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NursingBaseFunc;
using NursingBLL;
using NursingModel;
using NursingModel.TaskModels;
using ZlNursingCommon;
using ZlNursingServices.Controllers;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SqlSugar;
using MySqlX.XDevAPI.Common;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 健康宣教任务
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    [Route("pda/[controller]")]
    public class HeduTaskController : ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<HeduTaskController>();
                return Json(servicesStructures);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///产生健康宣教任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="baby">婴儿序号</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="groupId">宣教单ID</param>
        /// <param name="eventObjectId">操作对象ID，医嘱发送时为医嘱ID，表单完成时为表单ID</param>
        /// <param name="taskEventCode">事件编码，来源于TASK_EVENT表</param>
        /// <param name="time">执行时间</param>
        /// <returns></returns>
        [HttpGet("AddHeduTask")]
        [DisplayName(name = "产生健康宣教任务")]
        [Note(name = "产生健康宣教任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddHeduTask(string pid, string pvid, decimal baby, string wardId, string groupId,string eventObjectId, string taskEventCode, DateTime time)
        {
            try
            {
                HeduTaskBLL heduTaskBLL = new HeduTaskBLL();
                heduTaskBLL.AddHeduTask(pid, pvid, baby, wardId, groupId, eventObjectId, taskEventCode, time);
                return Ok();
            }
            catch (Exception )
            {
                throw new Exception("添加任务失败");
            }
        }

        /// <summary>
        ///执行并完成健康宣教任务。
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="baby">婴儿序号</param>
        /// <param name="groupId">宣教单ID</param>
        /// <param name="time">执行时间</param>
        /// <param name="executor">执行人</param>
        /// <returns></returns>
        [HttpGet("FinishTubeTask")]
        [DisplayName(name = "执行并完成健康宣教任务")]
        [Note(name = "执行并完成健康宣教任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult FinishTubeTask(int taskId, string pid, string pvid, decimal baby, string groupId, DateTime time, string executor)
        {
            try
            {
                HeduTaskBLL heduTaskBLL = new HeduTaskBLL();
                heduTaskBLL.FinishHeduTask(taskId, pid, pvid, baby, groupId, time, executor);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("完成任务失败");
            }
        }
        /// <summary>
        /// 删除健康宣教任务
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pvid"></param>
        /// <param name="baby"></param>
        /// <param name="eventObjectId"></param>
        /// <param name="taskEventCode"></param>
        /// <returns></returns>
        [HttpGet("DeleteHeduTask")]
        [DisplayName(name = "删除健康宣教任务")]
        [Note(name = "删除健康宣教任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DeleteHeduTask(string pid, string pvid, decimal baby, string eventObjectId, string taskEventCode)
        {
            try
            {
                HeduTaskBLL heduTaskBLL = new HeduTaskBLL();
                heduTaskBLL.DeleteHeduTask(pid, pvid, baby, eventObjectId, taskEventCode);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("删除任务失败");
            }
        }

        /// <summary>
        /// 获取医嘱发送产生的宣教任务
        /// </summary>
        /// <param name="itemId">诊疗项目ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="type">事件类型，e-执行,c-校对</param>
        /// <returns></returns>
        [HttpGet("GetOrderHeduTask")]
        [DisplayName(name = "获取医嘱发送需要产生的宣教任务")]
        [Note(name = "获取医嘱发送需要产生的宣教任务")]
        [ParaOutName(name = "HeduTaskGroup")]
        [SchemaContent(name = "HeduTaskGroup")]
        public IActionResult GetOrderHeduTask(string itemId, string wardId, string type)
        {
            try
            {
                HeduTaskBLL heduTaskBLL = new HeduTaskBLL();
                List<HeduTaskGroup> list = heduTaskBLL.GetOrderHeduTask(itemId, wardId, type);
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 填写表单后产生宣教任务
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddFormHeduTask")]
        [DisplayName(name = "填写表单后产生宣教任务")]
        [Note(name = "填写表单后产生宣教任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddFormHeduTask(HeduFormTask item)
        {
            try
            {
                HeduTaskBLL heduTaskBLL = new HeduTaskBLL();
                heduTaskBLL.AddFormHeduTask(item);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("添加任务失败");
            }
        }

        /// <summary>
        /// 获取入院要产生的宣教任务
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns></returns>
        [HttpGet("GetDeptInHeduTask")]
        [DisplayName(name = "获取入院需要产生的宣教任务")]
        [Note(name = "获取入院需要产生的宣教任务")]
        [ParaOutName(name = "HeduTaskGroup")]
        [SchemaContent(name = "HeduTaskGroup")]
        public IActionResult GetDeptInHeduTask(string wardId)
        {
            try
            {
                HeduTaskBLL heduTaskBLL = new HeduTaskBLL();
                List<HeduTaskGroup> list = heduTaskBLL.GetDeptInHeduTask(wardId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}