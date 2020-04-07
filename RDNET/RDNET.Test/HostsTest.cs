using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class HostsTest
    {
        [Fact]
        public async Task Hosts()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetHostsAsync();

            Assert.True(result.Count > 50);
        }

        [Fact]
        public async Task Status()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetHostsStatusAsync();

            Assert.True(result.Count > 50);
        }

        [Fact]
        public async Task Regex()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetHostsRegexAsync();

            Assert.True(result.Count > 150);
        }
        
        [Fact]
        public async Task Domains()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetHostsDomainsAsync();

            Assert.True(result.Count > 100);
        }
    }
}
