using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class ApiTest
    {
        [Fact]
        public async Task Time()
        {
            var client = new RdNetClient();

            var result = await client.TimeAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task TimeIso()
        {
            var client = new RdNetClient();

            var result = await client.TimeIsoAsync();

            Assert.NotNull(result);
        }
    }
}
