using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    /// <summary>
    /// 病人体温测量记录
    /// </summary>
    [Serializable]
    [SugarTable("PAT_OBSERV_REC")]
    public class PatObservRec
    {
        /// <summary>
        /// 病人ID
        /// </summary>
        [SugarColumn(ColumnName = "PAT_ID")]
        public string PID { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        [SugarColumn(ColumnName = "VALUE")]
        public string Temperature { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "REC_STATUS")]
        public string RecStatus { get; set; }

        /// <summary>
        /// 测量时间
        /// </summary>
        [SugarColumn(ColumnName = "OBSERV_TIME")]
        public DateTime? ObservTime { get; set; }

        [SugarColumn(ColumnName = "ENC_TYPE")]
        public int EncType { get; set; }

        /// <summary>
        /// 主页ID
        /// </summary>
        [SugarColumn(ColumnName = "ENCOUNTER")]
        public string PvID { get; set; }

        [SugarColumn(ColumnName = "REL_PROC")]
        public string RelProc { get; set; }

        [SugarColumn(ColumnName = "OBSERV_ITEM_ID")]
        public string ObservItemID { get; set; }
    }
}
