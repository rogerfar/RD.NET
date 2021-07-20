# RD.NET

Real-Debrid .NET wrapper library written in C#

Supports all API calls and OAuth2 authentication.

## Usage

Create an instance of `RdNetClient` for each user you want to authenticate. If you need to support multiple users you will need to create a new instance every time you switch users.

```csharp
var client = new RdNetClient();
```

When no parameters are given in the constructor the default app ID is used for open source application.

## Authentication

### Api Token

Call the `UseApiAuthentication` function with the API key for the user:

```csharp
client.UseApiAuthentication("user API key");
```

Each user has its own API key, which can be found here: <https://real-debrid.com/apitoken>.

### OAuth2 for open source applications

[Workflow for opensource apps](https://api.real-debrid.com/#device_auth_no_secret)

This method does not require a client_id or client_secret and can be used in open source applications.

Call the following function to setup OAuth2:

```csharp
client.UseOAuthAuthentication();
```

To authenticate the user call:

```csharp
var result = await client.Authentication.GetDeviceAuthorizeRequestAsync();
```

This will give you a URL and code to have the user verify their device.

You can poll the result by doing:

```csharp
var result = await client.Authentication.VerifyDeviceAuthentication();
```

If the result is `NULL` the user has not authorized the device. When the user has done so a response will be given with the `ClientId` and `ClientSecret`.

These tokens are now used to trade them in for authentication tokens:

```csharp
var result = await client.Authentication.GetOAuthAuthorizationTokensAsync("ClientId", "ClientSecret");
```

The `ClientId`, `ClientSecret`,  `AccessToken` and `RefreshToken` should be safely stored and are needed for future authentication.

To initialize the client again later with the tokens simply pass them to the `UseOAuthAuthentication` method:

```csharp
client.UseOAuthAuthentication("user client_id", "user client_secret", "user access token", "user refresh token");
```

### OAuth2 for closed source applications

[Workflow for opensource apps](https://api.real-debrid.com/#device_auth)

This method is the same as above except instead of passing in the user client_id and user client_secret you pass in your own client_id and client_secret.

### Three legged OAuth2 for websites

[Workflow for websites or client applications](https://api.real-debrid.com/#three_legged)

Start the process by calling the `GetOAuthAuthorizationUrl` method to retrieve a URL to pass to the user:

```csharp
var result = client.Authentication.GetOAuthAuthorizationUrl(new Uri("https://mywebsite"), "34f98j");
```

Navigate the user to the resulting URL, when the user accepts, the user will be redirected to the given reirect URL with 2 query parameters: `code` and `state`.

Use the `state` parameter to verify if the request is legit.

Use the `code` parameter to get the authentication tokens for the user:

```csharp
var result = await client.Authentication.GetOAuthAuthorizationTokensAsync("Your clientId", "Your clientSecret", "Code");
```

The result will give you the `AccessToken` and `RefreshToken`.

To initialize the client with the tokens simply pass them to the `UseOAuthAuthentication` method:

```csharp
client.UseOAuthAuthentication("your client_id", "your client_secret", "user access token", "user refresh token");
```

## Refreshing the access token

When the access token is expired you will retrieve an `AccessTokenExpired` exception. Use the refresh token to renew the access token and store the access token:

```csharp
var newCredentials = await client.Authentication.RefreshTokenAsync();
```

All tokens are cached in the client when refreshing, but it's your responsibility to retry the request with the new access token.

## Unit tests

The unit tests are not designed to be ran all at once, they are used to act as a test client.

Create a file `setup.txt` and put your API token in there.

Some functions will need replacement ID's to work properly.