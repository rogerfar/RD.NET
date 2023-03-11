using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace RDNET;

internal class Requests
{
    private readonly HttpClient _httpClient;
    private readonly Store _store;

    public Requests(HttpClient httpClient, Store store)
    {
        _httpClient = httpClient;
        _store = store;
    }

    private async Task<(String? Text, String? HeaderValue)> Request(String baseUrl,
                                                                    String url, 
                                                                    String? headerOutput,
                                                                    Boolean requireAuthentication, 
                                                                    RequestType requestType,
                                                                    HttpContent? data,
                                                                    CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Remove("Authorization");

        if (requireAuthentication)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_store.BearerToken}");
        }

        var retryCount = 0;
        while (true)
        {
            try
            {
                var response = requestType switch
                {
                    RequestType.Get => await _httpClient.GetAsync($"{baseUrl}{url}", cancellationToken),
                    RequestType.Post => await _httpClient.PostAsync($"{baseUrl}{url}", data, cancellationToken),
                    RequestType.Put => await _httpClient.PutAsync($"{baseUrl}{url}", data, cancellationToken),
                    RequestType.Delete => await _httpClient.DeleteAsync($"{baseUrl}{url}", cancellationToken),
                    _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
                };

                var buffer = await response.Content.ReadAsByteArrayAsync();
                var text = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                if (response.StatusCode == HttpStatusCode.Unauthorized && requireAuthentication && _store.AuthenticationType == AuthenticationType.OAuth2)
                {
                    var realDebridException = ParseRealDebridException(text);

                    if (realDebridException?.ErrorCode == 8)
                    {
                        throw new AccessTokenExpired();
                    }
                }

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    text = null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    var realDebridException = ParseRealDebridException(text);

                    if (realDebridException != null)
                    {
                        throw realDebridException;
                    }

                    throw new Exception(text);
                }

                if (headerOutput != null)
                {
                    response.Headers.TryGetValues(headerOutput, out var headerValues);

                    var headerValue = headerValues?.FirstOrDefault();

                    return (text, headerValue);
                }

                return (text, null);
            }
            catch (RealDebridException)
            {
                throw;
            }
            catch
            {
                if (retryCount >= _store.RetryCount)
                {
                    throw;
                }

                retryCount++;

                await Task.Delay(1000 * retryCount, cancellationToken);
            }
        }
    }
        
    private async Task<T> Request<T>(String baseUrl,
                                     String url,
                                     Boolean requireAuthentication,
                                     RequestType requestType,
                                     HttpContent? data,
                                     CancellationToken cancellationToken)
        where T : class, new()
    {
        var (result, _) = await Request(baseUrl, url, null, requireAuthentication, requestType, data, cancellationToken);

        if (result == null)
        {
            return new T();
        }

        try
        {
            return JsonConvert.DeserializeObject<T>(result) ?? throw new JsonSerializationException();
        }
        catch (JsonSerializationException ex)
        {
            throw new JsonSerializationException($"Unable to deserialize Real Debrid API response to {typeof(T).Name}. Response was: {result}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to deserialize Real Debrid API response to {typeof(T).Name}. Response was: {result}", ex);
        }
    }

    public async Task<T> GetAuthRequestAsync<T>(String url, CancellationToken cancellationToken)
        where T : class, new()
    {
        return await Request<T>(Store.AuthUrl, url, false, RequestType.Get, null, cancellationToken);
    }

    public async Task<T> PostAuthRequestAsync<T>(String url, IEnumerable<KeyValuePair<String, String?>> data, CancellationToken cancellationToken)
        where T : class, new()
    {
        var content = new FormUrlEncodedContent(data);
        return await Request<T>(Store.AuthUrl, url, false, RequestType.Post, content, cancellationToken);
    }

    public async Task<String?> GetRequestHeaderAsync(String url,
                                                     String header,
                                                     Boolean requireAuthentication,
                                                     CancellationToken cancellationToken)
    {
        var (_, headerValue) = await Request(Store.ApiUrl, url, header, requireAuthentication, RequestType.Get, null, cancellationToken);

        return headerValue;
    }

    public async Task<String?> GetRequestAsync(String url, Boolean requireAuthentication, CancellationToken cancellationToken)
    {
        var (text, _) = await Request(Store.ApiUrl, url, null, requireAuthentication, RequestType.Get, null, cancellationToken);

        return text;
    }

    public async Task<T> GetRequestAsync<T>(String url, Boolean requireAuthentication, CancellationToken cancellationToken)
        where T : class, new()
    {
        return await Request<T>(Store.ApiUrl, url, requireAuthentication, RequestType.Get, null, cancellationToken);
    }

    public async Task PostRequestAsync(String url, IEnumerable<KeyValuePair<String, String?>>? data, Boolean requireAuthentication, CancellationToken cancellationToken)
    {
        var content = data != null ? new FormUrlEncodedContent(data) : null;
        await Request(Store.ApiUrl, url, null, requireAuthentication, RequestType.Post, content, cancellationToken);
    }

    public async Task<T> PostRequestAsync<T>(String url, IEnumerable<KeyValuePair<String, String?>>? data, Boolean requireAuthentication, CancellationToken cancellationToken)
        where T : class, new()
    {
        var content = data != null ? new FormUrlEncodedContent(data) : null;
        return await Request<T>(Store.ApiUrl, url, requireAuthentication, RequestType.Post, content, cancellationToken);
    }

    public async Task PutRequestAsync(String url, Byte[] file, Boolean requireAuthentication, CancellationToken cancellationToken)
    {
        var content = new ByteArrayContent(file);
        await Request(Store.ApiUrl, url, null, requireAuthentication, RequestType.Put, content, cancellationToken);
    }

    public async Task<T> PutRequestAsync<T>(String url, Byte[] file, Boolean requireAuthentication, CancellationToken cancellationToken)
        where T : class, new()
    {
        var content = new ByteArrayContent(file);
        return await Request<T>(Store.ApiUrl, url, requireAuthentication, RequestType.Put, content, cancellationToken);
    }

    public async Task DeleteRequestAsync(String url, Boolean requireAuthentication, CancellationToken cancellationToken)
    {
        await Request(Store.ApiUrl, url, null, requireAuthentication, RequestType.Delete, null, cancellationToken);
    }

    private enum RequestType
    {
        Get,
        Post,
        Put,
        Delete
    }

    private static RealDebridException? ParseRealDebridException(String? text)
    {
        try
        {
            if (text == null)
            {
                return null;
            }

            var requestError = JsonConvert.DeserializeObject<RequestError>(text);

            if (requestError?.Error != null)
            {
                return new RealDebridException(requestError.Error, requestError.ErrorCode);
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}