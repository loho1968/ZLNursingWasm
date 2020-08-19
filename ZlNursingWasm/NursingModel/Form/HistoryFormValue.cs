using System;
namespace NursingModel.Form
{
    public class HistoryFormValue
    {

        public virtual FormList Form { get; set; }
        public string ObservItemId { get; set; }
        public string Value { get; set; }
        //记录数据
        public DateTime? RecTime { get; set; }
        //数据观察时间
        public DateTime? ObservTime { get; set; }
        //记录者姓名
        public string RecorderName { get; set; }


    }
}
