using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Option:Instruction
    {
        public Option(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Option() { }

        public override void Form()
        {
            Label = ArgsDict["label"];
            Button = new Button()
            {
                Label = Label,
                Filename = GlobalConfig.GAME_DEFAULT_BRANCHBUTTON,
                X = GlobalConfig.GAME_WINDOW_WIDTH / 2,
                Y = 0
            };
            Shift = SubInsts.Count == 0 ? null : SubInsts[0] as Shift;
        }

        public override string ToString()
        {
            return $"@option " +
                $" label=\"{Label}\"" +
                " " + (Shift == null ? "" : Shift.ToString()) +
                " " + GetBlockStr();
        }
        public string Label { get; set; }
        public Button Button { get; set; }
        public Shift Shift { get; set; }
    }
}
