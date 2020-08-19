using Jint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NursingBLL;
using NursingCommon;
using NursingModel.TaskModels;
using Quartz;
using ScheduledTasks;

namespace NursingServices.Controllers
{
    //[AllowAnonymous] 这里的所有方法都不验证token
    /// <summary>
    /// 继承ApiController
    /// </summary>
    public class ExampleController : ApiController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NForm()
        {
            return View();
        }
        //1、返回值统一使用IActionResult
        //2、此层不做过多的业务逻辑处理
        //3、Post类型可定义为dynamic

        /// <summary>
        /// Get-获取数据
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] //单个方法不使用token
        [HttpGet]
        public IActionResult GetData()
        {

            EventTimeBLL eventTimeBLL = new EventTimeBLL();
            return Json(eventTimeBLL.GetAll());

            ExampleBLL exampleBLL = new ExampleBLL();
            //登录时产生token，将来从BH中获取token
            string token = JwtTokenUtil.GetToken("账号", "姓名");
            return Json(exampleBLL.GetOneData());
        }

        /// <summary>
        /// post 获取参数示例
        /// </summary>
        /// <param name="example"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertData([FromBody] dynamic example)
        {
            return Ok();
        }

        /// <summary>
        /// 动态执行js示例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ExcuteJS()
        {
            //示例1,直接执行JS
            DynamicExcuteCode code = new DynamicExcuteCode();
            object result = code.ExcuteExpresionCode(@"
      function hello() { 
        return '小仙女';
      };
      hello();
    ");
            //示例2：带参数，参数来自于netcore
            var engine = new Engine().Execute("function add(a, b) { return a + b; }");
            object s= engine.Invoke("add", 1, 2).ToObject();

            return Ok();
        }

        /// <summary>
        /// 任务测试
        /// </summary>
        /// <param name="type"></param>
        [HttpPost]
        public IActionResult QuartzTest(int id)
        {
            JobKey jobKey = new JobKey("测试", "测试分组");
            switch (id)
            {
                //添加任务
                case 1:
                    var trigger = TriggerBuilder.Create()
                            .WithDescription("小仙女定时任务测试")
                            .WithIdentity("test")
                            //.WithSchedule(CronScheduleBuilder.CronSchedule("0 0/30 * * * ? *").WithMisfireHandlingInstructionDoNothing())
                            .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
                            .Build();
                    QuartzCore.Add(typeof(ExampleOne), jobKey, trigger);
                    break;
                //暂停任务
                case 2:
                    QuartzCore.Stop(jobKey);
                    break;
                //恢复任务
                case 3:
                    QuartzCore.Resume(jobKey);
                    break;
            }
            return Ok();
        }
    }
}