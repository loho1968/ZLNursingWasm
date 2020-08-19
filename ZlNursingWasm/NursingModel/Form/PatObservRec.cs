using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NursingModel.Form
{
    //病人所见项记录，前面用SqlSugar做了一个Model映射，这里是EF映射
    [Table("PAT_OBSERV_REC")]
    public class EFPatObservRec
    {
        //所见项记录ID
        [Column("OBSERV_INDEX_ID")]
        public int ObservIndexId { get; set; }

        //所见项ID
        [Column("OBSERV_ITEM_ID")]
        public string ObservItemId { get; set; }

        //项目名称
        [Column("ITEM_NAME")]
        public string ItemName { get; set; }

        //项目编码
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }

        //项目值
        [Column("VALUE")]
        public string Value { get; set; }

        //项目值ID
        [Column("VALUE_ID")]
        public string ValueId { get; set; }

        //相关操作
        [Column("REL_PROC")]
        public string ProcName { get; set; }

        //相关操作记录ID
        [Column("REL_PROC_ID")]
        public string ProcId { get; set; }

        //所见项类型
        [Column("OBSERV_CATEGORY")]
        public string ObservCategory { get; set; }

        //患者ID
        [Column("PAT_ID")]
        public string Pid { get; set; }

        //场景类型:1-门诊,2-住院
        [Column("ENC_TYPE")]
        public int? EncType { get; set; }

        //场景标识
        [Column("ENCOUNTER")]
        public string Pvid { get; set; }

        //记录状态
        [Column("REC_STATUS")]
        public string RecStatus { get; set; }

        //备注
        [Column("NOTE")]
        public string Note { get; set; }

        //记录人
        [Column("RECORDER")]
        public string Recorder { get; set; }

        //记录时间
        [Column("REC_TIME")]
        public DateTime? RecTime { get; set; }

        //记录人姓名
        [Column("RECORDER_NAME")]
        public string RecorderName { get; set; }

        //所见项观察时间|
        [Column("OBSERV_TIME")]
        public DateTime? ObservTime { get; set; }

        //患者附着物ID|
        [Column("PAT_SUPDEV_ID")]
        public int? PatSupdevId { get; set; }



        //附作物类型ID|
        [Column("SUPDEV_TYPE_ID")]
        public string SupdevTypeId { get; set; }

        //附作物类型名称|
        [Column("SUPDEV_TYPE_NAME")]
        public string SupdevTypeName { get; set; }

        //病人附作物名称|
        [Column("PAT_SUPDEV_NAME")]
        public string PatSupdevName { get; set; }

        //上级所见项ID｜
        [Column("PARENT_ITEM_ID")]
        public string ParentItemId { get; set; }

        //所见项分类|O-所见项;S-症状
        [Column("ITEM_TYPE")]
        public string ItemType { get; set; }

        //婴儿序号|不为零则为婴儿
        [Column("NB_SNO")]
        public int? Baby { get; set; }

    }
}
