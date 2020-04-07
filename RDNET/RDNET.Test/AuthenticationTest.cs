using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class AuthenticationTest
    {
        [Fact]
        public async Task Authenticate()
        {
            var client = new RdNetClient(Setup.APP_ID);

            var result = await client.DeviceAuthenticateAsync();

            Assert.Equal(5, result.Interval);
        }

        [Fact]
        public async Task VerifyActivation()
        {
            var client = new RdNetClient(Setup.APP_ID);

            var result = await client.DeviceVerifyAsync(Setup.DEVICE_CODE);

            Assert.Null(result.ClientId);
        }

        [Fact]
        public async Task Token()
        {
            var client = new RdNetClient(Setup.APP_ID);

            var result = await client.GetTokenAsync(Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.DEVICE_CODE);

            Assert.Equal("Bearer", result.TokenType);
        }

        [Fact]
        public async Task Refresh()
        {
            var client = new RdNetClient(Setup.APP_ID);

            var result = await client.RefreshTokenAsync();

            Assert.Equal("Bearer", result.TokenType);
        }
    }
}
