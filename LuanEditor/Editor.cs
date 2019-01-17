using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LuanCore;

namespace LuanEditor
{
    internal sealed class Editor
    {
        /// <summary>
        /// 建立一个新工程
        /// </summary>
        /// <param name="path">要建立工程的目录</param>
        /// <param name="projName">工程名称</param>
        public void NewProject(string path, string projName)
        {
            string dirpath = string.Format("{0}\\{1}", path, projName);
            Editor.projectFolder = dirpath;
            Editor.projectName = projName;
            // 建立根目录
            if (!Directory.Exists(dirpath))
            {
                DirectoryInfo dir = new DirectoryInfo(dirpath);
                dir.Create();
            }
            if (!Directory.Exists(dirpath+@"\Script"))
            {
                DirectoryInfo dir = new DirectoryInfo(dirpath + @"\Script");
                dir.Create();
            }
            //创建存放脚本的文件夹 记录后续场景的顺序
            File.Create(dirpath + @"\Script\index").Close();
            //建立资源子文件夹
            string sourcepath = string.Format("{0}\\{1}", dirpath, "Source");
            DirectoryInfo sourcedir = new DirectoryInfo(sourcepath);
            sourcedir.Create();
            string[] subsource = { "background", "bgm", "bgs", "vocal", "stand", "asset" };
            foreach (string str in subsource)
            {
                string subsourcepath = string.Format("{0}\\{1}", sourcepath, str);
                DirectoryInfo subsourcedir = new DirectoryInfo(subsourcepath);
                subsourcedir.Create();
            }
            //复制Platform运行时依赖
            LuanUtils.IOUtils.CopyDir(AppDomain.CurrentDomain.BaseDirectory+@"DLLS\", dirpath);
        }
        /// <summary>
        /// 目前工程的根目录
        /// </summary>
        public static string projectFolder { get; set; }

        /// <summary>
        /// 获取或设置工程的名字
        /// </summary>
        public static string projectName { get; set; }
        #region 类自身相关
        /// <summary>
        /// 工厂方法：获得类的唯一实例
        /// </summary>
        /// <returns>Editor实例</returns>
        public static Editor GetInstance()
        {
            return Editor.synObject;
        }

        /// <summary>
        /// 私有的构造器
        /// </summary>
        private Editor()
        {

        }

        /// <summary>
        /// 唯一实例
        /// </summary>
        private static readonly Editor synObject = new Editor();
        #endregion
    }
}
