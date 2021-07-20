using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class HostsTest
    {
        [Fact]
        public async Task Hosts()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Hosts.GetAsync();

            Assert.True(result.Count > 50);
        }

        [Fact]
        public async Task Status()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Hosts.GetStatusAsync();

            Assert.True(result.Count > 50);
        }

        [Fact]
        public async Task Regex()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Hosts.GetRegexAsync();

            Assert.True(result.Count > 150);
        }
        
        [Fact]
        public async Task RegexFolder()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Hosts.GetRegexFolderAsync();

            Assert.True(result.Count > 20);
        }

        [Fact]
        public async Task Domains()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Hosts.GetDomainsAsync();

            Assert.True(result.Count > 100);
        }
    }
}
