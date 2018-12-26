using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore
{
    /// <summary>
    /// 与脚本同步，描述应该进行的操作
    /// </summary>
    abstract public class Instruction
    {
        public Instruction(List<Stmt> block, 
            Dictionary<string, string> argsDict,
            List<Instruction> subinsts)
        { 
            this.Block = block;
            this.ArgsDict = argsDict;
            this.SubInsts = subinsts;

            Form();
        }
        abstract public void Form();

        protected List<Instruction> SubInsts { get; set; }
        protected List<Stmt> Block { get; set; }
        protected Dictionary<string, string> ArgsDict { get; set; } = new Dictionary<string, string>();
    }

    public class Expr
    {
        public Expr(string lexeme)
        {
            this.Lexeme = lexeme;
        }
        public string Lexeme { get; set; }
    }

    public class Stmt
    {
        public Stmt(string name, Expr expr)
        {
            this.Name = name;
            this.Rvalue = expr;
        }
        public string Name { get; set; }
        public Expr Rvalue { get; set; }
    }

}
