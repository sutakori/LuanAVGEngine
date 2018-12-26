using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    /// <summary>
    /// 文本命令
    /// </summary>
    public class Text: Instruction
    {
        public Text(List<Stmt> block, 
            Dictionary<string, string> argsDict, 
            List<Instruction> subinsts)
            :base(block, argsDict, subinsts)
        {
        }

        public override void Form()
        {
            Content = ArgsDict["content"];
        }

        public string Content { get; set; }
    }
}
