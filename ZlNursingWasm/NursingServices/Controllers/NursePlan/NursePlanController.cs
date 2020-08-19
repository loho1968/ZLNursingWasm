using Microsoft.AspNetCore.Mvc;
using NursingBLL;
using NursingModel;
using NursingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NursingBaseFunc;
using ZlNursingCommon;
using ZlNursingServices.Controllers;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 护理目标管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    [Route("pda/[controller]")]
    public class NursePlanController : ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<NursePlanController>();
            return Json(servicesStructures);
        }

        #region 护理目标模块

        #region 护理目标领域

        /// <summary>
        /// 获得单条护理目标领域
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNocDomain")]
        [DisplayName(name = "获得单条护理目标领域")]
        [Note(name = "根据编码获得单条护理目标领域")]
        [ParaOutName(name = "NocDomain")]
        [SchemaContent(name = "NocDomain")]
        public IActionResult GetSingleNocDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNocDomainBLL(code));
        }

        /// <summary>
        /// 获得所有护理目标领域
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNocDomain")]
        [DisplayName(name = "获得所有护理目标领域")]
        [Note(name = "获得所有护理目标领域")]
        [ParaOutName(name = "NocDomain")]
        [SchemaContent(name = "NocDomain")]
        public IActionResult GetAllNocDomain(int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNocDomainBLL(isAll));
        }

        /// <summary>
        /// 新增护理目标领域
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNocDomain")]
        [DisplayName(name = "新增护理目标领域")]
        [Note(name = "新增护理目标领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNocDomain(NocDomain item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNocDomainBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理目标领域失败！");
            }
        }

        /// <summary>
        /// 修改护理目标领域
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNocDomain")]
        [DisplayName(name = "修改护理目标领域")]
        [Note(name = "修改护理目标领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNocDomain(NocDomain item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNocDomainBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理目标领域失败！");
            }
        }

        /// <summary>
        /// 作废护理目标领域
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNocDomain")]
        [DisplayName(name = "作废护理目标领域")]
        [Note(name = "根据领域编码作废护理目标领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNocDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNocDomainStatusBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理目标领域失败！");
            }
        }

        /// <summary>
        /// 删除护理目标领域
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNocDomain")]
        [DisplayName(name = "删除护理目标领域")]
        [Note(name = "根据领域编码删除护理目标领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNocDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNocDomainBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理目标领域失败！");
            }
        }

        #endregion

        #region 护理目标类别

        /// <summary>
        /// 新增护理目标类别
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNocClass")]
        [DisplayName(name = "新增护理目标类别")]
        [Note(name = "新增护理目标类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNocClass(NocClass item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNocClassBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理目标类别失败！");
            }
        }

        /// <summary>
        /// 修改护理目标类别
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNocClass")]
        [DisplayName(name = "修改护理目标类别")]
        [Note(name = "修改护理目标类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNocClass(NocClass item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNocClassBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理目标类别失败！");
            }
        }

        /// <summary>
        /// 获得领域下所有护理目标类别
        /// </summary>
        /// <param name="parentCode"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        [HttpGet("GetAllNocClass")]
        [DisplayName(name = "获得领域下所有护理目标类别")]
        [Note(name = "获得领域下所有护理目标类别")]
        [ParaOutName(name = "NocClass")]
        [SchemaContent(name = "NocClass")]
        public IActionResult GetAllNocClass(string parentCode, int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNocClassBLL(parentCode, isAll));
        }

        /// <summary>
        /// 获得单条护理目标类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNocClass")]
        [DisplayName(name = "获得单条护理目标类别")]
        [Note(name = "获得单条护理目标类别")]
        [ParaOutName(name = "NocClass")]
        [SchemaContent(name = "NocClass")]
        public IActionResult GetSingleNocClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNocClassBLL(code));
        }

        /// <summary>
        /// 删除单条护理目标类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNocClass")]
        [DisplayName(name = "删除单条护理目标类别")]
        [Note(name = "根据类别编码删除单条护理目标类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNocClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNocClassBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除失败，请检查该类别下是否存在目标");
            }
        }

        /// <summary>
        /// 作废护理目标类别
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNocClass")]
        [DisplayName(name = "作废护理目标类别")]
        [Note(name = "根据类别编码作废护理目标类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNocClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNocClassStatusBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理目标类别失败");
            }
        }

        #endregion

        #region 护理目标

        /// <summary>
        /// 获得单条护理目标
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNocOutCome")]
        [DisplayName(name = "获得单条护理目标")]
        [Note(name = "根据编码获得单条护理目标")]
        [ParaOutName(name = "NocOutCome")]
        [SchemaContent(name = "NocOutCome")]
        public IActionResult GetSingleNocOutCome(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNocOutComeBLL(code));
        }

        /// <summary>
        /// 新增护理目标
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNocOutCome")]
        [DisplayName(name = "新增护理目标")]
        [Note(name = "新增护理目标")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNocOutCome(NocOutCome item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNocOutComeBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理目标失败");
            }
        }

        /// <summary>
        /// 修改护理目标
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNocOutCome")]
        [DisplayName(name = "修改护理目标")]
        [Note(name = "修改护理目标")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNocOutCome(NocOutCome item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNocOutComeBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理目标失败");
            }
        }

        /// <summary>
        /// 获得所有护理目标
        /// </summary>
        /// <param name="classCode"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        [HttpGet("GetAllNocOutCome")]
        [DisplayName(name = "获得类别下所有护理目标")]
        [Note(name = "获得类别下所有护理目标")]
        [ParaOutName(name = "NocOutCome")]
        [SchemaContent(name = "NocOutCome")]
        public IActionResult GetAllNocOutCome(string classCode, int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNocOutComeBLL(classCode, isAll));
        }

        /// <summary>
        /// 删除护理目标
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNocOutCome")]
        [DisplayName(name = "删除单条护理目标")]
        [Note(name = "根据目标编码删除单条护理目标")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNocOutCome(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DelNocOutComeBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理目标失败");
            }
        }

        /// <summary>
        /// 作废护理目标
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNocOutCome")]
        [DisplayName(name = "作废护理目标")]
        [Note(name = "根据目标编码作废护理目标")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNocOutCome(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNocOutComeStatusBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理目标失败");
            }
        }

        /// <summary>
        /// 根据病人护理计划主ID获取病人护理目标
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNocOutComeByID")]
        [DisplayName(name = "根据病人护理计划主ID获取病人护理目标")]
        [Note(name = "根据病人护理计划主ID获取病人护理目标")]
        [ParaOutName(name = "NocOutComeContent")]
        [SchemaContent(name = "NocOutComeContent")]
        public IActionResult GetNocOutComeByID(long id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetNocOutComeByIdBLL(id));
        }

        #endregion

        #endregion

        #region 护理措施模块

        #region 护理措施领域

        /// <summary>
        /// 新增护理措施领域
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNicDomain")]
        [DisplayName(name = "新增护理措施领域")]
        [Note(name = "新增护理措施领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNicDomain(NicDomain item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNicDomainBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理措施领域失败");
            }
        }

        /// <summary>
        /// 修改护理措施领域
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNicDomain")]
        [DisplayName(name = "修改护理措施领域")]
        [Note(name = "修改护理措施领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNicDomain(NicDomain item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNicDomainBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理措施领域失败");
            }
        }

        /// <summary>
        /// 获得单条护理措施领域
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNicDomain")]
        [DisplayName(name = "获得单条护理措施领域")]
        [Note(name = "根据编码获得单条护理措施领域")]
        [ParaOutName(name = "NicDomain")]
        [SchemaContent(name = "NicDomain")]
        public IActionResult GetSingleNicDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNicDomainBLL(code));
        }

        /// <summary>
        /// 获得所有护理措施领域
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNicDomain")]
        [DisplayName(name = "获得所有护理措施领域")]
        [Note(name = "获得所有护理措施领域")]
        [ParaOutName(name = "NicDomain")]
        [SchemaContent(name = "NicDomain")]
        public IActionResult GetAllNicDomain(int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNicDomainBLL(isAll));
        }

        /// <summary>
        /// 作废单条护理措施领域
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNicDomain")]
        [DisplayName(name = "作废护理措施领域")]
        [Note(name = "根据领域编码作废单条护理措施领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNicDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNicDomainStatusBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理措施领域失败");
            }
        }

        /// <summary>
        /// 删除护理措施领域
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNicDomain")]
        [DisplayName(name = "删除护理措施领域")]
        [Note(name = "根据领域编码删除护理措施领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNicDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteDelNicDomainBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理措施领域失败");
            }
        }

        #endregion

        #region 护理措施类别

        /// <summary>
        /// 新增护理措施类别
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNicClass")]
        [DisplayName(name = "新增护理措施类别")]
        [Note(name = "新增护理措施类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNicClass(NicClass item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNicClassBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理措施类别失败");
            }
        }

        /// <summary>
        /// 修改护理措施类别
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNicClass")]
        [DisplayName(name = "修改护理措施类别")]
        [Note(name = "修改护理措施类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNicClass(NicClass item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNicClassBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理措施类别失败");
            }
        }

        /// <summary>
        /// 获得所有护理措施类别
        /// </summary>
        /// <param name="parentCode"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        [HttpGet("GetAllNicClass")]
        [DisplayName(name = "获得领域下所有护理措施类别")]
        [Note(name = "获得领域下所有护理措施类别")]
        [ParaOutName(name = "NicClass")]
        [SchemaContent(name = "NicClass")]
        public IActionResult GetAllNicClass(string parentCode, int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNicClassBLL(parentCode, isAll));
        }

        /// <summary>
        /// 获得单条护理措施类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNicClass")]
        [DisplayName(name = "获得单条护理措施类别")]
        [Note(name = "获得单条护理措施类别")]
        [ParaOutName(name = "NicClass")]
        [SchemaContent(name = "NicClass")]
        public IActionResult GetSingleNicClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNicClassBLL(code));
        }

        /// <summary>
        /// 删除单条护理措施类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNicClass")]
        [DisplayName(name = "删除单条护理措施类别")]
        [Note(name = "根据类别编码删除单条护理措施类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNicClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNicClassBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理措施类别失败");
            }
        }

        /// <summary>
        /// 作废护理措施类别
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNicClass")]
        [DisplayName(name = "作废护理措施类别")]
        [Note(name = "根据领域编码作废护理措施类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNicClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNicClassStatusBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理措施类别失败");
            }
        }

        #endregion

        #region 护理措施

        /// <summary>
        /// 获得单条护理措施
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNicIntervention")]
        [DisplayName(name = "获得单条护理措施")]
        [Note(name = "根据编码获得单条护理措施")]
        [ParaOutName(name = "NicIntervention")]
        [SchemaContent(name = "NicIntervention")]
        public IActionResult GetSingleNicIntervention(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNicInterventionBLL(code));
        }

        /// <summary>
        /// 新增护理措施
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNicIntervention")]
        [DisplayName(name = "新增护理措施")]
        [Note(name = "新增护理措施")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNicIntervention(NicIntervention item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNicInterventionBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理措施失败");
            }
        }

        /// <summary>
        /// 修改护理措施
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNicIntervention")]
        [DisplayName(name = "修改护理措施")]
        [Note(name = "修改护理措施")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNicIntervention(NicIntervention item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNicInterventionBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理措施失败");
            }
        }

        /// <summary>
        /// 获得类别下所有护理措施
        /// </summary>
        /// <param name="classCode"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        [HttpGet("GetAllNicIntervention")]
        [DisplayName(name = "获得类别下所有护理措施")]
        [Note(name = "获得类别下所有护理措施")]
        [ParaOutName(name = "NicIntervention")]
        [SchemaContent(name = "NicIntervention")]
        public IActionResult GetAllNicIntervention(string classCode, int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNicInterventionBLL(classCode, isAll));
        }

        /// <summary>
        /// 删除单条护理措施
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNicIntervention")]
        [DisplayName(name = "删除单条护理措施")]
        [Note(name = "根据目标编码删除单条护理措施")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNicIntervention(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNicInterventionBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理措施失败");
            }
        }

        /// <summary>
        /// 作废护理措施
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNicIntervention")]
        [DisplayName(name = "作废护理措施")]
        [Note(name = "根据编码作废护理措施")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNicIntervention(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNicInterventionStatusBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理措施失败");
            }
        }

        /// <summary>
        /// 根据病人护理计划主ID获取病人护理措施
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNicInterventionByID")]
        [DisplayName(name = "根据病人护理计划主ID获取病人护理措施")]
        [Note(name = "根据病人护理计划主ID获取病人护理措施")]
        [ParaOutName(name = "NicInterventionContent")]
        [SchemaContent(name = "NicInterventionContent")]
        public IActionResult GetNicInterventionByID(long id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetNicInterventionByIdBLL(id));
        }

        #endregion

        #endregion

        #region 护理诊断模块

        #region 护理诊断领域

        /// <summary>
        /// 获得单条护理诊断领域
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNandaDomain")]
        [DisplayName(name = "获得单条护理诊断领域")]
        [Note(name = "根据编码获得单条护理诊断领域")]
        [ParaOutName(name = "NandaDomain")]
        [SchemaContent(name = "NandaDomain")]
        public IActionResult GetSingleNandaDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNandaDomainBLL(code));
        }

        /// <summary>
        /// 获得所有护理诊断领域
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNandaDomain")]
        [DisplayName(name = "获得所有护理诊断领域")]
        [Note(name = "获得所有护理诊断领域")]
        [ParaOutName(name = "NandaDomain")]
        [SchemaContent(name = "NandaDomain")]
        public IActionResult GetAllNandaDomain(int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNandaDomainBLL(isAll));
        }

        /// <summary>
        /// 新增护理诊断领域
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNandaDomain")]
        [DisplayName(name = "新增护理诊断领域")]
        [Note(name = "新增护理诊断领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNandaDomain(NandaDomain item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNandaDomainBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理诊断领域失败");
            }
        }

        /// <summary>
        /// 修改护理诊断领域
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNandaDomain")]
        [DisplayName(name = "修改护理诊断领域")]
        [Note(name = "修改护理诊断领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNandaDomain(NandaDomain item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNandaDomainBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理诊断领域失败");
            }
        }

        /// <summary>
        /// 作废护理诊断领域
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNandaDomain")]
        [DisplayName(name = "作废护理诊断领域")]
        [Note(name = "根据领域编码作废护理诊断领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNandaDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNandaDomainStatusBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理诊断领域失败");
            }
        }

        /// <summary>
        /// 删除护理诊断领域
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNandaDomain")]
        [DisplayName(name = "删除护理诊断领域")]
        [Note(name = "根据领域编码删除护理诊断领域")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNandaDomain(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNandaDomainBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理诊断领域失败");
            }
        }

        #endregion

        #region 护理诊断类别

        /// <summary>
        /// 新增护理诊断类别
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNandaClass")]
        [DisplayName(name = "新增护理诊断类别")]
        [Note(name = "新增护理诊断类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNandaClass(NandaClass item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNandaClassBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理诊断类别失败");
            }
        }

        /// <summary>
        /// 修改护理诊断类别
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNandaClass")]
        [DisplayName(name = "修改护理诊断类别")]
        [Note(name = "修改护理诊断类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNandaClass(NandaClass item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNandaClassBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理诊断类别失败");
            }
        }

        /// <summary>
        /// 获得领域下所有护理诊断类别
        /// </summary>
        /// <param name="parentCode"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        [HttpGet("GetAllNandaClass")]
        [DisplayName(name = "获得领域下所有护理诊断类别")]
        [Note(name = "获得领域下所有护理诊断类别")]
        [ParaOutName(name = "NandaClass")]
        [SchemaContent(name = "NandaClass")]
        public IActionResult GetAllNandaClass(string parentCode, int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNandaClassBLL(parentCode, isAll));
        }

        /// <summary>
        /// 获得单条护理诊断类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNandaClass")]
        [DisplayName(name = "获得单条护理诊断类别")]
        [Note(name = "获得单条护理诊断类别")]
        [ParaOutName(name = "NandaClass")]
        [SchemaContent(name = "NandaClass")]
        public IActionResult GetSingleNandaClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNandaClassBLL(code));
        }

        /// <summary>
        /// 删除单条护理诊断类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNandaClass")]
        [DisplayName(name = "删除单条护理诊断类别")]
        [Note(name = "根据类别编码删除单条护理诊断类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNandaClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNandaClassBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理诊断类别失败");
            }
        }

        /// <summary>
        /// 作废护理诊断类别
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNandaClass")]
        [DisplayName(name = "作废护理诊断类别")]
        [Note(name = "根据类别编码作废护理诊断类别")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNandaClass(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNandaClassStatusBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理诊断类别失败");
            }
        }

        #endregion

        #region 护理诊断

        /// <summary>
        /// 获得单条护理诊断
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNandaDiagnosis")]
        [DisplayName(name = "获得单条护理诊断")]
        [Note(name = "根据编码获得单条护理诊断")]
        [ParaOutName(name = "NandaDiagnosis")]
        [SchemaContent(name = "NandaDiagnosis")]
        public IActionResult GetSingleNandaDiagnosis(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNandaDiagnosisBLL(code));
        }

        /// <summary>
        /// 新增护理诊断
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNandaDiagnosis")]
        [DisplayName(name = "新增护理诊断")]
        [Note(name = "新增护理诊断")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNandaDiagnosis(NandaDiagnosis item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNandaDiagnosisBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理诊断失败");
            }
        }

        /// <summary>
        /// 修改护理诊断
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNandaDiagnosis")]
        [DisplayName(name = "修改护理诊断")]
        [Note(name = "修改护理诊断")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNandaDiagnosis(NandaDiagnosis item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNandaDiagnosisBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理诊断失败");
            }
        }

        /// <summary>
        /// 获得所有护理诊断
        /// </summary>
        /// <param name="classCode"></param>
        /// <returns></returns>
        [HttpGet("GetAllNandaDiagnosis")]
        [DisplayName(name = "获得类别下所有护理诊断")]
        [Note(name = "获得类别下所有护理诊断")]
        [ParaOutName(name = "NandaDiagnosis")]
        [SchemaContent(name = "NandaDiagnosis")]
        public IActionResult GetAllNandaDiagnosis(string classCode, int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNandaDiagnosisBLL(classCode, isAll));
        }

        /// <summary>
        /// 获得所有有关联关系的护理诊断
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllDiagnosisByRel")]
        [DisplayName(name = "获得所有有关联关系的护理诊断")]
        [Note(name = "获得所有有关联关系的护理诊断")]
        [ParaOutName(name = "NandaDiagnosis")]
        [SchemaContent(name = "NandaDiagnosis")]
        public IActionResult GetAllDiagnosisByRel()
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllDiagnosisByRelBLL());
        }

        /// <summary>
        /// 获得护理诊断对应的护理措施
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllInterventionRelByCode")]
        [DisplayName(name = "获得护理诊断对应的护理措施")]
        [Note(name = "获得护理诊断对应的护理措施")]
        [ParaOutName(name = "NicIntervention")]
        [SchemaContent(name = "NicIntervention")]
        public IActionResult GetAllInterventionRelByCode(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllInterventionRelByCodeBLL(code));
        }

        /// <summary>
        /// 获得护理诊断对应的护理目标
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOutComeRelByCode")]
        [DisplayName(name = "获得护理诊断对应的护理目标")]
        [Note(name = "获得护理诊断对应的护理目标")]
        [ParaOutName(name = "NocOutCome")]
        [SchemaContent(name = "NocOutCome")]
        public IActionResult GetAllOutComeRelByCode(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllOutComeRelByCodeBLL(code));
        }

        /// <summary>
        /// 删除护理诊断
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNandaDiagnosis")]
        [DisplayName(name = "删除单条护理诊断")]
        [Note(name = "根据目标编码删除单条护理诊断")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNandaDiagnosis(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNandaDiagnosisBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理诊断失败");
            }
        }

        /// <summary>
        /// 作废护理诊断
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNandaDiagnosis")]
        [DisplayName(name = "作废护理诊断")]
        [Note(name = "根据目标编码作废护理诊断")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNandaDiagnosis(string code, int status)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNandaDiagnosisStatusBLL(code, status))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理诊断失败");
            }
        }

        #endregion

        #region 护理诊断因素关联

        /// <summary>
        /// 获得诊断对应所有护理诊断因素关联
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDiagnosisToNandaDiagnosisFactor")]
        [DisplayName(name = "获得诊断对应所有护理诊断因素关联")]
        [Note(name = "获得诊断对应所有护理诊断因素关联")]
        [ParaOutName(name = "NandaDiagnosisFactorMTQ")]
        [SchemaContent(name = "NandaDiagnosisFactorMTQ")]
        public IActionResult GetDiagnosisToNandaDiagnosisFactor(int isAll, string diagnosis)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetDiagnosisToNandaDiagnosisFactor(isAll, diagnosis));
        }

        /// <summary>
        /// 获得所有护理诊断因素关联
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNandaDiagnosisFactor")]
        [DisplayName(name = "获得所有护理诊断因素关联")]
        [Note(name = "获得所有护理诊断因素关联")]
        [ParaOutName(name = "NandaDiagnosisFactorMTQ")]
        [SchemaContent(name = "NandaDiagnosisFactorMTQ")]
        public IActionResult GetAllNandaDiagnosisFactor(int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNandaDiagnosisFactor(isAll));
        }

        /// <summary>
        /// 获得单条护理诊断因素关联
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNandaDiagnosisFactor")]
        [DisplayName(name = "获得单条护理诊断因素关联")]
        [Note(name = "获得单条护理诊断因素关联")]
        [ParaOutName(name = "NandaDiagnosisFactor")]
        [SchemaContent(name = "NandaDiagnosisFactor")]
        public IActionResult GetSingleNandaDiagnosisFactor(string code, string factorCode)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNandaDiagnosisFactor(code, factorCode));
        }

        /// <summary>
        /// 新增护理诊断因素关联
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNandaDiagnosisFactor")]
        [DisplayName(name = "新增护理诊断因素关联")]
        [Note(name = "新增护理诊断因素关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNandaDiagnosisFactor(NandaDiagnosisFactor item)
        {
            try
            {
                NurseTargetBLL nursePlan = new NurseTargetBLL();
                if (nursePlan.AddNandaDiagnosisFactorBLL(item))
                {
                    return Ok();
                }

                throw new Exception("添加护理诊断因素关联失败");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改护理诊断因素关联
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNandaDiagnosisFactor")]
        [DisplayName(name = "修改护理诊断因素关联")]
        [Note(name = "修改护理诊断因素关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNandaDiagnosisFactor(NandaDiagnosisFactor item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNandaDiagnosisFactorBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理诊断因素关联失败");
            }
        }

        /// <summary>
        /// 删除护理诊断因素关联
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNandaDiagnosisFactor")]
        [DisplayName(name = "删除单条护理诊断因素关联")]
        [Note(name = "根据目标编码删除单条护理诊断因素关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNandaDiagnosisFactor(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNandaDiagnosisFactorBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理诊断因素关联失败");
            }
        }

        /// <summary>
        /// 作废护理诊断因素关联
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNandaDiagnosisFactor")]
        [DisplayName(name = "作废护理诊断因素关联")]
        [Note(name = "根据目标编码作废护理诊断因素关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNandaDiagnosisFactor(string diagnosisCode, string factorCode)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNandaDiagnosisFactorStatusBLL(diagnosisCode, factorCode))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理诊断因素关联失败");
            }
        }

        #endregion

        #region 护理诊断因素

        /// <summary>
        /// 获得所有护理诊断因素
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNandaFactor")]
        [DisplayName(name = "获得所有护理诊断因素")]
        [Note(name = "获得所有护理诊断因素")]
        [ParaOutName(name = "NandaFactor")]
        [SchemaContent(name = "NandaFactor")]
        public IActionResult GetAllNandaFactor(int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNandaFactor(isAll));
        }

        /// <summary>
        /// 获得单条护理诊断因素
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNandaFactor")]
        [DisplayName(name = "获得单条护理诊断因素")]
        [Note(name = "获得单条护理诊断因素")]
        [ParaOutName(name = "NandaFactor")]
        [SchemaContent(name = "NandaFactor")]
        public IActionResult GetSingleNandaFactor(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNandaFactor(code));
        }

        /// <summary>
        /// 新增护理诊断因素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNandaFactor")]
        [DisplayName(name = "新增护理诊断因素")]
        [Note(name = "新增护理诊断因素")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNandaFactor(NandaFactor item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNandaFactorBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理诊断因素失败");
            }
        }

        /// <summary>
        /// 修改护理诊断因素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNandaFactor")]
        [DisplayName(name = "修改护理诊断因素")]
        [Note(name = "修改护理诊断因素")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNandaFactor(NandaFactor item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNandaFactorBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理诊断因素失败");
            }
        }

        /// <summary>
        /// 删除护理诊断
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("DelNandaFactor")]
        [DisplayName(name = "删除单条护理诊断因素")]
        [Note(name = "根据目标编码删除单条护理诊断因素")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelNandaFactor(string code)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.DeleteNandaFactorBLL(code))
            {
                return Ok();
            }
            else
            {
                throw new Exception("删除护理诊断因素失败");
            }
        }

        /// <summary>
        /// 作废护理诊断
        /// 启用/停用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNandaFactor")]
        [DisplayName(name = "作废护理诊断因素")]
        [Note(name = "根据目标编码作废护理诊断因素")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNandaFactor(string code, int status)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNandaFactorStatusBLL(code, status))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理诊断因素失败");
            }
        }

        #endregion

        #endregion

        #region 护理计划模板模块

        /// <summary>
        /// 新增护理计划模板
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNpt")]
        [DisplayName(name = "新增护理计划模板")]
        [Note(name = "新增护理计划模板")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNpt(NptDetail item)
        {
            if (string.IsNullOrEmpty(item.DiagnosisCode))
            {
                throw new Exception("DiagnosisCode参数错误");
            }

            if (string.IsNullOrEmpty(item.OutComeCode))
            {
                throw new Exception("OutComeCode参数错误");
            }

            if (string.IsNullOrEmpty(item.InterventionCode))
            {
                throw new Exception("InterventionCode参数错误");
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new Exception("Name参数错误");
            }

            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNptBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加护理计划模板失败");
            }
        }

        /// <summary>
        /// 获得单条护理计划模板明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNpt")]
        [DisplayName(name = "获得单条护理计划模板")]
        [Note(name = "根据ID获得单条护理计划模板")]
        [ParaOutName(name = "NptList")]
        [SchemaContent(name = "NptList")]
        public IActionResult GetSingleNpt(string id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNptBLL(id));
        }

        /// <summary>
        /// 修改护理计划模板名称
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyNptName")]
        [DisplayName(name = "修改护理计划模板名称")]
        [Note(name = "修改护理计划模板名称")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyNptName(NptList item)
        {
            if (string.IsNullOrEmpty(item.ID))
            {
                throw new Exception("ID参数错误");
            }

            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.ModifyNptNameBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("修改护理计划模板名称失败");
            }
        }

        ///// <summary>
        ///// 修改护理计划模板明细
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //[HttpPost("ModifyNptDetail")]
        //[DisplayName(name = "修改护理计划模板明细")]
        //[Note(name = "修改护理计划模板明细")]
        //[ParaOutName(name = "Result")]
        //[SchemaContent(name = "")]
        //public IActionResult ModifyNptDetail(NptDetail item)
        //{
        //    if (string.IsNullOrEmpty(item.NptListID))
        //    {
        //        return Json(new { Status = 0, Msg = "NptListID参数错误" });
        //    }

        //    NurseTargetBLL nursePlan = new NurseTargetBLL();
        //    if (nursePlan.ModifyNptDetailBLL(item))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return Json(new { Status = 0, Msg = "添加失败" });
        //    }
        //}

        /// <summary>
        /// 获得所有护理计划模板
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNpt")]
        [DisplayName(name = "获得所有护理计划模板")]
        [Note(name = "获得所有护理计划模板")]
        [ParaOutName(name = "NptList")]
        [SchemaContent(name = "NptList")]
        public IActionResult GetAllNpt(int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNptBLL(isAll));
        }

        /// <summary>
        /// 根据ID获取护理计划模板明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNptDetail")]
        [DisplayName(name = "根据ID获取护理计划模板明细")]
        [Note(name = "根据ID获取护理计划模板明细")]
        [ParaOutName(name = "NptDetail")]
        [SchemaContent(name = "NptDetail")]
        public IActionResult GetNptDetail(string id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetNptDetailBLL(id));
        }

        /// <summary>
        /// 根据ID获取护理计划模板详情及明细
        /// 展示用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNptDetailList")]
        [DisplayName(name = "根据ID获取护理计划模板详情及明细")]
        [Note(name = "根据ID获取护理计划模板详情及明细")]
        [ParaOutName(name = "NursePlanModelMTQ")]
        [SchemaContent(name = "NursePlanModelMTQ")]
        public IActionResult GetNptDetailList(string id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetNptDetailListBLL(id));
        }

        /// <summary>
        /// 获取所有在用护理计划模板详情及明细
        /// 展示用
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNptDetailList")]
        [DisplayName(name = "获取所有在用护理计划模板详情及明细")]
        [Note(name = "获取所有在用护理计划模板详情及明细")]
        [ParaOutName(name = "NursePlanModelMTQ")]
        [SchemaContent(name = "NursePlanModelMTQ")]
        public IActionResult GetAllNptDetailList()
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNptDetailListBLL());
        }

        /// <summary>
        /// 作废护理计划模板
        /// 启用/停用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNpt")]
        [DisplayName(name = "作废护理计划模板")]
        [Note(name = "根据ID作废护理计划模板")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNpt(string id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNptStatusBLL(id))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用护理计划模板失败");
            }
        }

        #endregion

        #region 病人护理计划诊断模块

        /// <summary>
        /// 新增病人护理计划诊断
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddPatNursePlanDiagnosis")]
        [DisplayName(name = "新增病人护理计划诊断")]
        [Note(name = "新增病人护理计划诊断")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddPatNursePlanDiagnosis(PatNursePlanDiagnosis item)
        {
            string content;
            if (CheckParams(item))
            {
                return Json(new {Status = 0, Msg = "参数错误"});
            }

            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddPatNursePlanDiagnosisBLL(item, out content))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加病人护理计划诊断失败  "+ content);
            }
        }

        /// <summary>
        /// 获得单条病人护理计划诊断
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetSinglePatNursePlanDiagnosis")]
        [DisplayName(name = "获得单条病人护理计划诊断")]
        [Note(name = "根据ID获得单条病人护理计划诊断")]
        [ParaOutName(name = "PatNursePlanDiagnosis")]
        [SchemaContent(name = "PatNursePlanDiagnosis")]
        public IActionResult GetSinglePatNursePlanDiagnosis(int id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSinglePatNursePlanDiagnosisBLL(id));
        }

        /// <summary>
        /// 根据ID修改病人护理计划
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyPatNursePlanDiagnosis")]
        [DisplayName(name = "根据ID修改病人护理计划")]
        [Note(name = "根据ID修改病人护理计划")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyPatNursePlanDiagnosis(PatNursePlanDiagnosis item)
        {
            try
            {
                string content;
                NurseTargetBLL nursePlan = new NurseTargetBLL();
                if (nursePlan.ModifyPatNursePlanDiagnosisBLL(item,out content))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("修改病人护理计划失败  "+ content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 根据ID删除病人护理计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DeletePatNursePlanDiagnosis")]
        [DisplayName(name = "根据ID删除病人护理计划")]
        [Note(name = "根据ID删除病人护理计划")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DeletePatNursePlanDiagnosis(int id)
        {
            try
            {
                NurseTargetBLL nursePlan = new NurseTargetBLL();
                if (nursePlan.DeletePatNursePlanDiagnosisBLL(id))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("删除病人护理计划失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 作废病人护理计划诊断
        /// 启用/停用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("IsEnablePatNursePlanDiagnosis")]
        [DisplayName(name = "作废病人护理计划诊断")]
        [Note(name = "根据ID作废病人护理计划诊断")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnablePatNursePlanDiagnosis(int id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdatePatNursePlanDiagnosisStatusBLL(id))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用病人护理计划诊断失败");
            }
        }

        /// <summary>
        /// 获得所有病人护理计划诊断
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPatNursePlanDiagnosis")]
        [DisplayName(name = "获得所有病人护理计划诊断")]
        [Note(name = "获得所有病人护理计划诊断")]
        [ParaOutName(name = "PatNursePlanDiagnosis")]
        [SchemaContent(name = "PatNursePlanDiagnosis")]
        public IActionResult GetAllPatNursePlanDiagnosis(int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllPatNursePlanDiagnosisBLL(isAll));
        }

        /// <summary>
        /// 获得单个病人所有护理计划诊断
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOncePatNursePlanDiagnosis")]
        [DisplayName(name = "获得单个病人所有护理计划诊断")]
        [Note(name = "获得单个病人所有护理计划诊断")]
        [ParaOutName(name = "PatNursePlanDiagnosis")]
        [SchemaContent(name = "PatNursePlanDiagnosis")]
        public IActionResult GetOncePatNursePlanDiagnosis(int isAll, string pid)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetOncePatNursePlanDiagnosisBLL(isAll, pid));
        }

        private bool CheckParams(PatNursePlanDiagnosis item)
        {
            if (string.IsNullOrEmpty(item.DiagnosisCode))
            {
                return false;
            }

            if (string.IsNullOrEmpty(item.DiagnosisName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(item.EvalDescription))
            {
                return false;
            }

            if (string.IsNullOrEmpty(item.Evaluator))
            {
                return false;
            }

            if (string.IsNullOrEmpty(item.Pid))
            {
                return false;
            }

            if (string.IsNullOrEmpty(item.Pvid))
            {
                return false;
            }

            if (string.IsNullOrEmpty(item.WardId))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region NNN链接模块

        /// <summary>
        /// 新增NNN链接
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddNNNLinkAge")]
        [DisplayName(name = "新增NNN链接")]
        [Note(name = "新增NNN链接")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddNNNLinkAge(NNNLinkAge item)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.AddNNNLinkAgeBLL(item))
            {
                return Ok();
            }
            else
            {
                throw new Exception("添加NNN链接失败");
            }
        }

        /// <summary>
        /// 作废NNN链接
        /// 启用/停用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("IsEnableNNNLinkAge")]
        [DisplayName(name = "作废NNN链接")]
        [Note(name = "作废NNN链接")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsEnableNNNLinkAge(string id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            if (nursePlan.UpdateNNNLinkAgeBLL(id))
            {
                return Ok();
            }
            else
            {
                throw new Exception("启用/停用NNN链接失败");
            }
        }

        /// <summary>
        /// 获得所有NNN链接
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNNNLinkAge")]
        [DisplayName(name = "获得所有NNN链接")]
        [Note(name = "获得所有NNN链接")]
        [ParaOutName(name = "NNNLinkAgeMTQ")]
        [SchemaContent(name = "NNNLinkAgeMTQ")]
        public IActionResult GetAllNNNLinkAge(int isAll)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetAllNNNLinkAgeBLL(isAll));
        }

        /// <summary>
        /// 获得单条NNN链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetSingleNNNLinkAge")]
        [DisplayName(name = "获得单条NNN链接")]
        [Note(name = "获得单条NNN链接")]
        [ParaOutName(name = "NNNLinkAge")]
        [SchemaContent(name = "NNNLinkAge")]
        public IActionResult GetSingleNNNLinkAge(string id)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetSingleNNNLinkAgeBLL(id));
        }

        /// <summary>
        /// 根据诊断编码和措施编码获得目标
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOutComeByNPDCodeAndNicCode")]
        [DisplayName(name = "根据诊断编码和措施编码获得目标")]
        [Note(name = "根据诊断编码和措施编码获得目标")]
        [ParaOutName(name = "NocOutComeMTQ")]
        [SchemaContent(name = "NocOutComeMTQ")]
        public IActionResult GetOutComeByNPDCodeAndNicCode(string diagnosisCode, string interventionCode)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetOutComeByNPDCodeAndNicCodeBLL(diagnosisCode, interventionCode));
        }

        /// <summary>
        /// 根据诊断编码获得关联的措施
        /// </summary>
        /// <param name="diagnosisCode"></param>
        /// <returns></returns>
        [HttpGet("GetNicByNPDCode")]
        [DisplayName(name = "根据诊断编码获得关联的措施")]
        [Note(name = "根据诊断编码获得关联的措施")]
        [ParaOutName(name = "NicInterventionMTQ")]
        [SchemaContent(name = "NicInterventionMTQ")]
        public IActionResult GetNicByNPDCode(string diagnosisCode)
        {
            NurseTargetBLL nursePlan = new NurseTargetBLL();
            return Ok(nursePlan.GetNicByNPDCodeBLL(diagnosisCode));
        }

        #endregion

        #region 护理计划工具接口

        /// <summary>
        /// 解析PatNursePlanDiagnosis中的Content
        /// </summary>
        /// <returns></returns>
        [HttpGet("ParsingPatNursePlanDiagnosisContent")]
        [DisplayName(name = "解析PatNursePlanDiagnosis中的Content")]
        [Note(name = "解析PatNursePlanDiagnosis中的Content")]
        [ParaOutName(name = "NursePlanResponse")]
        [SchemaContent(name = "NursePlanResponse")]
        public IActionResult ParsingPatNursePlanDiagnosisContent(string content)
        {
            try
            {
                return Ok(JsonConvert.DeserializeObject<NursePlanResponse>(content));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获得护理诊断对应的措施、目标
        /// <summary>
        /// 获得护理诊断对应的措施、目标
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetRelId")]
        [DisplayName(name = "根据诊断code获得护理诊断对应的措施、目标")]
        [Note(name = "根据诊断code获得护理诊断对应的措施、目标")]
        [ParaOutName(name = "DiagnosisInterventionOutcomeRel")]
        [SchemaContent(name = "DiagnosisInterventionOutcomeRel")]
        public IActionResult GetRelId(string code)
        {
            try
            {
                NurseTargetBLL nursePlan = new NurseTargetBLL();
                return Ok(nursePlan.GetRelId(code));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 内容查找：获得护理诊断未关联的护理措施
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetUnAssociatedIntervention")]
        [DisplayName(name = "获得护理诊断未关联的护理措施")]
        [Note(name = "获得护理诊断未关联的护理措施")]
        [ParaOutName(name = "NicIntervention")]
        [SchemaContent(name = "NicIntervention")]
        public IActionResult GetUnAssociatedIntervention(string code)
        {
            try
            {
                NurseTargetBLL nursePlan = new NurseTargetBLL();
                return Ok(nursePlan.GetUnAssociatedIntervention(code));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 内容查找：获得护理诊断未关联的护理目标
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetUnAssociatedOutcome")]
        [DisplayName(name = "获得护理诊断未关联的护理目标")]
        [Note(name = "获得护理诊断未关联的护理目标")]
        [ParaOutName(name = "NocOutCome")]
        [SchemaContent(name = "NocOutCome")]
        public IActionResult GetUnAssociatedOutcome(string code)
        {
            try
            {
                NurseTargetBLL nursePlan = new NurseTargetBLL();
                return Ok(nursePlan.GetUnAssociatedOutcome(code));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增护理诊断、措施、目标关联
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddDiagnosisInterventionOutcomeRel")]
        [DisplayName(name = "新增护理诊断、措施关联")]
        [Note(name = "新增护理诊断、措施关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]

        public IActionResult AddDiagnosisInterventionOutcomeRel([FromBody] List<DiagnosisInterventionOutcomeRel> item)
        {
            try
            {
                NurseTargetBLL nursePlan = new NurseTargetBLL();
                if (nursePlan.AddDiagnosisInterventionOutcomeRel(item))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("新增护理诊断、措施、目标关联失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除护理诊断、措施、目标关联
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpGet("DelDiagnosisInterventionOutcomeRel")]
        [DisplayName(name = "删除护理诊断、措施、目标关联")]
        [Note(name = "删除护理诊断、措施、目标关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]

        public IActionResult DelDiagnosisInterventionOutcomeRel(string problemId,string id)
        {
            try
            {
                NurseTargetBLL nursePlan = new NurseTargetBLL();
                if (nursePlan.DelDiagnosisInterventionOutcomeRel(problemId,id))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("删除关联失败");
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