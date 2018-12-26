using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using LuanPlatform.Core;

namespace LuanPlatform.Utils
{
    class IOUtils
    {
        public static string ParseURItoURL(string uri)
        {
            return World.BasePath + uri;
        }

        public static bool Serialize(object instance, string savePath)
        {
            try
            {
                Stream myStream = File.Open(savePath, FileMode.Create);
                var bf = new BinaryFormatter();
                bf.Serialize(myStream, instance);
                myStream.Close();
            }
            catch (Exception ex)
            {
                LogUtils.Log("Serialization failed. " + ex.ToString(), "IOUtils", LogLevel.Error);
                throw;
            }
            return true;
        }

        public static object Deserialize(string loadPath)
        {
            try
            {
                Stream s = File.Open(loadPath, FileMode.Open);
                var bf = new BinaryFormatter();
                var ob = bf.Deserialize(s);
                s.Close();
                return ob;
            }
            catch (Exception ex)
            {
                LogUtils.Log("Unserialization failed. " + ex.ToString(), "IOUtils", LogLevel.Error);
                return null;
            }
        }
    }
}
