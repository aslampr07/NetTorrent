using System.Diagnostics;
using Windows.Storage;
using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;

namespace NetTorrent
{
    public class Torrent
    {
        /// <summary>
        /// Adding new torrent
        /// </summary>
        /// <param name="torrentfile">The torrent file as a StorageFile objecct</param>
        public Torrent(StorageFile torrentFile)
        {
            parseBencode(torrentFile);
        }
        private async void parseBencode(StorageFile torrentFile)
        {
            string bencode = await FileIO.ReadTextAsync(torrentFile);
            metainfo metaInfo = new metainfo(bencode);
        }
    }
}
