using System;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    public class ScaleASFormItem
    {
        [JsonProperty("ITEMID")]
        public string ItemId { get; set; }

        [JsonProperty("ITEMNAME")]
        public string ItemName { get; set; }

        [JsonProperty("ITEMDISPNAME")]
        public string ItemDispName { get; set; }

        public string InputType { get; set; }
    }
}
