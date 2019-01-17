using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LuanCore;
using Sprache;

namespace LuanConvertor
{
    public class SectionLoader
    {

        public static void Load(string filepath, Section section)
        {
            List<Instruction> resVec = new List<Instruction>();
            FileStream fs = new FileStream(
                filepath + "\\" + GlobalConfig.Path_Section + section.Name + '.' + GlobalConfig.Ext_Section,
                FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string inst = sr.ReadLine();

                Instruction parsed = ParserExtensions.Parse(Script.Instruction, inst);
                if (parsed is LuanCore.Instructions.Scene s)
                    section.InitScene(s.Name);
                else
                    section.AddInstruction(parsed);
            }
            sr.Close();
            fs.Close();
        }
    }
}
