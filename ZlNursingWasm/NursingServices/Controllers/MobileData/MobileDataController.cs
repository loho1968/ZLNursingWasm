using Microsoft.AspNetCore.Mvc;
using NursingBLL.MobileData;
using NursingModel;

using System.Collections.Generic;
using NursingBaseFunc;
using ZlNursingCommon;
using ZlNursingServices.Controllers;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 配液管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    public class MobileDataController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<MobileDataController>();
            return Json(servicesStructures);
        }



        /// <summary>
        /// 获取配液情况病人列表
        /// </summary>
        /// <param name="patListIn"></param>
        /// <returns></returns>
        [HttpPost("GetPatList")]
        [DisplayName(name = "获取配液情况病人列表")]
        [Note(name = "获取配液情况病人列表")]
        [ParaOutName(name = "GetPatList")]
        [SchemaContent(name = "CompPatList")]
        public IActionResult GetPatList(CompPatListIn patListIn)
        {
            MobileDataBLL mobileDataBLL = new MobileDataBLL();
            List<CompPatList> patLists = mobileDataBLL.GetCompList(patListIn);
            return Json(patLists);

        }

        /// <summary>
        /// 获取待配液数据
        /// </summary>
        /// <param name="unCmpdIn"></param>
        /// <returns></returns>
        [HttpPost("GetUnCmpdData")]
        [DisplayName(name = "获取待配液数据")]
        [Note(name = "获取待配液数据")]
        [ParaOutName(name = "GetUnCmpdData")]
        [SchemaContent(name = "UnCmpd")]
        public IActionResult GetUnCmpdData(UnCmpdIn unCmpdIn)
        {
            MobileDataBLL mobileDataBLL = new MobileDataBLL();
            List<UnCmpd> unCmpds = mobileDataBLL.GetUnCmpdData(unCmpdIn);
            return Json(unCmpds);
        }

        /// <summary>
        /// 获取已配液数据
        /// </summary>
        ///  <param name="unCmpdIn"></param>

        /// <returns></returns>
        [HttpPost("GetCmpdedData")]
        [DisplayName(name = "获取已配液数据")]
        [Note(name = "获取已配液数据")]
        [ParaOutName(name = "GetCmpdedData")]
        [SchemaContent(name = "Cmpded")]
        public IActionResult GetCmpdedData(UnCmpdIn unCmpdIn)
        {
            MobileDataBLL mobileDataBLL = new MobileDataBLL();
            List<Cmpded> cmpdeds = mobileDataBLL.GetCmpdedData(unCmpdIn);
            return Json(cmpdeds);

        }

        /// <summary>
        /// 获取输液巡视病人列表
        /// </summary>
        ///  <param name="oiiPtlIn"></param>
        /// <returns></returns>
        [HttpPost("GetOiiPtlPat")]
        [DisplayName(name = "获取输液巡视病人列表")]
        [Note(name = "获取输液巡视病人列表")]
        [ParaOutName(name = "GetOiiPtlPat")]
        [SchemaContent(name = "Oii")]
        public IActionResult GetOiiPtlPat(OiiPtlIn oiiPtlIn)
        {
            MobileDataBLL mobileDataBLL = new MobileDataBLL();
            List<Oii> oiiPats = mobileDataBLL.GetOiiPtlPat(oiiPtlIn);
            return Json(oiiPats);

        }

        /// <summary>
        /// 获取病人输液巡视详情
        /// </summary>
        ///  <param name="oiiPtlIn"></param>
        /// <returns></returns>
        [HttpPost("GetOiiInfo")]
        [DisplayName(name = "获取病人输液巡视详情")]
        [Note(name = "获取病人输液巡视详情")]
        [ParaOutName(name = "GetOiiInfo")]
        [SchemaContent(name = "OiiInfo")]
        public IActionResult GetOiiInfo(OiiPtlIn oiiPtlIn)
        {
            MobileDataBLL mobileDataBLL = new MobileDataBLL();
            List<OiiInfo> oiiInfos = mobileDataBLL.GetOiiInfo(oiiPtlIn);
            return Ok(oiiInfos);

        }
        /// <summary>
        /// 获取病人护理巡视详情
        /// </summary>
        ///  <param name="oiiPtlIn"></param>
        /// <returns></returns>
        [HttpPost("GetCarePtrl")]
        [DisplayName(name = "获取病人护理巡视详情")]
        [Note(name = "获取病人护理巡视详情")]
        [ParaOutName(name = "GetCarePtrl")]
        [SchemaContent(name = "CarePtrl")]
        public IActionResult GetCarePtrl(OiiPtlIn oiiPtlIn)
        {
            MobileDataBLL mobileDataBLL = new MobileDataBLL();
            List<CarePtrl> carePtrls = mobileDataBLL.GetCarePtrl(oiiPtlIn);
            return Ok(carePtrls);

        }
        /// <summary>
        /// 获取输液状态详情
        /// </summary>
        ///  <param name="ward"></param>
        /// <returns></returns>
        [HttpGet("GetPtrlStatus")]
        [DisplayName(name = "获取输液状态详情")]
        [Note(name = "获取输液状态详情")]
        [ParaOutName(name = "GetPtrlStatus")]
        [SchemaContent(name = "OiiStatus")]
        public IActionResult GetPtrlStatus(int ward)
        {
            MobileDataBLL mobileDataBLL = new MobileDataBLL();
            List<OiiStatus> carePtrls = mobileDataBLL.GetPtrlStatus(ward);
            return Ok(carePtrls);
        }

        /// <summary>
        /// 获得病人预交款清单
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pvid"></param>
        /// <returns></returns>
        [HttpGet("GetPatAcceptingAmountList")]
        [DisplayName(name = "获得病人预交款清单")]
        [Note(name = "获得病人预交款清单")]
        [ParaOutName(name = "PatFeeList")]
        [SchemaContent(name = "PatFeeList")]
        public IActionResult GetPatAcceptingAmountList(long pid, int pvid)
        {
            MobileDataBLL mobileDataBLL = new MobileDataBLL();
            List<PatFeeList> patFeeList = mobileDataBLL.GetPatAcceptingAmountList(pid,pvid);
            return Ok(patFeeList);
        }
    }
}
