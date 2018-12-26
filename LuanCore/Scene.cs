using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore
{
    public class Scene
    {
        public string Name { get; }

        public List<Instruction> Instructions { get; set; }

        public Scene(string name)
        {
            Name = name;
        }
    }
}
