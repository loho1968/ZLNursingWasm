using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    public class SignValue
    {
        /// <summary>
        /// 病人id
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 主页id
        /// </summary>
        public string Pvid { get; set; }
        /// <summary>
        /// 婴儿序号
        /// </summary>
        public string NbSno { get; set; }
        //体征项
        public string ItemName { get; set; }
        //体温测量时间
        public DateTime ObservTime { get; set; }
        //体征值
        public string ItemValue { get; set; }
        //体温项
        public string TemperatureName { get; set; }
    }
    public class ChineseSignValue
    {
        //体温值
        public string 体温 { get; set; }
        
        //脉率值
        public string 脉搏 { get; set; }
        //呼吸值
        public string 呼吸 { get; set; }
        //心率值
        public string 血压 { get; set; }
    }
}
