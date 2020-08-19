using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;
using Mapster;
namespace NursingModel.Form
{
    //病人评分表记录名称
    [Table("PAT_SCALE_FORM_REC_DETAIL")]
    public partial class PatScaleFormRecDetail
    {
        //项目取值
        [Column("DETAIL_VALUE")]
        [JsonProperty("DETAIL_VALUE")]
        public string DetailValue { get; set; }


        //项目取值ID
        [Column("DETAIL_VALUE_ID")]
        [JsonProperty("DETAIL_VALUE_ID")]
        public string DetailValueId { get; set; }

        //评分项目ID
        [Column("SCALE_ITEM_ID")]
        [JsonProperty("SCALE_ITEM_ID")]
        public string ScaleItemId { get; set; }

        //评分项目名称
        [Column("SCALE_ITEM_NAME")]
        [JsonProperty("SCALE_ITEM_NAME")]
        public string ScaleItemName { get; set; }

        //评分表明细ID
        [Column("SCALE_REC_DETAIL_ID")]
        [JsonProperty("SCALE_REC_DETAIL_ID")]
        public decimal ScaleRecDetailId { get; set; }

        //评分表记录ID
        [Column("SCALE_REC_ID")]
        [JsonProperty("SCALE_REC_ID")]
        public decimal ScaleRecId { get; set; }

        //项目得分
        [Column("VALUE_SCORE")]
        [JsonProperty("VALUE_SCORE")]
        public decimal? ValueScore { get; set; }

        //反向导航
        //[AdaptIgnore]
        //public virtual PatScaleFormRec PatScaleFormRec { get; set; }

        /// <summary>
        /// 组合项目的值，用于修改历史是，把值“翻译”为可以人读的Html内容，页面上直接显示
        /// </summary>
        [NotMapped]
        //public string ComboItemValue { get; set; }

        //组合项目的子项
        public List<PatScaleFormComboItemDetail> PatScaleFormComboItemDetail { get; set; }

        /// <summary>
        /// 表单项目表单历史值
        /// </summary>
        [NotMapped]
        public List<FormData> HistoryValues { get; set; }

        [NotMapped]
        public string InputType { get; set; }
    }
}
