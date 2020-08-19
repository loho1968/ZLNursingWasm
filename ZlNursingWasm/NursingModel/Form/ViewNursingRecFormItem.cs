using System;
using System.Collections.Generic;

namespace NursingModel.Form
{
    public class ViewNursingRecFormItem
    {
        //public string FormId { get; set; }

        //public string ItemId { get; set; }
        //项目名称
        public string ItemName { get; set; }
        //项目标签-显示名称
        public string ItemLabel { get; set; }
        //项目序号
        //public decimal ItemNo { get; set; }
        //是否新行
        //public decimal? NewRow { get; set; }
        //选项列数
        //public decimal? LovColumns { get; set; }

        public string ObservItemId { get; set; }
        //public string InputType { get; set; }

        public int RowNumber { get; set; }
        public int ColNumber { get; set; }

        public decimal? LowerLimit { get; set; }
        //所见项数字上限

        public decimal? UpperLimit { get; set; }

        //public string UnitCode { get; set; }

        //public string ObservItemName { get; set; }
        //对应所见项编码=
        //public string ObservItemCode { get; set; }

        //public decimal? ItemWidth { get; set; }

        public virtual List<ViewNursingRecFormItemListValues> FormItemListValues { get; set; }

        //表单的项目的历史值，用于修改表单时，展现项目的变动历史
        //public List<FormData> HistoryFormValues { get; set; }

        //表单的项目上次值，用于新增时，提示显示
        //public FormData LastFormValues { get; set; }
    }
}
