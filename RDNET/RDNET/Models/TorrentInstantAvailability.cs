using System;
using Newtonsoft.Json;

namespace RDNET
{
    public class TorrentInstantAvailabilityFile
    {
        [JsonProperty("filename")]
        public String Filename { get; set; }

        [JsonProperty("filesize")]
        public Int64 Filesize { get; set; }
    }
}
