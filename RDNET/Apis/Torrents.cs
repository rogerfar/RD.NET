using System.Collections.Specialized;
using Newtonsoft.Json;
using AvailableFiles = System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.String,
    System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.String, RDNET.TorrentInstantAvailabilityFile>>>>;
using AvailableFiles2 = System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.String, RDNET.TorrentInstantAvailabilityFile>>>;

namespace RDNET;

public class TorrentsApi
{
    private readonly Requests _requests;

    internal TorrentsApi(HttpClient httpClient, Store store)
    {
        _requests = new(httpClient, store);
    }

    /// <summary>
    ///     Get the number of torrents.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>The number of torrents.</returns>
    public async Task<Int64> GetTotal(CancellationToken cancellationToken = default)
    {
        var result = await _requests.GetRequestHeaderAsync($"torrents?limit=1", "X-Total-Count", true, cancellationToken);

        if (result == null)
        {
            return 0;
        }

        return Int64.Parse(result);
    }

    /// <summary>
    ///     Get user torrents list by offset.
    /// </summary>
    /// <param name="offset">Starting offset</param>
    /// <param name="limit">Entries returned per page / request (max 100, default: 50)</param>
    /// <param name="filter">"active", list active torrents first</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>List of torrents.</returns>
    public async Task<IList<Torrent>> GetAsync(Int32? offset = null,
                                               Int32? limit = null,
                                               String? filter = null,
                                               CancellationToken cancellationToken = default)
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

        if (!String.IsNullOrWhiteSpace(filter))
        {
            parameters.Add("filter", filter);
        }

        var list = await _requests.GetRequestAsync<List<Torrent>>($"torrents{parameters.ToQueryString()}", true, cancellationToken);

        return list;
    }
        
    /// <summary>
    ///     Get user torrents list by page.
    /// </summary>
    /// <param name="page">Pagination system</param>
    /// <param name="limit">Entries returned per page / request (max 100, default: 50)</param>
    /// <param name="filter">"active", list active torrents first</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>List of torrents.</returns>
    public async Task<IList<Torrent>> GetByPageAsync(Int32? page = null,
                                                     Int32? limit = null,
                                                     String? filter = null,
                                                     CancellationToken cancellationToken = default)
    {
        var parameters = new NameValueCollection();

        if (limit > 0)
        {
            parameters.Add("limit", limit.ToString());
        }

        if (page > 0)
        {
            parameters.Add("page", page.ToString());
        }

        if (!String.IsNullOrWhiteSpace(filter))
        {
            parameters.Add("filter", filter);
        }

        var list = await _requests.GetRequestAsync<List<Torrent>>($"torrents{parameters.ToQueryString()}", true, cancellationToken);

        return list;
    }

    /// <summary>
    ///     Get information about the torrent.
    /// </summary>
    /// <param name="id">The ID of the torrent</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Info about the torrent.</returns>
    public async Task<Torrent> GetInfoAsync(String id, CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<Torrent>($"torrents/info/{id}", true, cancellationToken);
    }

    /// <summary>
    ///     Get the files available on Real-Debrid for the given torrent.
    /// </summary>
    /// <param name="id">The ID of the torrent</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>List of files available.</returns>
    public async Task<AvailableFiles> GetAvailableFiles(String id, CancellationToken cancellationToken = default)
    {
        var result = await _requests.GetRequestAsync($"torrents/instantAvailability/{id}", true, cancellationToken);

        if (result == null)
        {
            return [];
        }

        try
        {
            return JsonConvert.DeserializeObject<AvailableFiles>(result) ?? throw new JsonSerializationException();
        }
        catch (JsonSerializationException)
        {
            try
            {
                var result2 = JsonConvert.DeserializeObject<AvailableFiles2>(result) ?? throw new JsonSerializationException();

                return result2.ToDictionary(r => r.Key,
                                            r => new AvailableFiles2
                                            {
                                                {
                                                    "rd", r.Value
                                                }
                                            });
            }
            catch (JsonSerializationException)
            {
                try
                {
                    var result2 = JsonConvert.DeserializeObject<List<String>>(result) ?? throw new JsonSerializationException();

                    return result2.ToDictionary(r => r,
                                                r => new AvailableFiles2
                                                {
                                                    {
                                                        "rd", [
                                                        new()
                                                        {
                                                            [r] = new()
                                                            {
                                                                Filename = r
                                                            }
                                                        }
                                                        ]
                                                    }
                                                });
                }
                catch (JsonSerializationException ex)
                {
                    throw new JsonSerializationException($"Unable to deserialize Real Debrid API response to {typeof(AvailableFiles).Name}. Response was: {result}", ex);   
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to deserialize Real Debrid API response to {typeof(AvailableFiles).Name}. Response was: {result}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to deserialize Real Debrid API response to {typeof(AvailableFiles).Name}. Response was: {result}", ex);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to deserialize Real Debrid API response to {typeof(AvailableFiles).Name}. Response was: {result}", ex);
        }
    }

    /// <summary>
    ///     Get currently active torrents number and the current maximum limit.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Torrent limits and active count.</returns>
    public async Task<TorrentActiveCount> GetActiveCountAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<TorrentActiveCount>("torrents/activeCount", true, cancellationToken);
    }

    /// <summary>
    ///     Get available hosts to upload the torrent to.
    /// </summary>
    /// <returns>List of available hosters.</returns>
    public async Task<IList<TorrentHost>> GetAvailableHostsAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<List<TorrentHost>>("torrents/availableHosts", true, cancellationToken);
    }

    /// <summary>
    ///     Add a torrent file to add to the torrent client.
    /// </summary>
    /// <param name="file">The byte array of the file.</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Info about the added torrent.</returns>
    public async Task<TorrentAddResult> AddFileAsync(Byte[] file, CancellationToken cancellationToken = default)
    {
        return await _requests.PutRequestAsync<TorrentAddResult>("torrents/addTorrent", file, true, cancellationToken);
    }

    /// <summary>
    ///     Add a magnet link to add to the torrent client.
    /// </summary>
    /// <param name="magnet">Magnet link</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Info about the added torrent.</returns>
    public async Task<TorrentAddResult> AddMagnetAsync(String magnet, CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<String, String?>("magnet", magnet)
        };

        return await _requests.PostRequestAsync<TorrentAddResult>("torrents/addMagnet", data, true, cancellationToken);
    }

    /// <summary>
    ///     Select files of a torrent to start the torrent.
    /// </summary>
    /// <param name="id">The ID of the torrent</param>
    /// <param name="fileIds">Selected files IDs or "all"</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    public async Task SelectFilesAsync(String id, String[] fileIds, CancellationToken cancellationToken = default)
    {
        var files = String.Join(",", fileIds);

        var data = new[]
        {
            new KeyValuePair<String, String?>("files", files)
        };

        await _requests.PostRequestAsync($"torrents/selectFiles/{id}", data, true, cancellationToken);
    }

    /// <summary>
    ///     Delete a torrent from torrents list.
    /// </summary>
    /// <param name="id">The ID of the torrent</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    public async Task DeleteAsync(String id, CancellationToken cancellationToken = default)
    {
        await _requests.DeleteRequestAsync($"torrents/delete/{id}", true, cancellationToken);
    }
}