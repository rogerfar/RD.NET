using System;
using RDNET.Enums;

namespace RDNET.Apis
{
    internal class Store
    {
        public String AuthUrl = "https://api.real-debrid.com/oauth/v2/";
        public String ApiUrl = "https://api.real-debrid.com/rest/1.0/";

        public String AppId;
        public Int32 RetryCount { get; set; }

        public AuthenticationType AuthenticationType;

        public String ApiKey;

        public String DeviceCode { get; set; }

        public String OAuthAccessToken;
        public String OAuthClientId;
        public String OAuthClientSecret;
        public String OAuthRefreshToken;

        public String BearerToken
        {
            get
            {
                return AuthenticationType switch
                {
                    AuthenticationType.Api when String.IsNullOrWhiteSpace(ApiKey) => 
                        throw new Exception("No API key set, make sure to call UseApiAuthentication with a valid API key."),
                    AuthenticationType.Api => ApiKey,
                    AuthenticationType.OAuth2 when String.IsNullOrWhiteSpace(OAuthAccessToken) => 
                        throw new Exception("No access token set, make sure to call UseOAuthFlowAuthentication with a valid access token."),
                    AuthenticationType.OAuth2 => OAuthAccessToken,
                    _ => throw new Exception("No valid authentication token found")
                };
            }
        }
    }
}
