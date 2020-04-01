using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class DownloadTest
    {
        [Fact]
        public async Task Download()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.DownloadAsync(2, 2);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Delete()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            await client.DownloadDelete("JD762AXQTGYOU");
        }
    }
}
