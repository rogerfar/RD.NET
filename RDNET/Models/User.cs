using System;
using Newtonsoft.Json;

namespace RDNET
{
    public class User
    {
        /// <summary>
        ///     The ID of the user.
        /// </summary>
        [JsonProperty("id")]
        public Int64 Id { get; set; }

        /// <summary>
        ///     The username.
        /// </summary>
        [JsonProperty("username")]
        public String Username { get; set; }

        /// <summary>
        ///     The email of the user (obfuscated).
        /// </summary>
        [JsonProperty("email")]
        public String Email { get; set; }

        /// <summary>
        ///     Fidelity points.
        /// </summary>
        [JsonProperty("points")]
        public Int64 Points { get; set; }

        /// <summary>
        ///     User language.
        /// </summary>
        [JsonProperty("locale")]
        public String Locale { get; set; }

        /// <summary>
        ///     URL to the avatar file.
        /// </summary>
        [JsonProperty("avatar")]
        public String Avatar { get; set; }

        /// <summary>
        ///     "premium" or "free".
        /// </summary>
        [JsonProperty("type")]
        public String Type { get; set; }

        /// <summary>
        ///     Seconds left as a Premium user.
        /// </summary>
        [JsonProperty("premium")]
        public Int64 Premium { get; set; }

        /// <summary>
        ///     Expiration date as a Premium user.
        /// </summary>
        [JsonProperty("expiration")]
        public DateTimeOffset Expiration { get; set; }
    }
}
