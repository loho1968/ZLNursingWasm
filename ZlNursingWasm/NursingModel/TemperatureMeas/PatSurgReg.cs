using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    [Serializable]
    [SugarTable("PAT_SURG_REG")]
    public class PatSurgReg
    {
        [SugarColumn(ColumnName ="PID")]
        public string PID { get; set; }

        [SugarColumn(ColumnName ="MR_ID")]
        public string PvID { get; set; }

        [SugarColumn(ColumnName ="SURG_TIME_START")]
        public DateTime? SurgTimeStart { get; set; }
    }
}
