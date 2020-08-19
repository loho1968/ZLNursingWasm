using Microsoft.AspNetCore.Mvc;
using NursingBaseFunc;
using NursingBLL;
using NursingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZlNursingCommon;
using ZlNursingServices.Controllers;


namespace NursingServices.Controllers
{   /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    public class InfusionFeeController : ApiController
    {   /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<InfusionFeeController>();
            return Json(servicesStructures);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCitemItem")]
        [DisplayName(name = "获取诊疗项目目录")]
        [Note(name = "获取诊疗项目目录")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "CitemItem")]
        public List<CitemItem> GetCitemItem()
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetCitemItem();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetFitemItem")]
        [DisplayName(name = "获取收费项目目录")]
        [Note(name = "获取收费项目目录")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "FitemItem")]
        public List<FitemItem> GetFitemItem()
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetFitemItem();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infusionitem"></param>
        [HttpPost("SaveInfusionItem")]
        [DisplayName(name = "保存基本项目到nurse")]
        [Note(name = "保存基本项目到nurse")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void SaveInfusionItem([FromBody] List<SaveBaseItem> infusionitem)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.SaveInfusionItem(infusionitem);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetItemList")]
        [DisplayName(name = "获取已有的基本项目")]
        [Note(name = "获取已有的基本项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "BaseItem")]
        public List<BaseItem> GetItemList()
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetItemList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("InfusionBaseSet")]
        [DisplayName(name = "设置输液基础项目")]
        [Note(name = "设置输液基础项目")]
        [ParaOutName(name = "InfusionBaseSet")]
        [SchemaContent(name = "BaseItem")]
        public List<BaseItem> InfusionBaseSet(string CitemIdIn)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.InfusionBaseSet(CitemIdIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseitemlist"></param>
        [HttpPost("UpdBaseItem")]
        [DisplayName(name = "修改基础项目")]
        [Note(name = "修改基础项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void UpdBaseItem(List<SaveBaseItem> baseitemlist)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.UpdBaseItem(baseitemlist);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WardId"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        [HttpGet("GetPatOrderInfo")]
        [DisplayName(name = "获取可计费的项目")]
        [Note(name = "获取可计费的项目")]
        [ParaOutName(name = "GetPatOrderInfo")]
        [SchemaContent(name = "PatOrderInfo")]
        public List<PatOrderInfo> GetPatOrderInfo(string WardId, DateTime BeginTime, DateTime EndTime)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetPatOrderInfo(WardId, BeginTime, EndTime);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="savefeelist"></param>
        [HttpPost("SaveFeeRec")]
        [DisplayName(name = "保存输液计费项目")]
        [Note(name = "保存输液计费项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void SaveFeeRec([FromBody] List<SaveFeeList> savefeelist)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.SaveFeeRec(savefeelist);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delfeeinfo"></param>
        [HttpPost("DelFeeInfo")]
        [DisplayName(name = "删除已有计价")]
        [Note(name = "删除已有计价")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void DelFeeInfo([FromBody] List<RemoveFeeInfo> delfeeinfo)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.DelFeeInfo(delfeeinfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MtrlId"></param>
        /// <param name="WardId"></param>
        /// <returns></returns>
        [HttpGet("GetInfusionMtrlInfo")]
        [DisplayName(name = "获取基础材料项目")]
        [Note(name = "获取基础材料项目")]
        [ParaOutName(name = "GetInfusionMtrlInfo")]
        [SchemaContent(name = "MtrlInfo")]
        public List<MtrlInfo> GetInfusionMtrlInfo(string MtrlId,string WardId)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetInfusionMtrlInfo(MtrlId, WardId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMtrlCitem")]
        [DisplayName(name = "获取输液材料诊疗项目")]
        [Note(name = "获取输液材料诊疗项目")]
        [ParaOutName(name = "GetMtrlCitem")]
        [SchemaContent(name = "MtrlCitemItem")]
        public List<MtrlCitemItem> GetMtrlCitem()
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetMtrlCitem();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInfusionMtrlList")]
        [DisplayName(name = "获取已有的输液项目材料")]
        [Note(name = "获取已有的输液项目材料")]
        [ParaOutName(name = "GetInfusionMtrlList")]
        [SchemaContent(name = "MtrlInfo")]
        public List<MtrlInfo> GetInfusionMtrlList()
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetInfusionMtrlList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MtrlList"></param>
        [HttpPost("SaveInfusionMtrlInfo")]
        [DisplayName(name = "保存设置的输液材料费")]
        [Note(name = "保存设置的输液材料费")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void SaveInfusionMtrlInfo([FromBody] List<MtrlInfo> MtrlList)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.SaveInfusionMtrlInfo(MtrlList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mtrllist"></param>
        [HttpPost("UpdInfusionMtrlInfo")]
        [DisplayName(name = "修改设置的输液材料费")]
        [Note(name = "修改设置的输液材料费")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void UpdInfusionMtrlInfo(List<MtrlInfoNew> mtrllist)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.UpdInfusionMtrlInfo(mtrllist);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeIn"></param>
        /// <param name="WardId"></param>
        /// <param name="FitemIdIn"></param>
        /// <returns></returns>
        [HttpGet("GetInfusionMtrlFitemSet")]
        [DisplayName(name = "获取可以设置的基础材料项目")]
        [Note(name = "获取可以设置的基础材料项目")]
        [ParaOutName(name = "GetInfusionMtrlFitemSet")]
        [SchemaContent(name = "FitemListMtrl")]
        public List<FitemListMtrl> GetInfusionMtrlFitemSet(string TypeIn, string WardId, string FitemIdIn,string CitemIdIn)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetInfusionMtrlFitemSet(TypeIn,WardId, FitemIdIn, CitemIdIn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patmtrlinfo"></param>
        [HttpPost("SavePatInfusionMtrlFee")]
        [DisplayName(name = "保存病人配置的输液材料费")]
        [Note(name = "保存病人配置的输液材料费")]
        [ParaOutName(name = "SavePatInfusionMtrlFee")]
        [SchemaContent(name = "")]
        public void SavePatInfusionMtrlFee(List<PatMtrlInfo> patmtrlinfo)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.SavePatInfusionMtrlFee(patmtrlinfo);
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
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.DelMtrlFee(Pid,MrId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CitemIdIn"></param>
        /// <param name="MtrlIdIn"></param>
        /// <param name="WardIdIn"></param>
        [HttpGet("DelMtrlInfo")]
        [DisplayName(name = "删除配置好的材料费")]
        [Note(name = "删除配置好的材料费")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void DelMtrlInfo(string CitemIdIn, string MtrlIdIn, string WardIdIn)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.DelMtrlInfo(CitemIdIn, MtrlIdIn, WardIdIn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IvItemIdIn"></param>
        [HttpGet("DelInfusionBaseItem")]
        [DisplayName(name = "删除配置好的输液基础项目")]
        [Note(name = "删除配置好的输液基础项目")]
        [ParaOutName(name = "")]
        [SchemaContent(name = "")]
        public void DelInfusionBaseItem(string IvItemIdIn)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            infusionFeeBll.DelInfusionBaseItem(IvItemIdIn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CitemId"></param>
        /// <returns></returns>
        [HttpGet("GetDaysMaxFee")]
        [DisplayName(name = "获取输液最大金额")]
        [Note(name = "获取输液最大金额")]
        [ParaOutName(name = "GetDaysMaxFee")]
        [SchemaContent(name = "CbhItemList")]
        public List<CbhItemList> GetDaysMaxFee(string CitemId)
        {
            InfusionFeeBll infusionFeeBll = new InfusionFeeBll();
            return infusionFeeBll.GetDaysMaxFee(CitemId);
        }
        }
}
