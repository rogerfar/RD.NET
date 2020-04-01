using System;
using Newtonsoft.Json;

namespace RDNET.Models
{
    public class TorrentActiveCount
    {
        /// <summary>
        /// Number of currently active torrents
        /// </summary>
        [JsonProperty("nb")]
        public Int32 Active { get; set; }
        
        /// <summary>
        /// Maximum number of active torrents you can have
        /// </summary>
        [JsonProperty("limit")]
        public Int32 Limit { get; set; }
    }
}
