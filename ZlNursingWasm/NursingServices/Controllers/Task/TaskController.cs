using NursingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [ApiController]
    [Route("api/[controller]")]
    [Route("bh/[controller]")]
    [Route("pda/[controller]")]
    public class TaskController : ApiController
    {
        /// <summary>
        /// bh解析控制器下所有接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ServicesStructure> servicesStructures = BaseFunction.GetControllerInfo<TaskController>();
            return Json(servicesStructures);
        }

        /// <summary>
        /// 获取病人的任务列表，包括医嘱分解任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="hasOrder">是否包括医嘱分解任务</param>
        /// <returns></returns>
        [HttpPost("GetPatientTask")]
        public IActionResult GetPatientTask(string pid, string pvid, bool hasOrder = false)
        {
            return Json("");
        }

        ///// <summary>
        ///// 查询建病人入院任务数据
        ///// </summary>
        ///// <param name="pid">病人ID</param>
        ///// <param name="pvid">主页ID</param>
        ///// /// <param name="eventObjectID">病人变动记录ID</param>
        ///// <param name="time">入院时间</param>
        ///// <param name="wardID">病区ID</param>
        ///// <returns></returns>
        //[HttpGet("AdmissionTask")]
        //public IActionResult AdmissionTask(string pid, string pvid, string eventObjectId, DateTime time, string wardId)
        //{
        //    FormTaskBLL deptInTask = new FormTaskBLL();
        //    List<PatNursingTask> taskList = new List<PatNursingTask>();
        //    taskList = deptInTask.AdmissionTask(pid, pvid, eventObjectId, time, wardId);
        //    return Json(taskList);
        //}
        /// <summary>
        /// 查询建病人入院、转科入科任务数据
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// /// <param name="eventObjectID">病人变动记录ID</param>
        /// <param name="time">入院时间</param>
        /// <param name="wardID">病区ID</param>
        /// <returns></returns>
        [HttpGet("AdmissionTask")]
        [DisplayName(name = "查询建病人入院、转科入科任务数据")]
        [Note(name = "查询建病人入院、转科入科任务数据")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AdmissionTask(string pid, string pvid, string eventObjectId, DateTime time, string wardId,
            int way, decimal baby = 0)
        {
            FormTaskBLL deptInTask = new FormTaskBLL();
            TaskBLL nurseTask = new TaskBLL();
            List<PatNursingTask> taskList = new List<PatNursingTask>();
            taskList = deptInTask.AdmissionTask(pid, pvid, eventObjectId, time, wardId, way, baby);
            foreach (PatNursingTask task in taskList)
            {
                nurseTask.AddTaskBLL(task);
            }

            return Ok();
        }

        /// <summary>
        /// 完成评分表后生成任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="eventObjectId">完成的表单ID</param>
        /// <param name="startTime">完成表单时间</param>
        /// <param name="formscale">表单分数</param>
        /// <returns></returns>
        [HttpGet("FormFinishTask")]
        [DisplayName(name = "完成评分表后生成任务")]
        [Note(name = "完成评分表后生成任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult FormFinishTask(string pid, string pvid, string wardId, string eventObjectId,
            DateTime startTime, string formscale, decimal baby = 0)
        {
            FormTaskBLL deptInTask = new FormTaskBLL();
            TaskBLL nurseTask = new TaskBLL();
            List<PatNursingTask> taskList = new List<PatNursingTask>();
            taskList = deptInTask.FormFinishTask(pid, pvid, wardId, eventObjectId, startTime, formscale, baby);
            foreach (PatNursingTask task in taskList)
            {
                nurseTask.AddTaskBLL(task);
            }

            return Ok();
            //return Json(taskList);
        }

        /// <summary>
        /// 完成表单后生成任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="eventObjectId">完成的表单ID</param>
        /// <param name="info">参数</param>
        /// <param name="formscale">表单分数</param>
        /// <returns></returns>
        [HttpPost("FinishFormCreateTask")]
        [DisplayName(name = "完成表单后生成任务")]
        [Note(name = "完成表单后生成任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult FinishFormCreateTask(FinishFormTask info)
        {
            FormTaskBLL deptInTask = new FormTaskBLL();
            TaskBLL nurseTask = new TaskBLL();
            List<PatNursingTask> taskList = new List<PatNursingTask>();
            DateTime startTime = DateTime.Now;
            string pid = info.Pid;
            string pvid = info.Pvid;
            string wardId = info.WardId;
            string eventObjectId = info.EventObjectId;
            string formscale = info.FormScale;
            decimal baby = info.Baby;
            taskList = deptInTask.FinishFormCreateTask(pid, pvid, wardId, eventObjectId, startTime, formscale, baby);
            foreach (PatNursingTask task in taskList)
            {
                nurseTask.AddTaskBLL(task);
            }
            return Ok();
        }

        #region 护理任务模块

        /// <summary>
        /// 新增护理任务
        /// </summary>
        /// <param name="tasks">任务详细信息</param>
        /// <returns></returns>
        [HttpPost("AddTask")]
        public IActionResult AddTask(List<PatNursingTask> tasks)
        {
            try
            {
                foreach (var task in tasks)
                {
                    TaskBLL nurseTask = new TaskBLL();
                    var taskInfo = nurseTask.AddTaskBLL(task);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 医嘱产生的护理任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="orderItemId">诊断项目ID</param>
        /// <param name="taskEventCode">来源系统编码</param>
        /// <param name="taskTypeCode">任务类型</param>
        /// <param name="info">参数</param>
        /// <returns></returns>
        [HttpPost("CreateTask")]
        [DisplayName(name = "医嘱产生的护理任务")]
        [Note(name = "医嘱产生的护理任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult CreateTask(List<FinishFormTask> infos)
        {
            try
            {
                foreach (var info in infos)
                {
                    string pid = info.Pid;
                    string pvid = info.Pvid;
                    string wardId = info.WardId;
                    string orderItemId = info.OrderItemId;
                    string taskEventCode = info.TaskEventCode;
                    string taskTypeCode = info.TaskTypeCode;
                    decimal baby = info.Baby;
                    TaskBLL nurseTask = new TaskBLL();
                    var taskInfo = nurseTask.GetNeedAddTask(pid, pvid, wardId, orderItemId, taskEventCode, taskTypeCode, baby);
                    foreach (var item in taskInfo)
                    {
                        var result = AddTask(new List<PatNursingTask>() { item });
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取未执行的护理任务
        /// </summary>
        /// <returns></returns>
        [HttpGet("TaskList")]
        public IActionResult TaskList()
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                var taskList = nurseTask.GetTaskList();
                return Json(new { TaskList = taskList });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行护理任务
        /// </summary>
        /// <param name="tasks">任务详细信息(可同时执行多条)</param>
        /// <returns></returns>
        [HttpPost("DoTask")]
        [DisplayName(name = "执行护理任务")]
        [Note(name = "执行护理任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DoTask(List<PatNursingTaskFinish> tasks)
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                var result = nurseTask.DoTask(tasks);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return Json(new { Status = 0, Msg = "执行任务失败" });
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据taskObjectId串执行护理任务
        /// </summary>
        /// <param name="task">病人ID、主页ID、操作对象ID、任务类型、任务对象ID、执行人</param>
        /// <returns></returns>
        [HttpPost("DoTaskByTaskObjectId")]
        [DisplayName(name = "执行护理任务")]
        [Note(name = "执行护理任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DoTaskByTaskObjectId(PatNursingTaskFinish task)
        {
            string pid = task.Pid;
            string pvid = task.Pvid;
            string eventObjectId = task.EventObjectId;
            string taskTypeCode = task.TaskTypeCode;
            string taskObjectId = task.TaskObjectId;
            string executor = task.Executor;
            if (task.TaskTypeCode.ToUpper() == "TFORM")
            {
                executor = Encoding.UTF8.GetString(Convert.FromBase64String(task.Executor.Replace("%2B", "+")
                    .Replace("%2F", "/").Replace("%3D", "=").Replace(" ", "+")));
            }

            TaskBLL nurseTask = new TaskBLL();
            var taskList = nurseTask.GetTaskListByType(pid, pvid, eventObjectId, taskTypeCode, taskObjectId, "", "", "",
                "", "", task.Baby).ToList();
            IList<PatNursingTaskFinish> tasks = new List<PatNursingTaskFinish>();
            var taskInfo = new PatNursingTaskFinish();
            task.ExecuteTime = DateTime.Now;
            DateTime? minTime = DateTime.Now;
            //获取所有任务创建时间的最早时间
            foreach (var item in taskList)
            {
                if (item.CreateTime < minTime)
                {
                    minTime = item.CreateTime;
                }
            }
            string taskObjectIds = "";
            foreach (var item in taskList)
            {
                taskInfo = new PatNursingTaskFinish();
                taskInfo.Pid = item.Pid;
                taskInfo.Pvid = item.Pvid;
                taskInfo.EventObjectId = item.EventObjectId;
                taskInfo.TaskTypeCode = item.TaskTypeCode;
                taskInfo.TaskObjectId = item.TaskObjectId;
                taskInfo.Executor = executor;
                taskInfo.BeginTime = item.BeginTime;
                taskInfo.CreateTime = item.CreateTime;
                taskInfo.EventCode = item.TaskEventCode;
                taskInfo.ExectueDescript = item.ExectueDescript;
                taskInfo.RequestFinishTime = item.RequestFinishTime;
                taskInfo.ResponsibleId = item.ResponsibleId;
                taskInfo.TaskEventCode = item.TaskEventCode;
                taskInfo.TaskId = item.TaskId;
                taskInfo.Title = item.Title;
                taskInfo.WardId = item.WardId;
                if (item.CreateTime == minTime || !taskObjectIds.Contains(item.TaskObjectId))
                {
                    tasks.Add(taskInfo);
                }
                taskObjectIds += item.TaskObjectId + ",";
            }

            var result = DoTask(tasks.ToList());
            return result;
        }

        /// <summary>
        ///轮询获取护理任务队列，产生任务
        /// </summary>
        /// <returns></returns>
        [HttpPost("AutoTask")]
        public IActionResult AutoTask()
        {
            //获取本地任务队列
            TaskBLL nurseTask = new TaskBLL();
            var localTask = nurseTask.GetTaskLocalQueue();
            if (localTask.Count > 0)
            {
                foreach (var item in localTask)
                {
                    var taskInfo = JsonConvert.DeserializeObject<PatNursingTask>(item.TaskJson);
                    taskInfo.Pid = item.Pid;
                    taskInfo.Pvid = item.Pvid;
                    //taskInfo.EventCode = item.EventCode;
                    taskInfo.EventObjectId = item.EventObjectId;
                    var addTask = nurseTask.AddTaskBLL(taskInfo);
                    //创建任务成功则删除该队列
                    if (addTask)
                    {
                        var delTask = nurseTask.DeleteTaskLocalQueue(item.EventCode, item.EventObjectId);
                    }
                }
            }

            return Ok();
        }

        /// <summary>
        /// 病人出院或转科删除护理任务
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <returns></returns>
        [HttpPost("OutHuspitalTask")]
        public IActionResult OutHuspitalTask(string patientId)
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                bool delTask = nurseTask.DeleteTaskByPatient(patientId);
                if (delTask)
                {
                    return Ok();
                }

                return Json(new { Status = 0, Msg = "删除任务失败" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 通过病人ID查询这部分病人的护理任务（病人ID拼接成字符串，例如："'4355969','123','4358178','111'"）
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <returns></returns>
        [HttpPost("GetTaskListByPatient")]
        [DisplayName(name = "通过病人ID查询这部分病人的护理任务")]
        [Note(name = "通过病人ID查询这部分病人的护理任务")]
        [ParaOutName(name = "TaskList")]
        [SchemaContent(name = "")]
        public IActionResult GetTaskListByPatient(string patientId)
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                var taskList = nurseTask.GetTaskListByPatient(patientId);
                return Json(new { TaskList = taskList });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 通过病人ID查询该病人当天已完成的护理任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        [HttpGet("GetFinishTaskByPatient")]
        [DisplayName(name = "通过病人ID查询该病人当天已完成的护理任务")]
        [Note(name = "通过病人ID查询该病人当天已完成的护理任务")]
        [ParaOutName(name = "GetFinishTaskByPatient")]
        [SchemaContent(name = "")]
        public IActionResult GetFinishTaskByPatient(string pid, string pvid, string beginTime, string endTime)
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                var taskList = nurseTask.GetFinishTaskByPatient(pid, pvid, beginTime, endTime, "体温");
                return Json(new { TaskList = taskList });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 通过病人ID.主页ID、操作对象ID、任务类型、任务对象ID、病区ID查询护理任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="eventObjectId">操作对象ID</param>
        /// <param name="taskTypeCode">任务类型</param>
        /// <param name="taskObjectId">任务对象ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="responsibleId">操作对象id，操作对应的业务对象ID。比如入科就是病人变得记录id</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="sign">是否查询体温任务</param>
        /// <param name="nbSno">婴儿序号</param>
        /// <param name="code">护理任务编码</param>
        /// <returns></returns>
        [HttpGet("GetTaskListByType")]
        [DisplayName(name = "通过病人ID.主页ID、操作对象ID、任务类型、任务对象ID、病区ID查询护理任务")]
        [Note(name = "通过病人ID.主页ID、操作对象ID、任务类型、任务对象ID、病区ID查询护理任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetTaskListByType(string pid = "", string pvid = "", string eventObjectId = "",
            string taskTypeCode = "", string taskObjectId = "", string wardId = "", string responsibleId = "",
            string beginTime = "", string endTime = "", string sign = "", decimal nbSno = 0, string code = "")
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                List<PatNursingTaskList> taskList = new List<PatNursingTaskList>();
                taskList = nurseTask.GetTaskListByType(pid, pvid, eventObjectId, taskTypeCode, taskObjectId, wardId,
                    responsibleId, beginTime, endTime, sign, nbSno, code).ToList();
                return Json(taskList);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 通过任务ID获取任务内容
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        [HttpGet("GetTaskById")]
        [DisplayName(name = "通过任务ID获取任务内容")]
        [Note(name = "通过任务ID获取任务内容")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetTaskById(decimal id)
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                PatNursingTask taskList = new PatNursingTask();
                taskList = nurseTask.GetTaskById(id);
                return Json(taskList);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 病人撤销操作删除护理任务
        /// </summary>
        /// <param name="id">病人ID</param>
        /// <param name="objectId">操作对象ID</param>
        /// <returns></returns>
        /// <summary>
        [HttpGet("RevockTask")]
        [DisplayName(name = "病人撤销操作删除护理任务")]
        [Note(name = "病人撤销操作删除护理任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult RevockTask(string id, string objectId, string eventCode, DateTime inTime)
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                bool delTask = nurseTask.DelTaskByPatientObject(id, objectId, eventCode, inTime);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 通过病人ID、主页ID、操作对象ID查询护理任务
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <param name="eventObjectId">操作对象ID</param>
        /// <returns></returns>
        [HttpGet("GetTaskListCount")]
        [DisplayName(name = "通过病人ID、主页ID、操作对象ID查询护理任务")]
        [Note(name = "通过病人ID、主页ID、操作对象ID查询护理任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult GetTaskListCount(string pid, string pvid, string eventObjectId, string eventCode, DateTime inTime)
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                List<PatNursingTaskList> taskList = new List<PatNursingTaskList>();
                taskList = nurseTask.GetTaskListCount(pid, pvid, eventObjectId, eventCode, inTime).ToList();
                return Json(taskList);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 通过任务ID删除护理任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns></returns>
        [HttpGet("DeleteTaskByTaskId")]
        [DisplayName(name = "通过任务ID删除护理任务")]
        [Note(name = "通过任务ID删除护理任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DeleteTaskByTaskId(decimal taskId)
        {
            try
            {
                TaskBLL nurseTask = new TaskBLL();
                var result = nurseTask.DeleteTask(taskId);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行任务并创建任务
        /// </summary>
        /// <param name="task">任务参数</param>
        /// <returns></returns>
        [HttpPost("DoTaskAndCreateTask")]
        [DisplayName(name = "执行任务并创建任务")]
        [Note(name = "执行任务并创建任务")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult DoTaskAndCreateTask(FinishFormTask task)
        {
            try
            {
                //执行护理任务
                var taskInfo = new PatNursingTaskFinish();
                taskInfo.Pid = task.Pid;
                taskInfo.Pvid = task.Pvid;
                taskInfo.EventObjectId = task.EventObjectId1;
                taskInfo.TaskTypeCode = task.TaskTypeCode;
                taskInfo.TaskObjectId = task.TaskObjectId;
                taskInfo.Executor = task.Executor;
                taskInfo.ExecuteTime = task.ExecuteTime;
                taskInfo.BeginTime = task.BeginTime;
                taskInfo.CreateTime = task.CreateTime;
                taskInfo.EventCode = task.TaskEventCode;
                taskInfo.ExectueDescript = task.ExectueDescript;
                taskInfo.RequestFinishTime = task.RequestFinishTime;
                taskInfo.ResponsibleId = task.ResponsibleId;
                taskInfo.TaskEventCode = task.TaskEventCode;
                taskInfo.TaskId = task.TaskId;
                taskInfo.Title = task.Title;
                taskInfo.WardId = task.WardId;
                taskInfo.Baby = task.Baby;
                DoTaskByTaskObjectId(taskInfo);
                //完成表单后生成任务
                FormTaskBLL deptInTask = new FormTaskBLL();
                TaskBLL nurseTask = new TaskBLL();
                List<PatNursingTask> taskList = new List<PatNursingTask>();
                DateTime startTime = DateTime.Now;
                taskList = deptInTask.FinishFormCreateTask(task.Pid, task.Pvid, task.WardId, task.EventObjectId2, startTime, task.FormScale, task.Baby);
                string eventObjectIds = "";
                foreach (PatNursingTask item in taskList)
                {
                    nurseTask.AddTaskBLL(item);
                    if (!eventObjectIds.Contains(item.EventObjectId))
                    {
                        eventObjectIds += item.EventObjectId + ",";
                    }
                }
                eventObjectIds = eventObjectIds.Length > 0 ? eventObjectIds.Substring(0, eventObjectIds.Length - 1) : eventObjectIds;
                //填写表单后产生宣教任务
                HeduTaskBLL heduTaskBLL = new HeduTaskBLL();
                var heduFormTask = new HeduFormTask();
                heduFormTask.Baby = task.Baby;
                heduFormTask.EventObjectIds = eventObjectIds;
                heduFormTask.FormIds = task.FormIds;
                heduFormTask.FormScales = task.FormScale;
                heduFormTask.Pid = task.Pid;
                heduFormTask.Pvid = task.Pvid;
                heduFormTask.TaskEventCode = task.TaskEventCode;
                heduFormTask.WardId = task.WardId;
                heduTaskBLL.AddFormHeduTask(heduFormTask);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region 患者体温测量

        /// <summary>
        /// 获得指定病人体温测量类型改变记录
        /// </summary>
        /// <param name="pid">病人ID</param>
        /// <param name="pvid">主页ID</param>
        /// <returns></returns>
        [HttpGet("GetPatientTemperatureTypeLog")]
        [DisplayName(name = "获得指定病人体温测量类型改变记录")]
        [Note(name = "获得指定病人体温测量类型改变记录")]
        [ParaOutName(name = "PatSignTypeChangeRec")]
        [SchemaContent(name = "PatSignTypeChangeRec")]
        public IActionResult GetPatientTemperatureTypeLog(string pid, string pvid, int nbSno)
        {
            if (string.IsNullOrEmpty(pid) || string.IsNullOrEmpty(pvid))
            {
                return Json(new { Status = 0, Msg = "入参错误，请检查" });
            }

            var bll = new TemperatureMeasBLL();
            return Ok(bll.GetPatSignTypeChangeRecBLL(pid, pvid, nbSno));
        }

        /// <summary>
        /// 新增、修改、删除病人体温测量类型
        /// </summary>
        /// <param name="item">数据</param>
        /// <returns></returns>
        [HttpPost("AddOrModifyOrDelPatSignType")]
        [DisplayName(name = "新增、修改、删除病人体温测量类型")]
        [Note(name = "新增、修改、删除病人体温测量类型")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult AddOrModifyOrDelPatSignType(PatSignType item)
        {
            try
            {
                if (item == null)
                {
                    return Json(new { Status = 0, Msg = "参数错误" });
                }

                var bll = new TemperatureMeasBLL();
                var res = false;
                if (item.IsNew == 0)
                {
                    res = bll.ModifyPatSignTypeBLL(item);
                    bll.AddPatSignTypeChangeRecBLL(item);
                }
                else if (item.IsNew == 1)
                {
                    res = bll.AddPatSignTypeBLL(item);
                }
                else
                {
                    res = bll.DelPatSignTypeBLL(item);
                }

                if (res)
                {
                    return Ok();
                }
                else
                {
                    return Json(new { Status = 0, Msg = "操作失败" });
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw;
            }
        }

        /// <summary>
        /// 批量修改病人体温测量类型
        /// </summary>
        /// <param name="items">数据</param>
        /// <returns></returns>
        [HttpPost("BacthModifyPatSignType")]
        [DisplayName(name = "批量修改病人体温测量类型")]
        [Note(name = "批量修改病人体温测量类型")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult BacthModifyPatSignType(PatSignType[] items)
        {
            try
            {
                if (items == null || items.Length < 1)
                {
                    return Json(new { Status = 0, Msg = "参数错误" });
                }

                var bll = new TemperatureMeasBLL();

                if (bll.BatchModifyPatSignTypeByColumnsAndExpressionBLL(items.ToList()))
                {
                    bll.BatchAddPatSignTypeChangeRecBLL(items.ToList());
                    return Ok();
                }
                else
                {
                    return Json(new { Status = 0, Msg = "操作失败" });
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw;
            }
        }

        /// <summary>
        /// 根据病人ID、主页ID、体温判断是否需要修改测量类型
        /// </summary>
        /// <param name="items">患者数据</param>
        /// <returns></returns>
        [HttpPost("IsModifyPatSignTypeByTemperature")]
        [DisplayName(name = "根据病人ID、主页ID、体温判断是否需要修改测量类型")]
        [Note(name = "根据病人ID、主页ID、体温判断是否需要修改测量类型")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsModifyPatSignTypeByTemperature(PatSignType[] items)
        {
            try
            {
                if (items == null || items.Length <= 0)
                {
                    return Json(new { Status = 0, Msg = "参数错误" });
                }

                var bll = new TemperatureMeasBLL();

                bll.IsModifyPatSignTypeByTemperatureBLL(items);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw;
            }
        }

        /// <summary>
        /// 根据病人ID、主页ID、体温判断是否需要修改测量类型
        /// </summary>
        /// <param name="items">患者数据</param>
        /// <returns></returns>
        [HttpPost("IsModifyPatSignTypeWithoutTemperature")]
        [DisplayName(name = "根据病人ID、主页ID、目标类型判断是否需要修改测量类型")]
        [Note(name = "根据病人ID、主页ID、目标类型判断是否需要修改测量类型")]
        [ParaOutName(name = "Result")]
        [SchemaContent(name = "")]
        public IActionResult IsModifyPatSignTypeWithoutTemperature(PatSignType[] items)
        {
            try
            {
                if (items == null || items.Length <= 0)
                {
                    return Json(new { Status = 0, Msg = "参数错误" });
                }

                var bll = new TemperatureMeasBLL();

                bll.IsModifyPatSignTypeWithoutTemperatureBLL(items);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex, "操作失败");
                throw;
            }
        }


        #endregion
    }
}