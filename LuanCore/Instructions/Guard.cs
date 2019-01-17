using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Guard: Instruction
    {
        public Guard(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Guard() { }

        public override void Form()
        {
            Expr = new Expr(ArgsDict["expr"]);
        }

        public override string ToString()
        {
            return $"@guard expr=\"{Expr.Lexeme}\" " + GetBlockStr();
        }
        public Expr Expr { get; set; }
    }

    [Serializable]
    public class Guardend: Instruction
    {
        public Guardend(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Guardend() { }

        public override string ToString()
        {
            return "@guardend " + " " + GetBlockStr();
        }

        public override void Form()
        {
        }
    }
}
