using System;
using System.Collections.Generic;

namespace NursingModel.Form
{
    public class ViewFormItem
    {
        public string ItemId { get; set; }
        //项目名称
        public string ItemName { get; set; }

        //对应所见项ID
        public string ObservItemId { get; set; }

        public string ObservItemName { get; set; }
        //对应所见项名称
        //public string ObservItemName { get; set; }

        public string DetailValue { get; set; }
        public string DetailValueId { get; set; }

        //表单的项目的历史值，用于修改表单时，展现项目的变动历史
        public List<FormData> HistoryValues { get; set; }

        //上次结果
        public FormData LastValues { get; set; }

        //输入类型
        public string InputType { get; set; }
        //组合项目的值，名称:值。
        //public string ComboItemValue { get; set; }

        //组合项目的明细
        public List<PatScaleFormComboItemDetail> PatScaleFormComboItemDetails { get; set; }


    }
}
