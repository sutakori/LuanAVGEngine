using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LuanCore;
using LuanCore.Instructions;
using LuanPlatform.Core.VM;
using Sprache;
using LuanConvertor;
using LuanUtils;
//using System.Windows.Forms;
using LuanPlatform.Core.Graphic;
using System.Windows;

namespace LuanPlatform.Core
{
    class ResourceManager
    {
        public Section GetSection(string sectionName)
        {
            if (sectionName == GlobalConfig.Index_End) return null;
            Section section = new Section()
            {
                Id = GetIdByName(sectionName),
                Name = sectionName
            };
            LuanConvertor.SectionLoader.Load(AppDomain.CurrentDomain.BaseDirectory, section);
            //Context.GetInstance().Shift(section, new List<Stmt>());
            return section;
        }

        public Section GetSectionById(int id)
        {
            return GetSection(sectionMap[id]);
        }

        /// <summary>
        /// 获取指定资源的内存流
        /// </summary>
        /// <param name="resType">资源类型</param>
        /// <param name="filename">资源名称</param>
        /// <returns></returns>
        private MemoryStream GetMusicMemoryStream(Type resType, string filename)
        {
            string fileURI="";
            switch (resType)
            {
                case Type _ when resType == typeof(Elem.Bgm):
                    fileURI = GlobalConfig.Path_Bgm + filename;
                    break;
                case Type _ when resType == typeof(Elem.Bgs):
                    fileURI = GlobalConfig.Path_Bgs + filename;
                    break;
                case Type _ when resType == typeof(Elem.Vocal):
                    fileURI = GlobalConfig.Path_Vocal + filename;
                    break;
            }
            if (File.Exists(fileURI))
            {
                var ptr = File.ReadAllBytes(fileURI);
                return new MemoryStream(ptr);
            }
            else
            {
                MessageBox.Show("[错误] 资源文件不存在：" + fileURI);
                return null;
            }
        }

        internal MemoryStream GetBGM(string filename)
        {
            return this.GetMusicMemoryStream(typeof(Elem.Bgm), filename);
        }

        internal MemoryStream GetBGS(string filename)
        {
            return this.GetMusicMemoryStream(typeof(Elem.Bgs), filename);
        }

        internal MemoryStream GetVocal(string filename)
        {
            return this.GetMusicMemoryStream(typeof(Elem.Vocal), filename);
        }

        /// <summary>
        /// 获取图片资源描述
        /// </summary>
        /// <param name="filename">资源名称</param>
        /// <param name="resType">资源类型</param>
        /// <param name="cutRect">切割矩</param>
        /// <returns></returns>
        private Sprite GetGraphicSprite(string filename, Type resType, Int32Rect? cutRect)
        {
            string fileURI = "";
            Sprite sprite = new Sprite();
            switch (resType)
            {
                case Type _ when resType == typeof(Elem.Bg):
                    fileURI = GlobalConfig.Path_Bg + filename;
                    break;
                case Type _ when resType == typeof(Elem.Stand):
                    fileURI = GlobalConfig.Path_Stand + filename;
                    break;
                default:
                    fileURI = GlobalConfig.Path_Asset + filename;
                    break;
            }
            byte[] ob = ResourceCachePool.Refer(resType.ToString() + "->" + filename, ResourceCacheType.Eden);
            if (ob == null)
            {
                if (File.Exists(fileURI))
                {
                    ob = File.ReadAllBytes(fileURI);
                    ResourceCachePool.Register(resType.ToString() + "->" + filename, ob, ResourceCacheType.Eden);
                }
                else
                {
                    MessageBox.Show("[错误] 资源文件不存在：" + fileURI);
                    return null;
                }
            }
            MemoryStream ms = new MemoryStream(ob);
            sprite.Init(filename, resType, ms, cutRect);
            return sprite;
        }

        public Sprite GetAsset(string filename)
        {
            return GetGraphicSprite(filename, typeof(Object), FullImageRect);
        }
        public Sprite GetBackground(string filename, Int32Rect cutRect)
        {
            return cutRect.X == -1
                ? this.GetGraphicSprite(filename, typeof(Elem.Bg), null)
                : this.GetGraphicSprite(filename, typeof(Elem.Bg), cutRect);
        }

        public Sprite GetStand(string filename, Int32Rect cutRect)
        {
            return cutRect.X == -1
                ? this.GetGraphicSprite(filename, typeof(Elem.Stand), null)
                : this.GetGraphicSprite(filename, typeof(Elem.Stand), cutRect);
        }

        private int GetIdByName(string name)
        {
            return sectionMap.FirstOrDefault(x => x.Value == name).Key;
        }

        private Dictionary<int, string> sectionMap = new Dictionary<int, string>();
        private void ParseIndex()
        {
            try
            {
                FileStream fs = File.Open(GlobalConfig.Path_Index, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                int cnt = 0;
                while (!sr.EndOfStream)
                {
                    var name = sr.ReadLine();
                    if (name != "")
                    {
                        sectionMap[cnt] = name;
                        cnt++;
                    }
                }
                sectionMap[cnt] = GlobalConfig.Index_End;
            }
            catch
            {
                MessageBox.Show("Read index failed, in:" + GlobalConfig.Path_Index);
            }
        }

        private ResourceManager()
        {
            ParseIndex();
        }
        internal static ResourceManager GetInstance()
        {
            return ResourceManager.instance ?? (ResourceManager.instance = new ResourceManager());
        }
        private static ResourceManager instance = null;

        /// <summary>
        /// 全图切割矩
        /// </summary>
        public static readonly Int32Rect FullImageRect = new Int32Rect(-1, 0, 0, 0);
    }
}
