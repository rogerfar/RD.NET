using Newtonsoft.Json;

namespace RDNET;

public class TrafficDetail
{
    /// <summary>
    ///     By Host main domain, bytes downloaded on concerned host.
    /// </summary>
    [JsonProperty("host")]
    public Dictionary<String, Int64>? Host { get; set; }

    /// <summary>
    ///     Total downloaded (in bytes) this day.
    /// </summary>
    [JsonProperty("bytes")]
    public Int64 Bytes { get; set; }
}