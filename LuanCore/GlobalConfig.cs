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
        /// ͼ��㣺������X
        /// </summary>
        public static double GAME_CHARACTERSTAND_LEFT_X = GAME_WINDOW_WIDTH * 2/7;
        /// <summary>
        /// ͼ��㣺������Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_LEFT_Y = GAME_WINDOW_HEIGHT / 2;
        /// <summary>
        /// ͼ��㣺��������X
        /// </summary>
        public static double GAME_CHARACTERSTAND_MIDLEFT_X = GAME_WINDOW_WIDTH * 3 / 7;
        /// <summary>
        /// ͼ��㣺��������Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_MIDLEFT_Y = GAME_WINDOW_HEIGHT/2;
        /// <summary>
        /// ͼ��㣺������X
        /// </summary>
        public static double GAME_CHARACTERSTAND_MID_X = GAME_WINDOW_WIDTH * 4 / 7;
        /// <summary>
        /// ͼ��㣺������Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_MID_Y = GAME_WINDOW_HEIGHT/2;
        /// <summary>
        /// ͼ��㣺��������X
        /// </summary>
        public static double GAME_CHARACTERSTAND_MIDRIGHT_X = GAME_WINDOW_WIDTH * 5 / 7;
        /// <summary>
        /// ͼ��㣺��������Y
        /// </summary>
        public static double GAME_CHARACTERSTAND_MIDRIGHT_Y = GAME_WINDOW_HEIGHT/2;
        /// <summary>
        /// ͼ��㣺������X
        /// </summary>
        public static double GAME_CHARACTERSTAND_RIGHT_X = GAME_WINDOW_WIDTH * 6 / 7;
        /// <summary>
        /// ͼ��㣺������Y
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
        /// ���壺��������
        /// </summary>
        public static string GAME_FONT_NAME = "����";
        /// <summary>
        /// ���壺��ɫ
        /// </summary>
        public static Color GAME_FONT_COLOR = Colors.Black;
        /// <summary>
        /// ���壺�о�
        /// </summary>
        public static int GAME_FONT_LINEHEIGHT = 22;
        /// <summary>
        /// ���壺�ֺ�
        /// </summary>
        public static int GAME_FONT_FONTSIZE = 16;
        /// <summary>
        /// �ı�չʾ��ģʽ
        /// </summary>
        public static string GAME_MESSAGE_MODE = "Dialog";
        /// <summary>
        /// �ı��㣺�ı�������
        /// </summary>
        public static int GAME_MESSAGELAYER_COUNT = 2;
        /// <summary>
        /// �ı��㣺�ı���Ĭ�Ͽ��
        /// </summary>
        public static double GAME_MESSAGELAYER_W = GAME_WINDOW_WIDTH - 30;
        /// <summary>
        /// �ı��㣺�ı���Ĭ�ϸ߶�
        /// </summary>
        public static double GAME_MESSAGELAYER_H = 170;
        /// <summary>
        /// �ı��㣺�ı���Ĭ��λ��X
        /// </summary>
        public static double GAME_MESSAGELAYER_X = 15;
        /// <summary>
        /// �ı��㣺�ı���Ĭ��λ��Y
        /// </summary>
        public static double GAME_MESSAGELAYER_Y =
            GlobalConfig.GAME_WINDOW_HEIGHT - GlobalConfig.GAME_MESSAGELAYER_H/2;
        /// <summary>
        /// �ı��㣺�ı���Ĭ�ϱ߾�
        /// </summary>
        public static Thickness GAME_MESSAGELAYER_PADDING = new Thickness(60, 20, 60, 0);
        public static Thickness GAME_BRANCHBUTTON_PADDING = new Thickness((GAME_BRANCHBUTTON_H - GAME_BUTTON_FONT_FONTSIZE) / 2, 0,  0, 0);
        /// <summary>
        /// �ı��㣺�Ի�С�����ļ���
        /// </summary>
        public static string GAME_MESSAGELAYER_TRIA_FILENAME = "MessageTria.png";
        /// <summary>
        /// �ı��㣺�Ի������ļ���
        /// </summary>
        public static string GAME_MESSAGELAYER_BACKGROUNDFILENAME = "originMessageBox2.png";
        /// <summary>
        /// �ı��㣺�Ի�����ͶӰ
        /// </summary>
        public static bool GAME_MESSAGELAYER_SHADOW = false;
        /// <summary>
        /// ���ֲ㣺�Ի�С����X����
        /// </summary>
        public static double GAME_MESSAGELAYER_TRIA_X = 960;
        /// <summary>
        /// ���ֲ㣺�Ի�С����Y����
        /// </summary>
        public static double GAME_MESSAGELAYER_TRIA_Y = 530;
        /// <summary>
        /// �ı�չʾ���Ƿ����ģʽ
        /// </summary>
        public static bool GAME_MSG_ISTYPING = true;
        /// <summary>
        /// �ı�չʾ������ģʽ�ӳ�
        /// </summary>
        public static int GAME_MSG_TYPING_DELAY = 60;
        /// <summary>
        /// �ı�չʾ������ģʽ�����ӳ�
        /// </summary>
        public static int GAME_MSG_PASSAGE_DELAY = 120;
        /// <summary>
        /// �ı�չʾ���Զ�������ʱ����
        /// </summary>
        public static int GAME_MSG_AUTOPLAY_DELAY = 2000;
        /// <summary>
        /// �ı�չʾ���Ƿ��Ѷ����
        /// </summary>
        public static bool GAME_MSG_SKIP = false;
        public static string GAME_BUTTON_FONT_NAME = "����";
        public static double GAME_BUTTON_FONT_FONTSIZE = 24;
        public static double GAME_BUTTON_FONT_LINEHEIGHT = 28;
        public static double GAME_BRANCHBUTTON_H = 40;
        public static double GAME_BRANCHBUTTON_W = GAME_WINDOW_WIDTH/10;
        public static string GAME_TEXTOVER_FILENAME = "textover.png";
        public static double GAME_TEXTOVER_X = GAME_WINDOW_WIDTH - 80;
        public static double GAME_TEXTOVER_Y = GAME_WINDOW_HEIGHT - 80;


        /// <summary>
        /// ö�٣���Ч����
        /// </summary>
        public enum PerformanceType
        {
            /// <summary>
            /// ȫ��Ч
            /// </summary>
            HighQuality = 0,
            /// <summary>
            /// ������Ч
            /// </summary>
            Weaken = 1,
            /// <summary>
            /// ����Ч
            /// </summary>
            NoEffect = 2
        }
    }
}
