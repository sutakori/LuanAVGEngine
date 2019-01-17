using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Sync : Instruction
    {
        public Sync(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Sync() { }

        public override void Form()
        {
        }

        public override string ToString()
        {
            return "@sync " + GetBlockStr();
        }
    }
   
}
