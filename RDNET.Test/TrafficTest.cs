using System;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test;

public class TrafficTest
{
    [Fact]
    public async Task Traffic()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        var result = await client.Traffic.GetAsync();

        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task TrafficDetails()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        var start = DateTime.Now.AddMonths(-2);
        var end = DateTime.Now.AddMonths(-1);

        var result = await client.Traffic.GetDetailsAsync(start, end);

        Assert.NotEmpty(result);
    }
}