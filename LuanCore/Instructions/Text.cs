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
    [Serializable]
    public class Text: Instruction
    {
        public Text(List<Stmt> block, 
            Dictionary<string, string> argsDict, 
            List<Instruction> subinsts)
            :base(block, argsDict, subinsts)
        {
        }
        public Text() { }

        public override void Form()
        {
            Content = ArgsDict["content"];
            Vocal = SubInsts.Count == 0 ? null : SubInsts[0] as Vocal;
        }

        public override string ToString()
        {
            return "@text" +
                $" content=\"{Content}\"" +
                " " + (Vocal == null ? "" : Vocal.ToString()) +
                " " + GetBlockStr();
        }
        public Vocal Vocal { get; set; }
        public string Content { get; set; }
    }
}
