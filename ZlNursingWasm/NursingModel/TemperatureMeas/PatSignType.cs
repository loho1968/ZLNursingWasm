using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    /// <summary>
    /// 病人体温测量类型
    /// </summary>
    [Serializable]
    [SugarTable("PAT_SIGN_TYPE")]
    public class PatSignType
    {
        /// <summary>
        /// 病人ID
        /// </summary>
        [SugarColumn(ColumnName = "PID")]
        public string PID { get; set; }

        /// <summary>
        /// 最后编辑者
        /// </summary>
        [SugarColumn(ColumnName = "LAST_MODIFIER")]
        public string LastModifier { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "LAST_MODIFY_TIME")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 体温时间方案ID
        /// </summary>
        [SugarColumn(ColumnName = "SIGN_TIMING_ID")]
        public string SignTimingID { get; set; }

        /// <summary>
        /// 主页ID
        /// </summary>
        [SugarColumn(ColumnName = "PVID")]
        public string PvID { get; set; }

        /// <summary>
        /// 0表示病人自己，大于0表示第几个婴儿
        /// </summary>
        [SugarColumn(ColumnName = "NB_SNO")]
        public int NbSno { get; set; }

        /// <summary>
        /// 标记。0：修改；1：新增；2：删除；
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int IsNew { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public double Temperature { get; set; }

        /// <summary>
        /// 目标测量类型
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string TargetType { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public DateTime? BeHospitalizedTime { get; set; }

        /// <summary>
        /// 诊疗ID
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string OrderID { get; set; }
    }
}