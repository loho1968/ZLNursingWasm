using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    [Serializable]
    [SugarTable("SIGN_TIMING_NURSING_GRADE")]
    public class SignTimingNursingGrade
    {
        [SugarColumn(ColumnName = "ID")]
        public string ID { get; set; }

        [SugarColumn(ColumnName = "TIMING_ID")]
        public string TimingID { get; set; }

        [SugarColumn(ColumnName = "NURSING_GRADE")]
        public int NursingGrade { get; set; }

        [SugarColumn(ColumnName = "ORDER_ID")]
        public string OrderID { get; set; }
    }
}
