using System.Web;

namespace RDNET;

public interface IAuthenticationApi
{
    /// <summary>
    ///     Get the URL to redirect the user to start with OAuth2 authentication.
    /// </summary>
    /// <param name="redirectUri">
    ///     One of your application's redirect URLs (must be url encoded)
    /// </param>
    /// <param name="state">
    ///     An arbitrary string that will be returned to your application, to help you check against CSRF.
    /// </param>
    /// <returns>
    ///     The user gets redirected to the URL you specified using the parameter redirect_uri, with the following query string
    ///     parameters:
    ///     code: the code that you will use to get a token
    ///     state: the same value that you sent earlier
    /// </returns>
    String GetOAuthAuthorizationUrl(Uri redirectUri, String state);

    /// <summary>
    ///     Using the received token from the user get the access tokens.
    /// </summary>
    /// <param name="clientId">
    ///     The client_id either from your application or from the device authorization.
    /// </param>
    /// <param name="clientSecret">
    ///     The client_secret either from your application or from the device authorization.
    /// </param>
    /// <param name="code">
    ///     The code that was retrieved from the redirect uri. When null the device code is used.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>
    ///     The authentication tokens.
    /// </returns>
    Task<AuthenticationToken> GetOAuthAuthorizationTokensAsync(String clientId, String clientSecret, String? code = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get new authentication credentials by requesting the authenticate the user manually.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>
    ///     An object with info how to have the user authenticate.
    /// </returns>
    Task<AuthenticationDevice> GetDeviceAuthorizeRequestAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Check if the user has verified its device.
    ///     This method will throw an exception when the user has not verified the device yet.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>
    ///     The authentication tokens or NULL when the user is not verified.
    /// </returns>
    Task<AuthenticationVerify?> VerifyDeviceAuthentication(CancellationToken cancellationToken = default);

    Task<AuthenticationToken> RefreshTokenAsync(CancellationToken cancellationToken = default);
}

public class AuthenticationApi : IAuthenticationApi
{
    private readonly Store _store;
    private readonly Requests _requests;

    internal AuthenticationApi(HttpClient httpClient, Store store)
    {
        _store = store;
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public String GetOAuthAuthorizationUrl(Uri redirectUri, String state)
    {
        var redirectUriEncoded = HttpUtility.UrlEncode(redirectUri.OriginalString);

        return $"{_store.AuthUrl}auth?client_id={_store.AppId}&redirect_uri={redirectUriEncoded}&response_type=code&state={state}";
    }

    /// <inheritdoc />
    public async Task<AuthenticationToken> GetOAuthAuthorizationTokensAsync(String clientId, String clientSecret, String? code = null, CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<String, String?>("client_id", clientId),
            new KeyValuePair<String, String?>("client_secret", clientSecret),
            new KeyValuePair<String, String?>("code", code ?? _store.DeviceCode),
            new KeyValuePair<String, String?>("grant_type", "http://oauth.net/grant_type/device/1.0")
        };

        var result = await _requests.PostAuthRequestAsync<AuthenticationToken>("token", data, cancellationToken);

        _store.OAuthAccessToken = result.AccessToken;
        _store.OAuthRefreshToken = result.RefreshToken;

        return result;
    }

    /// <inheritdoc />
    public async Task<AuthenticationDevice> GetDeviceAuthorizeRequestAsync(CancellationToken cancellationToken = default)
    {
        var result = await _requests.GetAuthRequestAsync<AuthenticationDevice>($"device/code?client_id={_store.AppId}&new_credentials=yes", cancellationToken);

        if (result.DeviceCode != null)
        {
            _store.DeviceCode = result.DeviceCode;
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<AuthenticationVerify?> VerifyDeviceAuthentication(CancellationToken cancellationToken = default)
    {
        if (String.IsNullOrWhiteSpace(_store.DeviceCode))
        {
            throw new ArgumentException("DeviceCode cannot be null or empty");
        }

        try
        {
            return await _requests.GetAuthRequestAsync<AuthenticationVerify>($"device/credentials?client_id={_store.AppId}&code={_store.DeviceCode}", cancellationToken);
        }
        catch (RealDebridException exception)
        {
            if (exception.ErrorCode == null)
            {
                return null;
            }

            throw;
        }
    }

    public async Task<AuthenticationToken> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<String, String?>("client_id", _store.OAuthClientId),
            new KeyValuePair<String, String?>("client_secret", _store.OAuthClientSecret),
            new KeyValuePair<String, String?>("code", _store.OAuthRefreshToken),
            new KeyValuePair<String, String?>("grant_type", "http://oauth.net/grant_type/device/1.0")
        };

        var result = await _requests.PostAuthRequestAsync<AuthenticationToken>("token", data, cancellationToken);

        _store.OAuthAccessToken = result.AccessToken;
        _store.OAuthRefreshToken = result.RefreshToken;

        return result;
    }
}