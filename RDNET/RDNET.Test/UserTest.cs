using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class UserTest
    {
        [Fact]
        public async Task User()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetUserAsync();

            Assert.Matches("@", result.Email);
        }
    }
}
