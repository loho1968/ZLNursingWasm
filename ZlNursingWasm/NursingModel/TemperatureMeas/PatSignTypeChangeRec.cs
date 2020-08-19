using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    [Serializable]
    [SugarTable("PAT_SIGN_TYPE_CHANGE_REC")]
    public class PatSignTypeChangeRec
    {
        /// <summary>
        /// 病人ID
        /// </summary>
        [SugarColumn(ColumnName = "PID")]
        public string Pid { get; set; }

        /// <summary>
        /// 主页ID
        /// </summary>
        [SugarColumn(ColumnName = "PVID")]
        public string PVid { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(ColumnName = "CHANGER")]
        public string Changer { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(ColumnName = "CHANGE_TIME")]
        public DateTime? ChangeTime { get; set; }

        /// <summary>
        /// 改变活动参数信息，JSON直接记录
        /// </summary>
        [SugarColumn(ColumnName = "CHANGE_PARA_INFO")]
        public string ChangeParaInfo { get; set; }
        
        /// <summary>
        /// 0表示病人自己，大于0表示第几个婴儿
        /// </summary>
        [SugarColumn(ColumnName = "NB_SNO")]
        public int NbSno { get; set; }
    }
}
