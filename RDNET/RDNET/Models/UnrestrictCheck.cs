using System;
using Newtonsoft.Json;

namespace RDNET
{
    public class UnrestrictCheck
    {
        /// <summary>
        ///     Host main domain.
        /// </summary>
        [JsonProperty("host")]
        public String Host { get; set; }

        /// <summary>
        ///     Host icon.
        /// </summary>
        [JsonProperty("host_icon")]
        public String HostIcon { get; set; }

        /// <summary>
        ///     Host icon but bigger.
        /// </summary>
        [JsonProperty("host_icon_big")]
        public String HostIconBig { get; set; }

        /// <summary>
        ///     Link to the file.
        /// </summary>
        [JsonProperty("link")]
        public String Link { get; set; }

        /// <summary>
        ///     Filename.
        /// </summary>
        [JsonProperty("filename")]
        public String Filename { get; set; }

        /// <summary>
        ///     Site of the file in bytes.
        /// </summary>
        [JsonProperty("filesize")]
        public Int64 Filesize { get; set; }

        /// <summary>
        ///     Is the file supported?
        /// </summary>
        [JsonProperty("supported")]
        public Boolean Supported { get; set; }
    }
}
