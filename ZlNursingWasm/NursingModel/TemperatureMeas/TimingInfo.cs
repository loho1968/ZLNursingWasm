using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    public class TimingInfo
    {
        //单次持续时间
        public long? Duration { get; set; }
        //单次持续最长时间
        public long? DurationMax { get; set; }
        //单次持续时间单位
        public string DurationUnit { get; set; }
        //重复次数
        public long? Count { get; set; }
        //最大重复次数
        public long? CountMax { get; set; }
        //一个周期次数
        public long? Frequency { get; set; }
        //间隔周期数
        public long Period { get; set; }
        //最大间隔周期数
        public long? PeriodMax { get; set; }
        //间隔周期单位
        public string PeriodUnit { get; set; }
        //执行时间点
        public string[] TimeOfDay { get; set; }
       
    }
}
