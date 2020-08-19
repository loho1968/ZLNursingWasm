using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    //病人管道操作记录
    [Table("PAT_SUPDEV_OPERATING")]
    public class PatSupdevOperating
    {
        //操作者姓名|
        [Column("OPER_BY")]
        [JsonProperty("OPER_BY")]
        public string OperBy { get; set; }

        //操作者ID|
        [Column("OPER_BY_ID")]
        [JsonProperty("OPER_BY_ID")]
        public string OperById { get; set; }

        //操作状态|1-开始；2-完成。用于处理评估表单在APEX中填写的同步
        [Column("OPER_STATUS")]
        [JsonProperty("OPER_STATUS")]
        public int OperStatus { get; set; }

        //操作时间|
        [Column("OPER_TIME")]
        [JsonProperty("OPER_TIME")]
        public DateTime? OperTime { get; set; }

        //操作类型|置管、拔管、评估
        [Column("OPER_TYPE")]
        [JsonProperty("OPER_TYPE")]
        public string OperType { get; set; }

        //病人附着物ID|
        [Column("PAT_SUPDEV_ID")]
        [JsonProperty("PAT_SUPDEV_ID")]
        public int PatSupdevId { get; set; }

        //病人附着物操作记录ID|
        [Column("PAT_SUPDEV_OPER_ID")]
        [JsonProperty("PAT_SUPDEV_OPER_ID")]
        public int PatSupdevOperId { get; set; }

        //病区id
        [Column("WARD_ID")]
        [JsonProperty("WARD_ID")]
        public int WardId { get; set; }

        //病区名称
        [Column("WARD_NAME")]
        [JsonProperty("WARD_NAME")]
        public string WardName { get; set; }

        //评估数据
        [NotMapped]
        public List<FormData> FormDatas { get; set; }

        //public PatSupdevList PatSupdevList { get; set; }
    }
}
