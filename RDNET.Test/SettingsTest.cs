using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test;

public class SettingsTest
{
    [Fact]
    public async Task Settings()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        var result = await client.Settings.GetAsync();

        Assert.Equal("secured", result.DownloadPort);
    }

    [Fact]
    public async Task Update()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        await client.Settings.UpdateAsync("download_port", "secured");
    }
}