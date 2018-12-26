using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuanCore;
using LuanPlatform.Core.VM;

namespace LuanPlatform.Core
{
    class RuntimeManager
    {
        internal void RunSection(Section section)
        {
            Interpreter interpreter = Interpreter.GetInstance();
            interpreter.LoadInstructions();
        }

   

    }
}
