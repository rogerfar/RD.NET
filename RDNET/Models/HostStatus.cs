using Newtonsoft.Json;

namespace RDNET;

public class HostStatus
{
    /// <summary>
    /// ID of the host.
    /// </summary>
    [JsonProperty("id")]
    public String Id { get; set; } = null!;

    /// <summary>
    /// Name of the host.
    /// </summary>
    [JsonProperty("name")]
    public String? Name { get; set; }

    /// <summary>
    /// Image of the host.
    /// </summary>
    [JsonProperty("image")]
    public String?  Image { get; set; }

    /// <summary>
    /// Image of the host (but bigger).
    /// </summary>
    [JsonProperty("image_big")]
    public String? ImageBig { get; set; }

    /// <summary>
    /// Is the host supported?
    /// </summary>
    [JsonProperty("supported")]
    public Boolean Supported { get; set; }

    /// <summary>
    /// One of "up" / "down" / "unsupported".
    /// </summary>
    [JsonProperty("status")]
    public String? Status { get; set; }

    /// <summary>
    /// Datetime when last checked.
    /// </summary>
    [JsonProperty("check_time")]
    public DateTimeOffset CheckTime { get; set; }

    /// <summary>
    /// List of statuses of competitors.
    /// </summary>
    [JsonProperty("competitors_status")]
    public IDictionary<String, HostCompetitorsStatus>? CompetitorsStatus { get; set; }
}
    
public class HostCompetitorsStatus
{
    /// <summary>
    /// One of "up" / "down" / "unsupported".
    /// </summary>
    [JsonProperty("status")]
    public String? Status { get; set; }

    /// <summary>
    /// Datetime when last checked.
    /// </summary>
    [JsonProperty("check_time")]
    public DateTimeOffset CheckTime { get; set; }
}