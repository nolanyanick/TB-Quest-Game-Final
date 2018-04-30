using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// The ConsoleWindowHelper class provides extensions to the Console class to help 
    /// develop console-based applications
    /// </summary>
    static class ConsoleWindowHelper
    {
        public static int ConsoleWindowWidth { get; set; }
        public static int ConsoleWindowHeight { get; set; }
        public static ConsoleColor ConsoleWindowForeground { get; set; }
        public static ConsoleColor ConsoleWindowBackground { get; set; }
        public static ConsoleColor HeaderForeground { get; set; }
        public static ConsoleColor HeaderBackground { get; set; }

        public static void SetWidth(int width)
        {
            Console.WindowWidth = width;
        }

        public static void SetHeight(int height)
        {
            Console.WindowWidth = height;
        }
        
        /// <summary>
        /// initialize the console window to the default values
        /// </summary>
        /// <param name="consoleWindowWidth">console window width</param>
        /// <param name="consoleWindowHeight">console window height</param>
        /// <param name="consoleWindowForeground">console window text color</param>
        /// <param name="consoleWindowBackground">console window background color </param>
        public static void InitializeConsoleWindow()
        {
            //
            // set default values for console window from layout class
            //
            ConsoleWindowWidth = ConsoleLayout.WindowWidth;
            ConsoleWindowHeight = ConsoleLayout.WindowHeight;

            //
            // set default values for console window from theme class
            //
            ConsoleWindowForeground = ConsoleTheme.WindowForegroundColor;
            ConsoleWindowBackground = ConsoleTheme.WindowBackgroundColor;
            HeaderBackground = ConsoleTheme.HeaderBackgroundColor;
            HeaderForeground = ConsoleTheme.HeaderForegroundColor;

            //
            // set initial position of console window
            //
            Console.SetWindowPosition(ConsoleLayout.WindowPositionLeft, ConsoleLayout.WindowPositionTop);

            //
            // set console window to default values
            //
            ResetConsoleWindow();

        }

        public static void ResetConsoleWindow()
        {
            Console.WindowWidth = ConsoleWindowWidth;
            Console.WindowHeight = ConsoleWindowHeight;
            Console.ForegroundColor = ConsoleWindowForeground;
            Console.BackgroundColor = ConsoleWindowBackground;
            Console.Clear();
        }

        public static void InitializeHeader(
            List<string> headerText,
            ConsoleColor headerForeground,
            ConsoleColor headerBackground
            )
        {
            HeaderForeground = HeaderForeground;
            HeaderBackground = headerBackground;
            DisplayHeader(headerText);
        }

        /// <summary>
        /// place an box around an area in the console window
        /// </summary>
        /// <param name="topLeftRow">top left row position</param>
        /// <param name="topLeftColumn">top left column position</param>
        /// <param name="width">box width</param>
        /// <param name="height">box height</param>
        public static void DisplayBoxOutline(int topLeftRow, int topLeftColumn, int width, int height)
        {
            string topLeftCorner = "\u2554";     // ╔
            string topRightCorner = "\u2557";    // ╗
            string bottomLeftCorner = "\u255A";  // ╚
            string bottomRightCorner = "\u255D"; // ╝
            string horizontal = "\u2550";        // ═
            string vertical = "\u2551";          // ║

            Console.SetCursorPosition(topLeftColumn, topLeftRow);

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    Console.SetCursorPosition(topLeftColumn + column, topLeftRow + row);

                    // displaying top row
                    if (row == 0)
                    {
                        if (column == 0)
                        {
                            Console.Write(topLeftCorner);
                        }
                        else if (column == width - 1)
                        {
                            Console.Write(topRightCorner);
                        }
                        else
                        {
                            Console.WriteLine(horizontal);
                        }
                    }
                    // displaying bottom row
                    else if (row == height - 1)
                    {
                        if (column == 0)
                        {
                            Console.Write(bottomLeftCorner);
                        }
                        else if (column == width - 1)
                        {
                            Console.Write(bottomRightCorner);
                        }
                        else
                        {
                            Console.WriteLine(horizontal);
                        }
                    }
                    // displaying middle row
                    else
                    {
                        if (column == 0 || column == width - 1)
                        {
                            Console.Write(vertical);
                        }
                    }
                }
            }
        }


        public static void DisplayHeader(List<string> headerText)
        {
            List<string> formatedHeaderText = new List<string>();

            formatedHeaderText.Add(new String(' ', ConsoleLayout.WindowWidth));
            foreach (string lineOfText in headerText)
            {
                formatedHeaderText.Add(Center(lineOfText, ConsoleLayout.WindowWidth));
            }
            formatedHeaderText.Add(new String(' ', ConsoleLayout.WindowWidth));

            Console.BackgroundColor = ConsoleTheme.HeaderBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.HeaderForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.HeaderPositionLeft, ConsoleLayout.HeaderPositionTop);
            foreach (string lineOfText in formatedHeaderText)
            {
                Console.Write(lineOfText);
            }
        }


        public static void DisplayFooter(List<string> headerText)
        {
            List<string> formatedHeaderText = new List<string>();

            formatedHeaderText.Add(new String(' ', ConsoleLayout.WindowWidth));
            foreach (string lineOfText in headerText)
            {
                formatedHeaderText.Add(Center(lineOfText, ConsoleLayout.WindowWidth));
            }
            formatedHeaderText.Add(new String(' ', ConsoleLayout.WindowWidth));

            Console.BackgroundColor = ConsoleTheme.FooterBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.FooterForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.FooterPositionLeft, ConsoleLayout.FooterPositionTop);
            foreach (string lineOfText in formatedHeaderText)
            {
                Console.Write(lineOfText);
            }

            Console.SetCursorPosition(0, 0);
        }

        public static string Center(string text, int stringLength)
        {
            int leftPadding = (stringLength - text.Length) / 2 + text.Length;
            return text.PadLeft(leftPadding).PadRight(stringLength);
        }

        /// <summary>
        /// convert camelCase to all upper case and spaces
        /// reference URL - http://stackoverflow.com/questions/15458257/how-to-have-enum-values-with-spaces
        /// </summary>
        /// <param name="s"></param>
        /// <returns>converted string</returns>
        public static String ToLabelFormat(String s)
        {
            var newStr = Regex.Replace(s, "(?<=[A-Z])(?=[A-Z][a-z])", " ");
            newStr = Regex.Replace(newStr, "(?<=[^A-Z])(?=[A-Z])", " ");
            //newStr = Regex.Replace(newStr, "(?<=[A-Za-z])(?=[^A-Za-z])", " ");

            return newStr;
        }

        /// <summary>
        /// wraps text using a list of strings
        /// Original code from Mike Ward's website
        /// http://mike-ward.net/2009/07/19/word-wrap-in-a-console-app-c/
        /// Adapted to format for text boxes
        /// </summary>
        /// <param name="text">text to wrap</param>
        /// <param name="textAreaWidth">length of each line</param>
        /// <param name="leftMargin">left margin</param>
        /// <returns>list of lines as strings</returns>
        public static List<string> MessageBoxWordWrap(string text, int textAreaWidth)
        {
            var lines = new List<string>();

            string[] paragraphs = Regex.Split(text, "\n");

            foreach (string paragraph in paragraphs)
            {
                int start = 0, end;

                //
                // removes formatting characters
                //
                text = Regex.Replace(text, @"\s", " ").Trim();

                while ((end = start + textAreaWidth) < paragraph.Length)
                {
                    while (paragraph[end] != ' ' && end > start)
                        end -= 1;

                    if (end == start)
                        end = start + textAreaWidth;

                    string textLine = paragraph.Substring(start, end - start);

                    lines.Add(textLine);
                    start = end + 1;
                }

                if (start < paragraph.Length)
                    lines.Add(paragraph.Substring(start));
            }

            return lines;
        }
    }
}
