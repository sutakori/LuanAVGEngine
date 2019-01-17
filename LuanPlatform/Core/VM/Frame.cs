using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using LuanCore;
using LuanCore.Instructions;

namespace LuanPlatform.Core.VM
{
    /// <summary>
    /// 管理页面状态
    /// </summary>
    [Serializable]
    class Frame
    {
        public Elem.Bg Bg { get; set; }
        public Elem.Bgm Bgm { get; set; }
        public Elem.Text Text { get; set; }
        public Elem.Branch Branch { get; set; } = null;
        public Dictionary<string, Elem.Stand> Stands = new Dictionary<string, Elem.Stand>();
        public List<Elem.Button> sysButtons = new List<Elem.Button>();
        public List<Elem.Button> sceneButtons = new List<Elem.Button>();

        public Frame Copy()
        {
            Frame f = new Frame();
            return f;
        }

        public void Stabilize()
        {
            Text?.Over();
        }

        public void ShowButton()
        {
            if (sceneButtons.Count >0)
                foreach (var b in sceneButtons)
                    b.Perform();
            else
                foreach (var b in sysButtons)
                    b.Perform();
        }

        public void SetButton(Button b)
        {
            if (b.ArgsDict.ContainsKey("sys"))
            {
                sysButtons.Add(new Elem.Button(b));
                sysButtons[sysButtons.Count()-1].Perform();
            }
            else
            {
                sceneButtons.Add(new Elem.Button(b));
                sceneButtons[sceneButtons.Count()-1].Perform();
            }
        }

        public Frame()
        {
            Bg = new Elem.Bg();
            Bgm = new Elem.Bgm();
            Text = new Elem.Text();
        }

        public void Clear()
        {
            Bg?.Remove();
            Bgm?.Remove();
            Text?.Remove();
            Branch?.Remove();
            foreach(var item in Stands)
            {
                item.Value?.Remove();
            }
            foreach (var b in sceneButtons)
            {
                b.Remove();
            }
            sceneButtons = new List<Elem.Button>();
        }

        public void SceneClear()
        {
            
        }
        public void Perform(Instruction inst)
        {
            // 去掉不需要转移的项
            Branch?.Remove();
            Branch = null;
            Text?.Clear();
            switch (inst)
            {
                case Bgm b:
                    Bgm.Shift(b);
                    break;
                case Bg bg:
                    Bg.Shift(bg);
                    break;
                case Text t:
                    Text.Shift(t);
                    break;
                case Bgs b:
                    new Elem.Bgs().Shift(b);
                    break;
                case Stand s:
                    if (Stands.ContainsKey(s.Name))
                        Stands[s.Name].Shift(s);
                    else
                    {
                        Elem.Stand stand = new Elem.Stand();
                        stand.Shift(s);
                        Stands.Add(s.Name, stand);
                    }
                    break;
                case Branch b:
                    Branch = new Elem.Branch(b);
                    Branch.Perform();
                    break;
                case Signal s:
                    Type tworld = typeof(World);
                    MethodInfo method = tworld.GetMethod(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.Name));
                    method.Invoke(World.GetInstance(), new object[]{
                    s.ArgsDict });
                    break;
                default:
                    //inst.Perform();
                    break;
            }
        }
    }
}
