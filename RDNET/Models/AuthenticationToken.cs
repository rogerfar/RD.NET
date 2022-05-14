using Newtonsoft.Json;

namespace RDNET;

public class AuthenticationToken
{
    /// <summary>
    /// The AccessToken
    /// </summary>
    [JsonProperty("access_token")]
    public String AccessToken { get; set; } = null!;

    /// <summary>
    /// Token validity in seconds
    /// </summary>
    [JsonProperty("expires_in")]
    public String ExpiresIn { get; set; } = null!;

    /// <summary>
    /// "Bearer"
    /// </summary>
    [JsonProperty("token_type")]
    public String TokenType { get; set; } = null!;

    /// <summary>
    /// Token that only expires when your application rights are revoked by user
    /// </summary>
    [JsonProperty("refresh_token")]
    public String RefreshToken { get; set; } = null!;
}