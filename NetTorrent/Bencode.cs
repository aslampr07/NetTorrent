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
        /// <summary>
        /// Deserialize the Benocoded dictionary to objects.
        /// </summary>
        /// <typeparam name="T">Object of the class or structure to deserialize to.</typeparam>
        /// <param name="bencodeByte">Byte array of the bencoded.</param>
        public static T DeserializeBencode<T>(byte[] bencodeByte)
        {
            string metaString = Encoding.UTF8.GetString(bencodeByte);
            return DeserializeBencode<T>(metaString);
        }
        /// <summary>
        /// Deserialze the Bencoded dictionary to objects.
        /// </summary>
        /// <typeparam name="T">Object of the class or structure to deserialize to</typeparam>
        /// <param name="bencodeString">String in UTF-8 encoded</param>
        public static T DeserializeBencode<T>(string bencodeString)
        {
            //For creting an instance of an object of type T
            T bencodeObject = (T)Activator.CreateInstance(typeof(T));
            var props = bencodeObject
            return bencodeObject;
        }

    }
}
