using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class DownloadTest
    {
        [Fact]
        public async Task DownloadGetTotal()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Downloads.GetTotal();

            Assert.Equal(24, result);
        }

        [Fact]
        public async Task DownloadByPage()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Downloads.GetPageAsync(1, 2);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DownloadByOffset()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Downloads.GetAsync(0, 4);

            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async Task Delete()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            await client.Downloads.DeleteAsync("JD762AXQTGYOU");
        }
    }
}
