using NursingCommon;using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NursingBLL;
using NursingBaseFunc;
using ZlNursingCommon;
using ZlNursingServices.Controllers;
using NursingModel;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 输血执行
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    [Route("pda/[controller]")]
    public class BloodExecuteController : ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<BloodExecuteController>();
            return Json(servicesStructures);
        }
        #region 血液检查项目

        /// <summary>
        /// 获取血液检查项目值域（根据项目名称）
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        [HttpGet("GetBloodCheckItemRange")]
        [DisplayName(name = "GetBloodCheckItemRange")]
        [Note(name = "获取血液检查项目值域（根据项目名称）")]
        [ParaOutName(name = "BloodCheckItemRange")]
        [SchemaContent(name = "BloodCheckItemRange")]
        public IActionResult GetBloodCheckItemRange(string itemName)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetBloodCheckItemRangeBLL(itemName));
        }

        /// <summary>
        /// 获取血液检查项目值域（根据项目名称）
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        [HttpGet("GetBloodCheckItemRangePDA")]
        public IActionResult GetBloodCheckItemRangePDA(string itemName)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetBloodCheckItemRangePDABLL(itemName));
        }
        #endregion


        #region his信息读取
        /// <summary>
        /// 获取待接收血液信息，根据病区id,时段,pda标识
        /// </summary>
        /// <param name="wardId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="receiveStatus"></param>
        /// <param name="PDASign"></param>
        /// <returns></returns>
        [HttpGet("GetWaitAccpetBlood")]
        [DisplayName(name = "GetWaitAccpetBlood")]
        [Note(name = "获取待接收血液信息，根据病区id,时段")]
        [ParaOutName(name = "WaitAccpetBlood")]
        [SchemaContent(name = "WaitAccpetBlood")]
        public IActionResult GetWaitAccpetBlood(int wardId, string beginDate, string endDate, int receiveStatus, int PDASign)
        {

            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetWaitAccpetBloodBLL(wardId, beginDate, endDate, receiveStatus, PDASign));
        }

        /// <summary>
        /// 获取待接收血液信息，根据血袋号（PDA）
        /// </summary>
        /// <param name="bloodNo"></param><param name="receiveStatus"></param>
        /// <returns></returns>
        [HttpGet("GetWaitAccpetBloodByBloodNo")]
        public IActionResult GetWaitAccpetBloodByBloodNo(string bloodNo, int receiveStatus)
        {

            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetWaitAccpetBloodBLL(bloodNo, receiveStatus));
        }

        /// <summary>
        /// 获取单个病人待接收血液信息，根据病人id,时段
        /// </summary>
        /// <param name="pid"></param><param name="pvid"></param><param name="beginDate"></param><param name="endDate"></param><param name="receiveStatus"></param>
        /// <returns></returns>
        [HttpGet("GetSingerWaitAccpetBlood")]
        public IActionResult GetSingerWaitAccpetBlood(int pid,int pvid, DateTime beginDate, DateTime endDate, int receiveStatus)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetSingerWaitAccpetBloodBLL(pid, pvid, beginDate, endDate, receiveStatus));
        }
        /*
        /// <summary>
        /// 获取单个病人采集类血液医嘱，根据病人id,时段(PDA)
        /// </summary>
        /// <param name="pid"></param><param name="pvid"></param><param name="beginDate"></param><param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("GetCollectBloodOrder")]
        public IActionResult GetCollectBloodOrder(int pid, int pvid, DateTime beginDate, DateTime endDate)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetCollectBloodOrderBLL(pid, pvid, beginDate, endDate));
        }*/

        /// <summary>
        /// 获取输血检验结果
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetBloodTransfusionCheckResults")]
        public IActionResult GetBloodTransfusionCheckResults(long orderId)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetBloodTransfusionCheckResultsBLL(orderId));
        }
        #endregion

        #region 待执行血液
        /// <summary>
        /// 血液接收
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("BloodReceive")]
        [DisplayName(name = "BloodReceive")]
        [Note(name = "血液接收")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult BloodReceive([FromBody]PatBloodReceive item)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            bldexe.BloodReceiveBLL(item);
            return Ok();
        }

        /// <summary>
        /// PAD血液接收
        /// </summary>
        /// <param name="bloodNum"></param>
        /// <param name="checker"></param>
        /// <param name="receiver"></param>
        /// <returns></returns>
        [HttpGet("BloodReceiveByPDA")]
        public IActionResult BloodReceiveByPDA(string bloodNum, string checker, string receiver)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            bldexe.BloodReceiveByPDABLL(bloodNum, checker, receiver);
            return Ok();
        }


        /// <summary>
        /// 撤销血液接收
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("CancelBloodReceive")]
        [DisplayName(name = "CancelBloodReceive")]
        [Note(name = "撤销血液接收")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult CancelBloodReceive([FromBody]WaitAccpetBlood item)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            bldexe.CancelBloodReceiveBLL(item);
            return Ok();
        }

        /// <summary>
        /// 修改待执行血液状态
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pvid"></param>
        /// <param name="wardId"></param>
        /// <param name="bloodNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("UpdatePatBloodWaitStstus")]
        [DisplayName(name = "UpdatePatBloodWaitStstus")]
        [Note(name = "修改待执行血液状态")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public IActionResult UpdatePatBloodWaitStstus(string pid, string pvid, long wardId, string bloodNo, int status)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            bldexe.UpdatePatBloodWaitStstusBLL(pid, pvid, wardId, bloodNo, status);
            return Ok();
        }

        /// <summary>
        /// 获取待执行血液，根据医嘱id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetPatBloodWaitByOrderId")]
        [DisplayName(name = "GetPatBloodWaitByOrderId")]
        [Note(name = "获取待执行血液，根据医嘱id")]
        [ParaOutName(name = "PatBloodWait")]
        [SchemaContent(name = "PatBloodWait")]
        public IActionResult GetPatBloodWaitByOrderId(long orderId)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetPatBloodWaitByOrderIdBLL(orderId));
        }

        /// <summary>
        /// 获取血液列表,根据病区id,时段
        /// </summary>
        /// <param name="wardId"></param><param name="beginDate"></param><param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("GetPatBloodWait")]
        [DisplayName(name = "GetPatBloodWait")]
        [Note(name = "获取血液列表,根据病区id,时段")]
        [ParaOutName(name = "PatBloodWaitDisplay")]
        [SchemaContent(name = "PatBloodWaitDisplay")]
        public IActionResult GetPatBloodWait(int wardId, DateTime beginDate, DateTime endDate)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetPatBloodWaitBLL(wardId, beginDate, endDate));
        }


        /// <summary>
        /// 获取单条血液信息，根据病人信息，血袋号
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pvid"></param>
        /// <param name="wardId"></param>
        /// <param name="bloodNo"></param>
        /// <returns></returns>
        [HttpGet("GetSingerPatBloodWait")]
        [DisplayName(name = "GetSingerPatBloodWait")]
        [Note(name = "获取单条血液信息，根据病人信息，血袋号")]
        [ParaOutName(name = "PatBloodWaitDisplay")]
        [SchemaContent(name = "PatBloodWaitDisplay")]
        public IActionResult GetSingerPatBloodWait(string pid, string pvid, long wardId, string bloodNo)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetPatBloodWaitBLL(pid, pvid, wardId, bloodNo));
        }
        #endregion

        #region 血液执行
        /// <summary>
        /// 新增血液执行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddPatBloodExecute")]
        [DisplayName(name = "AddPatBloodExecute")]
        [Note(name = "新增血液执行")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddPatBloodExecute([FromBody]PatBloodExecuteAll item)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            
            return Ok(bldexe.AddPatBloodExecuteBLL(item));

        }

        /// <summary>
        /// 获取正在输血的血袋数，根据医嘱id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetBloodingCountByOrderId")]
        [DisplayName(name = "GetBloodingCountByOrderId")]
        [Note(name = "获取未结束执行血袋数(包括未接收)，根据医嘱id")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetBloodingCountByOrderId(long orderId)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetBloodingCountByOrderIdBLL(orderId));
        }

        /// <summary>
        /// 修改血液执行记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("UpdatePatBloodExecute")]
        [DisplayName(name = "UpdatePatBloodExecute")]
        [Note(name = "修改血液执行记录")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult UpdatePatBloodExecute([FromBody]PatBloodExecute item)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            bldexe.UpdatePatBloodExecuteBLL(item);
            return Ok();
        }

        /// <summary>
        /// 删除血液执行记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DeletePatBloodExecute")]
        [DisplayName(name = "DeletePatBloodExecute")]
        [Note(name = "删除血液执行记录")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DeletePatBloodExecute(long id)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            bldexe.DeletePatBloodExecuteBLL(id);
            return Ok();
        }

        /// <summary>
        /// 获取单条执行血液记录（根据id）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetSinglePatBloodExecute")]
        [DisplayName(name = "GetSinglePatBloodExecute")]
        [Note(name = "获取单条执行血液记录（根据id）")]
        [ParaOutName(name = "PatBloodExecute")]
        [SchemaContent(name = "PatBloodExecute")]
        public IActionResult GetSinglePatBloodExecute(long id)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetSinglePatBloodExecuteBLL(id));
        }

        /// <summary>
        /// 获取执行血液记录（根据病人，病区，血袋号）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetPatBloodExecute")]
        [DisplayName(name = "GetPatBloodExecute")]
        [Note(name = "获取执行血液记录（根据病人，病区，血袋号）")]
        [ParaOutName(name = "PatBloodExecute")]
        [SchemaContent(name = "PatBloodExecute")]
        public IActionResult GetPatBloodExecute([FromBody]PatBloodExecute input)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetPatBloodExecuteBLL(input));
        }

        /// <summary>
        /// 获取执行血液记录（根据病人，病区，时段）
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pvid"></param>
        /// <param name="wardId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetPatBloodExecuteDisplay")]
        [DisplayName(name = "GetPatBloodExecuteDisplay")]
        [Note(name = "获取执行血液记录（根据病人，病区，时段）")]
        [ParaOutName(name = "PatBloodExecuteDisplay")]
        [SchemaContent(name = "PatBloodExecuteDisplay")]
        public IActionResult GetPatBloodExecuteDisplay(int pid, int pvid, int wardId, DateTime beginDate, DateTime endDate, long orderId = 0)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetPatBloodExecuteDisplayBLL(pid, pvid, wardId, beginDate, endDate, orderId));
        }

        /// <summary>
        /// 获取血液执行开始时间和结束时间
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetPatBloodExecutesSartEndTime")]
        [DisplayName(name = "GetPatBloodExecutesSartEndTime")]
        [Note(name = "获取血液执行开始时间和结束时间")]
        [ParaOutName(name = "PatBloodExecutesStartEndTime")]
        [SchemaContent(name = "PatBloodExecutesStartEndTime")]
        public IActionResult GetPatBloodExecutesSartEndTime([FromBody]PatBloodExecute input)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetPatBloodExecutesSartEndTimeBLL(input));
        }

        /// <summary>
        /// 获取一组血液的开始输血时间（根据医嘱id）
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetBloodExecuteStartTime")]
        [DisplayName(name = "GetBloodExecuteStartTime")]
        [Note(name = "获取一组血液的开始输血时间（根据医嘱id）")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetBloodExecuteStartTime(long orderId)
        {
            BloodExecuteBLL bldexe = new BloodExecuteBLL();
            return Ok(bldexe.GetBloodExecuteStartTimeBLL(orderId));
        }
        #endregion


    }
}
