using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LuanCore;
using LuanCore.Instructions;

namespace LuanPlatform.Core.VM
{
    /// <summary>
    /// 解释器类，逐行解释LuanIL形成
    /// </summary>
    class Interpreter
    {

        public void LoadInstructions(List<Instruction> instructions)
        {
            this.instructions = new Queue<Instruction>(instructions);
        }

        public void Run()
        {
            while (SteadyStep())
            {
                Perform();
            }
        }

        private void ShiftGuard(Guard guard)
        {
            // guard条件满足
            if (Context.EvalGuard(guard.Expr)) { return; }
            // 不满足，跳过指令直到guard块结束
            Instruction inst;
            int depth = 0;
            while (this.instructions.Count != 0)
            {
                inst = this.instructions.Dequeue();
                if(inst is Guard)
                    depth++;
                if(inst is Guardend)
                {
                    depth--;
                    if(depth==0)
                        return;
                }
            }
        }
        /// <summary>
        /// 前进到下一个frame
        /// </summary>
        public Frame SteadyStep()
        {
            Instruction inst;
            Frame next_frame;
            while (instructions.Count != 0)
            {
                inst = instructions.Dequeue();
                //
                switch (inst)
                {
                    case Guard g:
                        ShiftGuard(g);
                        break;
                    // frame同步点
                    case Text t:
                    case Branch b:

                        break;

                }
            }
        }
        private Queue<Instruction> instructions;

        private Interpreter() { }
        public static Interpreter GetInstance()
        {
            return instance ?? (instance = new Interpreter());
        }

        private static Interpreter instance = null;
    }
}
