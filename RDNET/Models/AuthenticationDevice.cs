using Newtonsoft.Json;

namespace RDNET;

public class AuthenticationDevice
{
    /// <summary>
    /// The device code user to poll if the user has activated
    /// </summary>
    [JsonProperty("device_code")]
    public String? DeviceCode { get; set; }

    /// <summary>
    /// The user code
    /// </summary>
    [JsonProperty("user_code")]
    public String? UserCode { get; set; }

    /// <summary>
    /// The interval how fast you should check if the user has authenticated
    /// </summary>
    [JsonProperty("interval")]
    public Int64 Interval { get; set; }

    /// <summary>
    /// Amount of seconds when the activation credentials expire
    /// </summary>
    [JsonProperty("expires_in")]
    public Int64 ExpiresIn { get; set; }

    /// <summary>
    /// The URL the user should go and enter the User Code to activate
    /// </summary>
    [JsonProperty("verification_url")]
    public String? VerificationUrl { get; set; }

    /// <summary>
    /// A direct verification URL
    /// </summary>
    [JsonProperty("direct_verification_url")]
    public String? DirectVerificationUrl { get; set; }
}