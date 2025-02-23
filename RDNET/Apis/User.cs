namespace RDNET;

public interface IUserApi
{
    /// <summary>
    ///     Returns some information on the current logged in user.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>
    ///     The currently logged in user.
    /// </returns>
    Task<User> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Convert fidelity points to premium time.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <exception>If not enough points it will throw an exception with the message "Not enough points".</exception>
    /// <returns></returns>
    Task ConvertPointsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Send a verification email to change the password for the user.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    Task ChangePasswordAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Upload a new avatar file.
    ///     Allowed extensions: .jpg, .gif, .png
    ///     Maximum image size: 1024KB
    ///     Maximum image resolution: 150px x 150px
    /// </summary>
    /// <param name="file">The avatar file.</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    Task UploadAvatar(Byte[] file, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reset user avatar image to default.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns></returns>
    Task DeleteAvatar(CancellationToken cancellationToken = default);
}

public class UserApi : IUserApi
{
    private readonly Requests _requests;

    internal UserApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<User> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<User>("user", true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task ConvertPointsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _requests.PostRequestAsync("settings/convertPoints", null, true, cancellationToken);
        }
        catch (RealDebridException ex)
        {
            if (ex.ErrorCode == 25)
            {
                throw new Exception("Not enough points");
            }
        }
    }

    /// <inheritdoc />
    public async Task ChangePasswordAsync(CancellationToken cancellationToken = default)
    {
        await _requests.PostRequestAsync("settings/changePassword", null, true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UploadAvatar(Byte[] file, CancellationToken cancellationToken = default)
    {
        await _requests.PutRequestAsync("settings/avatarFile", file, true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAvatar(CancellationToken cancellationToken = default)
    {
        await _requests.DeleteRequestAsync("settings/avatarDelete", true, cancellationToken);
    }
}