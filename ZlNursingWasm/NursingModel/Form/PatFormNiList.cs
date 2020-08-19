using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    [Table("PAT_FORM_NI_REC_DETAIL")]
    public class PatFormNiRecDetail
    {
        //|评分表单id
        [Column("FORM_ID")]
        [JsonProperty("FORM_ID")]
        public string FormId { get; set; }

        //|评分表名称
        [Column("FORM_NAME")]
        [JsonProperty("FORM_NAME")]
        public string FormName { get; set; }

        //措施类型|预防，治疗
        [Column("INTERVENTION_TYPE")]
        [JsonProperty("INTERVENTION_TYPE")]
        public string InterventionType { get; set; }

        //|措施id
        [Column("NI_LIST_ID")]
        [JsonProperty("NI_LIST_ID")]
        public string NiListId { get; set; }

        //|措施名称
        [Column("NI_NAME")]
        [JsonProperty("NI_NAME")]
        public string NiName { get; set; }

        //|评分措施明细id
        [Column("PAT_FORM_NI_REC_DETAIL_ID")]
        [JsonProperty("PAT_FORM_NI_REC_DETAIL_ID")]
        public int PatFormNiRecDetailId { get; set; }

        //|措施记录id
        [Column("PAT_FORM_NI_REC_ID")]
        [JsonProperty("PAT_FORM_NI_REC_ID")]
        public int PatFormNiRecId { get; set; }

        //|评分记录id
        [Column("SCALE_REC_ID")]
        [JsonProperty("SCALE_REC_ID")]
        public decimal ScaleRecId { get; set; }

        //public PatScaleFormRec PatScaleFormRec { get; set; }
    }
}
