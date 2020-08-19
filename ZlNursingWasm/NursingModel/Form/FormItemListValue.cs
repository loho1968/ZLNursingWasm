using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    [Table("FORM_ITEM_LOV_DATA")]
    public class FormItemListValue
    {
        ///表单id
        [Column("FORM_ID")]
        public string FormId { get; set; }
        //表单版本
        [Column("FORM_VERSION")]
        public string FormVersion { get; set; }
        //表单项目id

        [Column("ITEM_DATA_ID")]
        public string ItemDataId { get; set; }
        //值域id
        [Column("ITEM_ID")]
        public string ItemId { get; set; }

        [Column("ITEM_DATA_NAME")]
        public string ItemDataName { get; set; }
        //结果

        [Column("RETURN")]
        public string Return { get; set; }
        //显示名称

        [Column("DISPLAY")]
        public string Display { get; set; }
        //序号

        [Column("NO")]
        public int? No { get; set; }
        //评分得分

        [Column("SCORE")]
        public int? Score { get; set; }
        //默认值

        [Column("DEFAULT_SIGN")]
        public int? DefaultSign { get; set; }
        //是否其他类型值域|如果是其他类型值域，选择该值域后，可以再输入一个其他的文字。

        [Column("OTHERSIGN")]
        public decimal? OtherSign { get; set; }
        //所见项id

        [Column("OBSERV_ITEM_ID")]
        public string ObservItemId { get; set; }

        //值域简称
        [Column("SHORTCODE")]
        public string Shortcode { get; set; }

        //表单项目名称

        //public virtual FormItem FormItem { get; set; }



    }
}
