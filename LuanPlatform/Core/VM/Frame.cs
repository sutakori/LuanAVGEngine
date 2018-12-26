using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanPlatform.Core.VM
{
    class Frame
    {
        public Elem.Bg Bg { get; set; }
        public Elem.Bgm Bgm { get; set; }
        public Elem.Text Text { get; set; }
        public List<Elem.Stand> Stands { get; set; } = new List<Elem.Stand>();
    }
}
