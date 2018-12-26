using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    public class Stand : Instruction
    {
        public Stand(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }

        public override void Form()
        {
            Name = ArgsDict["name"];
            Face = ArgsDict["face"];
            X = ArgsDict["x"];
        }

        public string Name { get; set; }
        public string Face { get; set; }
        public string X { get; set; }
    }
}
