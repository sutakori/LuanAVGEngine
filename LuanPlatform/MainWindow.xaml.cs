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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LuanCore;
using LuanPlatform.Core;
using LuanPlatform.Core.Graphic;
using LuanPlatform.PageView;

namespace LuanPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private World world = World.GetInstance();
        public static MainWindow AppWindow;

        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;
            this.Left = 10;
            this.Top = 10;
            ViewManager.SetWindowReference(this);
            this.Title = GlobalConfig.GAME_TITLE_NAME;
            this.Width = GlobalConfig.GAME_VIEWPORT_WIDTH;
            this.Height = GlobalConfig.GAME_VIEWPORT_ACTUALHEIGHT;
            this.mainCanvas.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.mainCanvas.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            this.ResizeMode = ResizeMode.NoResize;

            this.mainFrame.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.mainFrame.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            ViewPageManager.RegisterPage("Stage", new LuanPlatform.PageView.Stage());
            this.maskFrame.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.maskFrame.Height = GlobalConfig.GAME_WINDOW_HEIGHT;
            this.uiFrame.Width = GlobalConfig.GAME_WINDOW_WIDTH;
            this.uiFrame.Height = GlobalConfig.GAME_WINDOW_HEIGHT;

            //ViewPageManager.ShowUIPage("Stage");
            var sp = ViewPageManager.RetrievePage("Stage");
            ViewPageManager.InitFirstPage(sp);
            //ViewPageManager.NavigateTo("Stage");
            this.mainFrame.NavigationService.Navigate(sp);
            //this.mainFrame.Navigate(new Uri("Stage.xaml", UriKind.Relative));
            World.ResumeUpdateContext();
        }

        public void GoToMainStage()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Click(object sender, MouseButtonEventArgs e)
        {
            //world.UpdateMouse(e);
        }

    }


}
