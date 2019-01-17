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
    public class Vocal 
    {
        public void Shift(Inst.Vocal vocal)
        {
            // 停止当前对话
            Form(vocal);

            if (player == null)
                player = NAudioPlayer.GetInstance();
            if(channel !=0)
                player.StopAndRelease(channel);
            // 如果有的话，播放下一个对话
            if (vocal != null)
            {
                channel = player.InvokeChannel();
                MemoryStream ms = ResourceManager.GetInstance().GetVocal(Filename);
                float volume = GlobalConfig.GAME_BGS_VOLUME;
                player.InitAndPlay(channel, ms, volume, false);
            }
        }

        public void Over()
        {
            if (channel != 0)
                player.StopAndRelease(channel);
        }

        public Vocal()
        {
            player = NAudioPlayer.GetInstance();
        }

        public void Form(Inst.Vocal vocal)
        {
            if(vocal!=null)
                this.Filename = vocal.Name + '\\' + vocal.Filename;
        }

        [NonSerialized]
        private NAudioPlayer player;
        [NonSerialized]
        private int channel = 0;

        public string Filename { get; set; }
    }
}
