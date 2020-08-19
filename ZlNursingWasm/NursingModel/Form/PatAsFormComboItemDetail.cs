using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    [Table("PAT_AS_FORM_COMBO_ITEM_DETAIL")]
    public class PatAsFormComboItemDetail
    {

        //评估表项目ID
        [Column("AS_FORM_ITEM_ID")]
        [JsonProperty("AS_FORM_ITEM_ID")]
        public string AsFormItemId { get; set; }

        //评估表项目名称
        [Column("AS_FORM_ITEM_NAME")]
        [JsonProperty("AS_FORM_ITEM_NAME")]
        public string AsFormItemName { get; set; }

        //病人评估表记录ID
        [Column("AS_FORM_REC_ID")]
        [JsonProperty("AS_FORM_REC_ID")]
        public decimal? AsFormRecId { get; set; }

        //明细项目ID
        [Column("DETAIL_ITEM_ID")]
        [JsonProperty("DETAIL_ITEM_ID")]
        public string DetailItemId { get; set; }

        //明细项目原值，373066001=是\373067005=否
        [Column("DETAIL_SOURCE_VALUE")]
        [JsonProperty("DETAIL_SOURCE_VALUE")]
        public string DetailSourceValue { get; set; }

        //明细项目值,是否
        [Column("DETAIL_VALUE")]
        [JsonProperty("DETAIL_VALUE")]
        public string DetailValue { get; set; }

        [NotMapped]
        public string DetailItemName { get; set; }
    }
}
