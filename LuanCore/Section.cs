using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore
{
    /// <summary>
    /// 剧本单元，由scene序列组成
    /// </summary>
    public sealed class Section
    {
        public string Name { get; }

        public List<Scene> Scenes { get; set; }

        public Section(string name)
        {
            Name = name;
        }

    }
}
