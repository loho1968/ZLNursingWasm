using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    //病人护理记录
    [Table("PAT_NURSE_DATA_REC")]
    public class PatNurseDataRec
    {
        //病人护理数据ID
        [Column("NURSE_DATA_ID")]
        public int NurseDataId { get; set; }

        //护理数据发生时间
        [Column("NURSE_DATA_TIME")]
        public DateTime NurseDataTime { get; set; }

        //护理数据记录时间
        [Column("NURSE_DATA_REC_TIME")]
        public DateTime NurseDataRecTime { get; set; }

        //记录状态|registered-已发布;corrected-已修正;cancelled-作废
        [Column("REC_STATUS")]
        public string RecStatus { get; set; }

        //病人ID
        [Column("PAT_ID")]
        public string Pid { get; set; }

        //就诊场景|1-门诊、2-住院
        [Column("ENC_TYPE")]
        public int? EncType { get; set; }

        //就诊标示|住院=主页ID
        [Column("ENCOUNTER")]
        public string Pvid { get; set; }

        //最后记录者姓名
        [Column("MODIFY_BY")]
        public string ModifyBy { get; set; }

        //最后记录者ID
        [Column("MODIFY_BY_ID")]
        public string ModifyById { get; set; }

        //病情观察和措施
        [Column("OBSERV_STEP")]
        public string NursingStep { get; set; }

        //未记项目说明|用JSON保存，{"reason": "","item": "observ_item_id1,observ_item_id1"}
        [Column("NO_REC_ITEM")]
        public string NoRecItem { get; set; }

        //护理记录单ID
        [Column("NURSE_DATA_FORM_ID")]
        public string NurseDataFormId { get; set; }

        //婴儿序号|非空或不为零则为婴儿
        [Column("NB_SNO")]
        public int? Baby { get; set; }

        //病区id
        [Column("WARD_ID")]
        public int? WardId { get; set; }

        //病区名称
        [Column("WARD_NAME")]
        public string WardName { get; set; }


        //未作废的表单数据
        [NotMapped]
        public List<FormData> FormDatas { get; set; }

        //护理记录单数据历史记录
        [NotMapped]
        public List<FormData> HistoryDatas { get; set; }

        //护理记录单上次记录值
        [NotMapped]
        public List<FormData> LastDatas { get; set; }

        //护理记录项目在记录时布局，这样修改时就可以不需要表单数据了
        [NotMapped]
        public List<FormRecLayout> FormLayout { get; set; }

        //[NotMapped]
        //作废的表单数据
        //public virtual List<FormData> HistoryFormValues { get; set;


    }
}