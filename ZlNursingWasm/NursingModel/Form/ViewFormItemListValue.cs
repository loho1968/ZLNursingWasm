using System;
namespace NursingModel.Form
{
    public class ViewFormItemListValue
    {

        ///表单id
        //public string FormId { get; set; }
        //表单版本
        //public string FormVersion { get; set; }
        //表单项目id
        //public string ItemDataId { get; set; }
        //值域id
        //public string ItemId { get; set; }

        //public string ItemDataName { get; set; }
        //结果
        public string Return { get; set; }
        //显示名称
        public string Display { get; set; }
        //序号
        //public int? No { get; set; }
        //评分得分
        public int? Score { get; set; }
        //默认值
        public int? DefaultSign { get; set; }
        //是否其他类型值域|如果是其他类型值域，选择该值域后，可以再输入一个其他的文字。
        public int? OtherSign { get; set; }
        //所见项id
        public string ObservItemId { get; set; }
        //值域简称
        //public string Shortcode { get; set; }
        //表单项目名称

        //public virtual FormItem FormItem { get; set; }

    }

}

