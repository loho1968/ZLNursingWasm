using System;
using System.Collections.Generic;

namespace NursingModel.Form
{
    public class ViewFormList
    {

        //表单id
        public string FormId { get; set; }

        //表单版本
        public string FormVersion { get; set; }

        //表单类型 r-评分，a-评估
        public string FormType { get; set; }
        //表单名称
        public string FormName { get; set; }

        //记录人ID
        public string Recorder { get; set; }

        //记录人姓名
        public string RecorderName { get; set; }

        //记录时间
        public DateTime? RecTime { get; set; }

        //记录时间
        public DateTime? ObservTime { get; set; }

        //表单记录ID|
        public decimal RecId { get; set; }

        //表单项目
        public List<ViewFormItem> FormItems { get; set; }

        //表单分数配置，用于前端计算评分表的分析，风险等级等
        public List<FormScore> FormScores { get; set; }

        //表单护理措施，用于选择护理措施
        public List<ViewFormNiList> PatFormNiLists { get; set; }

        //结果说明
        public string ResultExp { get; set; }

        //风险等级
        public string RiskLevel { get; set; }

        //评分结果
        public string ScaleResult { get; set; }
        //评分得分
        public decimal? ScaleScore { get; set; }

        //患者附着物ID|
        public int? PatSupdevId { get; set; }
        //病人附作物名称|
        public string PatSupdevName { get; set; }

        //附作物类型ID|
        public string SupdevTypeId { get; set; }

        //附作物类型名称|
        public string SupdevTypeName { get; set; }

        //病情观察和措施
        public string NursingStep { get; set; }
    }

}
