using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Scene : Instruction
    {
        public Scene(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Scene() { }

        public override void Form()
        {
            Name = ArgsDict["name"];
        }

        public string Name { get; set; }
    }
}
