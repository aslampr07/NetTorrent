using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Diagnostics;
using System.Collections;
using System.Reflection;

namespace NetTorrent
{
    /// <summary>
    /// Allows Serializing and Deserializing bencode and objects
    /// </summary>
    public class Bencode
    {
        public object DeserializeBencode(string bencode)
        {
            if (bencode[0] != '\0')
            {
                if (bencode[0] == 'l')
                {
                    List<object> lt = new List<object>();
                    while (true)
                    {
                        lt.Add(DeserializeBencode(bencode));
                    }
                }
                if(bencode[0] == 'd')
                {
                    Dictionary<string, object> dy = new Dictionary<string, object>();
                    while (true)
                    {

                    }
                }
                if (bencode[0] == 'i')
                {
                    return 0;
                }
                //If the current instance is a string
                if(char.IsDigit(bencode[0]))
                {
                    int i = 0;
                    int length = 0;
                    while (char.IsDigit(bencode[i]))
                    {
                        length = (length * 10) + int.Parse(bencode[i].ToString());
                        i++;
                    }
                    string s = bencode.Substring(i + 1, length);
                    return s;
                }
            }
            return null;
        }

    }
}
