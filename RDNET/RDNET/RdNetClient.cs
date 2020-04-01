using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RDNET.Models;

namespace RDNET
{
    public class RdNetClient
    {
        private const String BaseUrl = "https://api.real-debrid.com/rest/1.0/";

        private readonly String _appId;
        private readonly String _appSecret;

        private readonly HttpClient _authClient;
        private readonly HttpClient _client;

        private String _deviceCode;
        private String _clientId;
        private String _clientSecret;
        private String _accessToken;
        private String _refreshToken;

        public RdNetClient(String appId = null, String appSecret = null, String deviceCode = null, String clientId = null, String clientSecret = null, String accessToken = null, String refreshToken = null)
        {
            _appId = appId;
            _appSecret = appSecret;
            _deviceCode = deviceCode;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _accessToken = accessToken;
            _refreshToken = refreshToken;

            _client = new HttpClient();

            _authClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.real-debrid.com/oauth/v2/")
            };
        }

        /// <summary>
        /// Authenticate the app with new credentials.
        /// </summary>
        /// <returns>An object with info how to have the user authenticate.</returns>
        public async Task<AuthenticationDevice> DeviceAuthenticate()
        {
            var request = await _authClient.GetStringAsync($"device/code?client_id={_appId}&new_credentials=yes");

            var result = JsonConvert.DeserializeObject<AuthenticationDevice>(request);

            _deviceCode = result.DeviceCode;

            return result;
        }

        /// <summary>
        /// Check if the user has entered their activation code
        /// </summary>
        /// <returns>True if the user has activated your app, false if they haven't yet.</returns>
        public async Task<AuthenticationVerify> DeviceVerify(String deviceCode = null)
        {
            if (String.IsNullOrWhiteSpace(deviceCode))
            {
                deviceCode = _deviceCode;
            }

            var request = await _authClient.GetAsync($"device/credentials?client_id={_appId}&code={deviceCode}");

            if (!request.IsSuccessStatusCode)
            {
                return null;
            }

            var text = await request.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AuthenticationVerify>(text);

            _clientId = result.ClientId;
            _clientSecret = result.ClientSecret;

            return result;
        }

        /// <summary>
        /// Get the tokens with the usercode
        /// </summary>
        /// <returns></returns>
        public async Task<AuthenticationToken> Token(String clientId = null, String clientSecret = null, String deviceCode = null)
        {
            if (String.IsNullOrWhiteSpace(clientId))
            {
                clientId = _clientId;
            }
            if (String.IsNullOrWhiteSpace(clientSecret))
            {
                clientSecret = _clientSecret;
            }
            if (String.IsNullOrWhiteSpace(deviceCode))
            {
                deviceCode = _deviceCode;
            }

            var data = new[]
            {
                new KeyValuePair<String, String>("client_id", clientId),
                new KeyValuePair<String, String>("client_secret", clientSecret),
                new KeyValuePair<String, String>("code", deviceCode),
                new KeyValuePair<String, String>("grant_type", "http://oauth.net/grant_type/device/1.0")
            };

            var content = new FormUrlEncodedContent(data);

            var response = await _authClient.PostAsync("token", content);

            var text = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(text);
            }

            var result = JsonConvert.DeserializeObject<AuthenticationToken>(text);

            _accessToken = result.AccessToken;
            _refreshToken = result.RefreshToken;

            return result;
        }

        /// <summary>
        /// Refresh the access token based on the refresh token
        /// </summary>
        /// <returns></returns>
        public async Task<AuthenticationToken> Refresh()
        {
            var data = new[]
            {
                new KeyValuePair<String, String>("client_id", _clientId),
                new KeyValuePair<String, String>("client_secret", _clientSecret),
                new KeyValuePair<String, String>("code", _refreshToken),
                new KeyValuePair<String, String>("grant_type", "http://oauth.net/grant_type/device/1.0")
            };

            var content = new FormUrlEncodedContent(data);

            var response = await _authClient.PostAsync("token", content);

            var text = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(text);
            }

            var result = JsonConvert.DeserializeObject<AuthenticationToken>(text);

            _accessToken = result.AccessToken;
            _refreshToken = result.RefreshToken;

            return result;
        }

        /// <summary>
        /// Get server time, raw data returned. This request is not requiring authentication.
        /// </summary>
        /// <returns>DateTime with the current server time in UTC.</returns>
        public async Task<DateTime> TimeAsync()
        {
            var result = await _client.GetStringAsync($"{BaseUrl}time");

            return DateTime.ParseExact(result, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get server time in ISO, raw data returned. This request is not requiring authentication.
        /// </summary>
        /// <returns>DateTimeOffset with the current server time with offset.</returns>
        public async Task<DateTimeOffset> TimeIsoAsync()
        {
            var result = await _client.GetStringAsync($"{BaseUrl}time/iso");

            return DateTimeOffset.ParseExact(result, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns some informations on the current user.
        /// </summary>
        /// <returns>UserResponse</returns>
        public async Task<User> UserAsync()
        {
            return await Get<User>("user");
        }

        /// <summary>
        /// Get traffic details on each hoster used during a defined period
        /// </summary>
        /// <returns></returns>
        public async Task<Traffic> TrafficDetailsAsync(DateTime? start, DateTime? end)
        {
            var parameters = "";
            if (start.HasValue)
            {
                parameters += $"&start={start.Value:yyyy-MM-dd}";
            }
            if (end.HasValue)
            {
                parameters += $"&end={end.Value:yyyy-MM-dd}";
            }

            return await Get<Traffic>($"traffic?{parameters}");
        }

        /// <summary>
        /// Get traffic informations for limited hosters (limits, current usage, extra packages).
        /// </summary>
        /// <returns></returns>
        public async Task<Traffic> TrafficAsync()
        {
            return await Get<Traffic>("traffic");
        }

        /// <summary>
        /// Check if a file is downloadable on the concerned hoster. This request is not requiring authentication.
        /// </summary>
        /// <param name="link">The link to the hoster file</param>
        /// <param name="password">Optional password</param>
        /// <returns></returns>
        public async Task<UnrestrictCheck> UnrestrictCheckAsync(String link, String password = null)
        {
            var data = new[]
            {
                new KeyValuePair<String, String>("link", link),
                new KeyValuePair<String, String>("password", password)
            };

            return await Post<UnrestrictCheck>("unrestrict/check", data);
        }

        /// <summary>
        /// Unrestrict a hoster link and get a new unrestricted link
        /// </summary>
        /// <param name="link">The link to the hoster file</param>
        /// <param name="password">Optional password</param>
        /// <param name="remote">True to use remote data</param>
        /// <returns></returns>
        public async Task<UnrestrictLink> UnrestrictLinkAsync(String link, String password = null, Boolean remote = false)
        {
            var data = new[]
            {
                new KeyValuePair<String, String>("link", link),
                new KeyValuePair<String, String>("password", password),
                new KeyValuePair<String, String>("remote", remote ? "1" : "0"),
            };

            return await Post<UnrestrictLink>("unrestrict/link", data);
        }

        /// <summary>
        /// Unrestrict a hoster folder link and get individual links, returns an empty array if no links found.
        /// </summary>
        /// <param name="link">The link to the host folder.</param>
        /// <returns></returns>
        public async Task<IList<String>> UnrestrictFolderAsync(String link)
        {
            var data = new[]
            {
                new KeyValuePair<String, String>("link", link),
            };

            return await Post<IList<String>>("unrestrict/folder", data);
        }

        /// <summary>
        /// Get transcoding links for given file, {id} from /downloads or /unrestrict/link
        /// </summary>
        /// <param name="id">ID from /downloads or /unrestrict/link</param>
        /// <returns></returns>
        public async Task<StreamingTranscode> StreamingTranscodeAsync(String id)
        {
            return await Get<StreamingTranscode>($"streaming/transcode/{id}");
        }

        /// <summary>
        /// Get detailled media informations for given file.
        /// </summary>
        /// <param name="id">ID from /downloads or /unrestrict/link</param>
        /// <returns></returns>
        public async Task<StreamingMediaInfo> StreamingMediaInfoAsync(String id)
        {
            return await Get<StreamingMediaInfo>($"streaming/mediaInfos/{id}");
        }
        
        /// <summary>
        /// Get list of downloads of the user.
        /// </summary>
        /// <param name="offset">Starting offset</param>
        /// <param name="limit">Entries returned per page / request (must be within 0 and 100, default: 50)</param>
        /// <returns></returns>
        public async Task<IList<Download>> DownloadAsync(Int32? offset = null, Int32? limit = 50)
        {
            var parameters = "";
            if (offset > 0)
            {
                parameters += $"&offset={offset}";
            }
            if (limit > 0)
            {
                parameters += $"&limit={limit}";
            }

            var result = await Get<IList<Download>>($"downloads?{parameters}");

            return result;
        }

        /// <summary>
        /// Delete a link from downloads list.
        /// </summary>
        /// <param name="id">The ID of the file in the download folder</param>
        /// <returns></returns>
        public async Task DownloadDelete(String id)
        {
            await Delete($"downloads/delete/{id}");
        }

        /// <summary>
        /// Get supported hosts.
        /// </summary>
        /// <returns>A list of all supported hosts.</returns>
        public async Task<IList<Host>> HostsAsync()
        {
            return await Get<IList<Host>>("hosts");
        }

        /// <summary>
        /// Get status of supported hosters or not and their status on competitors.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Host>> HostsStatusAsync()
        {
            return await Get<IList<Host>>("hosts/status");
        }

        /// <summary>
        /// Get all supported links Regex, useful to find supported links inside a document.
        /// Does not require authentication.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<String>> HostsRegexAsync()
        {
            return await Get<IList<String>>("hosts/regex");
        }

        /// <summary>
        /// Get all hoster domains supported on the service.
        /// Does not require authentication.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<String>> HostsDomainsAsync()
        {
            return await Get<IList<String>>("hosts/domains");
        }

        /// <summary>
        /// Get current user settings with possible values to update.
        /// </summary>
        /// <returns></returns>
        public async Task<Settings> SettingsAsync()
        {
            return await Get<Settings>("settings");
        }

        /// <summary>
        /// Update a user setting
        /// </summary>
        /// <param name="settingName">"download_port", "locale", "streaming_language_preference", "streaming_quality", "mobile_streaming_quality", "streaming_cast_audio_preference"</param>
        /// <param name="settingValue">Possible values are available in /settings</param>
        public async Task SettingsUpdateAsync(String settingName, String settingValue)
        {
            var data = new[]
            {
                new KeyValuePair<String, String>("setting_name", settingName),
                new KeyValuePair<String, String>("setting_value", settingValue)
            };

            await Post("settings/update", data);
        }

        /// <summary>
        /// Convert fidelity points.
        /// </summary>
        public async Task SettingsConvertPointsAsync()
        {
            var data = new KeyValuePair<String, String>[0];

            await Post("settings/convertPoints", data);
        }

        /// <summary>
        /// Send the verification email to change the password.
        /// </summary>
        /// <returns></returns>
        public async Task SettingsChangePasswordAsync()
        {
            var data = new KeyValuePair<String, String>[0];

            await Post("settings/changePassword", data);
        }

        /// <summary>
        /// Get user torrents list.
        /// </summary>
        /// <param name="offset">Starting offset</param>
        /// <param name="limit">Entries returned per page / request (must be within 0 and 100, default: 50)</param>
        /// <param name="filter">"active", list active torrents first</param>
        /// <returns></returns>
        public async Task<IList<Torrent>> TorrentsAsync(Int32? offset = null, Int32? limit = 50, String filter = null)
        {
            var parameters = "";
            if (offset > 0)
            {
                parameters += $"&offset={offset}";
            }
            if (limit > 0)
            {
                parameters += $"&limit={limit}";
            }
            if (!String.IsNullOrWhiteSpace(filter))
            {
                parameters += $"&filter={filter}";
            }

            var list = await Get<IList<Torrent>>($"torrents?{parameters}");

            return list;
        }

        /// <summary>
        /// Get all informations on the asked torrent
        /// </summary>
        /// <param name="id">The ID of the torrent</param>
        /// <returns></returns>
        public async Task<Torrent> TorrentInfoAsync(String id)
        {
            return await Get<Torrent>($"torrents/info/{id}");
        }

        /// <summary>
        /// Get currently active torrents number and the current maximum limit
        /// </summary>
        /// <returns></returns>
        public async Task<TorrentActiveCount> TorrentActiveCountAsync()
        {
            return await Get<TorrentActiveCount>("torrents/activeCount");
        }

        /// <summary>
        /// Get available hosts to upload the torrent to.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TorrentHost>> TorrentAvailableHostsAsync()
        {
            return await Get<IList<TorrentHost>>("torrents/availableHosts");
        }

        /// <summary>
        /// Add a torrent file to download.
        /// </summary>
        /// <param name="file">The byte array of the file.</param>
        /// <returns></returns>
        public async Task<TorrentAddResult> TorrentAddFile(Byte[] file)
        {
            return await Put<TorrentAddResult>("torrents/addTorrent", file);
        }

        /// <summary>
        /// Add a magnet link to download.
        /// </summary>
        /// <param name="magnet">Magnet link</param>
        /// <returns></returns>
        public async Task<TorrentAddResult> TorrentAddMagnet(String magnet)
        {
            var data = new[]
            {
                new KeyValuePair<String, String>("magnet", magnet)
            };

            return await Post<TorrentAddResult>("torrents/addMagnet", data);
        }

        /// <summary>
        /// Select files of a torrent to start it
        /// </summary>
        /// <param name="id">The ID of the torrent</param>
        /// <param name="fileIds">Selected files IDs or "all"</param>
        /// <returns></returns>
        public async Task TorrentSelectFiles(String id, params String[] fileIds)
        {
            var files = String.Join(",", fileIds);

            var data = new[]
            {
                new KeyValuePair<String, String>("files", files)
            };

            await Post($"torrents/selectFiles/{id}", data);
        }

        /// <summary>
        /// Delete a torrent from torrents list
        /// </summary>
        /// <param name="id">The ID of the torrent</param>
        /// <returns></returns>
        public async Task TorrentDelete(String id)
        {
            await Delete($"torrents/delete/{id}");
        }

        private async Task<T> Get<T>(String url, Boolean repeatRequest = false)
        {
            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _client.GetAsync($"{BaseUrl}{url}");

            var text = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized && repeatRequest)
            {
                throw new Exception(text);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Refresh();

                return await Get<T>(url, true);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(text);
            }

            return JsonConvert.DeserializeObject<T>(text);
        }

        private async Task Post(String url, KeyValuePair<String, String>[] data, Boolean repeatRequest = false)
        {
            var content = new FormUrlEncodedContent(data);

            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _client.PostAsync($"{BaseUrl}{url}", content);

            var text = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized && repeatRequest)
            {
                throw new Exception(text);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Refresh();

                await Post(url, data, true);

                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(text);
            }
        }

        private async Task<T> Post<T>(String url, KeyValuePair<String, String>[] data, Boolean repeatRequest = false)
        {
            var content = new FormUrlEncodedContent(data);

            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _client.PostAsync($"{BaseUrl}{url}", content);

            var text = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized && repeatRequest)
            {
                throw new Exception(text);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Refresh();

                return await Post<T>(url, data, true);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(text);
            }

            return JsonConvert.DeserializeObject<T>(text);
        }

        private async Task<T> Put<T>(String url, Byte[] file, Boolean repeatRequest = false)
        {
            var streamContent = new ByteArrayContent(file);

            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _client.PutAsync($"{BaseUrl}{url}", streamContent);

            var text = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized && repeatRequest)
            {
                throw new Exception(text);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Refresh();

                return await Put<T>(url, file, true);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(text);
            }

            return JsonConvert.DeserializeObject<T>(text);
        }

        private async Task Delete(String url, Boolean repeatRequest = false)
        {
            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _client.DeleteAsync($"{BaseUrl}{url}");

            var text = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized && repeatRequest)
            {
                throw new Exception(text);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Refresh();

                await Delete(url, true);

                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(text);
            }
        }
    }
}