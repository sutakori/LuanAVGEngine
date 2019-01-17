using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inst = LuanCore.Instructions;
using LuanPlatform.Core.Audio;
using System.IO;
using LuanPlatform.Core.Graphic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using LuanCore;
using System.Threading;
using System.Windows.Threading;

namespace LuanPlatform.Core.Elem
{
    [Serializable]
    public class Text 
    {
        public void Shift(Inst.Text text)
        {
            StringBuilder builder = new StringBuilder(text.Content);
            builder.Replace("\\n", "\n");
            Content = builder.ToString();
            if (text.Vocal != null)
            {
                Vocal.Shift(text.Vocal);
            }
            if (msgBlock == null)
                msgBlock = new TextBlock();
            Show();
            TextBG.HideTextOver();
            stopTypeWriterToken = ViewManager.GetInstance().UpdateText(this);
        }
        
        public void Over()
        {
            //dispatcherOp.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Send);
            stopTypeWriterToken.Cancel();
            MsgBlock.Text = Content;
            Vocal?.Over();
            TextBG.ShowTextOver();
            World.GetInstance().State = WorldState.Wait;
        }

        public void Remove()
        {
            if (id != -1)
            {
                ViewManager.GetInstance().RemoveSprite(id, typeof(Elem.Text));
                id = -1;
            }
        }

        public void Show()
        {
            if(id==-1)
            {
                id = ViewManager.GetInstance().DrawMessageLayer(this);
            }
            else if(!visible)
            {
                ViewManager.GetInstance().ShowSprite(id, typeof(Elem.Text));
            }
            visible = true;
        }

        public void Hide()
        {
            if (id != -1)
            {
                ViewManager.GetInstance().HideSprite(id, typeof(Elem.Text));
            }
            visible = false;
        }

        public void Clear()
        {
            if (MsgBlock!=null)
                MsgBlock.Text = String.Empty;
            TextBG.HideTextOver();
        }

        public Text()
        {
            Content = String.Empty;
            Vocal = new Vocal();
            TextBG = new TextBG();

            MsgBlock = new TextBlock();
            
        }

        private bool visible = true;
        [NonSerialized]
        private CancellationTokenSource stopTypeWriterToken = null;
        [NonSerialized]
        private int id = -1;
        [NonSerialized]
        private TextBlock msgBlock = null;
        public TextBlock MsgBlock { get { return msgBlock; } set { msgBlock = value; } }

        public TextBG TextBG { get; set; }

        public Vocal Vocal { get; set; }
        public string Content { get; set; }
    }

    [Serializable]
    public class TextBG
    {
        public TextBG()
        {
            Filename = GlobalConfig.GAME_DEFAULT_TEXTBOX;
            Opacity = GlobalConfig.GAME_OPACITY_TEXTBOX;
            sprite = ResourceManager.GetInstance().GetAsset(Filename);
            md = new MessageLayerDescriptor()
            {
                BackgroundResourceName = GlobalConfig.GAME_DEFAULT_TEXTBOX,
                FontColorR = GlobalConfig.GAME_FONT_COLOR.R,
                FontColorG = GlobalConfig.GAME_FONT_COLOR.G,
                FontColorB = GlobalConfig.GAME_FONT_COLOR.B,
                FontName = GlobalConfig.GAME_FONT_NAME,
                FontSize = GlobalConfig.GAME_FONT_FONTSIZE,
                FontShadow = GlobalConfig.GAME_MESSAGELAYER_SHADOW,
                LineHeight = GlobalConfig.GAME_FONT_LINEHEIGHT,
                HorizonAlign = HorizontalAlignment.Left,
                VertiAlign = VerticalAlignment.Bottom,
                X = GlobalConfig.GAME_WINDOW_WIDTH/2,
                Y = GlobalConfig.GAME_MESSAGELAYER_Y,
                Z = GlobalConfig.GAME_Z_MESSAGELAYER,
                Height = GlobalConfig.GAME_MESSAGELAYER_H,
                Width = GlobalConfig.GAME_MESSAGELAYER_W,
                Padding = new MyThickness(GlobalConfig.GAME_MESSAGELAYER_PADDING),
                Opacity = 1.0,
                Visible = true,
                Text = String.Empty
            };
            TextOverSp = ResourceManager.GetInstance().GetAsset(GlobalConfig.GAME_TEXTOVER_FILENAME);
            TextOverSd = new SpriteDescriptor()
            {
                ResourceType = typeof(Elem.Stand),
                ResourceName = GlobalConfig.GAME_TEXTOVER_FILENAME,
                X = GlobalConfig.GAME_TEXTOVER_X,
                Y = GlobalConfig.GAME_TEXTOVER_Y,
                Z = GlobalConfig.GAME_Z_MESSAGELAYER+1,
                ScaleX = 1,
                ScaleY = 1,
                Opacity = 1,
                AnchorType = SpriteAnchorType.Center,
                CutRect = ResourceManager.FullImageRect
            };
        }

        // 显示倒三角
        public void ShowTextOver()
        {
            if (idOver == -1)
            {
                idOver = ViewManager.GetInstance().DrawSprite2D(TextOverSp, TextOverSd, typeof(Elem.Text), out _);
            }
            else
            {
                ViewManager.GetInstance().ShowSprite(idOver, typeof(Elem.Text));
            }
        }

        public void HideTextOver()
        {
            if (idOver != -1)
            {
                ViewManager.GetInstance().HideSprite(idOver, typeof(Elem.Text));
            }
        }

        [NonSerialized]
        public Sprite sprite = null;
        public MessageLayerDescriptor md = null;
        [NonSerialized]
        public Sprite TextOverSp = null;
        public SpriteDescriptor TextOverSd = null;
        [NonSerialized]
        public int idOver = -1;

        public string Filename { get; set; }
        public double Opacity { get; set; }
    }
}
