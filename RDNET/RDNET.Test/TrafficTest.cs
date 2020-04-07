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
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetTrafficAsync();
        }

        [Fact]
        public async Task TrafficDetails()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var start = DateTime.Now.AddDays(-25);
            var end = DateTime.Now.AddDays(-20);

            var result = await client.GetTrafficDetailsAsync(start, end);

        }
    }
}
