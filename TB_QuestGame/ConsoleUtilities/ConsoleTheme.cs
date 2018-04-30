using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// static class to manage the console game theme
    /// </summary>
    public static class ConsoleTheme
    {
        //
        // splash screen colors
        //
        public static ConsoleColor SplashScreenBackgroundColor = ConsoleColor.DarkGray;
        public static ConsoleColor SplashScreenForegroundColor = ConsoleColor.DarkRed;

        //
        // main console window colors
        //
        public static ConsoleColor WindowBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor WindowForegroundColor = ConsoleColor.DarkCyan;

        //
        // console window header colors
        //
        public static ConsoleColor HeaderBackgroundColor = ConsoleColor.DarkRed;
        public static ConsoleColor HeaderForegroundColor = ConsoleColor.White;

        //
        // console window footer colors
        //
        public static ConsoleColor FooterBackgroundColor = ConsoleColor.DarkRed;
        public static ConsoleColor FooterForegroundColor = ConsoleColor.White;

        //
        // menu box colors
        //
        public static ConsoleColor MenuBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor MenuForegroundColor = ConsoleColor.DarkGray;
        public static ConsoleColor MenuBorderColor = ConsoleColor.DarkGray;

        //
        // message box colors
        //
        public static ConsoleColor MessageBoxBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor MessageBoxForegroundColor = ConsoleColor.DarkGray;
        public static ConsoleColor MessageBoxBorderColor = ConsoleColor.DarkGray;
        public static ConsoleColor MessageBoxHeaderBackgroundColor = ConsoleColor.DarkCyan;
        public static ConsoleColor MessageBoxHeaderForegroundColor = ConsoleColor.Yellow;
        
        //
        // input box colors
        //
        public static ConsoleColor InputBoxBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor InputBoxForegroundColor = ConsoleColor.DarkGray;
        public static ConsoleColor InputBoxErrorMessageForegroundColor = ConsoleColor.Red;
        public static ConsoleColor InputBoxBorderColor = ConsoleColor.DarkGray;
        public static ConsoleColor InputBoxHeaderBackgroundColor = ConsoleColor.DarkCyan;
        public static ConsoleColor InputBoxHeaderForegroundColor = ConsoleColor.Yellow;

        //
        // status box colors
        //
        public static ConsoleColor StatusBoxBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor StatusBoxForegroundColor = ConsoleColor.DarkGray;
        public static ConsoleColor StatusBoxBorderColor = ConsoleColor.DarkGray;
        public static ConsoleColor StatusBoxHeaderBackgroundColor = ConsoleColor.DarkRed;
        public static ConsoleColor StatusBoxHeaderForegroundColor = ConsoleColor.White;
    }
}
