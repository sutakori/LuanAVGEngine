using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Button : Instruction
    {
        public Button(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Button() { }

        public override void Form()
        {
            Label = ArgsDict["label"];
            Filename = ArgsDict["filename"];
            Signal = SubInsts.Count == 0 ? null : SubInsts[0] as Signal;

            X = Convert.ToDouble(ArgsDict["x"]);
            Y = Convert.ToDouble(ArgsDict["y"]);
            ScaleX = ArgsDict.ContainsKey("scalex") && ArgsDict["scalex"] != String.Empty
                ? Convert.ToDouble(ArgsDict["scalex"]) : 1;
            ScaleY = ArgsDict.ContainsKey("scaley") && ArgsDict["scaley"] != String.Empty
                ? Convert.ToDouble(ArgsDict["scaley"]) : 1;
            Opacity = ArgsDict.ContainsKey("opacity") && ArgsDict["opacity"] != String.Empty
                ? Convert.ToDouble(ArgsDict["opacity"]) : 1;
        }

        public override string ToString()
        {
            return $"@button" +
                $" label=\"{Label}\"" +
                $" filename=\"{Filename}\"" +
                $" x=\"{X}\"" +
                $" y=\"{Y}\"" +
                $" scalex=\"{ScaleX}\"" +
                $" scaley=\"{ScaleY}\"" +
                $" opacity=\"{Opacity}\"" +
                " " + (Signal == null ? "" : Signal.ToString()) +
                " " + GetBlockStr();
        }
        public string Label { get; set; }
        public string Filename { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double Opacity { get; set; }
        public Signal Signal { get; set; }
    }
}
