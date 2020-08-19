using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    [Table("NI_FORM_RELATION")]
    public class FormNiList
    {
        [Column("FORM_ID")]
        public string FormId { get; set; }
        //表单版本
        [Column("FORM_VERSION")]
        public string FormVersion { get; set; }

        //表单护理措施
        [Column("NI_NAME")]
        public string NiName { get; set; }

        //护理措施I
        [Column("NI_LIST_ID")]
        public string NiListId { get; set; }

        //排序序号
        [Column("SNO")]
        public int? SNo { get; set; }

        //措施选择分数上线条件 >、>=...
        [Column("MAX_OPRSMB")]
        public string MaxOperator { get; set; }
        //措施选择分数上线分数
        [Column("MAX_COND_VALUE")]
        public decimal? MaxScore { get; set; }
        //措施选择分数下限条件 >、>=...
        [Column("MIN_OPRSMB")]
        public string LowerOperator { get; set; }
        //措施选择分数下限分数

        [Column("MIN_COND_VALUE")]
        public decimal? LowerScore { get; set; }


    }
}
