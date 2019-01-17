using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore
{
    [Serializable]
    public class Scene
    {
        public string Name { get; }

        public List<Instruction> Instructions { get; set; } = new List<Instruction>();

        public Scene(string name)
        {
            Name = name;
        }

        public bool Match(string name)
        {
            return Name == name;
        }
    }
}
