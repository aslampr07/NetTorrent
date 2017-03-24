using System;
using System.Collections.Generic;

namespace NetTorrent
{
    /// <summary>
    /// Class for creating and downloading files using torrent metadata.
    /// </summary>
    public class Torrent
    {
        private List<fileStruct> fileList = new List<fileStruct>();
        //For storing the metadata of the torrent dictionary.
        /// <summary>
        /// Get or set list of Announce URLs.
        /// </summary>
        public List<Uri> Announces { get; set; } = new List<Uri>();
        /// <summary>
        /// The Created By Disctiption.
        /// </summary>
        public string CreatedBy { get; }
        /// <summary>
        /// Time at which the torrent is created. Seconds since 1-Jan-1970 00:00:00 UTC.
        /// </summary>
        public long CreationDate { get; }
        /// <summary>
        /// Free text from the author of the Torrent.
        /// </summary>
        public string Comment { get; }
        //The info dictionary
        /// <summary>
        /// The length of individual pieces in bytes.
        /// </summary>
        public long PieceLength { get; }
        /// <summary>
        /// Currently broken. Return the concantenated SHA1 hash of file pieces.
        /// </summary>
        public string Pieces { get; set; }
        /// <summary>
        /// Name of the file if the torrent is in single file mode. Otherwise it is the directory name that store all files.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Applicable only in single file mode: Size of the entire file in bytes. If the torrent is Multiple file it will return 0.
        /// </summary>
        public long Length { get; set; }
        public List<fileStruct> Files { get { return fileList; } set { fileList = value; } }


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


                    case "comment":
                        Comment = item.Value.ToString();
                        break;

                    case "info":
                        foreach (KeyValuePair<string, dynamic> infoDct in item.Value)
                        {
                            switch (infoDct.Key)
                            {
                                case "pieces":
                                    Pieces = infoDct.Value.ToString();
                                    break;

                                case "piece length":
                                    PieceLength = infoDct.Value;
                                    break;

                                case "name":
                                    Name = infoDct.Value;
                                    break;

                                case "length":
                                    Length = infoDct.Value;
                                    break;

                                case "files":
                                    foreach (Dictionary<string, dynamic> filesList in infoDct.Value)
                                    {
                                        fileStruct fileObj = new fileStruct();
                                        foreach (KeyValuePair<string, dynamic> itemDict in filesList)
                                        {
                                            switch (itemDict.Key)
                                            {
                                                case "length":
                                                    fileObj.Length = itemDict.Value;
                                                    break;
                                                case "path":
                                                    string path = "";
                                                    foreach (string p in itemDict.Value)
                                                    {
                                                        path += "/" + p;
                                                    }
                                                    fileObj.Path = new Uri(path, UriKind.Relative);
                                                    break;
                                                default: continue;
                                            }
                                        }
                                        Files.Add(fileObj);
                                    }

                                    break;

                                default: continue;
                            }
                        }
                        break;

                    default: continue;
                }
            }
        }
        public struct fileStruct
        {
            public long Length { get; set; }
            public Uri Path { get; set; }
        }
    }
}
