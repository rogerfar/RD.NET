using System;
using Newtonsoft.Json;

namespace RDNET.Models
{
    public class User
    {
        [JsonProperty("id")]
        public Int64 Id { get; set; }

        [JsonProperty("username")]
        public String Username { get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("points")]
        public Int64 Points { get; set; }

        [JsonProperty("locale")]
        public String Locale { get; set; }

        [JsonProperty("avatar")]
        public String Avatar { get; set; }

        [JsonProperty("type")]
        public String Type { get; set; }

        [JsonProperty("premium")]
        public Int64 Premium { get; set; }

        [JsonProperty("expiration")]
        public DateTimeOffset Expiration { get; set; }
    }
}