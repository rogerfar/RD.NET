using System;
using Newtonsoft.Json;

namespace RDNET
{
    public class UnrestrictCheck
    {
        [JsonProperty("host")]
        public String Host { get; set; }

        [JsonProperty("host_icon")]
        public String HostIcon { get; set; }

        [JsonProperty("host_icon_big")]
        public String HostIconBig { get; set; }

        [JsonProperty("link")]
        public String Link { get; set; }

        [JsonProperty("filename")]
        public String Filename { get; set; }

        [JsonProperty("filesize")]
        public Int64 Filesize { get; set; }

        [JsonProperty("supported")]
        public Int64 Supported { get; set; }
    }
}