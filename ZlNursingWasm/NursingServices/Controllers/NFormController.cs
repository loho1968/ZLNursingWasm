using NursingCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using NursingModel.Form;
using NursingServices.Models;
using NursingBLL.Form;
using Mapster;
using Newtonsoft.Json.Linq;
using NursingModel.Base;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NursingServices.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Route("api/[controller]/[action]")]
    [Route("bh/[controller]/[action]")]
    [Route("pda/[controller]/[action]")]
    public class NFormController : Controller
    {
        private IMemoryCache memory { get; set; }

        public NFormController(IMemoryCache memoryCache)
        {
            memory = memoryCache;
        }

        //todo:修改时如果操作科室非原来的科室，就时查看模式

        // GET: /<controller>/
        /// <summary>
        /// 评分评估表网页
        /// </summary>
        /// <param name="formId">表单id，多个表单用*分隔</param>
        /// <param name="pid">病人id</param>
        /// <param name="pvid">就诊id</param>
        /// <param name="baby">婴儿标准</param>
        /// <param name="recoder">记录者id</param>
        /// <param name="wardId">病区id</param>
        /// <param name="formType">表单类型，用于修改表单时</param>
        /// <param name="recId">病人表单记录id，用于修改表单时</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string formId, string pid, string pvid, int baby, string recoder, int wardId, string formType, string recId, bool watchModel = false)
        {
            ViewBag.Title = "评分评估表";
            ViewBag.WatchModel = JsonConvert.SerializeObject(watchModel);
            TypeAdapterConfig<PatScaleFormRec, ViewFormList>.NewConfig()
                      .ShallowCopyForSameType(true)
                      .PreserveReference(true)
                      .Map(j => j.RecId, k => k.ScaleRecId)
                      .Map(j => j.FormId, k => k.ScaleListId)
                      .Map(j => j.FormVersion, k => k.ScaleVersion)
                      .Map(j => j.FormItems, k => k.PatScaleFormRecDetails)
                      ;
            TypeAdapterConfig<PatScaleFormRecDetail, ViewFormItem>.NewConfig()
                .ShallowCopyForSameType(true)
                .PreserveReference(true)
                .Map(j => j.ItemId, k => k.ScaleItemId)
                .Map(j => j.ItemName, k => k.ScaleItemName)
                .Map(j => j.PatScaleFormComboItemDetails, k => k.PatScaleFormComboItemDetail)
                ;


            TypeAdapterConfig<PatAsFormRec, ViewFormList>.NewConfig()
                      .ShallowCopyForSameType(true)
                      .PreserveReference(true)
                      .Map(j => j.RecId, k => k.AsFormRecId)
                      .Map(j => j.FormId, k => k.FormId)
                      .Map(j => j.FormVersion, k => k.FormVersion)
                      .Map(j => j.FormItems, k => k.PatAsFormRecDetail)
                      ;
            TypeAdapterConfig<PatAsFormRecDetail, ViewFormItem>.NewConfig()
               .ShallowCopyForSameType(true)
               .PreserveReference(true)
               .Map(j => j.ItemId, k => k.AsFormItemId)
               .Map(j => j.ItemName, k => k.AsFormItemName)
               .Map(j => j.PatScaleFormComboItemDetails, k => k.PatAsFormComboItemDetail)
               ;
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            if (recoder == null || recoder == "")
            {
                recoder = "fff1d999-1bd4-4839-9efe-b661009d677e";
            }
            ViewBag.HighRisk = JsonConvert.SerializeObject(ScaleRiskLevel.高);
            ViewBag.MiddleRisk = JsonConvert.SerializeObject(ScaleRiskLevel.中);
            ViewBag.LowRisk = JsonConvert.SerializeObject(ScaleRiskLevel.低);
            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.errofInfo = "";

            List<ViewFormList> viewFormLists = new List<ViewFormList>();
            List<FormList> formLists = new List<FormList>();

            if (formType == null || formType.Length == 0)
            {
                formType = "r";
            }
            dynamic formInfo = new JObject();
            formInfo.Recorder = recoder;
            formInfo.WardId = wardId;
            formInfo.RecId = recId;
            formInfo.EncType = 2;//不支持门诊，固定为住院
            //新增模式下，获取表单信息
            if (recId == null)
            {

                #region 参数检查
                if (pid == null || pid.Length == 0)
                {
                    errorViewModel.errofInfo = "无pid参数";
                }
                if (pvid == null || pvid.Length == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无pvid参数";
                }
                if (recoder == null || recoder.Length == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无recoder参数";
                }
                if (wardId == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无wardId参数";
                }
                if (errorViewModel.errofInfo != "")
                {
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }
                #endregion

                formLists = memory.Get<List<FormList>>("FormList").Where(o => formId.Contains(o.FormId)).ToList();

                if (formLists == null)
                {
                    errorViewModel.errofInfo = "未查询到对应的评分评估表，评分评估表ID:" + formId;
                }
            }

            int formRecid = 0;

            int.TryParse(recId, out formRecid);


            if (errorViewModel.errofInfo != "")
            {
                return RedirectToAction("Error", "NForm", errorViewModel);
            }

            List<PatScaleFormRec> patScaleForms = new List<PatScaleFormRec>();
            NursingFormBLL nursingFormBLL = new NursingFormBLL();

            List<PatAsFormRec> patAsFormRecs = new List<PatAsFormRec>();
            AsFormBLL asFormBLL = new AsFormBLL();
            var tempNurse = memory.Get<List<Nurse>>("NurseList").Where(o => o.NurseId == recoder).FirstOrDefault();
            if (tempNurse != null)
            {
                formInfo.RecoderName = tempNurse.NurseAlias.ToString();
            }

            var tempWard = memory.Get<List<Ward>>("WardList").Where(o => o.WardId == wardId).FirstOrDefault();
            if (tempWard != null)
            {
                formInfo.WardName = tempWard.WardName.ToString();
            }
            if (formRecid > 0)
            {
                //修改护理记录单,查询护理记录的数据

                formInfo.EditType = "update";

                if (formType == "r")
                {

                    //评分表修改
                    PatScaleFormRec patScaleFormRec = new PatScaleFormRec();
                    patScaleFormRec = nursingFormBLL.GetPatScaleFormRec(formRecid);
                    if (patScaleFormRec == null)
                    {
                        errorViewModel.errofInfo = "未查询到对应的表单，表单ID:" + recId;
                        return RedirectToAction("Error", "NForm", errorViewModel);
                    }
                    patScaleForms.Add(patScaleFormRec);
                    formLists = memory.Get<List<FormList>>("FormList").Where(o => o.FormId == patScaleFormRec.ScaleListId && o.FormVersion == patScaleFormRec.ScaleVersion).ToList();


                    formInfo.Pid = patScaleFormRec.Pid;
                    formInfo.Pvid = patScaleFormRec.Pvid;
                    formInfo.Baby = "0";//评分表没有婴儿

                    viewFormLists = patScaleForms.Adapt<List<ViewFormList>>();
                    viewFormLists[0].FormName = formLists[0].FormName;
                    //获取表单的分数
                    if (formLists != null)
                    {
                        viewFormLists[0].FormScores = formLists[0].FormScores;
                    }

                    else
                    {
                        errorViewModel.errofInfo = "未查询到对应的表单定义，表单ID:" + patScaleFormRec.ScaleListId + ",版本:" + patScaleFormRec.ScaleVersion;
                        return RedirectToAction("Error", "NForm", errorViewModel);
                    }
                }
                else
                {

                    PatAsFormRec patAsFormRec = new PatAsFormRec();

                    patAsFormRec = asFormBLL.GetPatAsFormRec(formRecid);
                    if (patAsFormRec == null)
                    {
                        errorViewModel.errofInfo = "未查询到对应的表单，表单ID:" + recId;
                        return RedirectToAction("Error", "NForm", errorViewModel);
                    }

                    patAsFormRecs.Add(patAsFormRec);
                    formLists = memory.Get<List<FormList>>("FormList").Where(o => o.FormId == patAsFormRec.FormId && o.FormVersion == patAsFormRec.FormVersion).ToList();
                    if (formLists == null)
                    {
                        errorViewModel.errofInfo = "未查询到对应的表单定义，表单ID:" + patAsFormRec.FormId + ",版本:" + patAsFormRec.FormVersion;
                        return RedirectToAction("Error", "NForm", errorViewModel);
                    }
                    formInfo.Pid = patAsFormRec.Pid;
                    formInfo.Pvid = patAsFormRec.Pvid;
                    formInfo.Baby = "0";//评评表没有婴儿

                    viewFormLists = patAsFormRecs.Adapt<List<ViewFormList>>();
                    viewFormLists[0].FormName = formLists[0].FormName;
                }
            }
            else
            {
                formLists = memory.Get<List<FormList>>("FormList").Where(o => formId.Contains(o.FormId) && o.EnableSign == 1).ToList();
                if (formLists == null)
                {
                    errorViewModel.errofInfo = "未查询到对应的表单定义，表单ID:" + formId;
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }
                for (int i = 0; i < formLists.Count; i++)
                {
                    //新增，如果一个评分表，就加载上次评分数据
                    switch (formLists[i].FormType)
                    {
                        case "r": //评分表
                            //获取上次护理记录;PatNurseDataRec
                            PatScaleFormRec tempScaleFormRec = new PatScaleFormRec();
                            tempScaleFormRec = nursingFormBLL.GetLastPatScaleFormRecs(pid, pvid, baby, formId);
                            if (tempScaleFormRec != null)
                            {
                                patScaleForms.Add(tempScaleFormRec);
                            }
                            ViewFormList viewScaleForm = new ViewFormList();
                            viewScaleForm = tempScaleFormRec.Adapt<ViewFormList>();
                            if (viewScaleForm == null)
                            {
                                viewScaleForm = formLists[i].Adapt<ViewFormList>();
                            }
                            // 获取表单的分数
                            if (formLists != null)
                            {
                                viewScaleForm.FormScores = formLists[i].FormScores;
                            }
                            viewScaleForm.FormName = formLists[i].FormName;
                            viewFormLists.Add(viewScaleForm);
                            break;
                        case "a": //评估表
                            ViewFormList viewAsForm = new ViewFormList();
                            viewAsForm = formLists[i].Adapt<ViewFormList>();
                            viewAsForm.FormName = formLists[i].FormName;
                            viewFormLists.Add(viewAsForm);
                            break;

                    }
                }

                formInfo.Pid = pid;
                formInfo.Pvid = pvid;
                formInfo.Baby = baby.ToString();//评分表没有婴儿的


                formInfo.EditType = "insert"; ;
            }


            ViewBag.viewFormLists = JsonConvert.SerializeObject(viewFormLists);
            ViewBag.formInfo = JsonConvert.SerializeObject(formInfo);
            ViewBag.bloodpressureItem = JsonConvert.SerializeObject(BloodItem.血压项目);
            return View(formLists);
        }

        /// <summary>
        /// 护理单界面（UI方式二）
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NursingRec(string formId, string pid, string pvid, int baby, string recoder, int wardId, string recId, bool watchModel = false)
        {
            ViewBag.Title = "护理记录单";
            ViewBag.WatchModel = JsonConvert.SerializeObject(watchModel);
            TypeAdapterConfig<PatNurseDataRec, ViewFormList>.NewConfig()
                      .ShallowCopyForSameType(true)
                      .PreserveReference(true)
                      .Map(j => j.RecId, k => k.NurseDataId)
                      .Map(j => j.FormId, k => k.NurseDataFormId)
                      .Map(j => j.FormItems, k => k.FormDatas)
                      ;
            TypeAdapterConfig<FormData, ViewFormItem>.NewConfig()
                .ShallowCopyForSameType(true)
                .PreserveReference(true)
                .Map(j => j.ItemId, k => k.ItemId)
                .Map(j => j.ItemName, k => k.ItemName)
                .Map(j => j.PatScaleFormComboItemDetails, k => k.PatScaleFormComboItemDetail)
                ;

            //返回前端的表单数据
            List<ViewFormList> viewFormLists = new List<ViewFormList>();
            FormList form = new FormList();
            ViewFormList viewForm = new ViewFormList();

            //错误页面的信息

            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.errofInfo = "";

            dynamic formInfo = new JObject();
            formInfo.Recorder = recoder;
            formInfo.WardId = wardId;
            formInfo.RecId = recId;
            formInfo.EncType = 2;//不支持门诊，固定为住院

            //新增模式下，获取表单信息
            if (recId == null)
            {

                #region 参数检查

                if (pid == null || pid.Length == 0)
                {
                    errorViewModel.errofInfo = "无pid参数";
                }
                if (pvid == null || pvid.Length == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无pvid参数";
                }
                if (recoder == null || recoder.Length == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无recoder参数";
                }
                if (wardId == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无wardId参数";
                }
                #endregion
                if (errorViewModel.errofInfo != "")
                {
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }
            }

            PatNurseDataRec patNurseDataRec = new PatNurseDataRec();

            List<ViewToDayNurseRec> viewToDayNurseRecs = new List<ViewToDayNurseRec>();
            List<ViewToDayNurseRecDetail> recDetails = new List<ViewToDayNurseRecDetail>();
            int nrseRecid = 0;

            int.TryParse(recId, out nrseRecid);
            NursingFormRecBLL nursingFormRecBLL = new NursingFormRecBLL();

            var tempNurse = memory.Get<List<Nurse>>("NurseList").Where(o => o.NurseId == recoder).FirstOrDefault();
            if (tempNurse != null)
            {
                formInfo.RecoderName = tempNurse.NurseAlias.ToString();
            }

            var tempWard = memory.Get<List<Ward>>("WardList").Where(o => o.WardId == wardId).FirstOrDefault();
            if (tempWard != null)
            {
                formInfo.WardName = tempWard.WardName.ToString();
            }

            //修改护理记录单,查询护理记录的数据
            if (nrseRecid > 0)
            {
                formInfo.EditType = "update"; ;

                patNurseDataRec = nursingFormRecBLL.GetPatNurseData(nrseRecid);
                viewForm = patNurseDataRec.Adapt<ViewFormList>();

                if (patNurseDataRec == null)
                {
                    errorViewModel.errofInfo = "未查询到对应的护理记录单，护理记录ID:" + recId;
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }

                formInfo.Pid = patNurseDataRec.Pid;
                formInfo.Pvid = patNurseDataRec.Pvid;
                formInfo.Baby = patNurseDataRec.Baby;

                form = memory.Get<List<FormList>>("NursingRecFormList").Where(o => o.FormId == patNurseDataRec.NurseDataFormId).FirstOrDefault();
                if (form == null)
                {
                    errorViewModel.errofInfo = "未查询到对应的表单定义，表单ID:" + patNurseDataRec.NurseDataFormId;
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }
                form = nursingFormRecBLL.InitPatNursingForm(form, patNurseDataRec);

                if (patNurseDataRec != null)
                {
                    viewForm = patNurseDataRec.Adapt<ViewFormList>();
                }
                else
                {
                    viewForm.FormId = form.FormId;
                }



                viewForm.FormName = form.FormName;
            }
            else
            {
                formInfo.EditType = "insert";
                ViewBag.isNew = true;

                form = memory.Get<List<FormList>>("NursingRecFormList").Where(o => formId == o.FormId).FirstOrDefault();
                if (form == null)
                {
                    errorViewModel.errofInfo = "未查询到对应的表单定义，表单ID:" + formId;
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }
                //获取病人今日护理记录
                List<EFPatObservRec> toDayPatObservRecs = new List<EFPatObservRec>();
                toDayPatObservRecs = nursingFormRecBLL.GetToDayPatNurseData(pid, pvid, baby, formId);

                recDetails = toDayPatObservRecs.Adapt<List<ViewToDayNurseRecDetail>>();
                ViewToDayNurseRecDetail rec = new ViewToDayNurseRecDetail();
                for (int i = 0; i < recDetails.Count; i++)
                {
                    rec = recDetails[i];
                    ViewToDayNurseRec nurseRec = new ViewToDayNurseRec();
                    nurseRec.ItemRecCount = recDetails.Where(o => o.ObservItemId == rec.ObservItemId).Count();
                    nurseRec.RecDetail = recDetails.Where(o => o.ObservItemId == rec.ObservItemId).ToList();
                    nurseRec.ObservItemId = rec.ObservItemId;
                    recDetails.RemoveAll(o => o.ObservItemId == rec.ObservItemId);
                    viewToDayNurseRecs.Add(nurseRec);
                }
                formInfo.Pid = pid;
                formInfo.Pvid = pvid;
                formInfo.Baby = baby.ToString();

                viewForm = form.Adapt<ViewFormList>();

                viewForm.FormName = form.FormName;

            }


            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };


            viewForm.FormType = "n";
            viewFormLists.Add(viewForm);


            ViewBag.viewFormLists = JsonConvert.SerializeObject(viewFormLists, settings);

            ViewBag.formInfo = JsonConvert.SerializeObject(formInfo);
            ViewBag.bloodpressureItem = JsonConvert.SerializeObject(BloodItem.血压项目);
            ViewBag.toDayNurseRec = JsonConvert.SerializeObject(viewToDayNurseRecs);


            return View(form);
        }

        // GET: /<controller>/
        /// <summary>
        /// 病人管道护理评估
        /// </summary>
        /// <param name="formId">表单id，多个表单用*分隔</param>
        /// <param name="pid">病人id</param>
        /// <param name="pvid">就诊id</param>
        /// <param name="baby">婴儿标准</param>
        /// <param name="recoder">记录者id</param>
        /// <param name="wardId">病区id</param>
        /// <param name="formType">表单类型，用于修改表单时</param>
        /// <param name="recId">病人表单记录id，用于修改表单时</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Supdev(string pid, string pvid, int baby, string recoder, int wardId, string recId, bool watchModel = false)
        {
            ViewBag.Title = "管道评估单";
            ViewBag.WatchModel = JsonConvert.SerializeObject(watchModel);
            //配置病人管道记录和前端表单的对应关系
            TypeAdapterConfig<PatSupdevList, ViewFormList>.NewConfig()
                      .ShallowCopyForSameType(true)
                      .PreserveReference(true)
                      .Map(j => j.RecId, k => k.PatSupdevId)
                      .Map(j => j.FormId, k => k.SupdevTypeId)
                      .Map(j => j.FormItems, k => k.PatSupdevDatas)
                      ;
            //配置病人管道数据和前端表单数据的关系
            TypeAdapterConfig<FormData, ViewFormItem>.NewConfig()
                  .ShallowCopyForSameType(true)
                  .PreserveReference(true)
                  .Map(j => j.ItemId, k => k.ItemId)
                  .Map(j => j.ItemName, k => k.ItemName)
                  .Map(j => j.DetailValue, k => k.Value)
                  ;
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            //ViewBag.HighRisk = JsonConvert.SerializeObject(ScaleRiskLevel.高);
            //ViewBag.MiddleRisk = JsonConvert.SerializeObject(ScaleRiskLevel.中);
            //ViewBag.LowRisk = JsonConvert.SerializeObject(ScaleRiskLevel.低);

            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.errofInfo = "";

            //前端的表单数据对象
            List<ViewFormList> viewFormLists = new List<ViewFormList>();
            List<FormList> formLists = new List<FormList>();

            dynamic formInfo = new JObject();
            formInfo.Recorder = recoder;
            formInfo.WardId = wardId;
            formInfo.RecId = recId;
            formInfo.EncType = 2;//不支持门诊，固定为住院
            //新增模式下，获取表单信息
            if (recId == null)
            {

                #region 参数检查
                if (pid == null || pid.Length == 0)
                {
                    errorViewModel.errofInfo = "无pid参数";
                }
                if (pvid == null || pvid.Length == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无pvid参数";
                }
                if (recoder == null || recoder.Length == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无recoder参数";
                }
                if (wardId == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无wardId参数";
                }
                if (errorViewModel.errofInfo != "")
                {
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }
                #endregion

            }

            int formRecid = 0;

            int.TryParse(recId, out formRecid);


            if (errorViewModel.errofInfo != "")
            {
                return RedirectToAction("Error", "NForm", errorViewModel);
            }

            //病人的管道记录
            List<PatSupdevList> patSupdevLists = new List<PatSupdevList>();

            SupdevBLL supdevBLL = new SupdevBLL();

            //获取操作员的姓名
            var tempNurse = memory.Get<List<Nurse>>("NurseList").Where(o => o.NurseId == recoder).FirstOrDefault();

            if (tempNurse != null)
            {
                formInfo.RecoderName = tempNurse.NurseAlias.ToString();
            }
            //获取病区的名称
            var tempWard = memory.Get<List<Ward>>("WardList").Where(o => o.WardId == wardId).FirstOrDefault();
            if (tempWard != null)
            {
                formInfo.WardName = tempWard.WardName.ToString();
            }


            if (formRecid > 0)
            {
                formInfo.EditType = "update";
                PatSupdevList patSupdevList = new PatSupdevList();


                patSupdevList = supdevBLL.GetPatSupdevData(formRecid);
                if (patSupdevList == null)
                {
                    errorViewModel.errofInfo = "未查询到对应的管道评估记录，无法修改，记录ID:" + formRecid;
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }
                patSupdevLists.Add(patSupdevList);
                formLists = supdevBLL.GetPatUsingSupdevFormList(patSupdevList.SupdevTypeId, patSupdevList.PatSupdevId);
                if (formLists == null)
                {
                    errorViewModel.errofInfo = "未查询到对应的管道评估单，无法修改，记录ID:" + formRecid;
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }

                viewFormLists = patSupdevLists.Adapt<List<ViewFormList>>();
                viewFormLists[0].SupdevTypeName = formLists[0].FormName;

                viewFormLists[0].FormType = "s";

                viewFormLists[0].SupdevTypeId = patSupdevList.SupdevTypeId;
                viewFormLists[0].PatSupdevId = patSupdevList.PatSupdevId;
                viewFormLists[0].PatSupdevName = patSupdevList.PatSupdevName;

                formInfo.Pid = patSupdevList.Pid;
                formInfo.Pvid = patSupdevList.Pvid;
                formInfo.Baby = "0";//评分表没有婴儿


            }
            else
            {
                formInfo.EditType = "insert";
                formInfo.Pid = pid;
                formInfo.Pvid = pvid;
                formInfo.Baby = baby.ToString();//评分表没有婴儿的

                //获取病人的管道记录
                patSupdevLists = supdevBLL.GetPatUseingSupdevList(pid, pvid);
                if (patSupdevLists == null)
                {
                    errorViewModel.errofInfo = "病人无管道可评估，病人ID:" + pid + "，就诊ID：" + pvid;
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }
                //获取病人管道记录对应的表单
                formLists = supdevBLL.GetPatUsingSupdevFormList(patSupdevLists);
                if (patSupdevLists == null)
                {
                    errorViewModel.errofInfo = "病人管道未找到对应的评估单，病人ID:" + pid + "，就诊ID：" + pvid;
                    return RedirectToAction("Error", "NForm", errorViewModel);
                }


                viewFormLists = formLists.Adapt<List<ViewFormList>>();
                for (int i = 0; i < viewFormLists.Count; i++)
                {
                    PatSupdevList patSupdev = new PatSupdevList();
                    viewFormLists[i].SupdevTypeId = viewFormLists[i].FormId.Split("_")[0];
                    viewFormLists[i].PatSupdevId = int.Parse(viewFormLists[i].FormId.Split("_")[1]);
                    viewFormLists[i].PatSupdevName = viewFormLists[i].FormName;
                    viewFormLists[i].SupdevTypeName = viewFormLists[i].FormName.Split("-")[1];
                }

            }


            ViewBag.viewFormLists = JsonConvert.SerializeObject(viewFormLists);
            ViewBag.bloodpressureItem = JsonConvert.SerializeObject(BloodItem.血压项目);
            ViewBag.formInfo = JsonConvert.SerializeObject(formInfo);
            return View(formLists);
        }

        [HttpPost]
        //接收前端提交表单，FormName：变量直接传参，form：Model传参数
        public IActionResult Index(string FormName, FormList form)
        {
            //跳转页面
            return RedirectToAction("IndexNew");
        }
        [HttpGet]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {

            return View(errorViewModel);
        }
        /// <summary>
        /// 风险评估单界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RiskIndex(string pid, string pvid, int baby, string recoder, int wardId, string formId, string formType, string recId, bool watchModel = false)
        {
            ViewBag.Title = "管道评估单";
            ViewBag.WatchModel = JsonConvert.SerializeObject(watchModel);
            TypeAdapterConfig<PatScaleFormRec, ViewFormList>.NewConfig()
                     .ShallowCopyForSameType(true)
                     .PreserveReference(true)
                     .Map(j => j.RecId, k => k.ScaleRecId)
                     .Map(j => j.FormId, k => k.ScaleListId)
                     .Map(j => j.FormVersion, k => k.ScaleVersion)
                     .Map(j => j.FormItems, k => k.PatScaleFormRecDetails)
                     ;
            TypeAdapterConfig<PatScaleFormRecDetail, ViewFormItem>.NewConfig()
                .ShallowCopyForSameType(true)
                .PreserveReference(true)
                .Map(j => j.ItemId, k => k.ScaleItemId)
                .Map(j => j.ItemName, k => k.ScaleItemName)
                .Map(j => j.PatScaleFormComboItemDetails, k => k.PatScaleFormComboItemDetail)
                ;


            TypeAdapterConfig<PatAsFormRec, ViewFormList>.NewConfig()
                      .ShallowCopyForSameType(true)
                      .PreserveReference(true)
                      .Map(j => j.RecId, k => k.AsFormRecId)
                      .Map(j => j.FormId, k => k.FormId)
                      .Map(j => j.FormVersion, k => k.FormVersion)
                      .Map(j => j.FormItems, k => k.PatAsFormRecDetail)
                      ;
            TypeAdapterConfig<PatAsFormRecDetail, ViewFormItem>.NewConfig()
               .ShallowCopyForSameType(true)
               .PreserveReference(true)
               .Map(j => j.ItemId, k => k.AsFormItemId)
               .Map(j => j.ItemName, k => k.AsFormItemName)
               .Map(j => j.PatScaleFormComboItemDetails, k => k.PatAsFormComboItemDetail)
               ;
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ViewBag.HighRisk = JsonConvert.SerializeObject(ScaleRiskLevel.高);
            ViewBag.MiddleRisk = JsonConvert.SerializeObject(ScaleRiskLevel.中);
            ViewBag.LowRisk = JsonConvert.SerializeObject(ScaleRiskLevel.低);
            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.errofInfo = "";

            List<ViewFormList> viewFormLists = new List<ViewFormList>();
            List<FormList> formLists = new List<FormList>();


            dynamic formInfo = new JObject();

            if (formType == null || formType.Length == 0)
            {
                formType = "r";
            }


            //新增模式下，获取表单信息
            if (recId == null)
            {

                #region 参数检查

                if (pid.Length == 0)
                {
                    errorViewModel.errofInfo = "无pid参数";
                }
                if (pvid.Length == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无pvid参数";
                }
                if (recoder.Length == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无recoder参数";
                }
                if (wardId == 0)
                {
                    errorViewModel.errofInfo = errorViewModel.errofInfo + ",无wardId参数";
                }
                //if (errorViewModel.errofInfo != "")
                //{
                //    return RedirectToAction("Error", "NForm", errorViewModel);
                //}
                #endregion

                formLists = memory.Get<List<FormList>>("FormList").Where(o => formId.Contains(o.FormId)).ToList();

                if (formLists == null)
                {
                    errorViewModel.errofInfo = "未查询到对应的评分评估表，评分评估表ID:" + formId;
                }

                //if (errorViewModel.errofInfo != "")
                //{
                //    return RedirectToAction("Error", "NForm", errorViewModel);
                //}
            }

            int formRecid = 0;

            int.TryParse(recId, out formRecid);


            //if (errorViewModel.errofInfo != "")
            //{
            //    return RedirectToAction("Error", "NForm", errorViewModel);
            //}

            List<PatScaleFormRec> patScaleForms = new List<PatScaleFormRec>();
            NursingFormBLL nursingFormBLL = new NursingFormBLL();

            List<PatAsFormRec> patAsFormRecs = new List<PatAsFormRec>();
            AsFormBLL asFormBLL = new AsFormBLL();
            if (formRecid > 0)
            {
                //修改护理记录单,查询护理记录的数据

                formInfo.EditType = "update";

                if (formType == "r")
                {

                    //评分表修改
                    PatScaleFormRec patScaleFormRec = new PatScaleFormRec();
                    patScaleFormRec = nursingFormBLL.GetPatScaleFormRec(formRecid);
                    if (patScaleFormRec == null)
                    {
                        errorViewModel.errofInfo = "未查询到对应的表单，表单ID:" + recId;
                        return RedirectToAction("Error", "NForm", errorViewModel);
                    }
                    patScaleForms.Add(patScaleFormRec);
                    formLists = memory.Get<List<FormList>>("FormList").Where(o => o.FormId == patScaleFormRec.ScaleListId && o.FormVersion == patScaleFormRec.ScaleVersion).ToList();


                    formInfo.Pid = patScaleFormRec.Pid;
                    formInfo.Pvid = patScaleFormRec.Pvid;
                    formInfo.Baby = 0;//评分表没有婴儿

                    viewFormLists = patScaleForms.Adapt<List<ViewFormList>>();
                    //获取表单的分数
                    if (formLists != null)
                    {
                        viewFormLists[0].FormScores = formLists[0].FormScores;
                    }

                    else
                    {
                        errorViewModel.errofInfo = "未查询到对应的表单定义，表单ID:" + patScaleFormRec.ScaleListId + ",版本:" + patScaleFormRec.ScaleVersion;
                        return RedirectToAction("Error", "NForm", errorViewModel);
                    }
                }
                else
                {

                    PatAsFormRec patAsFormRec = new PatAsFormRec();

                    patAsFormRec = asFormBLL.GetPatAsFormRec(formRecid);
                    if (patAsFormRec == null)
                    {
                        errorViewModel.errofInfo = "未查询到对应的表单，表单ID:" + recId;
                        return RedirectToAction("Error", "NForm", errorViewModel);
                    }

                    patAsFormRecs.Add(patAsFormRec);
                    formLists = memory.Get<List<FormList>>("FormList").Where(o => o.FormId == patAsFormRec.FormId && o.FormVersion == patAsFormRec.FormVersion).ToList();
                    if (formLists == null)
                    {
                        errorViewModel.errofInfo = "未查询到对应的表单定义，表单ID:" + patAsFormRec.FormId + ",版本:" + patAsFormRec.FormVersion;
                        return RedirectToAction("Error", "NForm", errorViewModel);
                    }
                    formInfo.Pid = patAsFormRec.Pid;
                    formInfo.Pvid = patAsFormRec.Pvid;
                    formInfo.Baby = 0;//评评表没有婴儿

                    viewFormLists = patAsFormRecs.Adapt<List<ViewFormList>>();
                }
            }
            else
            {
                formLists = memory.Get<List<FormList>>("FormList").Where(o => formId.Contains(o.FormId) && o.EnableSign == 1).ToList();
                for (int i = 0; i < formLists.Count; i++)
                {
                    //新增，如果一个评分表，就加载上次评分数据
                    if (formLists[i].FormType == "r")
                    {
                        //获取上次护理记录;PatNurseDataRec
                        PatScaleFormRec tempScaleFormRec = new PatScaleFormRec();
                        tempScaleFormRec = nursingFormBLL.GetLastPatScaleFormRecs(pid, pvid, baby, formId);
                        if (tempScaleFormRec != null)
                        {
                            patScaleForms.Add(tempScaleFormRec);
                        }
                        ViewFormList viewForm = new ViewFormList();
                        viewForm = tempScaleFormRec.Adapt<ViewFormList>();
                        if (viewForm == null)
                        {
                            viewForm = formLists[i].Adapt<ViewFormList>();
                        }
                        // 获取表单的分数
                        if (formLists != null)
                        {
                            viewForm.FormScores = formLists[i].FormScores;
                        }
                        viewFormLists.Add(viewForm);
                    }
                }
                formInfo.Pid = pid;
                formInfo.Pvid = pvid;
                formInfo.Baby = baby;//评分表没有婴儿的


                formInfo.EditType = "insert"; ;
            }


            ViewBag.formInfo = JsonConvert.SerializeObject(formInfo);
            ViewBag.viewFormLists = JsonConvert.SerializeObject(viewFormLists);
            return View(formLists);
        }

        /// <summary>
        /// 保存评分评估表
        /// </summary>
        /// <param name="formResult"></param>
        /// <param name="editType"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveForms([FromBody] List<SaveForm> formResult, string editType)
        {
            SaveFormsBLL saveFormsBLL = new SaveFormsBLL();
            string result = saveFormsBLL.SaveForms(formResult, editType);
            return Ok(result);
        }
        /// <summary>
        /// 保存护理记录
        /// </summary>
        /// <param name="saveForm"></param>
        /// <param name="editType"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveNurseRec([FromBody] List<SaveForm> saveForm, string editType)
        {
            SaveFormsBLL saveFormsBLL = new SaveFormsBLL();
            string result = saveFormsBLL.SaveNurseRec(saveForm, editType);
            return Ok(result);
        }

        /// <summary>
        /// 保存管道评估
        /// </summary>
        /// <param name="saveForm"></param>
        /// <param name="editType"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveSupdev([FromBody] List<SaveForm> saveForm, string editType)
        {
            SaveFormsBLL saveFormsBLL = new SaveFormsBLL();
            string result = saveFormsBLL.SaveSupdev(saveForm, editType);
            return Ok(result);
        }
        /// <summary>
        /// 获取护理记录单的病情观察和措施供前端缓存
        /// </summary>
        /// <param name="lastTime">前端上次获取时间</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPhrases(string lastTime)
        {
            JObject result = new JObject();
            if (lastTime == null || lastTime == "")
            {
                lastTime = "1900-01-01";
            }
            NursingFormRecBLL nursingFormRecBLL = new NursingFormRecBLL();

            result = nursingFormRecBLL.GetPhrases(lastTime);

            return Ok(result);
        }
    }
}
