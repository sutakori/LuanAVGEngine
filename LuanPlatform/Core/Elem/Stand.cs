using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inst = LuanCore.Instructions;
using LuanPlatform.Core.Audio;
using System.IO;
using LuanPlatform.Core.Graphic;
using LuanCore;
namespace LuanPlatform.Core.Elem
{
    [Serializable]
    class Stand 
    {
        public void Shift(Inst.Stand stand)
        {
            Form(stand);
            Remove();
            if (Face == "exit") return;
            sprite = ResourceManager.GetInstance().GetStand(
                Filename, ResourceManager.FullImageRect);
            sd = new SpriteDescriptor()
            {
                ResourceType = typeof(Elem.Stand),
                ResourceName = Filename,
                X = (double)(typeof(GlobalConfig).GetField(
                    $"GAME_CHARACTERSTAND_{Pos.ToString().ToUpper()}_X").GetValue(null)),
                Y = (double)(typeof(GlobalConfig).GetField(
                    $"GAME_CHARACTERSTAND_{Pos.ToString().ToUpper()}_Y").GetValue(null)),
                Z = GlobalConfig.GAME_Z_CHARACTERSTAND,
                ScaleX = ScaleX,
                ScaleY = ScaleY,
                Opacity = 1,
                AnchorType = SpriteAnchorType.Center,
                CutRect = ResourceManager.FullImageRect
            };
            if (ViewPageManager.IsAtMainStage())
                id = ViewManager.GetInstance().DrawSprite2D(sprite, sd, typeof(Elem.Stand), out _);
        }

        public void Remove()
        {
            if (sprite != null)
            {
                ViewManager.GetInstance().RemoveSprite(id, typeof(Elem.Stand));
            }
        }
        private void Form(Inst.Stand s)
        {
            Name = s.Name;
            Face = s.Face;
            Ext = s.Ext;
            Filename = Name + '\\' + Face + '.' + Ext;
            Pos = s.Pos;
            ScaleX = s.ScaleX;
            ScaleY = s.ScaleY;
        }

        [NonSerialized]
        private Sprite sprite = null;
        private SpriteDescriptor sd = null;
        [NonSerialized]
        private int id = -1;

        public string Filename { get; set; } = null;
        public string Name { get; set; } = null;
        public string Face { get; set; } = null;
        public string Ext { get; set; } = null;
        public Inst.StandPos Pos { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }    
    }
}
