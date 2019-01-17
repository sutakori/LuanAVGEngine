using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore.Instructions
{
    [Serializable]
    public class Stand : Instruction
    {
        public Stand(List<Stmt> block,
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
            : base(block, argsDict, subinsts)
        {
        }
        public Stand() { }

        public override void Form()
        {
            Name = ArgsDict["name"];
            Face = ArgsDict["face"];
            Ext = ArgsDict.ContainsKey("ext") && ArgsDict["ext"] != String.Empty 
                ? ArgsDict["ext"] : "";
            ScaleX = ArgsDict.ContainsKey("scalex") && ArgsDict["scalex"] != String.Empty 
                ? Convert.ToDouble(ArgsDict["scalex"]) : 1;
            ScaleY = ArgsDict.ContainsKey("scalex") && ArgsDict["scaley"] != String.Empty 
                ? Convert.ToDouble(ArgsDict["scaley"]) : 1;
            Pos = ArgsDict.ContainsKey("pos") && ArgsDict["pos"] != String.Empty 
                ? (StandPos)Convert.ToInt32(ArgsDict["pos"]) : StandPos.NPOS;
        }

        public override string ToString()
        {
            return $"@stand" +
                $" name=\"{Name}\"" +
                $" face=\"{Face}\"" +
                " " + (Ext == null ? "" : $"ext=\"{Ext}\"") +
                " " + (Pos == StandPos.NPOS ? "" : $"pos=\"{(int)Pos}\"") +
                $" scalex=\"{ScaleX}\"" +
                $" scaley=\"{ScaleY}\"" +
                " " + GetBlockStr();
        }

        public string Name { get; set; }
        public string Face { get; set; }
        public string Ext { get; set; }
        public StandPos Pos { get; set; }
        public double ScaleX {get;set;}
        public double ScaleY { get; set; }
    }

    public enum StandPos
    {
        NPOS, // 无位置（移出命令不需要指定位置）
        Left,
        MidLeft,
        Mid,
        MidRight,
        Right
    }
}
