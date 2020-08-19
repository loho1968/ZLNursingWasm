using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    /// <summary>
    /// 体温测量类型时间方案
    /// </summary>
    [Serializable]
    [SugarTable("SIGN_TIMING")]
    public class SignTiming
    {
        /// <summary>
        /// 时间方案名称
        /// </summary>
        [SugarColumn(ColumnName = "TIMING_NAME")]
        public string TimingName { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "LAST_MODIFY_TIME")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [SugarColumn(ColumnName = "LAST_MODIFIER")]
        public string LastModifier { get; set; }

        /// <summary>
        /// 每天测量体温的时间方案
        /// </summary>
        [SugarColumn(ColumnName = "TIMING")]
        public string Timing { get; set; }

        /// <summary>
        /// 类型，固定值：发热、高热、护理等级、新入院、术期、术后、普通
        /// </summary>
        [SugarColumn(ColumnName = "TYPE")]
        public string Type { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(ColumnName = "TIMING_ID")]
        public string TimingID { get; set; }
    }

    /// <summary>
    /// 测量体温的时间方案
    /// </summary>
    public class Timing
    {
        public int Frequency { get; set; }
        public int Count { get; set; }
        public int Period { get; set; }
        public string PeriodUnit { get; set; }
        public Boundsduration BoundsDuration { get; set; }
        public string[] TimeOfDay { get; set; }
    }

    public class Boundsduration
    {
        public int Duration { get; set; }
        public string DurationUnit { get; set; }
    }

}
