using System.Globalization;

namespace RDNET;

public interface IApiApi
{
    /// <summary>
    ///     Disable current access token.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    Task DisableTokenAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get server time, raw data returned.
    ///     This request does not need to be authenticated.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>DateTime with the current server time in local time of the Real-Debrid server.</returns>
    Task<DateTime> GetTimeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get server time in ISO, raw data returned.
    /// This request does not need to be authenticated.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>DateTimeOffset with the current server time with offset.</returns>
    Task<DateTimeOffset> GetIsoTimeAsync(CancellationToken cancellationToken = default);
}

public class ApiApi : IApiApi
{
    private readonly Requests _requests;

    internal ApiApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task DisableTokenAsync(CancellationToken cancellationToken = default)
    {
        await _requests.GetRequestAsync("disable_access_token", true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DateTime> GetTimeAsync(CancellationToken cancellationToken = default)
    {
        var result = await _requests.GetRequestAsync("time", false, cancellationToken);
            
        return DateTime.ParseExact(result, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }

    /// <inheritdoc />
    public async Task<DateTimeOffset> GetIsoTimeAsync(CancellationToken cancellationToken = default)
    {
        var result = await _requests.GetRequestAsync("time/iso", false, cancellationToken);

        return DateTimeOffset.ParseExact(result, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
    }
}