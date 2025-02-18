namespace RDNET;

public interface IHostsApi
{
    /// <summary>
    ///     Get supported hosts.
    ///     This request does not require authentication.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>A list of all supported hosts.</returns>
    Task<IDictionary<String, Host>> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get status of supported hosters and their status on competitors.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>A list of hosts and their status.</returns>
    Task<IDictionary<String, HostStatus>> GetStatusAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get all supported links Regex, useful to find supported links inside a document.
    ///     This request does not require authentication.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    Task<IList<String>> GetRegexAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get all supported folder Regex, useful to find supported links inside a document.
    ///     This request does not require authentication.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    Task<IList<String>> GetRegexFolderAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get all hoster domains supported on the service.
    ///     This request does not require authentication.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    Task<IList<String>> GetDomainsAsync(CancellationToken cancellationToken = default);
}

public class HostsApi : IHostsApi
{
    private readonly Requests _requests;

    internal HostsApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }
        
    /// <inheritdoc />
    public async Task<IDictionary<String, Host>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<Dictionary<String, Host>>("hosts", false, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IDictionary<String, HostStatus>> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<Dictionary<String, HostStatus>>($"hosts/status", true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IList<String>> GetRegexAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<List<String>>("hosts/regex", false, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IList<String>> GetRegexFolderAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<List<String>>("hosts/regexFolder", false, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IList<String>> GetDomainsAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<List<String>>("hosts/domains", false, cancellationToken);
    }
}