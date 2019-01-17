using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Signal : Instruction
    {
        public Signal(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Signal() { }

        public override void Form()
        {
            Name = ArgsDict["name"];
            ArgsDict.Remove("name");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(20);
            sb.Append("@signal");
            sb.Append($" name=\"{Name}\"");
            foreach(var item in ArgsDict)
            {
                sb.Append(' ');
                sb.Append(item.Key);
                sb.Append('=');
                sb.Append(item.Value);
            }
            sb.Append(' ');
            sb.Append(GetBlockStr());
            return sb.ToString();
        }
        public string Name { get; set; }
    }
}
