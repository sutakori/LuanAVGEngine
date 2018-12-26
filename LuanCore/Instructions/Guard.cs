using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    public class Guard: Instruction
    {
        public Guard(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }

        public override void Form()
        {
            Expr = Block;
        }

        public List<Stmt> Expr { get; set; }
    }

    public class Guardend: Instruction
    {
        public Guardend(List<Stmt> block,
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
