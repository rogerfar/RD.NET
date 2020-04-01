using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class HostsTest
    {
        [Fact]
        public async Task Hosts()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.HostsAsync();

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task Status()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.HostsStatusAsync();

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task Regex()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.HostsRegexAsync();

            Assert.True(result.Count > 150);
        }
        
        [Fact]
        public async Task Domains()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.HostsDomainsAsync();

            Assert.True(result.Count > 100);
        }
    }
}
