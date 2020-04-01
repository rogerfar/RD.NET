using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RDNET.Models
{
    public class Settings
    {
        [JsonProperty("download_ports")]
        public List<String> DownloadPorts { get; set; }

        [JsonProperty("download_port")]
        public String DownloadPort { get; set; }

        [JsonProperty("locale")]
        public String Locale { get; set; }

        [JsonProperty("streaming_qualities")]
        public List<String> StreamingQualities { get; set; }

        [JsonProperty("streaming_quality")]
        public String StreamingQuality { get; set; }

        [JsonProperty("mobile_streaming_quality")]
        public String MobileStreamingQuality { get; set; }

        [JsonProperty("streaming_language_preference")]
        public String StreamingLanguagePreference { get; set; }

        [JsonProperty("streaming_cast_audio")]
        public List<String> StreamingCastAudio { get; set; }

        [JsonProperty("streaming_cast_audio_preference")]
        public String StreamingCastAudioPreference { get; set; }
    }
}