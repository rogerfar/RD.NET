using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RDNET
{
    public class StreamingMediaInfo
    {
        /// <summary>
        /// Cleaned filename
        /// </summary>
        [JsonProperty("filename")]
        public String Filename { get; set; }

        /// <summary>
        /// File hosted on
        /// </summary>
        [JsonProperty("hoster")]
        public String Hoster { get; set; }

        /// <summary>
        /// Original content link
        /// </summary>
        [JsonProperty("link")]
        public String Link { get; set; }

        /// <summary>
        /// "movie" / "show" / "audio"
        /// </summary>
        [JsonProperty("type")]
        public String Type { get; set; }

        /// <summary>
        /// if found, else null
        /// </summary>
        [JsonProperty("season")]
        public String Season { get; set; }

        /// <summary>
        /// if found, else null
        /// </summary>
        [JsonProperty("episode")]
        public String Episode { get; set; }

        /// <summary>
        /// if found, else null
        /// </summary>
        [JsonProperty("year")]
        public String Year { get; set; }

        /// <summary>
        /// media duration in seconds
        /// </summary>
        [JsonProperty("duration")]
        public Decimal Duration { get; set; }

        /// <summary>
        /// birate of the media file
        /// </summary>
        [JsonProperty("bitrate")]
        public Int64 Bitrate { get; set; }

        /// <summary>
        /// original filesize in bytes
        /// </summary>
        [JsonProperty("size")]
        public Int64 Size { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }

        [JsonProperty("baseUrl")]
        public String BaseUrl { get; set; }

        [JsonProperty("availableFormats")]
        public AvailableFormats AvailableFormats { get; set; }

        [JsonProperty("availableQualities")]
        public AvailableQualities AvailableQualities { get; set; }

        [JsonProperty("modelUrl")]
        public String ModelUrl { get; set; }

        [JsonProperty("host")]
        public String Host { get; set; }
        
        [JsonProperty("poster_path")]
        public String PosterPath { get; set; }
        
        [JsonProperty("audio_image")]
        public String AudioImage { get; set; }
        
        [JsonProperty("backdrop_path")]
        public String BackdropPath { get; set; }
    }

    public class AvailableFormats
    {
        [JsonProperty("apple")]
        public String Apple { get; set; }

        [JsonProperty("dash")]
        public String Dash { get; set; }

        [JsonProperty("liveMP4")]
        public String LiveMp4 { get; set; }

        [JsonProperty("h264WebM")]
        public String H264WebM { get; set; }
    }

    public class AvailableQualities
    {
        [JsonProperty("Original")]
        public String Original { get; set; }
    }

    public class Details
    {
        [JsonProperty("video")]
        public IDictionary<String, Video> Video { get; set; }

        [JsonProperty("audio")]
        public IDictionary<String, Audio> Audio { get; set; }

        [JsonProperty("subtitles")]
        public IList<IDictionary<String, Subtitle>> Subtitles { get; set; }
    }
    
    public class Subtitle
    {
        [JsonProperty("stream")]
        public String Stream { get; set; }

        /// <summary>
        /// Language in plain text (ex "English", "French")
        /// </summary>
        [JsonProperty("lang")]
        public String Lang { get; set; }

        /// <summary>
        /// Language in iso_639 (ex fre, eng)
        /// </summary>
        [JsonProperty("lang_iso")]
        public String LangIso { get; set; }

        /// <summary>
        /// Format of subtitles (ex "ASS" / "SRT")
        /// </summary>
        [JsonProperty("type")]
        public String Type { get; set; }
    }
    
    public class Audio
    {
        [JsonProperty("stream")]
        public String Stream { get; set; }

        /// <summary>
        /// Language in plain text (ex "English", "French")
        /// </summary>
        [JsonProperty("lang")]
        public String Lang { get; set; }

        /// <summary>
        /// Language in iso_639 (ex fre, eng)
        /// </summary>
        [JsonProperty("lang_iso")]
        public String LangIso { get; set; }

        /// <summary>
        /// Codec of the audio (ex "aac", "mp3")
        /// </summary>
        [JsonProperty("codec")]
        public String Codec { get; set; }

        /// <summary>
        /// Audio sampling rate
        /// </summary>
        [JsonProperty("sampling")]
        public Int64 Sampling { get; set; }

        /// <summary>
        /// Number of channels (ex 2, 5.1, 7.1)
        /// </summary>
        [JsonProperty("channels")]
        public Int64 Channels { get; set; }
    }
    
    public class Video
    {
        /// <summary>
        /// Stream URL
        /// </summary>
        [JsonProperty("stream")]
        public String Stream { get; set; }

        /// <summary>
        /// Language in plain text (ex "English", "French")
        /// </summary>
        [JsonProperty("lang")]
        public String Lang { get; set; }

        /// <summary>
        /// Language in iso_639 (ex fre, eng)
        /// </summary>
        [JsonProperty("lang_iso")]
        public String LangIso { get; set; }

        /// <summary>
        /// Codec of the video (ex "h264", "divx")
        /// </summary>
        [JsonProperty("codec")]
        public String Codec { get; set; }

        /// <summary>
        /// Colorspace of the video (ex "yuv420p")
        /// </summary>
        [JsonProperty("colorspace")]
        public String Colorspace { get; set; }

        /// <summary>
        /// Width of the video (ex 1980)
        /// </summary>
        [JsonProperty("width")]
        public Int64 Width { get; set; }

        /// <summary>
        /// Height of the video (ex 1080)
        /// </summary>
        [JsonProperty("height")]
        public Int64 Height { get; set; }
    }
}