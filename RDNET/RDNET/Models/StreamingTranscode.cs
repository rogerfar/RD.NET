using System;
using Newtonsoft.Json;

namespace RDNET.Models
{
    public class StreamingTranscode
    {
        [JsonProperty("apple")]
        public StreamingTranscodeLink Apple { get; set; }

        [JsonProperty("dash")]
        public StreamingTranscodeLink Dash { get; set; }

        [JsonProperty("liveMP4")]
        public StreamingTranscodeLink LiveMp4 { get; set; }

        [JsonProperty("h264WebM")]
        public StreamingTranscodeLink H264WebM { get; set; }
    }

    public class StreamingTranscodeLink
    {
        [JsonProperty("full")]
        public String Full { get; set; }
    }
}
