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
    class Bgs 
    {
        public void Shift(Inst.Bgs bgs)
        {
            // 开始播放
            channel = player.InvokeChannel();
            MemoryStream ms = ResourceManager.GetInstance().GetBGM(bgs.Filename);
            float volume = GlobalConfig.GAME_BGS_VOLUME;
            player.InitAndPlay(channel, ms, volume, true);
        }

        public Bgs()
        {
            player = NAudioPlayer.GetInstance();
        }

        private NAudioPlayer player;
        private int channel;
    }
}