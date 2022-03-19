using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class TorrentsTest
    {
        [Fact]
        public async Task TorrentsCount()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetTotal();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task TorrentsByOffset()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetAsync(0, 5);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task TorrentsByPage()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetByPageAsync(1, 5);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task Info()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetInfoAsync("GGZIOQZAZHDIE");

            Assert.Equal("GGZIOQZAZHDIE", result.Id);
        }

        [Fact]
        public async Task ActiveCount()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetActiveCountAsync();

            Assert.Equal(0, result.Active);
            Assert.Equal(10, result.Limit);
        }

        [Fact]
        public async Task AvailableHosts()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetAvailableHostsAsync();
        }
        
        [Fact]
        public async Task AddFile()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            const String filePath = @"big-buck-bunny.torrent";

            var file = await File.ReadAllBytesAsync(filePath);

            var result = await client.Torrents.AddFileAsync(file);

            Assert.Equal("7YYM4GTXFZXZC", result.Id);
        }
        
        [Fact]
        public async Task AddMagnet()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            const String magnet = "magnet:?xt=urn:btih:dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c&dn=Big+Buck+Bunny&tr=udp%3A%2F%2Fexplodie.org%3A6969&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.empire-js.us%3A1337&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=wss%3A%2F%2Ftracker.btorrent.xyz&tr=wss%3A%2F%2Ftracker.fastcast.nz&tr=wss%3A%2F%2Ftracker.openwebtorrent.com&ws=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2F&xs=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2Fbig-buck-bunny.torrent";
            
            var result = await client.Torrents.AddMagnetAsync(magnet);

            Assert.Equal("7YYM4GTXFZXZC", result.Id);
        }

        [Fact]
        public async Task GetAvailableFiles()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetAvailableFiles("dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c");
        }

        [Fact]
        public async Task GetAvailableFilesNone()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetAvailableFiles("dd8255ecdc7ca55fb0bbf81323d87062db1f6d1d");
        }

        [Fact]
        public async Task GetAvailableFilesBroken()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            var result = await client.Torrents.GetAvailableFiles("8EB17E22D70287B7013115A191305913CB2B2165");
        }

        [Fact]
        public async Task SelectFiles()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            await client.Torrents.SelectFilesAsync("KRMG55JOY3F7E", new []{ "1", "2" });
        }
        
        [Fact]
        public async Task Delete()
        {
            var client = new RdNetClient();
            client.UseApiAuthentication(Setup.API_KEY);

            await client.Torrents.DeleteAsync("KRMG55JOY3F7E");
        }
    }
}
