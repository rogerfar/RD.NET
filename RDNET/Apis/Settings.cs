namespace RDNET.Apis;

public class SettingsApi
{
    private readonly Requests _requests;

    internal SettingsApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <summary>
    ///     Get current user settings with possible values to update.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>A list of settings.</returns>
    public async Task<Settings> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<Settings>("settings", true, cancellationToken);
    }

    /// <summary>
    ///     Update a user setting
    /// </summary>
    /// <param name="settingName">
    ///     "download_port",
    ///     "locale",
    ///     "streaming_language_preference",
    ///     "streaming_quality",
    ///     "mobile_streaming_quality",
    ///     "streaming_cast_audio_preference"
    /// </param>
    /// <param name="settingValue">Possible values are available in /settings</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task UpdateAsync(String settingName, String settingValue, CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<String, String?>("setting_name", settingName), 
            new KeyValuePair<String, String?>("setting_value", settingValue)
        };

        await _requests.PostRequestAsync("settings/update", data, true, cancellationToken);
    }
}