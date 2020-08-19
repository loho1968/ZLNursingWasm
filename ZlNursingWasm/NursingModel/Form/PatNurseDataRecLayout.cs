using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    [Table("PAT_NURSE_DATA_REC_LAYOUT")]
    public class PatNurseDataRecLayout
    {
        [Column("NURSE_DATA_ID")]
        public int NurseDataId { get; set; }
        [Column("LAYOUT")]
        public string Layout { get; set; }

        [NotMapped]
        public List<FormRecLayout> FormLayout { get; set; }

    }

    public class FormRecLayout
    {
        public string ItemId { get; set; }
        public string ItemLabel { get; set; }
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }
    }

}
