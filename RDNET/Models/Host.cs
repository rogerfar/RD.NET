using Newtonsoft.Json;

namespace RDNET;

public class Host
{
    /// <summary>
    ///     ID of the host.
    /// </summary>
    [JsonProperty("id")]
    public String Id { get; set; } = null!;

    /// <summary>
    ///     Name of the host.
    /// </summary>
    [JsonProperty("name")]
    public String? Name { get; set; }

    /// <summary>
    ///     Image of the host.
    /// </summary>
    [JsonProperty("image")]
    public String? Image { get; set; }

    /// <summary>
    ///     Image of the host (but bigger).
    /// </summary>
    [JsonProperty("image_big")]
    public String? ImageBig { get; set; }
}