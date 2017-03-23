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
    /// Allows Serializing and Deserializing bencode and objects.
    /// </summary>
    public class Bencode
    {

        int stringPointer = 0;  // Should not have to do this, but i had no other way, I hate global variable
                                //It is cheating to use global variable for recursion :-(
        public dynamic DeserializeBencode(string bencode)
        {
            stringPointer = 0;              //Somebody fix this shit.
            return recursiveDeserialize(bencode);
        }
        private dynamic recursiveDeserialize(string bencode)
        {
            //if the current instance is a list
            if (bencode[stringPointer] == 'l')
            {
                stringPointer++;
                List<object> lt = new List<object>();
                while (bencode[stringPointer] != 'e')
                {
                    lt.Add(recursiveDeserialize(bencode));
                }
                stringPointer++;
                return lt;
            }
            //if the current instance is a dictionary
            if (bencode[stringPointer] == 'd')
            {
                stringPointer++;
                Dictionary<string, object> dy = new Dictionary<string, object>();
                while (bencode[stringPointer] != 'e')
                {
                    string key = (string)recursiveDeserialize(bencode);
                    dy.Add(key, recursiveDeserialize(bencode));
                }
                stringPointer++;
                return dy;
            }
            //if the current instance is a integer
            if (bencode[stringPointer] == 'i')
            {
                stringPointer++;
                long num = 0;
                while (bencode[stringPointer] != 'e')
                {
                    num = (num * 10) + int.Parse(bencode[stringPointer].ToString());
                    stringPointer++;
                }
                stringPointer++;
                return num;
            }
            //If the current instance is a string
            if (char.IsDigit(bencode[stringPointer]))
            {
                int length = 0;
                while (char.IsDigit(bencode[stringPointer]))
                {
                    length = (length * 10) + int.Parse(bencode[stringPointer].ToString());
                    stringPointer++;
                }
                string s = bencode.Substring(stringPointer + 1, length);
                stringPointer = stringPointer + s.Length + 1;
                return s;
            }
            //When there is an error in the bencode it may return null.
            return null;
        }
    }
}
