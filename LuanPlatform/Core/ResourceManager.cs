using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LuanCore.Concept;

namespace LuanPlatform.Core
{
    class ResourceManager
    {
        public Section GetSection(string sectionName)
        {

        }
        public static ResourceManager GetInstance()
        {
            return ResourceManager.instance ?? (ResourceManager.instance = new ResourceManager());
        }

        private ResourceManager()
        {

        }
        private static ResourceManager instance = null;

        private 
    }
}
