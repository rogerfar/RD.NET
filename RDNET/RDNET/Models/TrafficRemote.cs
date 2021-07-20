using System;
using Newtonsoft.Json;

namespace RDNET
{
    public class TrafficRemote
    {
        /// <summary>
        ///     Available bytes / links to use.
        /// </summary>
        [JsonProperty("left")]
        public Int64 Left { get; set; }

        /// <summary>
        ///     Bytes downloaded.
        /// </summary>
        [JsonProperty("bytes")]
        public Int64 Bytes { get; set; }

        /// <summary>
        ///     Links unrestricted.
        /// </summary>
        [JsonProperty("links")]
        public Int64 Links { get; set; }

        [JsonProperty("limit")]
        public Int64 Limit { get; set; }

        /// <summary>
        ///     One of "links", "gigabytes", "bytes"
        /// </summary>
        [JsonProperty("type")]
        public String Type { get; set; }

        /// <summary>
        ///     Additional traffic / links the user may have buy.
        /// </summary>
        [JsonProperty("extra")]
        public Int64 Extra { get; set; }

        /// <summary>
        ///     One of "daily", "weekly" or "monthly"
        /// </summary>
        [JsonProperty("reset")]
        public String Reset { get; set; }
    }
}
