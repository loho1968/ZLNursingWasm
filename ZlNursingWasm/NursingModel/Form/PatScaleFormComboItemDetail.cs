using System;
using System.ComponentModel.DataAnnotations.Schema;
using Mapster;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    [Table("PAT_SCALE_FORM_COMBO_ITEM_DETAIL")]
    public class PatScaleFormComboItemDetail
    {
        //组合明细项目ID
        [Column("DETAIL_ITEM_ID")]
        [JsonProperty("DETAIL_ITEM_ID")]
        public string DetailItemId { get; set; }

        [NotMapped]
        [JsonProperty("DETAIL_ITEM_NAME")]
        public string DetailItemName { get; set; }

        //组合明细项目分值
        [Column("DETAIL_SCORE")]
        [JsonProperty("DETAIL_SCORE")]
        public decimal? DetailScore { get; set; }

        //组合明细项目原值，373066001=是\373067005=否
        [Column("DETAIL_SOURCE_VALUE")]
        [JsonProperty("DETAIL_SOURCE_VALUE")]
        public string DetailSourceValue { get; set; }

        //组合明细项目值，是\否
        [Column("DETAIL_VALUE")]
        [JsonProperty("DETAIL_VALUE")]
        public string DetailValue { get; set; }

        //评分表项目ID
        [Column("SCALE_ITEM_ID")]
        [JsonProperty("SCALE_ITEM_ID")]
        public string ScaleItemId { get; set; }

        //评分表项目名称
        [Column("SCALE_ITEM_NAME")]
        [JsonProperty("SCALE_ITEM_NAME")]
        public string ScaleItemName { get; set; }

        //病人评分表ID
        [Column("SCALE_REC_ID")]
        [JsonProperty("SCALE_REC_ID")]
        public decimal ScaleRecId { get; set; }

        //子项分值
        [Column("VALUE_SCORE")]
        [JsonProperty("VALUE_SCORE")]
        public decimal? ValueScore { get; set; }

        //反向导航
        //[AdaptIgnore]
        //public virtual PatScaleFormRecDetail PatScaleFormRecDetail { get; set; }
    }
}
