using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NetTorrent
{
    class metainfo
    {
        public string Announce { get; set; }
        public List<int> AnnounceList { get; set; }

        /// <summary>
        /// Convert the the bencode to invidual properties
        /// </summary>
        /// <param name="bencode"></param>
        public metainfo(string bencode)
        {
            Debug.WriteLine(bencode);
        }
    }
}
