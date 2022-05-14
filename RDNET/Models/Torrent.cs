using Newtonsoft.Json;

namespace RDNET;

public class Torrent
{
    /// <summary>
    ///     ID of the torrent.
    /// </summary>
    [JsonProperty("id")]
    public String Id { get; set; } = null!;

    /// <summary>
    ///     Filename of the torrent.
    /// </summary>
    [JsonProperty("filename")]
    public String Filename { get; set; } = null!;

    /// <summary>
    ///     Original filename of the torrent.
    /// </summary>
    [JsonProperty("original_filename")]
    public String? OriginalFilename { get; set; }

    /// <summary>
    ///     SHA1 Hash of the torrent.
    /// </summary>
    [JsonProperty("hash")]
    public String Hash { get; set; } = null!;

    /// <summary>
    ///     Size of selected files only.
    /// </summary>
    [JsonProperty("bytes")]
    public Int64 Bytes { get; set; }

    /// <summary>
    ///     Size of all the files.
    /// </summary>
    [JsonProperty("original_bytes")]
    public Int64 OriginalBytes { get; set; }

    /// <summary>
    ///     Host main domain.
    /// </summary>
    [JsonProperty("host")]
    public String? Host { get; set; }

    /// <summary>
    ///     Split size of links.
    /// </summary>
    [JsonProperty("split")]
    public Int64 Split { get; set; }

    /// <summary>
    ///     Possible values: 0 to 100.
    /// </summary>
    [JsonProperty("progress")]
    public Int64 Progress { get; set; }

    /// <summary>
    ///     Current status of the torrent: magnet_error, magnet_conversion, waiting_files_selection, queued, downloading,
    ///     downloaded, error, virus, compressing, uploading, dead.
    /// </summary>
    [JsonProperty("status")]
    public String? Status { get; set; }

    /// <summary>
    ///     Date when torrent was added.
    /// </summary>
    [JsonProperty("added")]
    public DateTimeOffset Added { get; set; }

    /// <summary>
    ///     List of files in the torrent.
    /// </summary>
    [JsonProperty("files")]
    public List<TorrentFile>? Files { get; set; }

    /// <summary>
    ///     List of links.
    /// </summary>
    [JsonProperty("links")]
    public List<String>? Links { get; set; }

    /// <summary>
    ///     Only set when finished.
    /// </summary>
    [JsonProperty("ended")]
    public DateTimeOffset? Ended { get; set; }

    /// <summary>
    ///     Speed of the torrent.
    ///     Only present in "downloading", "compressing", "uploading" status.
    /// </summary>
    [JsonProperty("speed")]
    public Int64? Speed { get; set; }

    /// <summary>
    ///     Amount of seeders
    ///     Only present in "downloading", "magnet_conversion" status.
    /// </summary>
    [JsonProperty("seeders")]
    public Int64? Seeders { get; set; }
}

public class TorrentFile
{
    /// <summary>
    ///     ID of the file in the torrent.
    /// </summary>
    [JsonProperty("id")]
    public Int64 Id { get; set; }

    /// <summary>
    ///     Path to the file inside the torrent, starting with "/".
    /// </summary>
    [JsonProperty("path")]
    public String Path { get; set; } = null!;

    /// <summary>
    ///     Size of the file.
    /// </summary>
    [JsonProperty("bytes")]
    public Int64 Bytes { get; set; }

    /// <summary>
    ///     True if file is selected to be downloaded.
    /// </summary>
    [JsonProperty("selected")]
    public Boolean Selected { get; set; }
}