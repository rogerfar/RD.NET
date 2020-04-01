using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class StreamingTest
    {
        [Fact]
        public async Task Transcode()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.StreamingTranscodeAsync("YGNKHDGOP4X4C4");

            Assert.NotNull(result.H264WebM.Full);
        }

        [Fact]
        public async Task MediaInfo()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.StreamingMediaInfoAsync("YGNKHDGOP4X4C4");

            Assert.NotNull(result.Filename);
        }
    }
}