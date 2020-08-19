using System;
using System.Collections.Generic;

namespace NursingModel.Form
{
    public class FormData
    {
        //记录id
        public string Id { get; set; }
        //值
        public string Value { get; set; }

        //值的id
        public string ValueId { get; set; }
        //项目id
        public string ItemId { get; set; }

        //所见项项目id
        public string ObservItemId { get; set; }

        //未记项目说明|用JSON保存，{"reason": "","item": "observ_item_id1,observ_item_id1"}
        public string NoRecItem { get; set; }

        //记录人
        public string Recorder { get; set; }

        //记录人姓名
        public string RecorderName { get; set; }

        //记录时间
        public DateTime? RecTime { get; set; }

        //所见项观察时间|
        public DateTime? ObservTime { get; set; }

        //上级所见项ID｜
        public string ParentItemId { get; set; }

        //记录状态|registered-已发布;corrected-已修正;cancelled-作废
        public string RecStatus { get; set; }

        public string InputType { get; set; }

        public string ItemName { get; set; }

        //评分表组合项目的子项
        public virtual List<PatScaleFormComboItemDetail> PatScaleFormComboItemDetail { get; set; }

        public string EditTime { get; set; }


        //表单的项目的历史值，用于修改表单时，展现项目的变动历史
        public List<FormData> HistoryValues { get; set; }

        //上次结果
        public FormData LastValues { get; set; }
    }


}
