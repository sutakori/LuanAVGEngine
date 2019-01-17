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

using LuanPlatform.Core;
using LuanPlatform.Core.Graphic;
using LuanCore;

namespace LuanPlatform.PageView
{
    /// <summary>
    /// Interaction logic for Stage.xaml
    /// </summary>
    public partial class Stage : Page
    {
        private World world;

        public ObjectDataProvider TransitionDS = new ObjectDataProvider();
        public delegate void AppendTb(TextBlock tb, string s);

        private NavigationService navService;
        private void Sign_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
        }
        public Stage()
        {
            InitializeComponent();
            this.Width = this.BO_MainGrid.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            this.BO_MainGrid.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            this.Title = GlobalConfig.GAME_TITLE_NAME;
            this.TransitionBox.DataContext = this.TransitionDS;
            this.BO_Bg_Canvas.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.BO_Bg_Canvas.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            this.BO_Cstand_Canvas.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.BO_Cstand_Canvas.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            this.BO_Pics_Canvas.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.BO_Pics_Canvas.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            this.BO_MessageLayer_Canvas.Width = GlobalConfig.GAME_MESSAGELAYER_W;
            this.BO_MessageLayer_Canvas.Height = GlobalConfig.GAME_MESSAGELAYER_H;
            Panel.SetZIndex(this.BO_Bg_Viewbox, GlobalConfig.GAME_Z_BACKGROUND);
            Panel.SetZIndex(this.BO_Cstand_Viewbox, GlobalConfig.GAME_Z_CHARACTERSTAND);
            Panel.SetZIndex(this.BO_Pics_Viewbox, GlobalConfig.GAME_Z_PICTURES);
            Panel.SetZIndex(this.BO_MessageLayer_Viewbox, GlobalConfig.GAME_Z_MESSAGELAYER);

            this.Loaded += new RoutedEventHandler(Sign_Loaded);
        }

        private void Page_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.world = World.GetInstance();
            ViewManager.GetInstance().InitViewport2D();
            RuntimeManager.GetInstance().RunStep();
        }

        #region 窗体监听事件
        private void Page_MouseUp(object sender, MouseButtonEventArgs e)
        {
            world.UpdateMouse(e);
        }

        #endregion
    }
}
