using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Shift : Instruction
    {
        public Shift (List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Shift() { }

        public override void Form()
        {
            switch (ArgsDict["type"])
            {
                case "section":
                    Typ = ShiftTyp.section;break;
                case "scene":
                    Typ = ShiftTyp.scene;break;
            }
            Target = ArgsDict["target"];
        }

        public override string ToString()
        {
            return "@shift " +
                $" target=\"{Target}\"" +
                $" type=\"{Typ.ToString()}\" " + GetBlockStr();
        }
        public string Target { get; set; }
        public ShiftTyp Typ { get; set; }
    }

    public enum ShiftTyp
    {
        section,
        scene
    }
}
