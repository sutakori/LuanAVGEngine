﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LuanConvertor;
using Sprache;
namespace LuanCore
{
    /// <summary>
    /// 与脚本同步，描述应该进行的操作
    /// </summary>
    [Serializable]
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
        public Instruction() { }

        abstract public void Form();

        public void SetBlock(string blockStr)
        {
            Block = ParserExtensions.Parse(Script.Block, "{"+blockStr+"}");
        }

        public string GetBlockStr()
        {
            StringBuilder sb = new StringBuilder(10);
            sb.Append('{');
            foreach(var stmt in Block)
            {
                sb.Append(stmt.Name);
                sb.Append('=');
                sb.Append(stmt.Rvalue.Lexeme);
                sb.Append(';');
            }
            sb.Append('}');
            return sb.ToString();
        }

        public List<Instruction> SubInsts { get; set; } = new List<Instruction>();
        public List<Stmt> Block { get; set; } = new List<Stmt>();
        public Dictionary<string, string> ArgsDict { get; set; } = new Dictionary<string, string>();
    }

    [Serializable]
    public class Expr
    {
        public Expr(string lexeme)
        {
            this.Lexeme = lexeme;
        }
        public string Lexeme { get; set; }
        public List<String> GetNames()
        {
            List<string> names = Script.getNames(Lexeme);
            return names;
        }
    }

    [Serializable]
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
