using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RDNET
{
    public class HostStatus
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("image_big")]
        public Uri ImageBig { get; set; }

        [JsonProperty("supported")]
        public Int64 Supported { get; set; }

        [JsonProperty("status")]
        public String Status { get; set; }

        [JsonProperty("check_time")]
        public DateTimeOffset CheckTime { get; set; }

        [JsonProperty("competitors_status")]
        public IDictionary<String, HostCompetitorsStatus> CompetitorsStatus { get; set; }
    }
    
    public class HostCompetitorsStatus
    {
        [JsonProperty("status")]
        public String Status { get; set; }

        [JsonProperty("check_time")]
        public DateTimeOffset CheckTime { get; set; }
    }
}