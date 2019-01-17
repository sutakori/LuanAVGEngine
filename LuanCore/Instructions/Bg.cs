using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Bg : Instruction
    {
        public Bg(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }

        public Bg() { }

        public override void Form()
        {
            Filename = ArgsDict["filename"];
            ScaleX = ArgsDict.ContainsKey("scalex") && ArgsDict["scalex"] != String.Empty
                ? Convert.ToDouble(ArgsDict["scalex"]) : 1;
            ScaleY = ArgsDict.ContainsKey("scaley") && ArgsDict["scaley"] != String.Empty
                ? Convert.ToDouble(ArgsDict["scaley"]) : 1;
            Opacity = ArgsDict.ContainsKey("opacity") && ArgsDict["opacity"] != String.Empty
                ? Convert.ToDouble(ArgsDict["opacity"]) : 1;
        }

        public override string ToString()
        {
            return $"@bg " +
                $" filename=\"{Filename}\"" +
                $" scalex=\"{ScaleX}\"" +
                $" scaley=\"{ScaleY}\"" +
                $" opacity=\"{Opacity}\"" +
                " " + GetBlockStr();
        }
        public string Filename { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double Opacity { get; set; }
    }
}
