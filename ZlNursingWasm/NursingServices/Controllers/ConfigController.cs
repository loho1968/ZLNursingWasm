using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NursingCommon;
using NursingModel;

namespace OnePaperCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [Route("bh/[controller]/[action]")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class ConfigController : Controller
    {
        private static string ApiLogUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log/runtime");
        private static string ErrLogUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log/error");
        /// <summary>
        /// 获取API访问日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetApiLog()
        {
            if (Directory.Exists(ApiLogUrl) == false)
                return Ok("[]");
            string LogContent = "[";
            DirectoryInfo root = new DirectoryInfo(ApiLogUrl);
            FileInfo[] files = root.GetFiles();
            if (files.Length > 0)
            {
                LogContent += GetFileToString(files);
                LogContent += "]";
                List<ApiLog> objs = JsonConvert.DeserializeObject<List<ApiLog>>(LogContent);
                objs.Sort((l, r) => r.StartTime.CompareTo(l.StartTime));
                return Json(objs);
            }
            return Ok("");
        }
        /// <summary>
        /// 获取错误日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetErrLog()
        {
            if (Directory.Exists(ErrLogUrl) == false)
                return Ok("[]");
            string LogContent = "[";
            if (Directory.Exists(ErrLogUrl) == false)
                return Ok("[]");
            DirectoryInfo root = new DirectoryInfo(ErrLogUrl);
            FileInfo[] files = root.GetFiles();
            if (files.Length > 0)
            {
                LogContent += GetFileToString(files);
                LogContent += "]";
                List<ErrLog> objs = JsonConvert.DeserializeObject<List<ErrLog>>(LogContent);
                objs.Sort((l, r) => r.ErrTime.CompareTo(l.ErrTime));
                return Json(objs);
            }
            return Ok("[]");
        }
        /// <summary>
        /// 根据时间差获取Api日志
        /// </summary>
        /// <param name="TimeDifference">时间差参数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetApiLogByDate([FromBody] dynamic para)
        {
            if (Directory.Exists(ApiLogUrl) == false)
                return Ok("[]");
            string LogContent = "[";
            DirectoryInfo root = new DirectoryInfo(ApiLogUrl);
            FileInfo[] files = root.GetFiles();
            if (files.Length > 0)
            {
                LogContent += GetFileToString(files);
                LogContent += "]";
                List<ApiLog> contexts = JsonConvert.DeserializeObject<List<ApiLog>>(LogContent);
                contexts.Sort((l, r) => r.StartTime.CompareTo(l.StartTime));
                List<ApiLog> rusts = new List<ApiLog>();
                switch ((string)para.TimeDifference)
                {
                    case "th":
                        DateTime thonce = DateTime.Now.AddHours(-1);
                        foreach (ApiLog obj in contexts)
                        {
                            if (obj.StartTime > thonce)
                            {
                                rusts.Add(obj);
                            }
                        }
                        break;
                    case "day":
                        DateTime dayonce = DateTime.Now.AddDays(-1);
                        foreach (ApiLog obj in contexts)
                        {
                            if (obj.StartTime > dayonce)
                            {
                                rusts.Add(obj);
                            }
                        }
                        break;
                    case "week":
                        DateTime weekonce = DateTime.Now.AddDays(-7);
                        foreach (ApiLog obj in contexts)
                        {
                            if (obj.StartTime > weekonce)
                            {
                                rusts.Add(obj);
                            }
                        }
                        break;
                    default:
                        rusts = contexts;
                        break;
                }
                return Json(rusts);
            }
            return Ok("[]");
        }
        /// <summary>
        /// 根据时间差获取错误日志
        /// </summary>
        /// <param name="TimeDifference">时间差参数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetErrLogByDate([FromBody] dynamic para)
        {
            if (Directory.Exists(ApiLogUrl) == false)
                return Ok("[]");
            string LogContent = "[";
            DirectoryInfo root = new DirectoryInfo(ErrLogUrl);
            FileInfo[] files = root.GetFiles();
            if (files.Length > 0)
            {
                LogContent += GetFileToString(files);
                LogContent += "]";
                List<ErrLog> contexts = JsonConvert.DeserializeObject<List<ErrLog>>(LogContent);
                contexts.Sort((l, r) => r.ErrTime.CompareTo(l.ErrTime));
                List<ErrLog> rusts = new List<ErrLog>();
                switch ((string)para.TimeDifference)
                {
                    case "th":
                        DateTime thonce = DateTime.Now.AddHours(-1);
                        foreach (ErrLog obj in contexts)
                        {
                            if (obj.ErrTime > thonce)
                            {
                                rusts.Add(obj);
                            }
                        }
                        break;
                    case "day":
                        DateTime dayonce = DateTime.Now.AddDays(-1);
                        foreach (ErrLog obj in contexts)
                        {
                            if (obj.ErrTime > dayonce)
                            {
                                rusts.Add(obj);
                            }
                        }
                        break;
                    case "week":
                        DateTime weekonce = DateTime.Now.AddDays(-7);
                        foreach (ErrLog obj in contexts)
                        {
                            if (obj.ErrTime > weekonce)
                            {
                                rusts.Add(obj);
                            }
                        }
                        break;
                    default:
                        rusts = contexts;
                        break;
                }
                return Json(rusts);
            }
            return Ok("[]");
        }
        #region AppSetting私有方法
        /// <summary>
        /// 字符串截取方法
        /// </summary>
        /// <param name="text">原字符串</param>
        /// <param name="start">开始字符串</param>
        /// <param name="end">结束字符串</param>
        /// <returns></returns>
        private string TextSubstring(string text, string start, string end)
        {
            Regex regex = new Regex("(?<=(" + start + "))[.\\s\\S]*?(?=(" + end + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return regex.Match(text).Value;
        }

        private void _getAppKeys(JObject jobject, List<string> names, List<string> keys)
        {
            if (names == null || !names.Any())
            {
                foreach (KeyValuePair<string, JToken> item in jobject)
                {
                    keys.Add(item.Key);
                }
                return;
            }
            string name = names[0];
            foreach (KeyValuePair<string, JToken> item in jobject)
            {
                if (name == item.Key)
                {
                    names.RemoveAt(0);
                    _getAppKeys((JObject)item.Value, names, keys);
                }
            }
        }
        /// <summary>
        /// 获取keys
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<string> GetAppSettingKey(params string[] nodes)
        {
            try
            {

                string appSettingsJsonFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

                var json = System.IO.File.ReadAllText(appSettingsJsonFilePath);
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(json);
                List<string> keyList = new List<string>();
                _getAppKeys(jsonObj, nodes.ToList(), keyList);
                return keyList;
            }
            catch { return new List<string>(); }
        }


        private bool _getAppKeyValue(JObject jobject, List<string> names, ref string value)
        {
            if (names == null || !names.Any()) return false;
            string name = names[0];
            if (names.Count() == 1)
            {
                JToken jToken = jobject[name];
                if (jToken.Any())//json对象还有下级,无法获取value
                {
                    return false;
                }
                value = jToken.ToString();
                return true;
            }

            foreach (KeyValuePair<string, JToken> item in jobject)
            {
                if (name == item.Key)
                {
                    names.RemoveAt(0);
                    _getAppKeyValue((JObject)item.Value, names, ref value);
                }
            }

            return false;
        }
        /// <summary>
        /// 获取对应key的value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private bool GetAppSettingsKeyValue(ref string value, params string[] nodes)
        {
            try
            {

                string appSettingsJsonFilePath =
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

                var json = System.IO.File.ReadAllText(appSettingsJsonFilePath);
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(json);
                List<string> keyList = new List<string>();
                bool falg = _getAppKeyValue(jsonObj, nodes.ToList(), ref value);
                return falg;
            }
            catch
            {
                return false;
            }
        }



        private bool _setAppValue(JObject jobject, List<string> names, string value)
        {
            if (names == null || !names.Any()) return false;
            string name = names[0];
            if (names.Count() == 1)
            {
                JToken jToken = jobject[name];
                if (jToken.Any())//json对象还有下级,无法设置value
                {
                    return false;
                }
                jobject[name] = value;
                return true;
            }

            foreach (KeyValuePair<string, JToken> item in jobject)
            {
                if (name == item.Key)
                {
                    names.RemoveAt(0);
                    _setAppValue((JObject)item.Value, names, value);
                }
            }

            return false;
        }
        /// <summary>
        /// 设置keys的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private bool SetAppSettingKeyVAlue(string value, params string[] nodes)
        {
            try
            {

                string appSettingsJsonFilePath =
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

                var json = System.IO.File.ReadAllText(appSettingsJsonFilePath);
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(json);
                bool falg = _setAppValue(jsonObj, nodes.ToList(), value);
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                System.IO.File.WriteAllText(appSettingsJsonFilePath, output);
                return falg;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 多文本获取
        /// </summary>
        /// <param name="fileInfos"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetFileToString(FileInfo[] files)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string tempstr;
            for (int i = 0; i < files.Length; i++)
            {
                if (Path.GetExtension(files[i].FullName) == ".log")
                {
                    tempstr = System.IO.File.ReadAllText(files[i].FullName);
                    if (tempstr.Length < 1)
                        continue;
                    if (i != 0)
                        stringBuilder.Append(tempstr.Replace("\r\n", "").Replace("\\", "/"));
                    else
                        stringBuilder.Append(tempstr.Replace("\r\n", "").Replace("\\", "/").Substring(1));
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="jArray">传null或者空数组代表获取最外层节点</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetNodeKeys(JArray jArray)
        {
            var list = GetAppSettingKey(jArray.ToObject<string[]>());
            return Ok(list);
        }

        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetNodeKeyValue(JArray jArray)
        {
            string value = string.Empty;
            bool falg = GetAppSettingsKeyValue(ref value, jArray.ToObject<string[]>());
            if (falg)
            {
                return Ok(new { start = true, msg = value });

            }
            return Ok(new { start = false });
        }

        /// <summary>
        /// 设置节点值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="jArray"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetNodeKeyValue(string value, JArray jArray)
        {
            var falg = SetAppSettingKeyVAlue(value, jArray.ToObject<string[]>());
            return Ok(new { start = falg });
        }

        /// <summary>
        /// ApiLog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ApiLog()
        {
            string ip = RandomStringBuilder.ReadAddressinfo().Replace("0.0.0.0", "127.0.0.1");
            ViewBag.ip = ip;
            return View();
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ErrLog()
        {
            string ip = RandomStringBuilder.ReadAddressinfo().Replace("0.0.0.0", "127.0.0.1");
            ViewBag.ip = ip;
            return View();
        }

    }
}