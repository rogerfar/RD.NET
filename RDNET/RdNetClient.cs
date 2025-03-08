namespace RDNET;

public interface IRdNetClient
{
    IApiApi Api { get; }
    IAuthenticationApi Authentication { get; }
    IDownloadsApi Downloads { get; }
    IHostsApi Hosts { get; }
    ISettingsApi Settings { get; }
    IStreamingApi Streaming { get; }
    ITorrentsApi Torrents { get; }
    ITrafficApi Traffic { get; }
    IUnrestrictApi Unrestrict { get; }
    IUserApi User { get; }

    /// <summary>
    ///     Initialize the API to use ApiToken authentication. The token must be manually retrieved from
    ///     https://real-debrid.com/apitoken and stored in your application.
    /// </summary>
    /// <param name="apiKey">
    ///     The API for the user, retrieved from https://real-debrid.com/apitoken.
    /// </param>
    void UseApiAuthentication(String apiKey);

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
    void UseOAuthAuthentication(String? clientId = null, String? clientSecret = null, String? accessToken = null, String? refreshToken = null);
}

/// <summary>
///     The RdNetClient consumed the Real-Debrid.com API.
///     Documentation about the API can be found here: https://api.real-debrid.com/
/// </summary>
public class RdNetClient : IRdNetClient
{
    private readonly Store _store = new();

    public IApiApi Api { get; }
    public IAuthenticationApi Authentication { get; }
    public IDownloadsApi Downloads { get; }
    public IHostsApi Hosts { get; }
    public ISettingsApi Settings { get; }
    public IStreamingApi Streaming { get; }
    public ITorrentsApi Torrents { get; }
    public ITrafficApi Traffic { get; }
    public IUnrestrictApi Unrestrict { get; }
    public IUserApi User { get; }

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
    /// <param name="apiHostname">
    ///     Optional alternate hostname to use for the api base.
    ///     Useful if the normal hostname <c>api.real-debrid.com</c> is blocked in your region
    /// </param>
    public RdNetClient(String? appId = null, HttpClient? httpClient = null, Int32 retryCount = 1, String? apiHostname = null)
    {
        _store.AppId= appId ?? "X245A4XAIBGVM";
        _store.RetryCount = retryCount;

        if (apiHostname != null)
        {
            _store.ApiHostname = apiHostname;
        }

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

    /// <inheritdoc />
    public void UseApiAuthentication(String apiKey)
    {
        if (String.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("Api Key cannot be null");
        }

        _store.AuthenticationType = AuthenticationType.Api;

        _store.ApiKey = apiKey;
    }

    /// <inheritdoc />
    public void UseOAuthAuthentication(String? clientId = null, String? clientSecret = null, String? accessToken = null, String? refreshToken = null)
    {
        _store.AuthenticationType = AuthenticationType.OAuth2;

        _store.OAuthClientId = clientId;
        _store.OAuthClientSecret = clientSecret;
        _store.OAuthAccessToken = accessToken;
        _store.OAuthRefreshToken = refreshToken;
    }
}