using System;
using Newtonsoft.Json;

namespace RDNET
{
    public class TorrentAddResult
    {
        /// <summary>
        /// The ID of the torrent
        /// </summary>
        [JsonProperty("id")]
        public String Id { get; set; }

        /// <summary>
        /// The URL of the torrent as a link
        /// </summary>
        [JsonProperty("uri")]
        public String Url { get; set; }
    }
}
