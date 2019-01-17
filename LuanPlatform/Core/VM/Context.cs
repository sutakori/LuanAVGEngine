using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LuanCore;
using Eval = DynamicExpresso;
using LuanUtils;

namespace LuanPlatform.Core.VM
{
    /// <summary>
    /// 管理游戏的变量状态
    /// </summary>
    [Serializable]
    class Context
    {
        internal bool EvalGuard(Section section, Expr expr)
        {
            bool ret;
            try
            {
                ret = (bool)EvalExpr(section, expr);
                return ret;
            }
            catch
            {
                LogUtils.Log("Guard eval error", "EvalGuard", LogLevel.Error);
                return false;
            }
        }

        internal object EvalExpr(Section section, Expr expr)
        {
            try
            {
                var interpreter = new Eval.Interpreter();
                foreach (var name in expr.GetNames())
                {
                    if (symbolTable[section.Name].ContainsKey(name))
                        interpreter.SetVariable(name, symbolTable[section.Name][name]);
                    else
                        interpreter.SetVariable(name, symbolTable[GlobalConfig.CONTEXT_SECTION]);
                }
                var ret = interpreter.Eval(expr.Lexeme);
                return ret;
            }
            catch
            {
                LogUtils.Log("Eval expr error", "EvalExpr", LogLevel.Error);
                return null;
            }
        }

        internal void Shift(Section section, List<Stmt> stmts)
        {
            if (!symbolTable.ContainsKey(section.Name))
                symbolTable[section.Name] = new Dictionary<string, object>();
            foreach (var stmt in stmts)
                Shift(section, stmt);
        }

        private void Shift(Section section, Stmt stmt)
        {
            symbolTable[section.Name][stmt.Name] = EvalExpr(section, stmt.Rvalue);
        }

        private Context()
        {
            symbolTable[GlobalConfig.CONTEXT_SECTION] = new Dictionary<string, object>();
        }

        public static Context GetInstance()
        {
            return instance ?? (instance = new Context());
        }

        private static Context instance = null;

        private Dictionary<string, Dictionary <string, object>> symbolTable = 
            new Dictionary<string, Dictionary<string, object>>();
    }
}
