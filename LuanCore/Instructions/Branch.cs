using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Branch:Instruction
    {
        public Branch(List<Stmt> block, 
            Dictionary<string, string> argsDict, 
            List<Instruction> subinsts)
           : base(block, argsDict, subinsts)
        {
        }
        public Branch() { }

        public override void Form()
        {
            Options = SubInsts.Select((Instruction inst) =>
            {
                return inst as Option;
            }).ToList();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(20);
            sb.Append("@branch");
            foreach(var option in Options)
            {
                sb.Append(' ');
                sb.Append(option.ToString());
            }
            sb.Append(' ');
            sb.Append(GetBlockStr());
            return sb.ToString();
        }

        public List<Option> Options { get; set; }
    }
}
