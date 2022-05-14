using Newtonsoft.Json;

namespace RDNET;

public class TorrentInstantAvailabilityFile
{
    /// <summary>
    ///     Name of the file.
    /// </summary>
    [JsonProperty("filename")]
    public String? Filename { get; set; }

    /// <summary>
    ///     Size of the file in bytes.
    /// </summary>
    [JsonProperty("filesize")]
    public Int64 Filesize { get; set; }
}