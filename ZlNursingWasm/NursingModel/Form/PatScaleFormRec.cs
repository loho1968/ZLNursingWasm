using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    //病人评分表记录
    [Table("PAT_SCALE_FORM_REC")]
    public partial class PatScaleFormRec
    {

        //审核人
        [Column("AUDITOR")]
        [JsonProperty("AUDITOR")]
        public string Auditor { get; set; }

        //审核人id
        [Column("AUDITOR_ID")]
        [JsonProperty("AUDITOR_ID")]
        public string AuditorId { get; set; }

        //审核结果
        [Column("AUDIT_RESULT")]
        [JsonProperty("AUDIT_RESULT")]
        public string AuditResult { get; set; }

        //审核时间
        [Column("AUDIT_TIME")]
        [JsonProperty("AUDIT_TIME")]
        public DateTime? AuditTime { get; set; }

        //场景标识
        [Column("ENCOUNTER")]
        [JsonProperty("ENCOUNTER")]
        public string Pvid { get; set; }

        //场景类型:1-门诊,2-住院
        [Column("ENC_TYPE")]
        [JsonProperty("ENC_TYPE")]
        public decimal? EncType { get; set; }

        //备注
        [Column("NOTE")]
        [JsonProperty("NOTE")]
        public string Note { get; set; }

        //项目观察时间|等于一次就记录时间，后续修改不改变，对应于所见项的观察时间
        [Column("OBSERV_TIME")]
        [JsonProperty("OBSERV_TIME")]
        public DateTime? ObservTime { get; set; }

        //患者ID
        [Column("PAT_ID")]
        [JsonProperty("PAT_ID")]
        public string Pid { get; set; }

        //记录人ID
        [Column("RECORDER")]
        [JsonProperty("RECORDER")]
        public string Recorder { get; set; }

        //记录人姓名
        [Column("RECORDER_NAME")]
        [JsonProperty("RECORDER_NAME")]
        public string RecorderName { get; set; }

        //记录状态
        [Column("REC_STATUS")]
        [JsonProperty("REC_STATUS")]
        public string RecStatus { get; set; }

        //记录时间
        [Column("REC_TIME")]
        [JsonProperty("REC_TIME")]
        public DateTime? RecTime { get; set; }

        //结果说明
        [Column("RESULT_EXP")]
        [JsonProperty("RESULT_EXP")]
        public string ResultExp { get; set; }

        //风险等级
        [Column("RISK_LEVEL")]
        [JsonProperty("RISK_LEVEL")]
        public string RiskLevel { get; set; }

        //评分表ID
        [Column("SCALE_LIST_ID")]
        [JsonProperty("SCALE_LIST_ID")]
        public string ScaleListId { get; set; }

        //评分表记录ID|
        [Column("SCALE_REC_ID")]
        [JsonProperty("SCALE_REC_ID")]
        public decimal ScaleRecId { get; set; }

        //评分结果
        [Column("SCALE_RESULT")]
        [JsonProperty("SCALE_RESULT")]
        public string ScaleResult { get; set; }

        //结果所见项ID
        [Column("SCALE_RESULT_ID")]
        [JsonProperty("SCALE_RESULT_ID")]
        public string ScaleResultId { get; set; }

        //评分得分
        [Column("SCALE_SCORE")]
        [JsonProperty("SCALE_SCORE")]
        public decimal? ScaleScore { get; set; }

        //评分表版本
        [Column("SCALE_VERSION")]
        [JsonProperty("SCALE_VERSION")]
        public string ScaleVersion { get; set; }

        //病区id
        [Column("WARD_ID")]
        [JsonProperty("WARD_ID")]
        public decimal? WardId { get; set; }

        //病区名称
        [Column("WARD_NAME")]
        [JsonProperty("WARD_NAME")]
        public string WardName { get; set; }

        public List<PatScaleFormRecDetail> PatScaleFormRecDetails { get; set; }

        /// <summary>
        /// 评分表的护理措施
        /// </summary>

        public List<PatFormNiRecDetail> PatFormNiLists { get; set; }
        [NotMapped]
        public string FormType { get; set; } = "r";

    }
}
