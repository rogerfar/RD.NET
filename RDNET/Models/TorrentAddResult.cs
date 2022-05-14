using Newtonsoft.Json;

namespace RDNET;

public class TorrentAddResult
{
    /// <summary>
    ///     The ID of the torrent.
    /// </summary>
    [JsonProperty("id")]
    public String Id { get; set; } = null!;

    /// <summary>
    ///     The URL of the torrent as a link.
    /// </summary>
    [JsonProperty("uri")]
    public String Url { get; set; } = null!;
}