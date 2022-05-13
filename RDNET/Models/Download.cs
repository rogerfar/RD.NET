using System;
using Newtonsoft.Json;

namespace RDNET
{
    public class Download
    {
        /// <summary>
        ///     The ID of the download.
        /// </summary>
        [JsonProperty("id")]
        public String Id { get; set; }

        /// <summary>
        ///     The original file name.
        /// </summary>
        [JsonProperty("filename")]
        public String Filename { get; set; }

        /// <summary>
        ///     Mime Type of the file, guessed by the file extension.
        /// </summary>
        [JsonProperty("mimeType")]
        public String MimeType { get; set; }

        /// <summary>
        ///     Filesize in bytes, 0 if unknown.
        /// </summary>
        [JsonProperty("filesize")]
        public Int64 Filesize { get; set; }

        /// <summary>
        ///     Original link.
        /// </summary>
        [JsonProperty("link")]
        public String Link { get; set; }

        /// <summary>
        ///     Host main domain.
        /// </summary>
        [JsonProperty("host")]
        public String Host { get; set; }

        [JsonProperty("host_icon")]
        public String HostIcon { get; set; }

        /// <summary>
        ///     Max Chunks allowed.
        /// </summary>
        [JsonProperty("chunks")]
        public Int64 Chunks { get; set; }

        /// <summary>
        ///     Generated link.
        /// </summary>
        [JsonProperty("download")]
        public String DownloadUrl { get; set; }

        /// <summary>
        ///     True if streamable.
        /// </summary>
        [JsonProperty("streamable")]
        public Boolean Streamable { get; set; }

        /// <summary>
        ///     Date when generated.
        /// </summary>
        [JsonProperty("generated")]
        public DateTimeOffset Generated { get; set; }
    }
}
