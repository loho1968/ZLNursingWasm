using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    //病人评估表记录
    [Table("PAT_AS_FORM_REC")]
    public class PatAsFormRec
    {
        //评估表ID
        [Column("AS_FORM_ID")]
        public string FormId { get; set; }

        //评估表记录ID|
        [Column("AS_FORM_REC_ID")]
        public decimal AsFormRecId { get; set; }

        //评估表版本
        [Column("AS_FORM_VERSION")]
        public string FormVersion { get; set; }

        //场景标识
        [Column("ENCOUNTER")]
        public string Pvid { get; set; }

        //场景类型:1-门诊,2-住院
        [Column("ENC_TYPE")]
        public decimal? EncType { get; set; }

        //备注
        [Column("NOTE")]
        public string Note { get; set; }

        //项目观察时间|等于一次就记录时间，后续修改不改变，对应于所见项的观察时间
        [Column("OBSERV_TIME")]
        public DateTime? ObservTime { get; set; }

        //患者ID
        [Column("PAT_ID")]
        public string Pid { get; set; }

        //记录人ID
        [Column("RECORDER")]
        public string Recorder { get; set; }

        //记录人姓名
        [Column("RECORDER_NAME")]
        public string RecorderName { get; set; }

        //记录状态
        [Column("REC_STATUS")]
        public string RecStatus { get; set; }

        //记录时间
        [Column("REC_TIME")]
        public DateTime? RecTime { get; set; }

        //病区id
        [Column("WARD_ID")]
        public decimal? WardId { get; set; }

        //病区名称
        [Column("WARD_NAME")]
        public string WardName { get; set; }

        public List<PatAsFormRecDetail> PatAsFormRecDetail { get; set; }


    }
}
