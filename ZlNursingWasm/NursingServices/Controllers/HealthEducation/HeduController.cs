using NursingCommon;using System;
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

namespace NursingServices.Controllers
{
    /// <summary>
    /// 健康宣教
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    [Route("pda/[controller]")]
    public class HeduController : ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<HeduController>();
            return Json(servicesStructures);
        }
        #region  健康宣教措施
        /// <summary>
        /// 新增宣教项目Hedu_Step
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddHeduStep")]
        [DisplayName(name = "新增宣教措施")]
        [Note(name = "新增宣教措施")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddHeduStep(HeduStep item)
        {
            HeduBLL heduBLL = new HeduBLL();
            try
            {
                if (heduBLL.AddHeduStep(item))
                {
                    return Ok();
                }
                else
                {
                    return Json(new { Status = 0, Msg = "添加失败" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改宣教项目
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyHeduStep")]
        [DisplayName(name = "修改宣教措施")]
        [Note(name = "修改宣教措施")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyHeduStep(HeduStep item)
        {
            HeduBLL heduBLL = new HeduBLL();
            try
            {
                if (heduBLL.ModifyHeduStep(item))
                {
                    return Ok();
                }
                else
                {
                    return Json(new { Status = 0, Msg = "修改失败" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除宣教项目，已使用的不能删除
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet("DelHeduStep")]
        [DisplayName(name = "删除宣教措施")]
        [Note(name = "删除宣教措施")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelHeduStep(string stepId)
        {
            HeduBLL heduBLL = new HeduBLL();
            try
            {
                if (heduBLL.DelHeduStep(stepId, out string errorMsg))
                {
                    return Ok();
                }
                else
                {
                    return Json(new { Status = 0, Msg = errorMsg });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取宣教措施
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHeduStep")]
        [DisplayName(name = "获取宣教措施")]
        [Note(name = "获取宣教措施")]
        [ParaOutName(name = "HeduStep")]
        [SchemaContent(name = "HeduStep")]
        public IActionResult GetHeduStep()
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduStep());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过名称关键字获取宣教项目信息
        /// </summary>
        /// <param name="key">名称关健字，通过关健字进行查找</param>
        /// <returns></returns>
        [HttpGet("GetHeduStepByName")]
        [DisplayName(name = "通过名称获取宣教措施")]
        [Note(name = "通过名称获取宣教措施")]
        [ParaOutName(name = "HeduStep")]
        [SchemaContent(name = "HeduStep")]
        public IActionResult GetHeduStepByName(string key)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduStepByName(key));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 通过ID获取宣教项目信息
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduStepById")]
        [DisplayName(name = "通过ID获取宣教措施")]
        [Note(name = "通过ID获取宣教措施")]
        [ParaOutName(name = "HeduStep")]
        [SchemaContent(name = "HeduStep")]
        public IActionResult GetHeduStepById(string stepId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduStepById(stepId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 健康宣教措施组
        /// <summary>
        /// 新增宣教措施组
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddHeduGroup")]
        [DisplayName(name = "新增宣教措施组")]
        [Note(name = "新增宣教措施组")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddHeduGroup(HeduGroup item)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                if (heduBLL.AddHeduGroup(item))
                {
                    return Ok(item.GroupID);
                }
                else
                {
                    throw new Exception("添加失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改宣教措施组
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyHeduGroup")]
        [DisplayName(name = "修改宣教措施组")]
        [Note(name = "修改宣教措施组")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyHeduGroup(HeduGroup item)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                if (heduBLL.ModifyHeduGroup(item))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("修改失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除宣教措施组，已使用的不能删除
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("DelHeduGroup")]
        [DisplayName(name = "删除宣教措施组")]
        [Note(name = "删除宣教措施组")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelHeduGroup(string groupId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                if (heduBLL.DelHeduGroup(groupId, out string errorMsg))
                {
                    return Ok();
                }
                else
                {
                    return Json(new { Status = 0, Msg = errorMsg });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取健康宣教组列表
        /// </summary>
        /// <param name="wardId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduGroup")]
        [DisplayName(name = "获取健康宣教组列表")]
        [Note(name = "获取健康宣教组列表")]
        [ParaOutName(name = "HeduGroupWard")]
        [SchemaContent(name = "HeduGroupWard")]
        public IActionResult GetHeduGroup(string wardId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduGroup(wardId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取所有健康宣教组列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHeduGroupAll")]
        [DisplayName(name = "获取所有健康宣教组列表")]
        [Note(name = "获取所有健康宣教组列表")]
        [ParaOutName(name = "HeduGroupWard")]
        [SchemaContent(name = "HeduGroupWard")]
        public IActionResult GetHeduGroupAll()
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduGroupAll());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取健康宣教组列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduGroupById")]
        [DisplayName(name = "通过ID获取健康宣教组列表")]
        [Note(name = "通过ID获取健康宣教组列表")]
        [ParaOutName(name = "HeduGroup")]
        [SchemaContent(name = "HeduGroup")]
        public IActionResult GetHeduGroupById(string groupId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduGroupById(groupId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 健康宣教单与病区对应关系
        /// <summary>
        /// 添加病区和宣教组的对照关系
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddHeduWardGroup")]
        [DisplayName(name = "添加病区和宣教组的对照关系")]
        [Note(name = "添加病区和宣教组的对照关系")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddHeduWardGroup(HeduWardGroup item)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                if (heduBLL.AddHeduWardGroup(item))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("添加失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取某个宣教单已关联的病区
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduWardByGroupId")]
        [DisplayName(name = "获取某个宣教单已关联的病区")]
        [Note(name = "获取某个宣教单已关联的病区")]
        [ParaOutName(name = "HeduWard")]
        [SchemaContent(name = "HeduWard")]
        public IActionResult GetHeduWardByGroupId(string groupId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduWardByGroupId(groupId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取某个宣教单的病区关联情况（含未关联病区）
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduWardAllByGroupId")]
        [DisplayName(name = "获取某个宣教单的病区关联情况（含未关联病区）")]
        [Note(name = "获取某个宣教单的病区关联情况（含未关联病区）")]
        [ParaOutName(name = "HeduWardAll")]
        [SchemaContent(name = "HeduWardAll")]
        public IActionResult GetHeduWardAllByGroupId(string groupId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduWardAllByGroupId(groupId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除某宣教单所有关联病区
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("DelAllHeduWardByGroupId")]
        [DisplayName(name = "删除某宣教单所有关联病区")]
        [Note(name = "删除某宣教单所有关联病区")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelAllHeduWardByGroupId(string groupId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.DelAllHeduWardByGroupId(groupId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 健康宣教措施与措施组对照关系
        /// <summary>
        /// 新增宣教措施与措施组对应关系
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddHeduGroupStep")]
        [DisplayName(name = "新增宣教措施与措施组对应关系")]
        [Note(name = "新增宣教措施与措施组对应关系")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddHeduGroupStep(HeduGroupStep item)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                if (heduBLL.AddHeduGroupStep(item))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("添加失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改宣教措施与措施组对应关系
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("ModifyHeduGroupStep")]
        [DisplayName(name = "修改宣教措施与措施组对应关系")]
        [Note(name = "修改宣教措施与措施组对应关系")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult ModifyHeduGroupStep(HeduGroupStep item)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                if (heduBLL.ModifyHeduGroupStep(item))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("修改失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取某一组宣教措施与措施组对应关系
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduGroupStep")]
        [DisplayName(name = "获取某一组宣教措施与措施组对应关系")]
        [Note(name = "获取某一组宣教措施与措施组对应关系")]
        [ParaOutName(name = "HeduGroupStepName")]
        [SchemaContent(name = "HeduGroupStepName")]
        public IActionResult GetHeduGroupStep(string groupId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduGroupStep(groupId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除宣教措施与措施组对应关系
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet("DelHeduGroupStep")]
        [DisplayName(name = "删除宣教措施与措施组对应关系")]
        [Note(name = "删除宣教措施与措施组对应关系")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelHeduGroupStep(string groupId, string stepId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.DelHeduGroupStep(groupId, stepId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 健康宣教措施组与表单关联

        /// <summary>
        /// 获取宣教组适用病区列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHeduWard")]
        [DisplayName(name = "获取宣教组适用病区列表")]
        [Note(name = "获取宣教组适用病区列表")]
        [ParaOutName(name = "HeduWard")]
        [SchemaContent(name = "HeduWard")]
        public IActionResult GetHeduWard()
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduWard());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 增加宣教单与表单的关联
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddHeduGroupForm")]
        [DisplayName(name = "增加宣教单与表单的关联")]
        [Note(name = "增加宣教单与表单的关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddHeduGroupForm(HeduGroupForm item)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.AddHeduGroupForm(item));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除宣教单与表单的关联
        /// </summary>
        /// <param name="groupFormId"></param>
        /// <returns></returns>
        [HttpGet("DelHeduGroupForm")]
        [DisplayName(name = "删除宣教单与表单的关联")]
        [Note(name = "删除宣教单与表单的关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelHeduGroupForm(string groupFormId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                if (heduBLL.DelHeduGroupForm(groupFormId))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception("修改失败");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取宣教单与表单的关联
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduGroupForm")]
        [DisplayName(name = "获取宣教单与表单的关联")]
        [Note(name = "获取宣教单与表单的关联")]
        [ParaOutName(name = "HeduGroupForm")]
        [SchemaContent(name = "HeduGroupForm")]
        public IActionResult GetHeduGroupForm(string  groupId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduGroupForm(groupId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 通过名称获取表单列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("GetFormListByName")]
        [DisplayName(name = "通过名称获取表单列表")]
        [Note(name = "通过名称获取表单列表")]
        [ParaOutName(name = "FormListByName")]
        [SchemaContent(name = "FormListByName")]
        public IActionResult GetFormListByName(string key)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetFormListByName(key));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取表单分值设置结构
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFormScoreStruc")]
        [DisplayName(name = "获取表单分值设置结构")]
        [Note(name = "获取表单分值设置结构")]
        [ParaOutName(name = "HeduGroupForm")]
        [SchemaContent(name = "HeduGroupForm")]
        public IActionResult GetFormScoreStruc()
        {
            return Ok();
        }

        #endregion

        #region 健康宣教措施组与诊疗项目关联
        /// <summary>
        /// 获取诊疗项目
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("GetOrderItemListByName")]
        [DisplayName(name = "通过名称关键字获取诊疗项目")]
        [Note(name = "通过名称关键字获取诊疗项目")]
        [ParaOutName(name = "OrderItemList")]
        [SchemaContent(name = "OrderItemList")]
        public IActionResult GetOrderItemListByName(string key)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetOrderItemListByName(key));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增宣教单与诊疗项目关联
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("AddHeduGroupOrder")]
        [DisplayName(name = "新增宣教单与诊疗项目关联")]
        [Note(name = "新增宣教单与诊疗项目关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddHeduGroupOrder(HeduGroupOrder item)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.AddHeduGroupOrder(item));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除宣教单与诊疗项目关联
        /// </summary>
        /// <param name="groupItemId"></param>
        /// <returns></returns>
        [HttpGet("DelHeduGroupOrder")]
        [DisplayName(name = "删除宣教单与诊疗项目关联")]
        [Note(name = "删除宣教单与诊疗项目关联")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelHeduGroupOrder(string groupItemId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.DelHeduGroupOrder(groupItemId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取宣教单与诊疗项目关联
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduGroupOrder")]
        [DisplayName(name = "获取宣教单与诊疗项目关联")]
        [Note(name = "获取宣教单与诊疗项目关联")]
        [ParaOutName(name = "HeduGroupOrder")]
        [SchemaContent(name = "HeduGroupOrder")]
        public IActionResult GetHeduGroupOrder(string groupId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduGroupOrder(groupId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 基础设置所需其他接口
        /// <summary>
        /// 获取HIS病区列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHisWardList")]
        [DisplayName(name = "获取HIS病区列表")]
        [Note(name = "获取HIS病区列表")]
        [ParaOutName(name = "HisWardList")]
        [SchemaContent(name = "HisWardList")]
        public IActionResult GetHisWardList()
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHisWardList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定ID的HIS病区
        /// </summary>
        /// <param name="wardId"></param>
        /// <returns></returns>
        [HttpGet("GetHisWardListById")]
        [DisplayName(name = "获取指定ID的HIS病区")]
        [Note(name = "获取指定ID的HIS病区")]
        [ParaOutName(name = "HisWardList")]
        [SchemaContent(name = "HisWardList")]
        public IActionResult GetHisWardListById(string wardId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHisWardListById(wardId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region  病人健康宣教
        /// <summary>
        /// 获取病人健康宣教列表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pvid"></param>
        /// <returns></returns>
        [HttpGet("GetPatHeduRecList")]
        [DisplayName(name = "获取病人健康宣教列表")]
        [Note(name = "获取病人健康宣教列表")]
        [ParaOutName(name = "PatHeduRecList")]
        [SchemaContent(name = "PatHeduRecList")]
        public IActionResult GetPatHeduRecList(string pid,string pvid)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetPatHeduRecList(pid, pvid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取病人某次健康宣教内容
        /// </summary>
        /// <param name="patHeduRecId"></param>
        /// <returns></returns>
        [HttpGet("GetPatHeduRecDetailById")]
        [DisplayName(name = "获取病人某次健康宣教内容")]
        [Note(name = "获取病人某次健康宣教内容")]
        [ParaOutName(name = "PatHeduRecDetail")]
        [SchemaContent(name = "PatHeduRecDetail")]
        public IActionResult GetPatHeduRecDetailById(string patHeduRecId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetPatHeduRecDetailById(patHeduRecId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除病人某一次宣教记录
        /// </summary>
        /// <param name="patHeduRecId"></param>
        /// <returns></returns>
        [HttpGet("DelPatHeduRec")]
        [DisplayName(name = "删除病人某一次宣教记录")]
        [Note(name = "删除病人某一次宣教记录")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DelPatHeduRec(string patHeduRecId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.DelPatHeduRec(patHeduRecId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取病区可用的健康宣教组列表
        /// </summary>
        /// <param name="wardId"></param>
        /// <returns></returns>
        [HttpGet("GetHeduGroupUseable")]
        [DisplayName(name = "获取病区可用的健康宣教组列表")]
        [Note(name = "获取病区可用的健康宣教组列表")]
        [ParaOutName(name = "HeduGroupWard")]
        [SchemaContent(name = "HeduGroupWard")]
        public IActionResult GetHeduGroupUseable(string wardId)
        {
            try
            {
                HeduBLL heduBLL = new HeduBLL();
                return Ok(heduBLL.GetHeduGroupUseable(wardId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}