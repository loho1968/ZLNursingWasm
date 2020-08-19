using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    public class PatScaleLog
    {
        [JsonProperty("result")]
        public List<PatScaleFormResult> Result { get; set; }

    }
    public class PatScaleFormResult
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }
        [JsonProperty("item_name")]
        public string ItemName { get; set; }
        [JsonProperty("item_value")]
        public string ItemValue { get; set; }
        [JsonProperty("item_children")]
        public List<ItemChildren> ItemChildren { get; set; }

        public DateTime? ObservTime { get; set; }

    }
    public class ItemChildren
    {
        [JsonProperty("item_value")]
        public string ItemValue { get; set; }
        [JsonProperty("observ_itemID")]
        public string ItemId { get; set; }
    }
}
