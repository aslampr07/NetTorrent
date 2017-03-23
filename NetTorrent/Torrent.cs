using System.Diagnostics;
using Windows.Storage;
using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;

namespace NetTorrent
{
    /// <summary>
    /// Class for creating and downloading files using torrent metadata.
    /// </summary>
    public class Torrent
    {
        //Creating field of a Structure for storing the info dictionary.
        private InfoStruct info;

        //For storing the metadata of the torrent dictionary.
        public List<Uri> Announces { get; set; } = new List<Uri>();
        public string CreatedBy { get; set; }
        public long CreationDate { get; set; }
        public string Comment { get; set; }

        //Creating a property of info dictionary from the field, autoproperty is not working.
        public InfoStruct Info { get { return info; } set { info = value; } }

        /// <summary>
        /// Initialize the torrent and return properties.
        /// </summary>
        /// <param name="meta">The parsed object that contain the torrent meta data</param>
        public Torrent(dynamic meta)
        {
            foreach (KeyValuePair<string, dynamic> item in meta)
            {
                switch (item.Key)
                {
                    case "announce":
                        Announces.Add(new Uri(item.Value.ToString()));
                        break;

                    case "announce-list":
                        foreach (List<object> lev1 in item.Value)
                        {
                            foreach (string lev2 in lev1)
                                Announces.Add(new Uri(lev2));
                        }
                        break;

                    case "created by":
                        CreatedBy = item.Value;
                        break;

                    case "creation date":
                        CreationDate = item.Value;
                        break;

                    case "info":
                        foreach (KeyValuePair<string, dynamic> infoDct in item.Value)
                        {
                            switch (infoDct.Key)
                            {
                                case "pieces":
                                    info.Pieces = infoDct.Value.ToString();
                                    break;

                                case "piece length":
                                    info.PieceLength = infoDct.Value;
                                    break;

                                case "name":
                                    info.Name = infoDct.Value;
                                    break;

                                case "length":
                                    info.Length = infoDct.Value;
                                    break;
                                
                                

                                default: continue;
                            }
                        }
                        break;

                    case "comment":
                        Comment = item.Value.ToString();
                        break;


                    default: continue;
                }
            }
        }
        /// <summary>
        /// For defining the Info dictionary
        /// </summary>
        public struct InfoStruct
        {
            private ItemStruct items;
            private List<ItemStruct> itemList;

            public long PieceLength { get; set; }
            public string Pieces { get; set; }
            public string Name { get; set; }
            public long Length { get; set; }
            public List<ItemStruct> Item { get { return itemList; } set { itemList = value; } }
        }
        public struct ItemStruct
        {
            public long Length { get; set; }
            
        }
    }
}
