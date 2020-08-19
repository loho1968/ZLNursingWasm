using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


//护理记录日志
namespace NursingModel.Form
{

    public class NurseRecLog
    {
        [JsonProperty("pat_observ_rec")]
        public List<PatObservRec> PatObservRec { get; set; }
    }
    public class PatObservRec
    {
        [JsonProperty("result")]
        public List<ObservRecResult> Results { get; set; }
        [JsonProperty("recorder_name")]
        public string RecorderName { get; set; }
        [JsonProperty("observ_time")]
        public DateTime ObservTime { get; set; }
    }

    public class ObservRecResult
    {
        [JsonProperty("observ_item_id")]
        public string ObservItemId { get; set; }
        [JsonProperty("observ_item_name")]
        public string ObservItemName { get; set; }
        [JsonProperty("item_value")]
        public string Value { get; set; }
    }
}
