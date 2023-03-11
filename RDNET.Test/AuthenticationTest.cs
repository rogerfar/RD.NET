using System;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test;

public class AuthenticationTest
{
    [Fact]
    public async Task Authenticate()
    {
        var client = new RdNetClient();
        client.UseOAuthAuthentication();

        var result = await client.Authentication.GetDeviceAuthorizeRequestAsync();

        await Task.Delay(5000);

        var result2 = await client.Authentication.VerifyDeviceAuthentication();

        Assert.Equal(5, result.Interval);
        Assert.NotNull(result2.ClientId);
    }
        
    [Fact]
    public async Task OAuthResponse()
    {
        var client = new RdNetClient();
        client.UseOAuthAuthentication();

        var result = await client.Authentication.GetOAuthAuthorizationTokensAsync("", "");

        Assert.Equal("Bearer", result.TokenType);
    }
        
    [Fact]
    public async Task RefreshToken()
    {
        var client = new RdNetClient();
        client.UseOAuthAuthentication("", "", "", "");

        try
        {
            var result = await client.User.GetAsync();
        }
        catch (AccessTokenExpired)
        {
            var newCredentials = await client.Authentication.RefreshTokenAsync();
        }
    }
        
    [Fact]
    public void OAuthRedirectUrl()
    {
        var client = new RdNetClient();
        var result = client.Authentication.GetOAuthAuthorizationUrl(new Uri("http://www.google.com"), "Test");
    }
}