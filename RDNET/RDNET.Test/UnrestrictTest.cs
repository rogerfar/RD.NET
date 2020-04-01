using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class UnrestrictTest
    {
        [Fact]
        public async Task UnrestrictCheck()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.UnrestrictCheckAsync("https://www.4shared.com/video/ieSh94n_/Test.html");

            Assert.Matches("test.mp3", result.Filename);
        }

        [Fact]
        public async Task UnrestrictLink()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.UnrestrictLinkAsync("https://www.4shared.com/video/ieSh94n_/Test.html");

            Assert.Matches("test.mp3", result.Filename);
        }
        
        [Fact]
        public async Task UnrestrictFolder()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.UnrestrictFolderAsync("https://www.4shared.com/video/ieSh94n_/Test.html");

            Assert.Equal(1, result.Count);
        }
    }
}
