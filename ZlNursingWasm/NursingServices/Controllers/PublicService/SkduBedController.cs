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
    /// 护士床位
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    [Route("pda/[controller]")]
    public class SkduBedController : ApiController
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
        #region 床位责护
        /// <summary>
        /// 获取床位责护，根据病区id，查询时间
        /// </summary>
        /// <param name="timeNow"></param>
        /// <param name="wardId"></param>
        /// <returns></returns>
        [HttpGet("GetWardBedPnurs")]
        [DisplayName(name = "GetWardBedPnurs")]
        [Note(name = "获取床位责护，根据病区id，查询时间")]
        [ParaOutName(name = "BedPnurs")]
        [SchemaContent(name = "BedPnurs")]
        public IActionResult GetWardBedPnurs(DateTime timeNow, long wardId)
        {
            SkudBedBLL skduBed = new SkudBedBLL();
            return Ok(skduBed.GetWardBedPnursBLL(timeNow, wardId));
        }

        /// <summary>
        /// 获取床位责护，根据病区id，查询时间，床位
        /// </summary>
        /// <param name="timeNow"></param>
        /// <param name="wardId"></param>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        [HttpGet("GetSingerWardBedPnurs")]
        [DisplayName(name = "GetSingerWardBedPnurs")]
        [Note(name = "获取床位责护，根据病区id，查询时间，床位")]
        [ParaOutName(name = "BedPnurs")]
        [SchemaContent(name = "BedPnurs")]
        public IActionResult GetSingerWardBedPnurs(DateTime timeNow, long wardId,string bedCode)
        {
            SkudBedBLL skduBed = new SkudBedBLL();
            return Ok(skduBed.GetWardBedPnursBLL(timeNow, wardId, bedCode));
        }

        #endregion

        #region 护士床位分配
        /// <summary>
        /// 获取护士床位分配（根据护士id，病区id）
        /// </summary>
        /// <param name="nurseId"></param>
        /// <param name="wardId"></param>
        /// <returns></returns>
        [HttpGet("GetNurseBedList")]
        [DisplayName(name = "GetNurseBedList")]
        [Note(name = "获取护士床位分配（根据护士id，病区id）")]
        [ParaOutName(name = "NurseBed")]
        [SchemaContent(name = "NurseBed")]
        public IActionResult GetNurseBedList(string nurseId, long wardId)
        {
            SkudBedBLL skduBed = new SkudBedBLL();
            return Ok(skduBed.GetNurseBedListBLL(nurseId, wardId));
        }

        /// <summary>
        /// 获取护士床位分配（根据人员id，病区id）
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="wardId"></param>
        /// <returns></returns>
        [HttpGet("GetNurseBedListByStaffId")]
        [DisplayName(name = "GetNurseBedListByStaffId")]
        [Note(name = "获取护士床位分配（根据人员id，病区id）")]
        [ParaOutName(name = "NurseBed")]
        [SchemaContent(name = "NurseBed")]
        public IActionResult GetNurseBedListByStaffId(long staffId, long wardId)
        {
            SkudBedBLL skduBed = new SkudBedBLL();
            return Ok(skduBed.GetNurseBedListBLL(staffId, wardId));
        }
        #endregion





    }
}
