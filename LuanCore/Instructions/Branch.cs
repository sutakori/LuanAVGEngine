using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    public class Branch:Instruction
    {
        public Branch(List<Stmt> block, 
            Dictionary<string, string> argsDict, 
            List<Instruction> subinsts)
           : base(block, argsDict, subinsts)
        {
        }

        public override void Form()
        {
        }
    }
}
