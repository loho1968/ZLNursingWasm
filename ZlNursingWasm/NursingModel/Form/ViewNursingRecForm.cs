using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    //护理记录单
    public class ViewNursingRecForm
    {
        //表单id
        public string FormId { get; set; }


        //表单类型 r-评分，a-评估
        //public string FormType { get; set; }
        //表单名称
        public string FormName { get; set; }


        public string Description { get; set; }

        //表单记录ID|
        public decimal RecId { get; set; }


        //表单项目

        public virtual List<ViewNursingRecFormItem> FormItems { get; set; }


    }
}
