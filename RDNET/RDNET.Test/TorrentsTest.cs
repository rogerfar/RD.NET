using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace RDNET.Test
{
    public class TorrentsTest
    {
        [Fact]
        public async Task Torrents()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetTorrentsAsync();
        }

        [Fact]
        public async Task Info()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetTorrentInfoAsync("6N2QYT5PHW5EY");
        }

        [Fact]
        public async Task ActiveCount()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetTorrentActiveCountAsync();
        }

        [Fact]
        public async Task AvailableHosts()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            var result = await client.GetAvailableTorrentHostsAsync();
        }
        
        [Fact]
        public async Task AddFile()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            const String filePath = @"big-buck-bunny.torrent";

            var file = await File.ReadAllBytesAsync(filePath);

            var result = await client.AddTorrentFileAsync(file);
        }
        
        [Fact]
        public async Task AddMagnet()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            const String magnet = "magnet:?xt=urn:btih:dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c&dn=Big+Buck+Bunny&tr=udp%3A%2F%2Fexplodie.org%3A6969&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.empire-js.us%3A1337&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=wss%3A%2F%2Ftracker.btorrent.xyz&tr=wss%3A%2F%2Ftracker.fastcast.nz&tr=wss%3A%2F%2Ftracker.openwebtorrent.com&ws=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2F&xs=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2Fbig-buck-bunny.torrent";
            
            var result = await client.AddTorrentMagnetAsync(magnet);
        }

        [Fact]
        public async Task SelectFiles()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            await client.SelectTorrentFilesAsync("6N2QYT5PHW5EY", "1", "2");
        }
        
        [Fact]
        public async Task Delete()
        {
            var client = new RdNetClient(Setup.APP_ID, Setup.DEVICE_CODE, Setup.CLIENT_ID, Setup.CLIENT_SECRET, Setup.ACCESS_TOKEN, Setup.REFRESH_TOKEN);

            await client.DeleteTorrentAsync("6N2QYT5PHW5EY");
        }
    }
}
