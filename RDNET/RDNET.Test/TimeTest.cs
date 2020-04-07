using System;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class TimeTest
    {
        [Fact]
        public async Task Time()
        {
            var client = new RdNetClient();

            var result = await client.GetTimeAsync();

            Assert.InRange(result, DateTime.MinValue, DateTime.MaxValue);
        }

        [Fact]
        public async Task TimeIso()
        {
            var client = new RdNetClient();

            var result = await client.GetIsoTimeAsync();

            Assert.InRange(result, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
        }
    }
}
