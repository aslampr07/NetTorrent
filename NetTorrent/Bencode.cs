using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Diagnostics;

namespace NetTorrent
{
    /// <summary>
    /// allows the parsing of torrent file and contain properties for the torrent
    /// </summary>
    public class TorrentData
    {

        public List<string> Announces { get; set; }
        /// <summary>
        /// Create an object with all metadata as the properties
        /// </summary>
        /// <param name="torrentFile">Torrent file as the storagefile object</param>
        public TorrentData(StorageFile torrentFile)
        {
            parseBencode(torrentFile);
        }

        private async void parseBencode(StorageFile torrentFile)
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

            //the byte array is converted to a string for parsing purposes
            string torrentString = Encoding.UTF8.GetString(torrentByte);


            int listIndex = torrentString.IndexOf("announce-list");

            //When there is no announce-list, the announce is checked
            if(listIndex == -1)
            {
                int index = torrentString.IndexOf("announce");
                int announceLength = 0;
                for (int i = index+8; char.IsNumber(torrentString[i]); i++)
                {
                    announceLength = (int)((announceLength * 10) + char.GetNumericValue(torrentString[i]));
                    index++;
                }
                //announceLength is length of the announce URL
                string announceURL = torrentString.Substring(index + 9, announceLength);
                Announces.Add(announceURL);
            }


        }
    }
}
