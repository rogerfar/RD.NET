using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RDNET
{
    public class UnrestrictLink
    {
        /// <summary>
        ///     The ID of the file.
        /// </summary>
        [JsonProperty("id")]
        public String Id { get; set; }

        /// <summary>
        ///     The name of the file.
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

        /// <summary>
        ///     Icon for the host main domain.
        /// </summary>
        [JsonProperty("host_icon")]
        public String HostIcon { get; set; }

        /// <summary>
        ///     Max Chunks allowed.
        /// </summary>
        [JsonProperty("chunks")]
        public Int64 Chunks { get; set; }

        /// <summary>
        ///     Disable / enable CRC check.
        /// </summary>
        [JsonProperty("crc")]
        public Int64 Crc { get; set; }

        /// <summary>
        ///     Generated link.
        /// </summary>
        [JsonProperty("download")]
        public String Download { get; set; }

        /// <summary>
        ///     Is the file streamable on website.
        /// </summary>
        [JsonProperty("streamable")]
        public Int64 Streamable { get; set; }

        /// <summary>
        ///     Type of the file (in general, its quality).
        /// </summary>
        [JsonProperty("type")]
        public String Type { get; set; }

        /// <summary>
        ///     Alternative links.
        /// </summary>
        [JsonProperty("alternative")]
        public IList<UnrestrictLink> Alternative { get; set; }
    }
}
