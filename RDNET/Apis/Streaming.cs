namespace RDNET;

public class StreamingApi
{
    private readonly Requests _requests;

    internal StreamingApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <summary>
    ///     Get transcoding links for given file.
    /// </summary>
    /// <param name="id">ID from /downloads or /unrestrict/link</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    public async Task<Dictionary<String, Dictionary<String, String>>> GetTranscodeAsync(String id, CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<Dictionary<String, Dictionary<String, String>>>($"streaming/transcode/{id}", true, cancellationToken);
    }

    /// <summary>
    ///     Get detailed media informations for given file.
    /// </summary>
    /// <param name="id">ID from /downloads or /unrestrict/link</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    public async Task<StreamingMediaInfo> GetMediaInfoAsync(String id, CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<StreamingMediaInfo>($"streaming/mediaInfos/{id}", true, cancellationToken);
    }
}