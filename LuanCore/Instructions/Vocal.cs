using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Vocal:Instruction
    {
        public Vocal(List<Stmt> block, 
            Dictionary<string, string> argsDict, 
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Vocal() { }

        public override void Form()
        {
            Filename = ArgsDict["filename"];
            Name = ArgsDict["name"];
        }

        public override string ToString()
        {
            return $"@vocal" +
                $" name=\"{Name}\"" +
                $" filename=\"{Filename}\"" +
                " " + GetBlockStr();
        }

        public string Filename { get; set; }
        public string Name { get; set; }
    }
}
