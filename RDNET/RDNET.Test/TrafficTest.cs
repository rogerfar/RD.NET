using System;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class TrafficTest
    {
        [Fact]
        public async Task Traffic()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.TrafficAsync();
        }

        [Fact]
        public async Task TrafficDetails()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.APP_SECRET, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var start = DateTime.Now.AddDays(-25);
            var end = DateTime.Now.AddDays(-20);

            var result = await client.TrafficDetailsAsync(start, end);

        }
    }
}
