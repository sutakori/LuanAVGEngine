using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inst = LuanCore.Instructions;
using LuanPlatform.Core.VM;

using LuanCore;
namespace LuanPlatform.Core.Elem
{
    [Serializable]
    public class Branch
    {
        public void Perform()
        {
            foreach (var option in Options)
            {
                option.Perform();
            }
        }

        public void Remove()
        {
            foreach (var option in Options)
            {
                option.Remove();
            }
        }
        public Branch(Inst.Branch branch)
        {
            Options = branch.Options.Select((Inst.Option ioption)=>
            {
                Option option = new Option()
                {
                    Label = ioption.Label,
                    Button = new Button(ioption.Button),
                    IShift = ioption.Shift,
                    BranchBinding = this
                };
                option.Button.OptionBinding = option;
                return option;
            }).ToList();
        }
        public List<Option> Options { get; set; } = new List<Option>();
    }

    [Serializable]
    public class Option
    {
        public void Perform()
        {
            Button.Perform();
        }

        public void Remove()
        {
            Button.Remove();
        }
        public void Shift()
        {
            if(IShift!=null)
                Interpreter.GetInstance().Submit(IShift);
        }
        public string Label { get; set; }
        public Button Button { get; set; }
        public Branch BranchBinding { get; set; } 
        public Inst.Shift IShift = null;
    }
}
