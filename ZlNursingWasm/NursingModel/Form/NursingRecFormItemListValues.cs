using System;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    [Serializable]
    public class NursingRecFormItemListValues
    {
        [JsonProperty("FORMID")]
        public string FormId { get; set; }

        [JsonProperty("ITEMDATAID")]
        public string ItemDataId { get; set; }


        [JsonProperty("ITEMDATANAME")]
        public string ItemDataName { get; set; }

        [JsonProperty("RETURN")]
        public string Return { get; set; }
        [JsonProperty("DISPLAY")]
        public string Display { get; set; }
        [JsonProperty("NO")]
        public string No { get; set; }
        [JsonProperty("OTHERSIGN")]
        public decimal? OtherSign { get; set; }

        [JsonProperty("OBSERVITEMID")]
        public string ObservItemId { get; set; }
        //和评分评估表统一
        public int? Score { get; set; }



    }


}
