using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using LuanCore;
using LuanCore.Instructions;

namespace LuanPlatform.Core.VM
{
    /// <summary>
    /// 解释器类，逐行解释LuanIL形成
    /// </summary>
    [Serializable]
    class Interpreter
    {

        public void LoadInstructions(List<Instruction> instructions)
        {
            instructions.Reverse();
            this.instructions = new Stack<Instruction>(instructions);
        }

        public (RuntimeShift shift, string target, Frame frame) RunStep(Frame frame)
        {
            if (instructions.Count != 0)
            {
                var step = SteadyStep(ref frame);
                switch (step.shift)
                {
                    case RuntimeShift.Scene:
                        return (RuntimeShift.Scene, step.target, frame);
                    case RuntimeShift.Section:
                        return (RuntimeShift.Section, step.target, frame);
                }
            }
            return (RuntimeShift.Next, "", frame);
        }

        public void Clear(ref Frame frame)
        {
            frame.Clear();
        }

        private void ShiftGuard(Guard guard)
        {
            // guard条件满足
            if (context.EvalGuard(RuntimeManager.GetInstance().section, guard.Expr)) { return; }
            // 不满足，跳过指令直到guard块结束
            Instruction inst;
            int depth = 0;
            while (this.instructions.Count != 0)
            {
                inst = this.instructions.Pop();
                if(inst is Guard)
                    depth++;
                if(inst is Guardend)
                {
                    depth--;
                    if(depth==-1)
                        return;
                }
            }
            throw new Exception();
        }

        public void Submit(Instruction inst)
        {
            instructions.Push(inst);
        }

        /// <summary>
        /// 前进到下一个frame
        /// </summary>
        public (RuntimeShift shift, string target) SteadyStep(ref Frame frame)
        {
            Instruction inst;
            //Frame next_frame = frame.Copy();
            while (instructions.Count != 0)
            {
                inst = instructions.Pop();
                if(!(inst is Button) && RuntimeManager.GetInstance().section!=null)
                    Context.GetInstance().Shift(RuntimeManager.GetInstance().section, inst.Block);
                switch (inst)
                {
                    case Guard g:
                        ShiftGuard(g);
                        break;
                    // 跳转
                    case Shift sh:
                        switch (sh.Typ)
                        {
                            case ShiftTyp.scene:
                                return (RuntimeShift.Scene, sh.Target);
                            case ShiftTyp.section:
                                return (RuntimeShift.Section, sh.Target);
                        }
                        break;
                    // frame同步点
                    case Sync s:
                        return (RuntimeShift.Wait, "");
                    case Text t:
                    case Branch b:
                        frame.Perform(inst);
                        return (RuntimeShift.Wait, "");
                    case Button b:
                        frame.SetButton(b);
                        break;
                    default:
                        frame.Perform(inst);
                        break;
                }
            }
            return (RuntimeShift.Next, "");
        }

        private Stack<Instruction> instructions = new Stack<Instruction>();
        private Context context;
        private Interpreter()
        {
            context = Context.GetInstance();
        }
        public static Interpreter GetInstance()
        {
            return instance ?? (instance = new Interpreter());
        }

        private static Interpreter instance = null;
    }
}
