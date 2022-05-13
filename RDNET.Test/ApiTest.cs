using System;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class ApiTest
    {
        [Fact]
        public async Task DisableToken()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            await client.Api.DisableTokenAsync();
        }

        [Fact]
        public async Task Time()
        {
            var client = new RdNetClient();

            var result = await client.Api.GetTimeAsync();

            Assert.InRange(result, DateTime.MinValue, DateTime.MaxValue);
        }

        [Fact]
        public async Task TimeIso()
        {
            var client = new RdNetClient();

            var result = await client.Api.GetIsoTimeAsync();

            Assert.InRange(result, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
        }
    }
}
