using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sprache;
using LuanCore;
using LuanCore.Instructions;
using System.Globalization;

namespace LuanConvertor
{
    public static class Script
    {
        public static readonly Parser<string> QuotedText =
            (from open in Parse.Char('"')
             from content in Parse.CharExcept('"').Many().Text()
             from close in Parse.Char('"')
             select content).Token();

        public static readonly Parser<KeyValuePair<string, string>> ArgDict =
            (from key in Parse.Letter.Many().Text()
             from _ in Parse.Char('=')
             from value in QuotedText
             select new KeyValuePair<string, string>(key, value)).Token();

        public static readonly Parser<bool> Boolean =
            Parse.String("true").Return(true)
            .Or(Parse.String("false").Return(false));

        public static readonly Parser<char> Op =
            Parse.Char(c => "+=/*!-<>\"'".Contains(c), "op in expr");

        public static readonly Parser<Expr> LuanExpr =
            (from expr in Parse.LetterOrDigit.Or(Op).Many().Text()
             select new Expr(expr)).Token();

        public static readonly Parser<Stmt> Assign =
            (from name in Parse.Letter.Many().Text()
             from _ in Parse.Char('=')
             from value in LuanExpr
             from end in Parse.Char(';')
             select new Stmt(name, value)).Token();

        public static readonly Parser<List<Stmt>> Block =
            (from open in Parse.Char('{')
             from assigns in Assign.Many()
             from close in Parse.Char('}')
             select new List<Stmt>(assigns)).Token();

        public static readonly Parser<string> Header =
            (from director in Parse.Char('@')
             from name in Parse.Letter.Many().Text()
             select name).Token();

        public static readonly Parser<Instruction> UnitInstruction =
            from head in Header
            from argdicts in ArgDict.Many()
            from block in Block.Optional()
            select(Instruction)Activator.CreateInstance(
               Type.GetType("LuanCore.Instructions." + 
                    CultureInfo.InvariantCulture.TextInfo.ToTitleCase(head) +
                    ", LuanCore"),
                new object[]{block.IsEmpty? new List<Stmt>():block.Get(),
                    argdicts.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value),
                    new List<Instruction>()});

        public static readonly Parser<Instruction> Instruction =
            (from head in Header
            from insts in UnitInstruction.Many()
            from argdicts in ArgDict.Many()
            from block in Block.Optional()
            select (Instruction)Activator.CreateInstance(
                Type.GetType("LuanCore.Instructions." + 
                    CultureInfo.InvariantCulture.TextInfo.ToTitleCase(head) +
                    ", LuanCore"),
                new object[]{block.IsEmpty? new List<Stmt>():block.Get(),
                    argdicts.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value),
                    insts })).End();


    }
}
