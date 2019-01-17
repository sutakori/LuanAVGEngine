using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuanUtils
{
    public class IOUtils
    {
        public static string JoinPath(params string[] uriObj)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < uriObj.Length - 1; i++)
            {
                sb.Append(uriObj[i] + "\\");
            }
            sb.Append(uriObj.Last());
            return sb.ToString();
        }

        public static void CopyDir(string src, string dest)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(@"DLLS");
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (var f in fileinfo)
                {
                    if (f is DirectoryInfo)
                    {
                        if (!Directory.Exists(dest + '\\' + f.Name))
                        {
                            Directory.CreateDirectory(dest + '\\' + f.Name);
                        }
                        CopyDir(f.FullName, dest + '\\' + f.Name);
                    }
                    else
                    {
                        File.Copy(f.FullName, dest + '\\' + f.Name, true);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Move Dir Failed");
            }
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
