using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inst = LuanCore.Instructions;
using LuanPlatform.Core.Audio;
using System.IO;
using LuanCore;
namespace LuanPlatform.Core.Elem
{
    [Serializable]
    class Bgm
    {
        public void Shift(Inst.Bgm bgm)
        {
            if(player==null)
                player = NAudioPlayer.GetInstance();
            // bgm一致，继续播放
            if (this.Filename == bgm.Filename) return;
            // 停止当前bgm
            Remove();
            // 开始播放
            channel = player.InvokeChannel();
            MemoryStream ms = ResourceManager.GetInstance().GetBGM(bgm.Filename);
            float volume = GlobalConfig.GAME_BGM_VOLUME;
            player.InitAndPlay(channel, ms, volume, true);
            this.IsPlay = true;
            this.Filename = bgm.Filename;
        }

        public void Remove()
        {
            if (IsPlay)
                player.StopAndRelease(channel);
        }
        public Bgm()
        {
            IsPlay = false;
        }

        [NonSerialized]
        private NAudioPlayer player = null;
        private int channel;

        public bool IsPlay { get; set; }
        public string Filename { get; set; }
    }
}
