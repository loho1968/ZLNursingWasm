using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    [Serializable]
    [Table("NURSE_DATA_FORM_DETAIL")]
    public class NursingRecFormItem
    {
        //数据类型:1-数字;2-是否;3-选项;4-文本
        [Column("DATA_TYPE")]
        [JsonProperty("DATA_TYPE")]
        public int DataType { get; set; }

        //编辑类型|录入、查看
        [Column("EDIT_TYPE")]
        [JsonProperty("EDIT_TYPE")]
        public string EditType { get; set; }

        //项目排序
        [Column("ITEM_NO")]
        [JsonProperty("ITEM_NO")]
        public int ItemNo { get; set; }

        //项目宽度,px单位
        //[Column("ITEM_WIDTH")]
        //[JsonProperty("ITEM_WIDTH")]
        //public int ItemWidth { get; set; }

        //最后修改人
        [Column("LAST_MODIFIER")]
        [JsonProperty("LAST_MODIFIER")]
        public string LastModifier { get; set; }

        //最后修改时间
        [Column("LAST_MODIFY_TIME")]
        [JsonProperty("LAST_MODIFY_TIME")]
        public DateTime LastModifyTime { get; set; }

        //是否新行|1-新行
        [Column("NEWROW")]
        [JsonProperty("NEWROW")]
        public int Newrow { get; set; }

        //护理记录单项目id
        [Column("NURSE_DATA_FORM_DETAIL_ID")]
        [JsonProperty("NURSE_DATA_FORM_DETAIL_ID")]
        public string NurseDataFormDetailId { get; set; }

        //护理记录单id
        [Column("NURSE_DATA_FORM_ID")]
        [JsonProperty("NURSE_DATA_FORM_ID")]
        public string NurseDataFormId { get; set; }

        //所见项id
        [Column("OBSERV_ITEM_ID")]
        [JsonProperty("OBSERV_ITEM_ID")]
        public string ObservItemId { get; set; }

        //所见项名称
        [Column("OBSERV_ITEM_NAME")]
        [JsonProperty("OBSERV_ITEM_NAME")]
        public string ObservItemName { get; set; }

        //项目单位CODE
        [Column("UNIT_CODE")]
        [JsonProperty("UNIT_CODE")]
        public string UnitCode { get; set; }

        [NotMapped]
        [JsonProperty("FORMID")]
        public string FormId { get; set; }

        [NotMapped]
        [JsonProperty("ITEMID")]
        public string ItemId { get; set; }
        //项目名称
        [NotMapped]
        [JsonProperty("ITEMNAME")]
        public string ItemName { get; set; }
        //项目标签-显示名称
        [NotMapped]
        [JsonProperty("ITEMLABEL")]
        public string ItemLabel { get; set; }
        //是否新行
        [NotMapped]
        [JsonProperty("NEWROW")]
        public decimal? NewRow { get; set; }
        //选项列数
        [NotMapped]
        [JsonProperty("LOVCOLUMNS")]
        public decimal? LovColumns { get; set; }

        //为了和FormListItem保持同步
        [NotMapped]
        public int? ReqSign { get; set; }

        [NotMapped]
        [JsonProperty("INPUTTYPE")]
        public string InputType { get; set; }


        public int RowNumber { get; set; }
        public int ColNumber { get; set; }

        [NotMapped]
        [JsonProperty("LOWERLIMIT")]
        public decimal? LowerLimit { get; set; }
        //所见项数字上限
        [NotMapped]
        [JsonProperty("UPPERLIMIT")]
        public decimal? UpperLimit { get; set; }


        //对应所见项编码=
        [NotMapped]
        [JsonProperty("OBSERV_ITEM_CODE")]
        public string ObservItemCode { get; set; }


        //所见项精度
        [NotMapped]
        [JsonProperty("DEC_PREC")]
        public decimal? Precision { get; set; }

        //所见项的选项
        [JsonProperty("FORMITEMLISTVALUES")]
        [NotMapped]
        public List<FormItemListValue> FormItemListValues { get; set; }

    }
}
