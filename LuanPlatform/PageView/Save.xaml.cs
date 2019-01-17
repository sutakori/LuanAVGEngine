using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LuanCore;
namespace LuanPlatform.PageView
{
    /// <summary>
    /// Interaction logic for Save.xaml
    /// </summary>
    public partial class Save : Page
    {
        public Save()
        {
            InitializeComponent();
            this.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            this.Title = GlobalConfig.GAME_TITLE_NAME;
        }
    }
}
