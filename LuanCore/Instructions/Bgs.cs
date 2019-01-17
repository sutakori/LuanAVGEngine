using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Bgs : Instruction
    {
        public Bgs(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Bgs() { }

        public override void Form()
        {
            Filename = ArgsDict["filename"];
        }

        public override string ToString()
        {
            return $"@bgs" +
                $" filename=\"{Filename}\"" +
                " " + GetBlockStr();
        }
        public string Filename { get; set; }
    }
}
