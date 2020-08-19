using NursingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NursingBLL;
using NursingModel;


namespace NursingServices.Controllers
{
    /// <summary>
    /// 健康宣教
    /// </summary>
    [Route("[controller]/[action]")]
    [Route("bh/[controller]/[action]")]
    [Route("pda/[controller]/[action]")]
    public class HealthEducationController : Controller
    {
        /// <summary>
        /// 新增宣教单页面
        /// </summary>
        /// <param name="pId">病人ID</param>
        /// <param name="pvId">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="executor">执行人</param>
        /// <param name="groupIds">组ID</param>
        /// <param name="isMobile">是否移动端 1：移动端；0或者不传参为PC端</param>
        /// <param name="admissionDateTime">入院时间</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string pId, string pvId, string wardId, string executor, string[] groupIds,
            DateTime? admissionDateTime,
            int isMobile = 0)
        {
            var bll = new HealthEducationBLL();
            var list = bll.GetHeduFormBLL(groupIds);

            ViewData["pId"] = pId;
            ViewData["pvId"] = pvId;
            ViewData["wardId"] = wardId;
            ViewData["executor"] = executor;
            ViewData["admissionDateTime"] = admissionDateTime;
            if (isMobile == 0)
                return View(list);
            return View("IndexMobile", list);
        }

        /// <summary>
        /// 新增宣教单
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public IActionResult AddHeduForm(HeduRequestModel value)
        {
            try
            {
                if (value.AdmissionDateTime <= value.EvaluationTime && value.EvaluationTime <= DateTime.Now)
                {
                    var bll = new HealthEducationBLL();
                    if (bll.AddOrModifyPatHeduRecBLL(value, true))
                    {
                        return Ok();
                    }

                    return BadRequest(new {code = 0, msg = "添加病人宣教执行记录失败"});
                }

                return BadRequest(new {code = 0, msg = "错误：评价时间不能在入院时间之前，并且评价时间不能在当前时间之后"});
            }
            catch (Exception ex)
            {
                return BadRequest(new {code = 500, msg = ex.Message});
            }
        }

        /// <summary>
        /// 获取或者修改宣教单详情页面
        /// </summary>
        /// <param name="recId">宣教记录ID</param>
        /// <param name="executor">执行人</param>
        /// <param name="isView">是否仅查看：1：仅查看；0：修改</param>
        /// <param name="isMobile">是否移动端 1：移动端；0或者不传参为PC端</param>
        /// <param name="pId"></param>
        /// <param name="pvId"></param>
        /// <param name="wardId"></param>
        /// <param name="admissionDateTime">入院时间</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public IActionResult GetPatHeduRecByRecId(int recId, string executor, int isView, string pId, string pvId,
            string wardId, DateTime? admissionDateTime, int isMobile = 0)
        {
            try
            {
                var bll = new HealthEducationBLL();
                if (isView == 1)
                    ViewData["isView"] = "disabled";
                ViewData["newExecutor"] = executor;
                ViewData["pId"] = pId;
                ViewData["pvId"] = pvId;
                ViewData["wardId"] = wardId;
                ViewData["admissionDateTime"] = admissionDateTime;
                var obj = bll.GetPatHeduRecBLL(recId);
                if (isMobile == 0)
                    return View(obj);
                return View("GetPatHeduRecByRecIdMobile", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改宣教单
        /// </summary>
        /// <param name="value"></param>
        /// <param name="recId">宣教单记录ID</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public IActionResult ModifyHeduForm(HeduRequestModel value, int recId)
        {
            try
            {
                if (value.AdmissionDateTime <= value.EvaluationTime && value.EvaluationTime <= DateTime.Now)
                {
                    var bll = new HealthEducationBLL();
                    if (bll.AddOrModifyPatHeduRecBLL(value, false, recId))
                    {
                        return Ok();
                    }

                    return BadRequest(new {code = 0, msg = "添加病人宣教执行记录失败"});
                }

                return BadRequest(new {code = 0, msg = "错误：评价时间不能在入院时间之前，并且评价时间不能在当前时间之后"});
            }
            catch (Exception ex)
            {
                return BadRequest(new {code = 500, msg = ex.Message});
            }
        }

        /// <summary>
        /// 根据条件获取指定宣教记录
        /// </summary>
        /// <param name="pId">病人ID</param>
        /// <param name="pvId">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="isMobile">是否移动端 1：移动端；0或者不传参为PC端</param>
        /// <param name="admissionDateTime">入院时间</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HeduList(string pId, string pvId, string wardId, string executor,
            DateTime? admissionDateTime, int isMobile = 0)
        {
            var bll = new HealthEducationBLL();
            var list = bll.GetHeduListByPIdBLL(pId, pvId, wardId);

            ViewData["pId"] = pId;
            ViewData["pvId"] = pvId;
            ViewData["wardId"] = wardId;
            ViewData["executor"] = executor;
            ViewData["isMobile"] = isMobile;
            ViewData["admissionDateTime"] = admissionDateTime;
            return View(list);
        }

        /// <summary>
        /// 获取宣教分组列表
        /// </summary>
        /// <param name="pId">病人ID</param>
        /// <param name="pvId">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="executor">执行人</param>
        /// <param name="isMobile">是否移动端 1：移动端；0或者不传参为PC端</param>
        /// <param name="admissionDateTime">入院时间</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public IActionResult HeduGroupList(string pId, string pvId, string wardId, string executor,
            DateTime? admissionDateTime, int isMobile = 0)
        {
            try
            {
                ViewData["pId"] = pId;
                ViewData["pvId"] = pvId;
                ViewData["wardId"] = wardId;
                ViewData["executor"] = executor;
                ViewData["isMobile"] = isMobile;
                ViewData["admissionDateTime"] = admissionDateTime;

                var bll = new HealthEducationBLL();
                return View(bll.GetHeduGroupList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}