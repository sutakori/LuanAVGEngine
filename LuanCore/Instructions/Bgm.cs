using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Bgm:Instruction
    {
        public Bgm(List<Stmt> block, 
            Dictionary<string, string> argsDict, 
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Bgm() { }

        public override void Form()
        {
            Filename = ArgsDict["filename"];
        }

        public override string ToString()
        {
            return $"@bgm" +
                $" filename=\"{Filename}\"" +
                " " + GetBlockStr();
        }
        public string Filename { get; set; }
    }
}
