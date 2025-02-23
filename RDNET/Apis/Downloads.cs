using System.Collections.Specialized;

namespace RDNET;

public interface IDownloadsApi
{
    /// <summary>
    ///     Get the number of downloads.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>The number of downloads.</returns>
    Task<Int64> GetTotal(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get list of downloads for the user by page.
    /// </summary>
    /// <param name="page">Pagination system</param>
    /// <param name="limit">Entries returned per page / request (max 100)</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>A list of downloads.</returns>
    Task<IList<Download>> GetPageAsync(Int32? page = null, Int32? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get list of downloads for the user by offset.
    /// </summary>
    /// <param name="offset">Starting offset</param>
    /// <param name="limit">Entries returned per page / request (max 100)</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>A list of downloads.</returns>
    Task<IList<Download>> GetAsync(Int32? offset = null, Int32? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Delete a link from downloads list.
    /// </summary>
    /// <param name="id">The ID of the file in the download folder</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    Task DeleteAsync(String id, CancellationToken cancellationToken = default);
}

public class DownloadsApi : IDownloadsApi
{
    private readonly Requests _requests;

    internal DownloadsApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<Int64> GetTotal(CancellationToken cancellationToken = default)
    {
        var result = await _requests.GetRequestHeaderAsync($"downloads?limit=1", "X-Total-Count", true, cancellationToken);

        if (result == null)
        {
            return 0;
        }

        return Int64.Parse(result);
    }

    /// <inheritdoc />
    public async Task<IList<Download>> GetPageAsync(Int32? page = null, Int32? limit = null, CancellationToken cancellationToken = default)
    {
        var parameters = new NameValueCollection();
            
        if (page > 0)
        {
            parameters.Add("page", page.ToString());
        }

        if (limit > 0)
        {
            parameters.Add("limit", limit.ToString());
        }

        return await _requests.GetRequestAsync<List<Download>>($"downloads{parameters.ToQueryString()}", true, cancellationToken);
    }
        
    /// <inheritdoc />
    public async Task<IList<Download>> GetAsync(Int32? offset = null, Int32? limit = null, CancellationToken cancellationToken = default)
    {
        var parameters = new NameValueCollection();
            
        if (offset > 0)
        {
            parameters.Add("offset", offset.ToString());
        }

        if (limit > 0)
        {
            parameters.Add("limit", limit.ToString());
        }

        return await _requests.GetRequestAsync<List<Download>>($"downloads{parameters.ToQueryString()}", true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(String id, CancellationToken cancellationToken = default)
    {
        await _requests.DeleteRequestAsync($"downloads/delete/{id}", true, cancellationToken);
    }
}