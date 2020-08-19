using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    //病人评估表记录明细
    [Table("PAT_AS_FORM_REC_DETAIL")]
    public class PatAsFormRecDetail
    {
        //评估表明细ID
        [Column("AS_FORM_REC_DETAIL_ID")]
        public decimal? AsFormRecDetailId { get; set; }

        //评估表记录ID
        [Column("AS_FORM_REC_ID")]
        public decimal? AsFormRecId { get; set; }

        //评估项目ID
        [Column("AS_FORM_ITEM_ID")]
        public string AsFormItemId { get; set; }

        //评分项目名称
        [Column("AS_FORM_ITEM_NAME")]
        public string AsFormItemName { get; set; }

        //项目取值
        [Column("DETAIL_VALUE")]
        public string DetailValue { get; set; }

        //项目取值ID
        [Column("DETAIL_VALUE_ID")]
        public string DetailValueId { get; set; }

        [NotMapped]
        public string IteName { get; set; }
        //public virtual PatAsFormRec PatAsFormRec { get; set; }

        public List<PatAsFormComboItemDetail> PatAsFormComboItemDetail { get; set; }


        [NotMapped]
        public List<FormData> HistoryValues { get; set; }

        [NotMapped]
        public string InputType { get; set; }

    }
}
