using System;
using Newtonsoft.Json;

namespace RDNET.Models
{
    public class UnrestrictLink
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("filename")]
        public String Filename { get; set; }

        [JsonProperty("mimeType")]
        public String MimeType { get; set; }

        [JsonProperty("filesize")]
        public Int64 Filesize { get; set; }

        [JsonProperty("link")]
        public String Link { get; set; }

        [JsonProperty("host")]
        public String Host { get; set; }

        [JsonProperty("host_icon")]
        public String HostIcon { get; set; }

        [JsonProperty("chunks")]
        public Int64 Chunks { get; set; }

        [JsonProperty("crc")]
        public Int64 Crc { get; set; }

        [JsonProperty("download")]
        public String Download { get; set; }

        [JsonProperty("streamable")]
        public Int64 Streamable { get; set; }
    }
}