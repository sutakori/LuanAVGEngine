using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace LuanCore
{
    public class GlobalConfig
    {
        public static readonly string Section_Title = "title";
        public static string CONTEXT_SECTION = "context";
        public static readonly string Path_Section = @"Script\";
        public static readonly string Ext_Section = "lls";
        public static readonly string Path_Bg = @"Source\background\";
        public static readonly string Path_Bgm = @"Source\bgm\";
        public static readonly string Path_Bgs = @"Source\bgs\";
        public static readonly string Path_Vocal = @"Source\vocal\";
        public static readonly string Path_Stand = @"Source\stand\";
        public static readonly string Path_Asset = @"Source\asset\";
        public static string SAVE_PATH = @"SAVEPATH\";

        public static readonly string Path_Index = @"Script\index";
        public static readonly float GAME_BGM_VOLUME = 200;
        public static readonly float GAME_BGS_VOLUME = 200;
        public static readonly float GAME_VOCAL_VOLUME = 200;
        public static readonly string GAME_TITLE_NAME = "Luan";
        public static readonly double GAME_VIEWPORT_WIDTH = 1280;
        public static double GAME_VIEWPORT_ACTUALHEIGHT => GlobalConfig.GAME_WINDOW_HEIGHT + 30;
        public static readonly double GAME_WINDOW_WIDTH = 1280;
        public static readonly double GAME_WINDOW_HEIGHT = 720;
        public static readonly int GAME_Z_BACKGROUND = 0;
        public static readonly int GAME_Z_CHARACTERSTAND = 11;
        public static readonly int GAME_Z_PICTURES = 101;
        public static readonly int GAME_Z_MESSAGELAYER = 51;
        /// <summary>
        /// 图像层：左立绘X
        /// </summary>
        public static double GAME_CHARACTERSTAND_LEFT_X = GAME_WINDOW_WIDTH * 2/7;
        /// <summary>
        /// 图像层：左立绘Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_LEFT_Y = GAME_WINDOW_HEIGHT / 2;
        /// <summary>
        /// 图像层：左中立绘X
        /// </summary>
        public static double GAME_CHARACTERSTAND_MIDLEFT_X = GAME_WINDOW_WIDTH * 3 / 7;
        /// <summary>
        /// 图像层：左中立绘Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_MIDLEFT_Y = GAME_WINDOW_HEIGHT/2;
        /// <summary>
        /// 图像层：中立绘X
        /// </summary>
        public static double GAME_CHARACTERSTAND_MID_X = GAME_WINDOW_WIDTH * 4 / 7;
        /// <summary>
        /// 图像层：中立绘Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_MID_Y = GAME_WINDOW_HEIGHT/2;
        /// <summary>
        /// 图像层：右中立绘X
        /// </summary>
        public static double GAME_CHARACTERSTAND_MIDRIGHT_X = GAME_WINDOW_WIDTH * 5 / 7;
        /// <summary>
        /// 图像层：右中立绘Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_MIDRIGHT_Y = GAME_WINDOW_HEIGHT/2;
        /// <summary>
        /// 图像层：右立绘X
        /// </summary>
        public static double GAME_CHARACTERSTAND_RIGHT_X = GAME_WINDOW_WIDTH * 6 / 7;
        /// <summary>
        /// 图像层：右立绘Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_RIGHT_Y = GAME_WINDOW_HEIGHT/2;
        public static string GAME_DEFAULT_TEXTBOX = "textbox.png";
        public static readonly string GAME_DEFAULT_BRANCHBUTTON = "button.png";
        public static double GAME_OPACITY_TEXTBOX = 0.6;
        public static readonly string FirstViewPage = "Stage";
        public static readonly int EdenResourceCacheSize = 128;
        public static readonly PerformanceType GAME_PERFORMANCE_TYPE=PerformanceType.NoEffect;
        public static readonly string Index_End = "$$";

        /// <summary>
        /// 字体：字体名称
        /// </summary>
        public static string GAME_FONT_NAME = "黑体";
        /// <summary>
        /// 字体：颜色
        /// </summary>
        public static Color GAME_FONT_COLOR = Colors.Black;
        /// <summary>
        /// 字体：行距
        /// </summary>
        public static int GAME_FONT_LINEHEIGHT = 22;
        /// <summary>
        /// 字体：字号
        /// </summary>
        public static int GAME_FONT_FONTSIZE = 16;
        /// <summary>
        /// 文本展示：模式
        /// </summary>
        public static string GAME_MESSAGE_MODE = "Dialog";
        /// <summary>
        /// 文本层：文本层数量
        /// </summary>
        public static int GAME_MESSAGELAYER_COUNT = 2;
        /// <summary>
        /// 文本层：文本层默认宽度
        /// </summary>
        public static double GAME_MESSAGELAYER_W = GAME_WINDOW_WIDTH - 30;
        /// <summary>
        /// 文本层：文本层默认高度
        /// </summary>
        public static double GAME_MESSAGELAYER_H = 170;
        /// <summary>
        /// 文本层：文本层默认位置X
        /// </summary>
        public static double GAME_MESSAGELAYER_X = 15;
        /// <summary>
        /// 文本层：文本层默认位置Y
        /// </summary>
        public static double GAME_MESSAGELAYER_Y =
            GlobalConfig.GAME_WINDOW_HEIGHT - GlobalConfig.GAME_MESSAGELAYER_H/2;
        /// <summary>
        /// 文本层：文本层默认边距
        /// </summary>
        public static Thickness GAME_MESSAGELAYER_PADDING = new Thickness(60, 20, 60, 0);
        public static Thickness GAME_BRANCHBUTTON_PADDING = new Thickness((GAME_BRANCHBUTTON_H - GAME_BUTTON_FONT_FONTSIZE) / 2, 0,  0, 0);
        /// <summary>
        /// 文本层：对话小三角文件名
        /// </summary>
        public static string GAME_MESSAGELAYER_TRIA_FILENAME = "MessageTria.png";
        /// <summary>
        /// 文本层：对话背景文件名
        /// </summary>
        public static string GAME_MESSAGELAYER_BACKGROUNDFILENAME = "originMessageBox2.png";
        /// <summary>
        /// 文本层：对话文字投影
        /// </summary>
        public static bool GAME_MESSAGELAYER_SHADOW = false;
        /// <summary>
        /// 文字层：对话小三角X坐标
        /// </summary>
        public static double GAME_MESSAGELAYER_TRIA_X = 960;
        /// <summary>
        /// 文字层：对话小三角Y坐标
        /// </summary>
        public static double GAME_MESSAGELAYER_TRIA_Y = 530;
        /// <summary>
        /// 文本展示：是否打字模式
        /// </summary>
        public static bool GAME_MSG_ISTYPING = true;
        /// <summary>
        /// 文本展示：打字模式延迟
        /// </summary>
        public static int GAME_MSG_TYPING_DELAY = 60;
        /// <summary>
        /// 文本展示：打字模式过段延迟
        /// </summary>
        public static int GAME_MSG_PASSAGE_DELAY = 120;
        /// <summary>
        /// 文本展示：自动播放延时毫秒
        /// </summary>
        public static int GAME_MSG_AUTOPLAY_DELAY = 2000;
        /// <summary>
        /// 文本展示：是否已读快进
        /// </summary>
        public static bool GAME_MSG_SKIP = false;
        public static string GAME_BUTTON_FONT_NAME = "黑体";
        public static double GAME_BUTTON_FONT_FONTSIZE = 24;
        public static double GAME_BUTTON_FONT_LINEHEIGHT = 28;
        public static double GAME_BRANCHBUTTON_H = 40;
        public static double GAME_BRANCHBUTTON_W = GAME_WINDOW_WIDTH/10;
        public static string GAME_TEXTOVER_FILENAME = "textover.png";
        public static double GAME_TEXTOVER_X = GAME_WINDOW_WIDTH - 80;
        public static double GAME_TEXTOVER_Y = GAME_WINDOW_HEIGHT - 80;


        /// <summary>
        /// 枚举：特效类型
        /// </summary>
        public enum PerformanceType
        {
            /// <summary>
            /// 全特效
            /// </summary>
            HighQuality = 0,
            /// <summary>
            /// 减弱特效
            /// </summary>
            Weaken = 1,
            /// <summary>
            /// 无特效
            /// </summary>
            NoEffect = 2
        }
    }
}
