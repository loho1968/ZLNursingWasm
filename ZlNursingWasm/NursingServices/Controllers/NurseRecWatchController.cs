using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NursingBLL;
using NursingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NursingServices.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NurseRecWatchController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pid"></param>
        /// <param name="MrId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string Pid, string MrId)
        {
            NurseRecWatchBLL nurseRecWatchBLL = new NurseRecWatchBLL();
            var list = nurseRecWatchBLL.GetAllInfo(Pid, MrId);
            //var path = AppContext.BaseDirectory + "NurseDay.json";
            //var list = JsonConvert.DeserializeObject<List<NurseDay>>(System.IO.File.ReadAllText(path));
            return View(list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Drainage()
        {
            var path = AppContext.BaseDirectory + "Drainage.json";
            var list = JsonConvert.DeserializeObject<List<Drainage>>(System.IO.File.ReadAllText(path, Encoding.UTF8));
            return View(list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Thermometer()
        {
            return View();
        }
    }
}
