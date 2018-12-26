using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LuanCore;
using LuanConvertor;
using System.IO;
using Sprache;
namespace LuanEditor
{
    class Scriptor
    {
        public static List<Instruction> Parse(string script_path)
        {
            List<Instruction> resVec = new List<Instruction>();
            FileStream fs = new FileStream(script_path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string inst = sr.ReadLine();
                Instruction parsed = Sprache.ParserExtensions.Parse(Script.Instruction, inst);
                resVec.Add(parsed);
            }
            sr.Close();
            fs.Close();
            return resVec;
        }
    }
}
