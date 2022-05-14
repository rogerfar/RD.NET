using System.Collections.Specialized;
using RDNET.Helpers;

namespace RDNET.Apis;

public class TrafficApi
{
    private readonly Requests _requests;

    internal TrafficApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <summary>
    ///     Get traffic informations for limited hosters (limits, current usage, extra packages).
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    public async Task<Dictionary<String, TrafficRemote>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<Dictionary<String, TrafficRemote>>("traffic", true, cancellationToken);
    }

    /// <summary>
    ///     Get traffic details on each hoster used during a defined period.
    ///     Warning: The period can not exceed 31 days.
    /// </summary>
    /// <param name="start">Start period, default: a week ago</param>
    /// <param name="end">End period, default: today (This parameter does not seem to work!)</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    public async Task<Dictionary<DateTime, TrafficDetail>> GetDetailsAsync(DateTime? start, DateTime? end, CancellationToken cancellationToken = default)
    {
        var parameters = new NameValueCollection();

        if (start.HasValue)
        {
            parameters.Add("start", start.Value.ToString("yyyy-MM-dd"));
        }

        if (end.HasValue)
        {
            parameters.Add("end", end.Value.ToString("yyyy-MM-dd"));
        }

        return await _requests.GetRequestAsync<Dictionary<DateTime, TrafficDetail>>($"traffic/details{parameters.ToQueryString()}", true, cancellationToken);
    }
}