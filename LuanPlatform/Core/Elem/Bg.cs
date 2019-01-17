using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inst = LuanCore.Instructions;
using LuanPlatform.Core.Graphic;
using System.IO;
using LuanCore;
namespace LuanPlatform.Core.Elem
{
    [Serializable]
    class Bg
    {
        public void Shift(Inst.Bg bg)
        {
            if (bg.Filename != Filename)
            {
                Form(bg);
                Remove();
                sprite = ResourceManager.GetInstance().GetBackground(
                    Filename, ResourceManager.FullImageRect);
                sd = new SpriteDescriptor()
                {
                    ResourceType = typeof(Elem.Bg),
                    ResourceName = Filename,
                    X = GlobalConfig.GAME_WINDOW_WIDTH / 2.0,
                    Y = GlobalConfig.GAME_WINDOW_HEIGHT / 2.0,
                    Z = GlobalConfig.GAME_Z_BACKGROUND,
                    ScaleX = Scalex,
                    ScaleY = Scaley,
                    Opacity = Opacity,
                    AnchorType = SpriteAnchorType.Center,
                    CutRect = ResourceManager.FullImageRect
                };
                if (ViewPageManager.IsAtMainStage())
                    id = ViewManager.GetInstance().DrawSprite2D(sprite, sd, typeof(Elem.Bg), out _);
            }
        }
        
        public void Remove()
        {
            if (sprite != null)
            {
                ViewManager.GetInstance().RemoveSprite(id, typeof(Elem.Bg));
            }
        }

        public Bg()
        {
            Filename = null;
        }

        private void Form(Inst.Bg bg)
        {
            Filename = bg.Filename;
            Scalex = bg.ScaleX;
            Scaley = bg.ScaleY;
            Opacity = bg.Opacity;
        }

        [NonSerialized]
        private Sprite sprite = null;
        private SpriteDescriptor sd = null;
        [NonSerialized]
        private int id = -1;

        public string Filename { get; set; }
        public double Scalex { get; set; }
        public double Scaley { get; set; }
        public double Opacity { get; set; }
    }
}
