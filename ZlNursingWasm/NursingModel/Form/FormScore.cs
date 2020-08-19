using System;
using System.ComponentModel.DataAnnotations.Schema;
using Mapster;
namespace NursingModel.Form
{
    [Table("SCALE_FORM_RESULT_CONDITION")]
    public class FormScore
    {

        [Column("SCALE_FORM_ID")]
        public string FormId { get; set; }

        [Column("SCALE_VERSION")]
        public string FormVersion { get; set; }
        [Column("RESULT_OBSERV_ITEM_ID")]
        public string ResultObservItemId { get; set; }
        [Column("RISK_LEVEL")]
        public string RiskLevel { get; set; }
        [Column("RESULT_EXP")]
        public string ResultExp { get; set; }
        [Column("EXPR")]
        public string Expr { get; set; }
        [Column("DETAIL_CODE")]
        public string DetailCode { get; set; }

        //反向导航
        //public FormList Form { get; set; }
    }
}
