using Newtonsoft.Json;

namespace RDNET;

internal class RequestError
{
    [JsonProperty("error")]
    public String? Error { get; set; }

    [JsonProperty("error_code")]
    public Int32? ErrorCode { get; set; }
}