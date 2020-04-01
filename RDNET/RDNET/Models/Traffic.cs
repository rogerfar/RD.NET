using System;
using Newtonsoft.Json;

namespace RDNET.Models
{
    public class Traffic
    {
        [JsonProperty("scribd.com")]
        public TrafficRemote ScribdCom { get; set; }

        [JsonProperty("ulozto.net")]
        public TrafficRemote UloztoNet { get; set; }

        [JsonProperty("remote")]
        public TrafficRemote Remote { get; set; }
    }

    public class TrafficRemote
    {
        [JsonProperty("left")]
        public Int64 Left { get; set; }

        [JsonProperty("bytes")]
        public Int64 Bytes { get; set; }

        [JsonProperty("links")]
        public Int64 Links { get; set; }

        [JsonProperty("limit")]
        public Int64 Limit { get; set; }

        [JsonProperty("type")]
        public String Type { get; set; }

        [JsonProperty("extra")]
        public Int64 Extra { get; set; }

        [JsonProperty("reset")]
        public String Reset { get; set; }
    }
}