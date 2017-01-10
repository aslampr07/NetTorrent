using System.Diagnostics;
using Windows.Storage;
using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;

namespace NetTorrent
{
    public class Torrent
    {
        
        /// <summary>
        /// Create a Torrent
        /// </summary>
        /// <param name="torrentFile">The torrent file as Storage</param>
        public async void create(StorageFile torrentFile)
        {
            byte[] torrentByte;
            using (Stream stream = await torrentFile.OpenStreamForReadAsync())
            {
                using (var memorystream = new MemoryStream())
                {
                    stream.CopyTo(memorystream);
                    torrentByte = memorystream.ToArray();
                }
            }

            string torrentString = Encoding.UTF8.GetString(torrentByte);
            Debug.WriteLine(torrentString);
        }
    }
}
