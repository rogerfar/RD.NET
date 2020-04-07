using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class SettingsTest
    {
        [Fact]
        public async Task Settings()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetSettingsAsync();

            Assert.Equal("secured", result.DownloadPort);
        }

        [Fact]
        public async Task Update()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            await client.UpdateSettingsAsync("download_port", "secured");
        }
        
        [Fact]
        public async Task ConvertPoints()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            await client.ConvertPointsAsync();
        }

        [Fact]
        public async Task ChangePassword()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            await client.ChangePasswordAsync();
        }
    }
}
