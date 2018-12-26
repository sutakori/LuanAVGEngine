using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LuanCore.Concept;

namespace LuanPlatform.Core
{
    class World
    {

        private void InitConfig()
        {
            
        }

        private void InitRuntime()
        {
            Section titleSection = this.resMana.GetSection(GlobalConfig.Section_Title);
            World.RunMana.RunSection(titleSection);
        }
        //
        public static World GetInstance()
        {
            return World.instance ?? (World.instance = new World());
        }
        private World()
        {
            this.InitConfig();

            World.RunMana = new RuntimeManager();
            World.Renderer = new Renderer();
            this.resMana = ResourceManager.GetInstance();
            this.InitRuntime();
        }

        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;

        private static RuntimeManager RunMana;

        private static Renderer Renderer;

        private ResourceManager resMana;
        // 唯一实例
        private static World instance = null;
    }
}
