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
        #region stmt
        public static string deleteWhiteSpace(string rawString)
        {
            return rawString.Replace(" ", "");
        }

        public static bool isBoolStmt(string rawString)
        {
            if (rawString.IndexOf("==") != -1)
                return true;
            return false;
        }

        public static List<string> getVarListInBoolStmt(string rawstring)
        {
            List<string> res = new List<string>();
            string string1 = rawstring.Substring(0, rawstring.IndexOf("=="));
            string string2 = rawstring.Substring(rawstring.IndexOf("==") + 2);
            if (isIdentifier(string1))
                res.Add(deleteWhiteSpace(string1));
            if (isIdentifier(string2))
                res.Add(deleteWhiteSpace(string2));
            return res;
        }

        public static bool isIdentifier(string str)
        {
            str = deleteWhiteSpace(str);
            if (str == "true" || str == "false")
                return false;
            if (str[0] >= '0' && str[0] <= '9')
                return false;
            else return true;
        }

        public static List<string> combine(string right, IEnumerable<string> remaining)
        {
            List<string> result = new List<string>();
            //result.Add(deleteWhiteSpace(left));

            if (isBoolStmt(right))
            {
                List<string> varListInBoolStmt = getVarListInBoolStmt(right);
                foreach (string s in varListInBoolStmt)
                    result.Add(s);
            }
            else
            {
                if (isIdentifier(right))
                    result.Add(deleteWhiteSpace(right));
            }
            foreach (string str in remaining)
            {
                if (isBoolStmt(str))
                {
                    List<string> varListInBoolStmt = getVarListInBoolStmt(str);
                    foreach (string s in varListInBoolStmt)
                        result.Add(s);
                }
                else
                {
                    if (isIdentifier(str))
                        result.Add(deleteWhiteSpace(str));
                }
            }
            return result;
        }

        public static readonly Parser<string> VarName =
                (
                    from Name in Parse.CharExcept('=').Many().Text()
                    select Name
                ).Token();

        public static readonly Parser<string> RightValue =
        (
            from Rvalue in Parse.CharExcept(';').Many().Text()
            select Rvalue
        ).Token();

        public static readonly Parser<Stmt> BlockResolve =
         (
             from Name in VarName
             from equal in Parse.Char('=')
             from Rvalue in RightValue
             from end in Parse.Char(';')
             select new Stmt(Name, new Expr(Rvalue))
          ).Token();

        public static readonly Parser<List<Stmt>> StmtList =
        (
            from Stmts in BlockResolve.Many()
            select new List<Stmt>(Stmts)
        ).Token();

        public static readonly Parser<string> plus_Operator =
        (
            from plus in Parse.Char('+')
            from single in Parse.CharExcept("+;").Many().Text()
            select single
        ).Token();


        public static readonly Parser<List<string>> IdentifierResolve =
       (
           //from left in Parse.CharExcept("=").Many().Text()
           //from _ in Parse.Char('=')
           from right in Parse.CharExcept("+;").Many().Text()
           from remaining in plus_Operator.Many()
           //from __ in Parse.Char(';')
           select new List<string>(combine(right, remaining))
       ).Token();

        public static List<string> getNames(string input)
        {
            List<string> parsedIdentifier = ParserExtensions.Parse(IdentifierResolve, input);
            return parsedIdentifier;
        }

        public static List<Stmt> getStmts(string input)
        {
            List<Stmt> parsedStatement = ParserExtensions.Parse(StmtList, input);
            return parsedStatement;
        }
#endregion

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

        public static readonly Parser<Instruction> InstructionInner =
            from head in Header
            from argdicts in ArgDict.Many()
            from insts in InstructionInner.Many()
            from block in Block
            select (Instruction)Activator.CreateInstance(
                Type.GetType("LuanCore.Instructions." +
                    CultureInfo.InvariantCulture.TextInfo.ToTitleCase(head) +
                    ", LuanCore"),
                new object[]{block,
                    argdicts.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value),
                    insts });
        
        public static readonly Parser<Instruction> Instruction =
            (from head in Header
             from argdicts in ArgDict.Many()
             from insts in InstructionInner.Many()
             from block in Block
             select (Instruction)Activator.CreateInstance(
                Type.GetType("LuanCore.Instructions." + 
                    CultureInfo.InvariantCulture.TextInfo.ToTitleCase(head) +
                    ", LuanCore"),
                new object[]{block,
                    argdicts.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value),
                    insts })).End();

       
    }
}
