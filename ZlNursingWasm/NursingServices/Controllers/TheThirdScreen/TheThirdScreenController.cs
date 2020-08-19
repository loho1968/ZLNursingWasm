using Microsoft.AspNetCore.Mvc;
using NursingBaseFunc;
using NursingBLL;
using NursingModel;
using NursingCommon;using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZlNursingCommon;
using ZlNursingServices.Controllers;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 第三屏展示配置
    /// 编写：王乔友
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    public class TheThirdScreenController:ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<TheThirdScreenController>();
            return Json(servicesStructures);
        }

        /// <summary>
        /// 新增医嘱执行提醒配置
        /// </summary>
        /// <param name="orderExecuteRemind"></param>
        /// <returns></returns>
        [HttpPost("OrderExecuteRemindSet")]
        [DisplayName(name = "新增医嘱执行提醒配置")]
        [Note(name = "新增医嘱执行提醒配置")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult OrderExecuteRemindSet(OrderExecuteRemind orderExecuteRemind)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            theThirdScreenBLL.OrderExecuteRemindSet(orderExecuteRemind);
            return Ok();
        }

        /// <summary>
        /// 修改医嘱执行提醒配置
        /// </summary>
        /// <param name="orderExecuteRemind"></param>
        /// <returns></returns>
        [HttpPost("UpdOrderExecuteRemind")]
        [DisplayName(name = "修改医嘱执行提醒配置")]
        [Note(name = "修改医嘱执行提醒配置")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult UpdOrderExecuteRemind(OrderExecuteRemind orderExecuteRemind)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            if (theThirdScreenBLL.UpdOrderExecuteRemind(orderExecuteRemind))
            {
                return Ok();
            }
            else
            {
                return Json(new { Status = 0, Msg = "更新失败" });
            }
        }

        /// <summary>
        /// 删除医嘱执行提醒配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DelOrderExecuteRemind")]
        [DisplayName(name = "删除医嘱执行提醒配置")]
        [Note(name = "删除医嘱执行提醒配置")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelOrderExecuteRemind(string id)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            if (theThirdScreenBLL.DelOrderExecuteRemind(id))
            {
                return Ok();
            }
            else
            {
                return Json(new { Status = 0, Msg = "删除失败" });
            }
        }

        /// <summary>
        /// 获得所有医嘱执行提醒配置列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAllOrderExecuteRemindSetList")]
        [DisplayName(name = "获得所有医嘱执行提醒配置列表")]
        [Note(name = "获得所有医嘱执行提醒配置列表")]
        [ParaOutName(name = "OrderExecuteRemind")]
        [SchemaContent(name = "OrderExecuteRemind")]
        public IActionResult GetAllOrderExecuteRemindSetList()
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            return Ok(theThirdScreenBLL.GetAllOrderExecuteRemindSetList());
        }
        /// <summary>
        /// 传入id，分组名称，诊疗项目id判断是否存在诊疗项目
        /// </summary>
        [HttpGet("GetOrderRemingCount")]
        [DisplayName(name = "传入id，分组名称，诊疗项目id判断是否存在诊疗项目")]
        [Note(name = "传入id，分组名称，诊疗项目id判断是否存在诊疗项目")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetOrderRemingCount(string id, string groupName, string OrderItemId)
        {

            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            return Ok(theThirdScreenBLL.GetOrderRemingCount(id, groupName, OrderItemId));
        }

        /// <summary>
        /// 获得单条医嘱执行提醒配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSingleOrderExecuteRemindSetById")]
        [DisplayName(name = "获得单条医嘱执行提醒配置")]
        [Note(name = "获得单条医嘱执行提醒配置")]
        [ParaOutName(name = "OrderExecuteRemind")]
        [SchemaContent(name = "OrderExecuteRemind")]
        public IActionResult GetSingleOrderExecuteRemindSetById(string Id)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            return Ok(theThirdScreenBLL.GetSingleOrderExecuteRemindSetById(Id));
        }
        /// <summary>
        ///  删除病区医嘱执行提醒配置
        /// </summary>
        [HttpGet("DeleteWardOrderExecuteRemind")]
        [DisplayName(name = "删除病区医嘱执行提醒配置")]
        [Note(name = "删除病区医嘱执行提醒配置")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DeleteWardOrderExecuteRemind(string id)
        {
            TheThirdScreenBLL theThirdScreenDAL = new TheThirdScreenBLL();
            return Ok(theThirdScreenDAL.DeleteWardOrderExecuteRemind(id));
        }
        /// <summary>
        /// 获得诊疗项目列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDiagnosisAndTreatmentList")]
        [DisplayName(name = "医嘱执行提醒配置获得诊疗项目列表")]
        [Note(name = "医嘱执行提醒配置获得诊疗项目列表")]
        [ParaOutName(name = "DiagnosisAndTreatmentItemList")]
        [SchemaContent(name = "DiagnosisAndTreatmentItemList")]
        public IActionResult GetDiagnosisAndTreatmentList()
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();           
            return Ok(theThirdScreenBLL.GetDiagnosisAndTreatmentList());
        }

        /// <summary>
        /// 传入名称，获得诊疗项目
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDiagnosisAndTreatmentItem")]
        [DisplayName(name = "传入名称，获得诊疗项目")]
        [Note(name = "传入名称，获得诊疗项目")]
        [ParaOutName(name = "DiagnosisAndTreatmentItemList")]
        [SchemaContent(name = "DiagnosisAndTreatmentItemList")]
        public IActionResult GetDiagnosisAndTreatmentItem(string name)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            return Ok(theThirdScreenBLL.GetDiagnosisAndTreatmentItem(name));
        }

        /// <summary>
        /// 根据分组序号获得医嘱执行提醒配置列表总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCountByNo")]
        [DisplayName(name = "根据分组序号获得医嘱执行提醒配置列表总数")]
        [Note(name = "根据分组序号获得医嘱执行提醒配置列表总数")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetCountByNo(string id, int No)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            return Ok(theThirdScreenBLL.GetAllListByNo(id,No));
        }
        /// <summary>
        /// 根据分组名称获得医嘱执行提醒配置列表总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCountByName")]
        [DisplayName(name = "根据分组名称获得医嘱执行提醒配置列表总数")]
        [Note(name = "根据分组名称获得医嘱执行提醒配置列表总数")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetCountByName(string id,string name)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            return Ok(theThirdScreenBLL.GetAllListByName(id,name));
        }

        /// <summary>
        /// 新增病区医嘱执行提醒配置
        /// </summary>
        /// <param name="wardOrderExecuteRemind"></param>
        /// <returns></returns>
        [HttpPost("WardOrderExecuteRemindSet")]
        [DisplayName(name = "新增病区医嘱执行提醒配置")]
        [Note(name = "新增病区医嘱执行提醒配置")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult WardOrderExecuteRemindSet(WardOrderExecuteRemind wardOrderExecuteRemind)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            theThirdScreenBLL.WardOrderExecuteRemindSet(wardOrderExecuteRemind);
            return Ok();
        }

        /// <summary>
        /// 修改病区医嘱执行提醒配置
        /// </summary>
        /// <param name="wardOrderExecuteRemind"></param>
        /// <returns></returns>
        [HttpPost("UpdWardOrderExecuteRemind")]
        [DisplayName(name = "修改病区医嘱执行提醒配置")]
        [Note(name = "修改病区医嘱执行提醒配置")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult UpdWardOrderExecuteRemind(WardOrderExecuteRemind wardOrderExecuteRemind)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            if (theThirdScreenBLL.UpdWardOrderExecuteRemind(wardOrderExecuteRemind))
            {
                return Ok();
            }
            else
            {
                return Json(new { Status = 0, Msg = "更新失败" });
            }
        }

        /// <summary>
        /// 根据医嘱执行提醒配置ID删除关联信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DelWardOrderExecuteRemindById")]
        [DisplayName(name = "根据医嘱执行提醒配置ID删除关联信息")]
        [Note(name = "根据医嘱执行提醒配置ID删除关联信息")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelWardOrderExecuteRemindById(string Id)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            if (theThirdScreenBLL.DelWardOrderExecuteRemindById(Id))
            {
                return Ok();
            }
            else
            {
                return Json(new { Status = 0, Msg = "更新失败" });
            }
        }
        /// <summary>
        /// 根据医嘱执行提醒配置ID获得病区信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetWardOrderExecuteRemindByOrderExecuteRemind")]
        [DisplayName(name = "根据医嘱执行提醒配置ID获得病区信息")]
        [Note(name = "根据医嘱执行提醒配置ID获得病区信息")]
        [ParaOutName(name = "WardOrderExecuteRemindAndWardName")]
        [SchemaContent(name = "WardOrderExecuteRemindAndWardName")]
        public IActionResult GetWardOrderExecuteRemindByOrderExecuteRemind(string id)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            return Ok(theThirdScreenBLL.GetWardOrderExecuteRemindByOrderExecuteRemind(id));
        }
        /// <summary>
        /// 获得电子白板信息
        /// </summary>
        /// <param name="wardId">病区id</param>
        /// <param name="nurseId">护士id</param>
        /// <returns></returns>
        [HttpGet("GetMainInfo")]
        public IActionResult GetMainInfo(long wardId,string nurseId)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            return Ok(theThirdScreenBLL.GetMainInfo(wardId,nurseId));
        }
        /// <summary>
        /// 获取电子白板信息
        /// </summary>
        /// <param name="wardId">病区id</param>
        /// <param name="nurseId">护士id</param>
        /// <returns></returns>
        [HttpGet("GetElectronicWhiteBoard")]
        [DisplayName(name = "获取电子白板信息")]
        [Note(name = "获取电子白板信息")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetElectronicWhiteBoard(string wardId, string nurseId)
        {
            TheThirdScreenBLL theThirdScreenBLL = new TheThirdScreenBLL();
            var electronicWhiteBoard = theThirdScreenBLL.ElectronicWhiteBoard(wardId, nurseId);
            return Ok(electronicWhiteBoard);
        }
    }
}
