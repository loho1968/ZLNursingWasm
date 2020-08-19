using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    //评分评估表单对象
    [Table("FORMS_LIST")]
    public partial class FormList
    {


        //表单id
        [Column("FORM_ID")]
        public string FormId { get; set; }

        //表单版本
        [Column("FORM_VERSION")]
        public string FormVersion { get; set; }

        //表单类型 r-评分，a-评估
        [Column("FORM_TYPE")]
        public string FormType { get; set; }
        [Column("FORM_TYPE_NAME")]
        public string FormTypeName { get; set; }
        //表单名称
        [Column("FORM_NAME")]
        public string FormName { get; set; }

        //使用标识 0-停用 ,1-在用
        [Column("ENABLE_SIGN")]
        public int EnableSign { get; set; }

        //表单填写说明（HTML）
        [Column("DESCRIPTION")]
        public string Description { get; set; }



        //标准表单ID,用于判断是否标准表单，null=用户自定义表单
        [Column("STRD_SCALE_ID")]
        public string StrdScaleId { get; set; }

        //表单项目
        public virtual List<FormItem> FormItems { get; set; }

        //表单分数
        public List<FormScore> FormScores { get; set; }

        //表单护理措施
        public List<FormNiList> FormNiLists { get; set; }

        //护理记录的病情观察和措施
        [NotMapped]
        public string NursingStep { get; set; }

        //管道类型风险等级
        [NotMapped]
        public string RiskLevl { get; set; }
        //下面属性不使用
        /*
        //评分表对应所见项ID
        [Column("OBSERV_SCORE_ID")]
        public string ObservScoreId { get; set; }
        //评分表结果对应所见项ID
        [Column("OBSERV_RESULT_ID")]
        public string ObservResultId { get; set; }
        //表单简称
        [Column("FORM_SHORT_NAME")]
        public string FormShortName { get; set;}
        */
    }
}
