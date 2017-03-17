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
        public List<Uri> Announce { get; set; } = new List<Uri>();
        public string CreatedBy { get; set; }
        public int CreationDate { get; set; }
        public string Comment { get; set; }
        /// <summary>
        /// Initialize the torrent
        /// </summary>
        /// <param name="meta">The parsed object that contain the torrent meta data</param>
        public Torrent(dynamic meta)
        {
            foreach (KeyValuePair<string,dynamic> item in meta)
            {
                switch (item.Key)
                {
                    case "announce":        Announce.Add(new Uri(item.Value.ToString()));
                                            break;

                    case "announce-list":   foreach (List<object> lev1 in item.Value)
                                            {
                                                 foreach (string lev2 in lev1)
                                                 Announce.Add(new Uri(lev2.ToString()));
                                            }
                                            break;

                    case "created by":      CreatedBy = item.Value;
                                            break;

                    case "creation date":   CreationDate = item.Value;
                                            break;

                    case "comment":         Comment = item.Value;
                                            break;

                    default:                continue;
                }
            }
        }
    }
}
