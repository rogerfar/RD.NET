# RD.NET

Real-Debrid .NET wrapper library written in C#

Support most API calls and oAuth authentication.

For best results use your own API key found here: <https://real-debrid.com/apitoken>

## Authentication

Create a new RdNetClient and pass it your app ID and app secret, if you have an open source project, use `X245A4XAIBGVM` as the app ID and no secret.

Call `Authenticate` and it will return the URL and code the user will need to validate your app.

Call `VerifyActivation` to check if the user approved your app yet (keep in mind the `interval` parameter).

Call `Token` to get your first `AccessToken`, `AccessSecret` and `RefreshToken`, store this, together with the `ClientId`, `ClientSecret` and `DeviceCode` from the `VerifyActivation` call.

Any subsequent calls pass all the parameters to the constructor:

```c#
var client = new RdNetClient('MyAppID', 'MyAppSecret', 'UserDeviceCode', 'User ClientID', 'User Client Secret', 'UserAccessToken', 'UserRefreshToken');`
```

When the `AccessToken` expires a new one will be requested based on the `RefreshToken`.

Each request will return also the `AccessToken` in case it has renewed.

## Unit tests

The unit tests are not designed to be ran all at one, they are used to act as a test client. Make sure fill in the `Setup.cs` file with the required parameters.

Create a file `setup.txt` and put your API token in there.
