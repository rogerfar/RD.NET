using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class StreamingTest
    {
        [Fact]
        public async Task Transcode()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Streaming.GetTranscodeAsync("XFQVPHIWPXRZS");

            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async Task MediaInfo()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Streaming.GetMediaInfoAsync("XFQVPHIWPXRZS");

            Assert.NotNull(result.Filename);
        }
    }
}