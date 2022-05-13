using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class UserTest
    {
        [Fact]
        public async Task User()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.User.GetAsync();

            Assert.Matches("@", result.Email);
        }

        [Fact]
        public async Task ConvertPoints()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            await client.User.ConvertPointsAsync();
        }

        [Fact]
        public async Task ChangePassword()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            await client.User.ChangePasswordAsync();
        }

        [Fact]
        public async Task UploadAvatar()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            const String filePath = @"avatar.png";

            var file = await File.ReadAllBytesAsync(filePath);

            await client.User.UploadAvatar(file);
        }
        
        [Fact]
        public async Task DeleteAvatar()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            await client.User.DeleteAvatar();
        }
    }
}
