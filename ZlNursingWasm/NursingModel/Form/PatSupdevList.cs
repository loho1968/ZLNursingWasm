using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    [Table("PAT_SUPDEV_LIST")]
    public class PatSupdevList
    {
        //场景标识|
        [Column("ENCOUNTER")]
        [JsonProperty("ENCOUNTER")]
        public string Pvid { get; set; }

        //场景类型:1-门诊,2-住院
        [Column("ENC_TYPE")]
        [JsonProperty("ENC_TYPE")]
        public int EncType { get; set; }

        //医嘱ID
        [Column("ORDER_ID")]
        [JsonProperty("ORDER_ID")]
        public int OrderId { get; set; }

        //患者ID
        [Column("PAT_ID")]
        [JsonProperty("PAT_ID")]
        public string Pid { get; set; }

        //病人附着物ID|
        [Column("PAT_SUPDEV_ID")]
        [JsonProperty("PAT_SUPDEV_ID")]
        public int PatSupdevId { get; set; }

        //病人附着物名称|
        [Column("PAT_SUPDEV_NAME")]
        [JsonProperty("PAT_SUPDEV_NAME")]
        public string PatSupdevName { get; set; }

        //病人附着物状态|1-使用,2-移除,3-作废
        [Column("PAT_SUPDEV_STATUS")]
        [JsonProperty("PAT_SUPDEV_STATUS")]
        public int PatSupdevStatus { get; set; }

        //安置时间｜
        [Column("PLACED_TIME")]
        [JsonProperty("PLACED_TIME")]
        public DateTime? PlacedTime { get; set; }

        //拔管时间|
        [Column("PULL_TIME")]
        [JsonProperty("PULL_TIME")]
        public DateTime? PullTime { get; set; }

        //拔管类型|计划、患者自拔、管路滑脱、阻塞、感染、材质问题、其它
        [Column("PULL_TYPE")]
        [JsonProperty("PULL_TYPE")]
        public string PullType { get; set; }

        //附着物部位|对应一个所见项
        [Column("SUPDEV_BODY_SITE")]
        [JsonProperty("SUPDEV_BODY_SITE")]
        public string SupdevBodySite { get; set; }

        //管道深度|cm，对应一个所见项
        [Column("SUPDEV_DEPTH")]
        [JsonProperty("SUPDEV_DEPTH")]
        public int SupdevDepth { get; set; }

        //附着物固定方式|胶布，固定器，缝合
        [Column("SUPDEV_FIXED_MODE")]
        [JsonProperty("SUPDEV_FIXED_MODE")]
        public string SupdevFixedMode { get; set; }

        //管道等级|高危、中危、低危等，对应一个所见项
        [Column("SUPDEV_LEVEL")]
        [JsonProperty("SUPDEV_LEVEL")]
        public string SupdevLevel { get; set; }

        //附着物来源|院内置管；院外带入
        [Column("SUPDEV_SOURCE")]
        [JsonProperty("SUPDEV_SOURCE")]
        public string SupdevSource { get; set; }

        //附着物类型ID|
        [Column("SUPDEV_TYPE_ID")]
        [JsonProperty("SUPDEV_TYPE_ID")]
        public string SupdevTypeId { get; set; }

        //病人的管道操作记录
        //[NotMapped]
        //public List<PatSupdevOperating> PatSupdevOperating { get; set; }

        //病人管道评估数据
        [NotMapped]
        public List<FormData> PatSupdevDatas { get; set; }


        //表单分数
        //[NotMapped]
        //public List<FormScore> FormScores { get; set; }

        ////表单护理措施
        //[NotMapped]
        //public List<FormNiList> FormNiLists { get; set; }

    }
}
