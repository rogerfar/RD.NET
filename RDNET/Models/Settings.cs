using Newtonsoft.Json;

namespace RDNET;

public class Settings
{
    /// <summary>
    ///     Available "download_port" values.
    /// </summary>
    [JsonProperty("download_ports")]
    public List<String> DownloadPorts { get; set; } = [];

    /// <summary>
    ///     Available "locale" values.
    /// </summary>
    [JsonProperty("locales")]
    public Dictionary<String, String> Locales { get; set; } = [];

    /// <summary>
    ///     Available "streaming_quality" values.
    /// </summary>
    [JsonProperty("streaming_qualities")]
    public List<String> StreamingQualities { get; set; } = [];

    /// <summary>
    ///     Available "streaming_language" values.
    /// </summary>
    [JsonProperty("streaming_languages")]
    public Dictionary<String, String> StreamingLanguages { get; set; } = [];

    /// <summary>
    ///     Audio on Google Cast devices setting.
    /// </summary>
    [JsonProperty("streaming_cast_audio")]
    public List<String> StreamingCastAudio { get; set; } = [];

    /// <summary>
    ///     Download port setting
    /// </summary>
    [JsonProperty("download_port")]
    public String DownloadPort { get; set; } = null!;

    /// <summary>
    ///     Locale setting.
    /// </summary>
    [JsonProperty("locale")]
    public String Locale { get; set; } = null!;

    /// <summary>
    ///     Streaming quality setting.
    /// </summary>
    [JsonProperty("streaming_quality")]
    public String StreamingQuality { get; set; } = null!;

    /// <summary>
    ///     Mobile streaming quality setting.
    /// </summary>
    [JsonProperty("mobile_streaming_quality")]
    public String MobileStreamingQuality { get; set; } = null!;

    /// <summary>
    ///     Streaming language setting.
    /// </summary>
    [JsonProperty("streaming_language_preference")]
    public String StreamingLanguagePreference { get; set; } = null!;

    /// <summary>
    ///     Available audio on Google Cast values.
    /// </summary>
    [JsonProperty("streaming_cast_audio_preference")]
    public String StreamingCastAudioPreference { get; set; } = null!;
}