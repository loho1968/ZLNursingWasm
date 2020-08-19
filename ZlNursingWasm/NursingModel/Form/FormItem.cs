using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    [Table("FORM_ITEMS_LIST")]
    public partial class FormItem
    {

        //项目ID，评分表就是所见项目ID，评估表就项目ID
        //表单ID
        [Column("FORM_ID")]
        [JsonProperty("FORMID")]
        public string FormId { get; set; }
        //表单版本
        [Column("FORM_VERSION")]
        [JsonProperty("FORMVERSION")]
        public string FormVersion { get; set; }

        [Column("ITEM_ID")]
        [JsonProperty("ITEMID")]
        public string ItemId { get; set; }
        //项目名称
        [Column("ITEM_NAME")]
        [JsonProperty("ITEMNAME")]
        public string ItemName { get; set; }
        //项目标签-显示名称
        [Column("ITEM_LABEL")]
        [JsonProperty("ITEMLABEL")]
        public string ItemLabel { get; set; }
        //项目序号
        [Column("ITEM_NO")]
        [JsonProperty("ITEMNO")]
        public decimal? ItemNo { get; set; }
        //是否新行
        [Column("NEW_ROW")]
        [JsonProperty("NEWROW")]
        public decimal? NewRow { get; set; }
        //选项列数
        [Column("LOV_COLUMNS")]
        [JsonProperty("LOVCOLUMNS")]
        public decimal? LovColumns { get; set; }
        //必须否|0-否，1-是
        [Column("REQ_SIGN")]
        [JsonIgnore]
        public decimal? ReqSign { get; set; }
        //对应所见项ID
        [Column("OBSERV_ITEM_ID")]
        [JsonProperty("OBSERVITEMID")]
        public string ObservItemId { get; set; }
        //输入类型
        [Column("INPUT_TYPE")]
        [JsonProperty("INPUTTYPE")]
        public string InputType { get; set; }
        //单位
        [Column("UNIT_CODE")]
        [JsonProperty("UNITCODE")]
        public string UnitCode { get; set; }

        //通过NewRow属性，计算Form的项目的布局
        [NotMapped]
        public int RowNumber { get; set; }
        [NotMapped]
        public int ColNumber { get; set; }

        //所见项数字下限
        [Column("LOWER_LIMIT")]
        [JsonProperty("LOWERLIMIT")]
        public decimal? LowerLimit { get; set; }
        //所见项数字上限

        [Column("UPPER_LIMIT")]
        [JsonProperty("UPPERLIMIT")]
        public decimal? UpperLimit { get; set; }



        //对应所见项名称
        [Column("OBSERV_ITEM_NAME")]
        [JsonProperty("OBSERVITEMNAME")]
        public string ObservItemName { get; set; }
        //对应所见项编码=
        [Column("OBSERV_ITEM_CODE")]
        [JsonProperty("OBSERVITEMCODE")]
        public string ObservItemCode { get; set; }



        [Column("DEC_PREC")]
        [JsonProperty("DECPREC")]
        public decimal? Precision { get; set; }

        //反向导航
        //[JsonIgnore]
        //public virtual FormList? Form { get; set; }

        //表单的项目的选项
        public List<FormItemListValue> FormItemListValues { get; set; }


        [NotMapped]
        [JsonProperty("FORMTYPE")]
        public string FormType { get; set; }
        //下面属性暂时不使用
        /*
        //缺省值
        //[Column("DEFAULT_VALUE")]
        //public string DefaultValue { get; set; }
        //项目分数
        [Column("SCORE")]
        public decimal? Score { get; set; }

        ////项目宽度
        //[Column("ITEM_WIDTH")]
        //public decimal? ItemWidth { get; set; }

        //表单类型:r-评分表;a-评估表
        [Column("FORM_TYPE")]
        public string FormType { get; set; }

        //只读否|0-否，1-是
        [Column("READ_SIGN")]
        public decimal? ReadSign { get; set; }

 
        //所见项数据类型:1-数字;2-是否;3-选项
        [Column("DATA_TYPE")]
        public decimal? DataType { get; set; }
        //占位符
        [Column("PLACEHOLDER")]
        public string Placeholder { get; set; }
        //输入掩码
        [Column("INPUT_MASK")]
        public string InputMask { get; set; }
        */
    }
}
