using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using LuanPlatform.PageView;
using Transitionals.Controls;
using System.IO;
using System.Drawing.Imaging;
using LuanCore;
using System.Threading;
using System.Windows.Threading;

namespace LuanPlatform.Core.Graphic
{
    public class ViewManager
    {
        public void RemoveSprite(int id, Type rType)
        {
            FrameworkElement elem = this.GetCurElemVec(rType)[id];
            this.GetDrawingCanvas2D(rType).Children.Remove(elem);
            this.GetCurElemVec(rType)[id] = null;
        }

        public void ShowSprite(int id, Type rType)
        {
            FrameworkElement elem = this.GetCurElemVec(rType)[id];
            var Ic = this.GetDrawingCanvas2D(rType).Children.IndexOf(elem);
            this.GetDrawingCanvas2D(rType).Children[Ic].Visibility = Visibility.Visible;
        }

        public void HideSprite(int id, Type rType)
        {
            FrameworkElement elem = this.GetCurElemVec(rType)[id];
            var Ic = this.GetDrawingCanvas2D(rType).Children.IndexOf(elem);
            this.GetDrawingCanvas2D(rType).Children[Ic].Visibility = Visibility.Hidden;
        }

        public CancellationTokenSource UpdateText(Elem.Text t)
        {
            TextBlock tb = t.MsgBlock;
            string text = t.Content;
            tb.Text = String.Empty;
            World.GetInstance().State = WorldState.Ani;
            /*var dispatcherOp = MainWindow.AppWindow.Dispatcher.BeginInvoke((Action)(async () =>
            {
                foreach (var c in text)
                {
                    tb.Text += c;
                    await Task.Delay(t.Speed);
                }
                //t.Over();
            }));*/
            var tokenSource = new CancellationTokenSource();
            var uiTask = Task.Factory.StartNew(async () =>
            {
                foreach (var c in text)
                {
                    tokenSource.Token.ThrowIfCancellationRequested();
                    tb.Text += c;
                    await Task.Delay(50);
                }
                t.Over();
            }, tokenSource.Token, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            return tokenSource;
        }

        public int DrawMessageLayer(
            TextBlock textBlock, Sprite sprite, 
            MessageLayerDescriptor md, Type rType,
            out FrameworkElement handle)
        {
            ImageBrush ib = new ImageBrush(sprite.SpriteBitmapImage)
            {
                Stretch = Stretch.Fill,
                TileMode = TileMode.None,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top
            };
            textBlock.Background = ib;
            textBlock.Width = md.Width;
            textBlock.Height = md.Height;
            textBlock.Opacity = md.Opacity;
            textBlock.Padding = (Thickness)md.Padding;
            textBlock.LineHeight = md.LineHeight;
            textBlock.HorizontalAlignment = md.HorizonAlign;
            textBlock.VerticalAlignment = md.VertiAlign;
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(
                md.FontColorR, md.FontColorG, md.FontColorB));
            textBlock.FontSize = md.FontSize;
            textBlock.FontFamily = new FontFamily(md.FontName);
            textBlock.Text = md.Text;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.TextAlignment = md.TextHorizonAlign;
            textBlock.Visibility = Visibility.Visible;
            Canvas.SetLeft(textBlock, md.X - textBlock.Width/2);
            Canvas.SetTop(textBlock, md.Y - textBlock.Height/2);
            Panel.SetZIndex(textBlock, md.Z);
            handle = textBlock;
            this.GetDrawingCanvas2D(rType).Children.Add(textBlock);
            var elems = this.GetCurElemVec(rType);
            elems.Add(handle);
            int id = elems.Count() - 1;
            return id;
        }

        public int DrawMessageLayer(Elem.Text msglay)
        {
            int id = DrawMessageLayer(msglay.MsgBlock, msglay.TextBG.sprite, 
                msglay.TextBG.md, typeof(Elem.Text), out _);
            return id;
        }

        public int DrawButton(Elem.Button button, int id = -1)
        {
            FrameworkElement handle;
            if(button.IsStandAlone())
                id = DrawSprite2D(button.SpriteNorm, button.SdNorm, typeof(Elem.Button), out handle);
            else
            {
                TextBlock textBlock = new TextBlock();
                id = DrawMessageLayer(textBlock, button.SpriteNorm, button.md, 
                    typeof(Elem.Option), out handle);
            }
            handle.MouseDown += button.MouseDown;
            handle.MouseEnter += button.MouseEnter;
            handle.MouseLeave += button.MouseLeave;
            handle.MouseUp += button.MouseUp;

            return id;
        }

        /// <summary>
        /// 在2D画布上描绘精灵
        /// </summary>
        /// <param name="sprite">精灵</param>
        /// <param name="descriptor">精灵描述子</param>
        /// <param name="rType">资源类型</param>
        public int DrawSprite2D(Sprite sprite, SpriteDescriptor descriptor, Type rType, out FrameworkElement handle)
        {
            Image spriteImage = new Image();
            handle = spriteImage;
            BitmapImage bmp = sprite.SpriteBitmapImage;
            spriteImage.Width = bmp.PixelWidth;
            spriteImage.Height = bmp.PixelHeight;
            spriteImage.Source = bmp;
            spriteImage.Opacity = descriptor.Opacity;
            sprite.CutRect = descriptor.CutRect;
            sprite.DisplayBinding = spriteImage;
            sprite.Anchor = descriptor.AnchorType;
            sprite.Descriptor = descriptor;
            Canvas.SetLeft(spriteImage, descriptor.X - bmp.PixelWidth / 2.0);
            Canvas.SetTop(spriteImage, descriptor.Y - bmp.PixelHeight / 2.0);
            Panel.SetZIndex(spriteImage, descriptor.Z);
            spriteImage.Visibility = Visibility.Visible;
            this.GetDrawingCanvas2D(rType).Children.Add(spriteImage);
            var elems = this.GetCurElemVec(rType);
            elems.Add(spriteImage);
            int id = elems.Count() - 1;
            sprite.InitAnimationRenderTransform();
            if (rType != typeof(Elem.Bg))
            {
                sprite.AnimationElement = sprite.DisplayBinding;
                descriptor.ToScaleX = descriptor.ScaleX;
                descriptor.ToScaleY = descriptor.ScaleY;
                SpriteAnimation.RotateToAnimation(sprite, TimeSpan.Zero, descriptor.Angle);
                SpriteAnimation.ScaleToAnimation(sprite, TimeSpan.Zero, descriptor.ScaleX, descriptor.ScaleY);
            }
            return id;
        }

        /// <summary>
        /// 获取对象要描绘上去的2D画布
        /// </summary>
        /// <param name="rType">资源类型</param>
        /// <returns>画布的引用</returns>
        private Canvas GetDrawingCanvas2D(Type rType)
        {
            switch (rType)
            {
                case Type _ when rType == typeof(Elem.Bg):
                    return this.viewbox2dVec[(int)ViewportType.VTBackground].CanvasBinding;
                case Type _ when rType == typeof(Elem.Stand):
                    return this.viewbox2dVec[(int)ViewportType.VTCharacterStand].CanvasBinding;
                case Type _ when rType == typeof(Elem.Text):
                    return this.viewbox2dVec[(int)ViewportType.VTMessage].CanvasBinding;
                case Type _ when rType == typeof(Elem.Button):
                    return this.viewbox2dVec[(int)ViewportType.VTPictures].CanvasBinding;
                default:
                    return ViewManager.View2D.BO_MainGrid;
            }
        }

        private List<FrameworkElement> GetCurElemVec(Type rType)
        {
            switch (rType)
            {
                case Type _ when rType == typeof(Elem.Bg):
                    return bgElements;
                case Type _ when rType == typeof(Elem.Stand):
                    return csElements;
                case Type _ when rType == typeof(Elem.Text):
                    return textElements;
                case Type _ when rType == typeof(Elem.Button):
                    return picElements;
                case Type _ when rType == typeof(Elem.Option):
                    return branchElements;
                default:
                    return null;
            }
        }
        private List<FrameworkElement> bgElements = new List<FrameworkElement>();
        private List<FrameworkElement> csElements = new List<FrameworkElement>();
        private List<FrameworkElement> textElements = new List<FrameworkElement>();
        private List<FrameworkElement> picElements = new List<FrameworkElement>();
        private List<FrameworkElement> branchElements = new List<FrameworkElement>();
        /// <summary>
        /// 获取主视窗上的过渡容器
        /// </summary>
        /// <returns>过渡容器引用</returns>
        public TransitionElement GetTransitionBox()
        {
            return ViewManager.View2D.TransitionBox;
        }

        /// <summary>
        /// 初始化2D视图
        /// </summary>
        public void InitViewport2D()
        {
            // 初始化视窗向量
            this.viewbox2dVec[(int)ViewportType.VTBackground] = new Viewport2D()
            {
                Type = ViewportType.VTBackground,
                ViewboxBinding = View2D.BO_Bg_Viewbox,
                CanvasBinding = View2D.BO_Bg_Canvas
            };
            this.viewbox2dVec[(int)ViewportType.VTCharacterStand] = new Viewport2D()
            {
                Type = ViewportType.VTCharacterStand,
                ViewboxBinding = View2D.BO_Cstand_Viewbox,
                CanvasBinding = View2D.BO_Cstand_Canvas
            };
            this.viewbox2dVec[(int)ViewportType.VTPictures] = new Viewport2D()
            {
                Type = ViewportType.VTPictures,
                ViewboxBinding = View2D.BO_Pics_Viewbox,
                CanvasBinding = View2D.BO_Pics_Canvas
            };
            this.viewbox2dVec[(int)ViewportType.VTMessage] = new Viewport2D()
            {
                Type = ViewportType.VTMessage,
                ViewboxBinding = View2D.BO_MessageLayer_Viewbox,
                CanvasBinding = View2D.BO_MessageLayer_Canvas
            };
            // 初始化变换动画
            for (int i = 0; i < 4; i++)
            {
                TransformGroup aniGroup = new TransformGroup();
                TranslateTransform XYTransformer = new TranslateTransform();
                ScaleTransform ScaleTransformer = new ScaleTransform
                {
                    CenterX = GlobalConfig.GAME_WINDOW_WIDTH / 2.0,
                    CenterY = GlobalConfig.GAME_WINDOW_HEIGHT / 2.0
                };
                RotateTransform RotateTransformer = new RotateTransform
                {
                    CenterX = GlobalConfig.GAME_WINDOW_WIDTH / 2.0,
                    CenterY = GlobalConfig.GAME_WINDOW_HEIGHT / 2.0
                };
                aniGroup.Children.Add(XYTransformer);
                aniGroup.Children.Add(ScaleTransformer);
                aniGroup.Children.Add(RotateTransformer);
                this.viewbox2dVec[i].ViewboxBinding.RenderTransform = aniGroup;
                this.viewbox2dVec[i].RotateTransformer = RotateTransformer;
                this.viewbox2dVec[i].TranslateTransformer = XYTransformer;
                this.viewbox2dVec[i].ScaleTransformer = ScaleTransformer;
            }
        }

        /// <summary>
        /// 视窗向量
        /// </summary>
        private readonly List<Viewport2D> viewbox2dVec;

        /// <summary>
        /// 获取2D主舞台页面的引用
        /// </summary>
        public static Stage View2D => ViewPageManager.RetrievePage(GlobalConfig.FirstViewPage) as Stage;

        private ViewManager()
        {
            this.viewbox2dVec = new List<Viewport2D>();
            for (int i = 0; i < 4; i++)
            {
                this.viewbox2dVec.Add(null);
            }
        }
        public static void Clear()
        {
            ViewManager.synObject = new ViewManager();
            synObject.InitViewport2D();
        }
        public static ViewManager GetInstance()
        {
            return ViewManager.synObject ?? (ViewManager.synObject = new ViewManager());
        }
        private static ViewManager synObject = null;

        /// <summary>
        /// 为视窗管理器设置窗体的引用并更新视窗向量
        /// </summary>
        /// <param name="wnd">主窗体的引用</param>
        public static void SetWindowReference(MainWindow wnd) => ViewManager.mWnd = wnd;

        /// <summary>
        /// 获取应用程序主窗体
        /// </summary>
        /// <returns>主窗体的引用</returns>
        public static MainWindow GetWindowReference() => ViewManager.mWnd;

        /// <summary>
        /// 获取主窗体的引用
        /// </summary>
        public static MainWindow mWnd { get; private set; } = null;
    }
}
