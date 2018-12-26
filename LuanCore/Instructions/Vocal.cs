using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    public class Vocal:Instruction
    {
        public Vocal(List<Stmt> block, 
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
