namespace RDNET;

/// <summary>
///     The RdNetClient consumed the Real-Debrid.com API.
///     Documentation about the API can be found here: https://api.real-debrid.com/
/// </summary>
public class RdNetClient
{
    private readonly Store _store = new();

    public ApiApi Api { get; }
    public AuthenticationApi Authentication { get; }
    public DownloadsApi Downloads { get; }
    public HostsApi Hosts { get; }
    public SettingsApi Settings { get; }
    public StreamingApi Streaming { get; }
    public TorrentsApi Torrents { get; }
    public TrafficApi Traffic { get; }
    public UnrestrictApi Unrestrict { get; }
    public UserApi User { get; }
        
    /// <summary>
    ///     Initialize the RdNet API.
    ///     To use authentication make sure to call either UseApiAuthentication for Api Key authentication
    ///     or UseOAuthAuthentication for Auth2 authentication. The latter is also used for Device authentication.
    /// </summary>
    /// <param name="appId">
    ///     The ID of your application. If NULL the app id will be set to the default Opensource App ID
    ///     X245A4XAIBGVM. You can request a new key through the Help section on Real-Debrid.
    /// </param>
    /// <param name="httpClient">
    ///     Optional HttpClient if you want to use your own HttpClient.
    /// </param>
    /// <param name="retryCount">
    ///     The API will retry this many times before failing.
    /// </param>
    public RdNetClient(String? appId = null, HttpClient? httpClient = null, Int32 retryCount = 1)
    {
        _store.AppId= appId ?? "X245A4XAIBGVM";
        _store.RetryCount = retryCount;

        var client = httpClient ?? new HttpClient();

        Api = new ApiApi(client, _store);
        Authentication = new AuthenticationApi(client, _store);
        Downloads = new DownloadsApi(client, _store);
        Hosts = new HostsApi(client, _store);
        Settings = new SettingsApi(client, _store);
        Streaming = new StreamingApi(client, _store);
        Torrents = new TorrentsApi(client, _store);
        Traffic = new TrafficApi(client, _store);
        Unrestrict = new UnrestrictApi(client, _store);
        User = new UserApi(client, _store);
    }

    /// <summary>
    ///     Initialize the API to use ApiToken authentication. The token must be manually retrieved from
    ///     https://real-debrid.com/apitoken and stored in your application.
    /// </summary>
    /// <param name="apiKey">
    ///     The API for the user, retrieved from https://real-debrid.com/apitoken.
    /// </param>
    public void UseApiAuthentication(String apiKey)
    {
        if (String.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("Api Key cannot be null");
        }

        _store.AuthenticationType = AuthenticationType.Api;

        _store.ApiKey = apiKey;
    }

    /// <summary>
    ///     Initialize the API to use three legged OAuth2 authentication.
    ///     This method should also be used for device authentication.
    ///     To see the flow use https://api.real-debrid.com/#device_auth as a reference.
    ///     To use call the following methods:
    ///     - OAuthAuthorizationUrl
    ///     - OAuthAuthorizationResponseAsync
    ///     When receiving the authentication tokens, save the "AccessToken" and "RefreshToken" in your database for future for each user.
    /// 
    ///     To use device authentication use the following methods first:
    ///     - GetDevicdeCode
    ///     - DeviceAuthVerifyAsync. Poll this method every 5 seconds.
    ///     - OAuthAuthorizationResponseAsync. Use this to trade the device code for an access token.
    ///     When receiving the authentication tokens, save the "ClientId", "ClientSecret", "AccessToken" and "RefreshToken" in your database for future for each user.
    /// </summary>
    /// <param name="clientId">
    ///     The client_id for your application or received client_id from token authentication.
    /// </param>
    /// <param name="clientSecret">
    ///     The client_secret for your application or received client_id from token authentication.
    /// </param>
    /// <param name="accessToken">
    ///     The access_token from previously authenticated user.
    /// </param>
    /// <param name="refreshToken">
    ///     The refresh_token from previously authenticated user.
    /// </param>
    public void UseOAuthAuthentication(String? clientId = null, String? clientSecret = null, String? accessToken = null, String? refreshToken = null)
    {
        _store.AuthenticationType = AuthenticationType.OAuth2;

        _store.OAuthClientId = clientId;
        _store.OAuthClientSecret = clientSecret;
        _store.OAuthAccessToken = accessToken;
        _store.OAuthRefreshToken = refreshToken;
    }
}