using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuanPlatform.Utils
{
    class LogUtils
    {
        public static void Log(string info, string causer, LogLevel logLevel)
        {
            Console.ResetColor();
            switch (logLevel)
            {
                case LogLevel.Error:
                    Console.ForegroundColor = Style[logLevel];
                    Console.WriteLine(@"[{0}] {1} {2}",
                        logLevel.ToString().ToUpper(),
                        DateTime.Now, causer);
                    Console.WriteLine(info);
                    MessageBox.Show(@"At: " + causer + Environment.NewLine + info, @"LuanERROR");
                    break;
                case LogLevel.Plain:
                    Console.ForegroundColor = Style[logLevel];
                    Console.WriteLine(info);
                    break;
                default:
                    Console.ForegroundColor = Style[logLevel];
                    Console.WriteLine(@"[{0}] {1} {2}",
                        logLevel.ToString().ToUpper(),
                        DateTime.Now, causer);
                    Console.WriteLine(info);
                    break;
            }
        }

        private static Dictionary<LogLevel, ConsoleColor> Style = new Dictionary<LogLevel, ConsoleColor>()
        {
            { LogLevel.Plain, ConsoleColor.Black },
            { LogLevel.Info, ConsoleColor.Green },
            { LogLevel.Debug, ConsoleColor.Blue },
            { LogLevel.Warning, ConsoleColor.Yellow},
            { LogLevel.Error, ConsoleColor.Red }
        };

    }

    enum LogLevel
    {
        Plain,
        Info,
        Debug,
        Warning,
        Error
    }

}
