using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NursingBLL;
using NursingCommon;
using NursingModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NursingBaseFunc;
using ZlNursingCommon;
using ZlNursingServices.Controllers;


namespace NursingServices.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    public class GetDisposeController : ApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<GetDisposeController>();
            return Json(servicesStructures);
        }

        [HttpGet("GetFeeList")]
        [DisplayName(name = "获取病人持续计费清单")]
        [Note(name = "获取病人持续计费清单")]
        [ParaOutName(name = "CbhItemList")]
        [SchemaContent(name = "CbhItemList")]
        public IActionResult GetFeeList(string CbhItmIdIn)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            List<CbhItemList> CbhItemList = feeSustainabilityBLL.GetFeeList(CbhItmIdIn);
            return Json(CbhItemList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCitemList")]
        [DisplayName(name = "获取持续性诊疗项目")]
        [Note(name = "获取持续性诊疗项目")]
        [ParaOutName(name = "GetCitemList")]
        [SchemaContent(name = "CitemList")]
        public IActionResult GetCitemList()
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            List<CitemList> CitemList = feeSustainabilityBLL.GetCitemList();
            return Json(CitemList);
        }

        [HttpGet("GetFitemList")]
        [DisplayName(name = "获取收费项目目录")]
        [Note(name = "获取收费项目目录")]
        [ParaOutName(name = "GetFitemList")]
        [SchemaContent(name = "FitemList")]
        public IActionResult GetFitemList()
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            List<FitemList> fitemlist = feeSustainabilityBLL.GetFitemList();
            return Json(fitemlist);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbhitemlist"></param>
        [HttpPost("SaveFitemList")]
        [DisplayName(name = "保存收费项目设置")]
        [Note(name = "保存收费项目设置")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void SaveFitemList([FromBody] List<CbhItemList> cbhitemlist)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.SaveFitemList(cbhitemlist);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBaseDispose")]
        [DisplayName(name = "获取已有的基础配置")]
        [Note(name = "获取已有的基础配置")]
        [ParaOutName(name = "CbhItemList")]
        [SchemaContent(name = "CbhItemList")]
        public List<CbhItemList> GetBaseDispose()
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetBaseDispose();
        }

        [HttpPost("UpdBaseItem")]
        [DisplayName(name = "修改基础配置")]
        [Note(name = "修改基础配置")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void UpdBaseItem([FromBody] List<CbhItemList> cbhitemlist)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.UpdBaseItem(cbhitemlist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WardIdIn"></param>
        /// <returns></returns>
        [HttpGet("GetPatList")]
        [DisplayName(name = "获取符合项目的病人")]
        [Note(name = "获取符合项目的病人")]
        [ParaOutName(name = "GetPatList")]
        [SchemaContent(name = "OrderPatList")]
        public List<OrderPatList> GetPatList(string WardIdIn)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetPatList(WardIdIn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WardId"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        [HttpGet("GetPatFeeItemList")]
        [DisplayName(name = "获取该病人的项目")]
        [Note(name = "获取该病人的项目")]
        [ParaOutName(name = "GetPatFeeItemList")]
        [SchemaContent(name = "PatFeeItem")]
        public List<PatFeeItem> GetPatFeeItemList(string WardId, string BeginTime, string EndTime)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetPatFeeItemList(WardId, BeginTime, EndTime);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="savefeelist"></param>
        [HttpPost("SaveFeeRec")]
        [DisplayName(name = "保存收费项目")]
        [Note(name = "保存收费项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void SaveFeeRec([FromBody] List<SaveFeeList> savefeelist)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.SaveFeeRec(savefeelist);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delfeeinfo"></param>
        [HttpPost("DelFeeInfo")]
        [DisplayName(name = "销账")]
        [Note(name = "销账")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void DelFeeInfo([FromBody] List<RemoveFeeInfo> delfeeinfo)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.DelFeeInfo(delfeeinfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MtrlId"></param>
        /// <param name="WardId"></param>
        /// <returns></returns>
        [HttpGet("GetMtrlInfo")]
        [DisplayName(name = "获取材料费配置")]
        [Note(name = "获取材料费配置")]
        [ParaOutName(name = "GetMtrlInfo")]
        [SchemaContent(name = "MtrlInfo")]
        public List<MtrlInfo> GetMtrlInfo(string MtrlId,string WardId)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetMtrlInfo(MtrlId, WardId);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMtrlFitem")]
        [DisplayName(name = "获取材料收费项目")]
        [Note(name = "获取材料收费项目")]
        [ParaOutName(name = "GetMtrlFitem")]
        [SchemaContent(name = "FitemList")]
        public List<FitemList> GetMtrlFitem()
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetMtrlFitem();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MtrlList"></param>
        [HttpPost("SaveMtrlInfo")]
        [DisplayName(name = "保存设置材料项目")]
        [Note(name = "保存设置材料项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void SaveMtrlInfo([FromBody] List<MtrlInfo> MtrlList)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.SaveMtrlInfo(MtrlList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMtrlList")]
        [DisplayName(name = "获取已有的材料项目")]
        [Note(name = "获取已有的材料项目")]
        [ParaOutName(name = "GetMtrlList")]
        [SchemaContent(name = "MtrlInfo")]
        public List<MtrlInfo> GetMtrlList()
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetMtrlList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mtrllist"></param>
        [HttpPost("UpdMtrlInfo")]
        [DisplayName(name = "修改已有的项目")]
        [Note(name = "修改已有的项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void UpdMtrlInfo([FromBody] List<MtrlInfoNew> mtrllist)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.UpdMtrlInfo(mtrllist);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeIn"></param>
        /// <param name="WardId"></param>
        /// <param name="FitemIdIn"></param>
        /// <returns></returns>
        [HttpGet("GetMtrlFitemSet")]
        [DisplayName(name = "获取可以配置的收费项目")]
        [Note(name = "获取可以配置的收费项目")]
        [ParaOutName(name = "GetMtrlFitemSet")]
        [SchemaContent(name = "FitemListMtrl")]
        public List<FitemListMtrl> GetMtrlFitemSet(string TypeIn, string WardId, string FitemIdIn,string CitemIdIn)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetMtrlFitemSet(TypeIn, WardId, FitemIdIn, CitemIdIn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patmtrlinfo"></param>
        [HttpPost("SavePatMtrlFee")]
        [DisplayName(name = "保存病人配置材料费")]
        [Note(name = "保存病人配置材料费")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void SavePatMtrlFee([FromBody] List<PatMtrlInfo> patmtrlinfo)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.SavePatMtrlFee(patmtrlinfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pid"></param>
        /// <param name="MrId"></param>
        [HttpGet("DelMtrlFee")]
        [DisplayName(name = "删除病人材料费")]
        [Note(name = "删除病人材料费")]
        [ParaOutName(name = "DelMtrlFee")]
        [SchemaContent(name = "")]
        public void DelMtrlFee(string Pid, string MrId)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.DelMtrlFee(Pid,MrId);
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CitemIdIn"></param>
        /// <param name="MtrlIdIn"></param>
        /// <param name="WardIdIn"></param>
        [HttpGet("DelMtrlInfo")]
        [DisplayName(name = "删除配置好的材料项目")]
        [Note(name = "删除配置好的材料项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void DelMtrlInfo(string CitemIdIn, string MtrlIdIn, string WardIdIn)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.DelMtrlInfo(CitemIdIn, MtrlIdIn, WardIdIn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CbhItemIdIn"></param>
        [HttpGet("DelBaseItem")]
        [DisplayName(name = "删除配置好的基础项目")]
        [Note(name = "删除配置好的基础项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void DelBaseItem(string CbhItemIdIn)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            feeSustainabilityBLL.DelBaseItem(CbhItemIdIn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpGet("GetSendQunt")]
        [DisplayName(name = "获取医嘱是否已经发送")]
        [Note(name = "获取医嘱是否已经发送")]
        [ParaOutName(name = "sendqunt")]
        [SchemaContent(name = "")]
        public int GetSendQunt(string OrderId)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetSendQunt(OrderId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CitemId"></param>
        /// <returns></returns>
        [HttpGet("GetDaysMaxFee")]
        [DisplayName(name = "获得最大金额")]
        [Note(name = "获得最大金额")]
        [ParaOutName(name = "GetDaysMaxFee")]
        [SchemaContent(name = "CbhItemList")]
        public List<CbhItemList> GetDaysMaxFee(string CitemId)
        {
            FeeSustainabilityBLL feeSustainabilityBLL = new FeeSustainabilityBLL();
            return feeSustainabilityBLL.GetDaysMaxFee(CitemId);
        }

        }
}
