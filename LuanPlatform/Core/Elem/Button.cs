using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuanCore;
using Inst = LuanCore.Instructions;
using LuanPlatform.Core.Graphic;
using System.Windows.Input;
using LuanPlatform.Core.VM;
using System.Windows;
using System.Windows.Controls;

namespace LuanPlatform.Core.Elem
{
    [Serializable]
    public class Button
    {
        public void Perform()
        {
            if (OptionBinding != null)
                AdaptBranch();
            id = ViewManager.GetInstance().DrawButton(this, id);
        }

        public void Remove()
        {
            if(IsStandAlone())
                ViewManager.GetInstance().RemoveSprite(id, typeof(Elem.Button));
            else
                ViewManager.GetInstance().RemoveSprite(id, typeof(Elem.Option));
        }

        private void AdaptBranch()
        {
            int l = OptionBinding.BranchBinding.Options.Count();
            int i = OptionBinding.BranchBinding.Options.IndexOf(OptionBinding);
            Y = (GlobalConfig.GAME_WINDOW_HEIGHT - GlobalConfig.GAME_MESSAGELAYER_H * 2) * 
                (2 * i + 2) / (2 * l + 1);
            if (IsStandAlone())
                SdNorm.Y = Y;
            else
                md.Y = Y;
        }

        public Button(Inst.Button button)
        {
            Filename = button.Filename;
            Label = button.Label;
            Stmts = button.Block;
            X = button.X;
            Y = button.Y;
            ScaleX = button.ScaleX;
            ScaleY = button.ScaleY;
            Opacity = button.Opacity;

            Signal = button.Signal;

            SpriteNorm = ResourceManager.GetInstance().GetAsset(Filename);
            md = new MessageLayerDescriptor()
            {
                BackgroundResourceName = GlobalConfig.GAME_DEFAULT_BRANCHBUTTON,
                FontColorR = GlobalConfig.GAME_FONT_COLOR.R,
                FontColorG = GlobalConfig.GAME_FONT_COLOR.G,
                FontColorB = GlobalConfig.GAME_FONT_COLOR.B,
                FontName = GlobalConfig.GAME_BUTTON_FONT_NAME,
                FontSize = GlobalConfig.GAME_BUTTON_FONT_FONTSIZE,
                FontShadow = GlobalConfig.GAME_MESSAGELAYER_SHADOW,
                LineHeight = GlobalConfig.GAME_BUTTON_FONT_LINEHEIGHT,
                HorizonAlign = HorizontalAlignment.Center,
                VertiAlign = VerticalAlignment.Center,
                TextHorizonAlign = TextAlignment.Center,
                X = X,
                Y = Y,
                Z = GlobalConfig.GAME_Z_MESSAGELAYER,
                Height = GlobalConfig.GAME_BRANCHBUTTON_H,
                Width = GlobalConfig.GAME_BRANCHBUTTON_W,
                Padding = new MyThickness(GlobalConfig.GAME_BRANCHBUTTON_PADDING),
                Opacity = 0.8,
                Visible = true,
                Text = Label
            };
            SdNorm = new SpriteDescriptor()
            {
                ResourceType = typeof(Elem.Button),
                ResourceName = Filename,
                X = X,
                Y = Y,
                Z = GlobalConfig.GAME_Z_PICTURES,
                ScaleX = (ScaleX != 0 ? ScaleX : 1),
                ScaleY = (ScaleY != 0 ? ScaleY : 1),
                Opacity = (Opacity != 0 ? Opacity : 1),
                AnchorType = SpriteAnchorType.Center,
                CutRect = ResourceManager.FullImageRect
            };
        }

        public bool IsStandAlone()
        {
            //return Label == null || Label == string.Empty;
            return OptionBinding == null;
        }

        public int id  = -1;
        public MessageLayerDescriptor md = null;
        [NonSerialized]
        public Sprite SpriteNorm = null;
        public SpriteDescriptor SdNorm { get; set; }

        public Option OptionBinding { get; set; } = null;
        public string Filename { get; set; }
        public string Label { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double Opacity { get; set; }
        public List<Stmt> Stmts { get; set; } = null;
        public Inst.Signal Signal { get; set; }

        internal void MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        internal void MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        internal void MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        internal void MouseUp(object sender, MouseButtonEventArgs e)
        {
            Context.GetInstance().Shift(RuntimeManager.GetInstance().section, Stmts);
            if (OptionBinding != null)
                OptionBinding.Shift();
            if (Signal != null)
                Interpreter.GetInstance().Submit(Signal);
            RuntimeManager.GetInstance().RunStep();
            e.Handled = true;
        }
    }
}

