using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test;

public class UnrestrictTest
{
    [Fact]
    public async Task UnrestrictCheck()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        var result = await client.Unrestrict.CheckAsync("https://www.4shared.com/mp3/BilLPtwmea/file_example_MP3_5MG.html");

        Assert.Matches("file example MP3 5MG.mp3", result.Filename);
    }

    [Fact]
    public async Task UnrestrictLink()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        var result = await client.Unrestrict.LinkAsync("https://www.4shared.com/mp3/BilLPtwmea/file_example_MP3_5MG.html");

        Assert.Matches("file example MP3 5MG.mp3", result.Filename);
    }
        
    [Fact]
    public async Task UnrestrictFolder()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        var result = await client.Unrestrict.FolderAsync("https://www.4shared.com/mp3/BilLPtwmea/file_example_MP3_5MG.html");

        Assert.Single(result);
    }
        
    [Fact]
    public async Task UnrestrictContainerFile()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        const String fileContentsString = "AH59qERkQIdwmyv0eLHxSMvqHsGkcE4lEQ32LbOz1XS9cKz1Z35wMy/NUclUmhmqm/oIPgENcE2zwP3+Yg5RyuXoOMr+LXVDaq2xk2bjwrk5SXsxGQDAVT8vXmvL8TR/nxO+LsWwTlgwg/fJv4wgZFonrQbDo0/TuqOh60V0yTIonPKPgypCUspxzwqwg/S7rPnXKDU0CY4LDo4XD5DVewmoJtCIfYntIYNVS+iHQ9O0Cgd+bA+N160I4WF48hkvVzc9+zU2+vtXkM0GgW5OJx9tFlHp/58h9FOLjJCTDdq0ZlqnXI9KOXIBGYzq10O/aqubzscrQuzAtqog2H38bDHFO80DcbR4Y0kfubAi2fImGJeULa/8ml6CqCuktM4o3Hu1VLuqyMz+JLLUo6qGiXXTSQ2yf/OvYniFzHGUHKI1DeUU4RTBEr4XPjYloUUuR4vn6PdR48d9hmVjGsn27o2hHihgMTASPUqI1l1TlEokk7wIsDPb2kvahAs+zgRiT1W7ojA9rxNQxPgD+z88X8QzkBZu/FG7YaY7PagRZhWdZkCCNKZyZUhgc8l24NDI3ykfDhftINEo+n2C44vkQ+ZAJfy+wNi1VL1hy9qiNIPw9BxGTLqaF9j8oC9Fg72iIIR7ElebUUxGwwV4AQrc6ACborozTAeuOIAZ+DZP0oB/oCSQifbZ2Vr2fnwg5daqfXWwPBDF/MLZubjdqZYlIIucg2X8L2S9iXVzbbjWdLh1CxJDWOJRTu8xQXqb0+JWBVlI8X7A4fHgD/f8AWCllvokkL293/IlZSEtpUzLRF4=WUZWdlluNDA2ZzZ5N0RGNXJDRWlTdWJXcE90R3UwKzdwWFdBejlReDRKS1g5OGRYcUJyMFFYTlVOc3dzek9aMw==";

        var fileContents = Encoding.ASCII.GetBytes(fileContentsString);

        var result = await client.Unrestrict.ContainerFileAsync(fileContents);

        Assert.Single(result);
        Assert.Equal("https://google.com", result[0]);
    }
        
    [Fact]
    public async Task UnrestrictContainerLink()
    {
        var client = new RdNetClient();
        client.UseApiAuthentication(Setup.API_KEY);

        var result = await client.Unrestrict.ContainerLinkAsync("https://filecrypt.co/Container/49D157238E.html");

        Assert.Single(result);
        Assert.Equal("https://google.com", result[0]);
    }
}