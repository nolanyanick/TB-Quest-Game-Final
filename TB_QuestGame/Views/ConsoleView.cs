using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TB_QuestGame
{
    /// <summary>
    /// view class
    /// </summary>
    public class ConsoleView
    {
        #region ENUMS

        public enum ViewStatus
        {
            PrisonerInitialization,
            PlayingGame,            
        }

        #endregion

        #region FIELDS

        //
        // declare game objects for the ConsoleView object to use
        //
        Player _gamePrisoner;
        Universe _gameUniverse;

        ViewStatus _viewStatus;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView(Player gamePrisoner, Universe gameUniverse)
        {
            _gamePrisoner = gamePrisoner;
            _gameUniverse = gameUniverse;

            _viewStatus = ViewStatus.PrisonerInitialization;

            InitializeDisplay();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// display all of the elements on the game play screen on the console
        /// </summary>
        /// <param name="messageBoxHeaderText">message box header title</param>
        /// <param name="messageBoxText">message box text</param>
        /// <param name="menu">menu to use</param>
        /// <param name="inputBoxPrompt">input box text</param>
        public void DisplayGamePlayScreen(string messageBoxHeaderText, string messageBoxText,  Menu menu, string inputBoxPrompt)
        {
                //
                // reset screen to default window colors
                //
                Console.BackgroundColor = ConsoleTheme.WindowBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.WindowForegroundColor;
                Console.Clear();

                ConsoleWindowHelper.DisplayHeader(Text.HeaderText);
                ConsoleWindowHelper.DisplayFooter(Text.FooterText);

                DisplayMessageBox(messageBoxHeaderText, messageBoxText);
                DisplayMenuBox(menu);
                DisplayStatusBox();
                DisplayInputBox();
         }

        /// <summary>
        /// wait for any keystroke to continue
        /// </summary>
        public void GetContinueKey()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// get a action menu choice from the user
        /// </summary>
        /// <returns>action menu choice</returns>
        public PlayerAction GetActionMenuChoice(Menu menu)
        {
            PlayerAction choosenAction = PlayerAction.None;
            Console.CursorVisible = false;
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 100, ConsoleLayout.MenuBoxPositionTop + 22);


            //
            // an array of valid keys form the menu dicitonary
            //
            char[] validKeys = menu.MenuChoices.Keys.ToArray();

            //
            // validate key pressed in menu choices dictionary
            //
            char keyPressed;
            do
            {
                ConsoleKeyInfo keyPressedInfo = Console.ReadKey();
                keyPressed = keyPressedInfo.KeyChar;

                if (!validKeys.Contains(keyPressed))
                {
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 100, ConsoleLayout.MenuBoxPositionTop + 22);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
            } while (!validKeys.Contains(keyPressed));

            choosenAction = menu.MenuChoices[keyPressed];
            Console.CursorVisible = true;
            
            return choosenAction;
        }

        /// <summary>
        /// get a string value from the user
        /// </summary>
        /// <returns>string value</returns>
        public string GetString()
        {
            bool validResponse = false;
            string userResponse;

            while (!validResponse)
            {
                userResponse = Console.ReadLine().ToUpper();

                if (userResponse == "YES")
                {
                    validResponse = true;
                    return userResponse;
                }
                else if(userResponse == "NO")
                {
                    validResponse = true;
                    return userResponse;
                }
                else
                {
                        ClearInputBox();
                        DisplayInputErrorMessage($"You must enter 'Yes' or 'No'. Please try again.");
                        DisplayInputBoxPrompt("Would you like to continue this conversation? (Yes or No): ");                                                           
                }
            }

            Console.CursorVisible = false;

            return "";
        }

        /// <summary>
        /// get an integer value from the user
        /// </summary>
        /// <returns>integer value</returns>
        public bool GetInteger(string prompt, int minimumValue, int maximumValue, out int integerChoice)
        {
            bool validResponse = false;
            integerChoice = 0;

            //
            // validate on range if either mininumValue or maximumValue are not 0
            //
            bool validateRange = (minimumValue != 0 || maximumValue != 0);

            DisplayInputBoxPrompt(prompt);
            while (!validResponse)
            {
                if (int.TryParse(Console.ReadLine(), out integerChoice))
                {
                    if (validateRange)
                    {
                        if (integerChoice >= minimumValue && integerChoice <= maximumValue)
                        {
                            validResponse = true;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                            DisplayInputBoxPrompt(prompt);
                        }
                    }
                    else
                    {
                        validResponse = true;
                    }
                }
                else
                {
                    ClearInputBox();                    
                    DisplayInputErrorMessage($"You must enter a valid Id number. Please try again.                                          ");
                    DisplayInputBoxPrompt(prompt);
                }
            }

            DisplayInputErrorMessage("                                                                         ");

            Console.CursorVisible = false;

            return true;
        }

        /// <summary>
        /// get a character race value from the user
        /// </summary>
        /// <returns>character race value</returns>
        public Character.GenderType GetGender()
        {
            Character.GenderType genderType;
            string userResponse = Console.ReadLine().ToUpper();
            if (userResponse != "MALE" && userResponse != "FEMALE" && userResponse != "OTHER")
            {
                DisplayInputErrorMessage($"Invalid input. Your gender as been set to: '{Character.GenderType.OTHER}'. " +
               "You may change this at any time. Press any key to continue.");
                GetContinueKey();
            }
            if (!Enum.TryParse<Character.GenderType>(userResponse.ToUpper(), out genderType))
            {
                DisplayInputErrorMessage($"Invalid input. Your gender as been set to: '{Character.GenderType.OTHER}'. " +
                "You may change this at any time. Press any key to continue.");
                GetContinueKey();
            }      
            
            return genderType;
        }

        /// <summary>
        /// display splash screen
        /// </summary>
        /// <returns>player chooses to play</returns>
        public bool DisplaySpashScreen()
        {
            bool playing = true;
            ConsoleKeyInfo keyPressed;

            Console.BackgroundColor = ConsoleTheme.SplashScreenBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.SplashScreenForegroundColor;
            Console.Clear();
            Console.CursorVisible = false;


            Console.SetCursorPosition(0, 10);
            string tabSpace = new String(' ', 48);
            Console.WriteLine(tabSpace + @" _____ _            _____                                        ");
            Console.WriteLine(tabSpace + @"|_   _| |          |  __ \                                       ");
            Console.WriteLine(tabSpace + @"  | | | |__   ___  | |  | |_   _ ____   ___   ___  ___  ____     ");
            Console.WriteLine(tabSpace + @"  | | | '_ \ / _ \ | |  | | | | |  _ \ / _ \ / _ \/ _ \|  _ \    ");
            Console.WriteLine(tabSpace + @"  | | | | | |  __/ | |__| | \_/ | | | | (_) |  __/ (_) | | | |   ");
            Console.WriteLine(tabSpace + @"  \_/ |_| |_|\___| |_____/ \___/|_| |_|\__  /\___|\___/|_| |_|   ");
            Console.WriteLine(tabSpace + @"                                        _/ /                     ");
            Console.WriteLine(tabSpace + @"                                       |__/                      ");               

            Console.SetCursorPosition(80, 25);
            Console.Write("Press any key to continue or Esc to exit.");
            keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                playing = false;
            }
     
            return playing;
        }

        /// <summary>
        /// initialize the console window settings
        /// </summary>
        private static void InitializeDisplay()
        {
            //
            // control the console window properties
            //
            ConsoleWindowControl.DisableResize();
            ConsoleWindowControl.DisableMaximize();
            ConsoleWindowControl.DisableMinimize();
            Console.Title = "The Dungeon";

            //
            // set the default console window values
            //
            ConsoleWindowHelper.InitializeConsoleWindow();

            Console.CursorVisible = false;            
        }

        /// <summary>
        /// display the correct menu in the menu box of the game screen
        /// </summary>
        /// <param name="menu">menu for current game state</param>
        private void DisplayMenuBox(Menu menu)
        {
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuBorderColor;

            //
            // display menu box border
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MenuBoxPositionTop,
                ConsoleLayout.MenuBoxPositionLeft,
                ConsoleLayout.MenuBoxWidth,
                ConsoleLayout.MenuBoxHeight);

            //
            // display menu box header
            //
            Console.BackgroundColor = ConsoleTheme.MenuBorderColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(menu.MenuTitle, ConsoleLayout.MenuBoxWidth - 4));

            //
            // display menu choices
            //
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
            int topRow = ConsoleLayout.MenuBoxPositionTop + 3;

            foreach (KeyValuePair<char, PlayerAction> menuChoice in menu.MenuChoices)
            {
                if (menuChoice.Value != PlayerAction.None)
                {
                    string formatedMenuChoice = ConsoleWindowHelper.ToLabelFormat(menuChoice.Value.ToString());
                    Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 3, topRow++);
                    Console.Write($"{menuChoice.Key}. {formatedMenuChoice}");
                }
            }
        }

        /// <summary>
        /// display the text in the message box of the game screen
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="messageText"></param>
        private void DisplayMessageBox(string headerText, string messageText)
        {
            //
            // display the outline for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxBorderColor;
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MessageBoxPositionTop,
                ConsoleLayout.MessageBoxPositionLeft,
                ConsoleLayout.MessageBoxWidth,
                ConsoleLayout.MessageBoxHeight);

            //
            // display message box header
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBorderColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MessageBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(headerText, ConsoleLayout.MessageBoxWidth - 4));

            //
            // display the text for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxForegroundColor;
            List<string> messageTextLines = new List<string>();
            messageTextLines = ConsoleWindowHelper.MessageBoxWordWrap(messageText, ConsoleLayout.MessageBoxWidth - 4);

            int startingRow = ConsoleLayout.MessageBoxPositionTop + 3;
            int endingRow = startingRow + messageTextLines.Count();
            int row = startingRow;
            foreach (string messageTextLine in messageTextLines)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, row);
                Console.Write(messageTextLine);
                row++;
            }            
        }

        /// <summary>
        /// draw the status box on the game screen
        /// </summary>
        public void DisplayStatusBox()
        {         
            Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.StatusBoxBorderColor;

            //
            // display the outline for the status box
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.StatusBoxPositionTop,
                ConsoleLayout.StatusBoxPositionLeft,
                ConsoleLayout.StatusBoxWidth,
                ConsoleLayout.StatusBoxHeight);

            //
            // display the text for the status box if playing game
            //
            if (_viewStatus == ViewStatus.PlayingGame)
            {
                //
                // display status box header with title
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("Game Stats", ConsoleLayout.StatusBoxWidth - 4));
                Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;

                //
                // display stats
                //
                int startingRow = ConsoleLayout.StatusBoxPositionTop + 3;
                int row = startingRow;
                foreach (string statusTextLine in Text.StatusBox(_gamePrisoner))
                {
                    Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 3, row);
                    Console.Write(statusTextLine);
                    row++;
                }
            }
            else
            {
                //
                // display status box header without header
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("", ConsoleLayout.StatusBoxWidth - 4));
                Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
            }
        }

        /// <summary>
        /// draw the input box on the game screen
        /// </summary>
        public void DisplayInputBox()
        {
            Console.BackgroundColor = ConsoleTheme.InputBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.InputBoxBorderColor;

            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.InputBoxPositionTop,
                ConsoleLayout.InputBoxPositionLeft,
                ConsoleLayout.InputBoxWidth,
                ConsoleLayout.InputBoxHeight);
        }

        /// <summary>
        /// display the prompt in the input box of the game screen
        /// </summary>
        /// <param name="prompt"></param>
        public void DisplayInputBoxPrompt(string prompt)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 1);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.Write(prompt);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the error message in the input box of the game screen
        /// </summary>
        /// <param name="errorMessage">error message text</param>
        public void DisplayInputErrorMessage(string errorMessage)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 2);
            Console.ForegroundColor = ConsoleTheme.InputBoxErrorMessageForegroundColor;
            Console.Write(errorMessage);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.CursorVisible = true;
        }

        /// <summary>
        /// clear the input box
        /// </summary>
        private void ClearInputBox()
        {
            string backgroundColorString = new String(' ', ConsoleLayout.InputBoxWidth - 4);

            Console.ForegroundColor = ConsoleTheme.InputBoxBackgroundColor;
            for (int row = 1; row < ConsoleLayout.InputBoxHeight -2; row++)
            {
                Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + row);
                DisplayInputBoxPrompt(backgroundColorString);
            }
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
        }

        /// <summary>
        /// clear the menu box
        /// </summary>
        private void ClearMenuBox()
        {
            string backgroundColorString = new String(' ', ConsoleLayout.MenuBoxWidth - 4);

            Console.ForegroundColor = ConsoleTheme.MenuBackgroundColor;
            for (int row = 1; row < ConsoleLayout.MenuBoxHeight; row++)
            { 
                Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 3, ConsoleLayout.MenuBoxPositionTop + row);
                Console.Write(backgroundColorString);
            }
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
        }

        /// <summary>
        /// clear the status box
        /// </summary>
        public void ClearStatusBox()
        {            
            string backgroundColorString = new String(' ', ConsoleLayout.StatusBoxWidth - 4);

            Console.ForegroundColor = ConsoleTheme.StatusBoxBackgroundColor;
            for (int row = 2; row < ConsoleLayout.StatusBoxHeight - 1; row++)
            {
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 3, ConsoleLayout.StatusBoxPositionTop + row);
                Console.Write(backgroundColorString);
            }
            Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;

            //
            // display status box header without header
            //
            Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
            Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center("", ConsoleLayout.StatusBoxWidth - 4));
            Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
        }


        #region ----- display colored text -----

        /// <summary>
        /// displays specific info in a different color
        /// </summary>        
        public void DisplayColoredText(string information, PlayerAction choosenAction, RoomLocation location)
        {
            int cursorPosition;
            Console.CursorVisible = false;

            switch (choosenAction)
            {
                //
                // DEAFULT PAGE
                //
                case PlayerAction.ReturnToMainMenu:
                    //-----NAME-----//
                    information = location.CommonName;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 20, ConsoleLayout.MenuBoxPositionTop + 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    //-----COORDIANTES-----//
                    information = location.Coordinates;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 15, ConsoleLayout.MenuBoxPositionTop + 4);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(information);
                    break;

                case PlayerAction.None:
                    #region ***COLORS FOR MISC/OTHER SCREENS



                    _viewStatus = ViewStatus.PlayingGame;

                    #region ---echo info screen---

                    if (_gamePrisoner.Name != null)
                    {
                        //----NAME----//
                        information = _gamePrisoner.Name;
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 22, ConsoleLayout.MenuBoxPositionTop + 7);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(information);

                        //----AGE----//
                        information = _gamePrisoner.Age.ToString();
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 21, ConsoleLayout.MenuBoxPositionTop + 8);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(information);

                        //----GENDER----//
                        information = Text.UppercaseFirst(_gamePrisoner.Gender.ToString());
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 24, ConsoleLayout.MenuBoxPositionTop + 9);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(information);

                        //----PERSONALITY----//
                        information = _gamePrisoner.PersonlityDescription();
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 29, ConsoleLayout.MenuBoxPositionTop + 10);
                        if (_gamePrisoner.Personality)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(information);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(information);
                        }

                        break;
                    }

                    #endregion

                    #region ---random name screen---

                    if (_gamePrisoner.Name == null)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 15, ConsoleLayout.MenuBoxPositionTop + 3);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(information);
                        break;
                    }

                    #endregion                

                    #endregion
                    break;

                case PlayerAction.LookAround:
                    #region ***COLORS FOR LOOK AROUND/CURRENT LOCATION SCREENS                  

                    //-----NAME-----//
                    information = location.CommonName;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 20, ConsoleLayout.MenuBoxPositionTop + 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    //-----COORDIANTES-----//
                    information = location.Coordinates;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 15, ConsoleLayout.MenuBoxPositionTop + 4);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(information);

                    #endregion
                    break;

                case PlayerAction.Travel:
                    #region ***COLORS FOR TRAVEL SCREEN

                    //----ID----// 
                    cursorPosition = 8;
                    foreach (RoomLocation room in _gameUniverse.RoomLocations)
                    {
                        if (room.RoomLocationID != _gamePrisoner.RoomLocationId)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(room.RoomLocationID);
                        }
                    }

                    //----NAME----//      
                    cursorPosition = 8;
                    foreach (RoomLocation room in _gameUniverse.RoomLocations)
                    {
                        if (room.RoomLocationID != _gamePrisoner.RoomLocationId)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(room.CommonName);
                        }
                    }

                    //----ACCESSIBILITY----//      
                    cursorPosition = 8;
                    foreach (RoomLocation room in _gameUniverse.RoomLocations)
                    {
                        if (room.RoomLocationID != _gamePrisoner.RoomLocationId)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            if (room.Accessible)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write(room.Accessible);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write(room.Accessible);
                            }
                        }
                    }

                    #endregion
                    break;

                case PlayerAction.PlayerInfo:
                    #region ***COLORS FOR PLAYER INFO SCREEN

                    //----NAME----//
                    information = _gamePrisoner.Name;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 22, ConsoleLayout.MenuBoxPositionTop + 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    //----AGE----//
                    information = _gamePrisoner.Age.ToString();
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 21, ConsoleLayout.MenuBoxPositionTop + 4);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    //----GENDER----//
                    information = Text.UppercaseFirst(_gamePrisoner.Gender.ToString());
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 24, ConsoleLayout.MenuBoxPositionTop + 5);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    //----WAEPON----//                    
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 26, ConsoleLayout.MenuBoxPositionTop + 6);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(_gamePrisoner.Weapon.Name);

                    //----DAMAGE----//                    
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 22, ConsoleLayout.MenuBoxPositionTop + 7);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(_gamePrisoner.DamageOutput);

                    //----EXP----//                    
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 26, ConsoleLayout.MenuBoxPositionTop + 8);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(_gamePrisoner.ExperiencePoints);


                    #endregion
                    break;

                case PlayerAction.EditPlayerInfo:
                    #region ***COLORS FOR EDIT INFO SCREEN

                    //-----NAME1-----//
                    information = _gamePrisoner.Name;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 34, ConsoleLayout.MenuBoxPositionTop + 7);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    //-----NAME2-----//
                    information = _gamePrisoner.Name;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 22, ConsoleLayout.MenuBoxPositionTop + 9);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    //-----AGE-----//
                    information = _gamePrisoner.Age.ToString();
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 21, ConsoleLayout.MenuBoxPositionTop + 10);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    //-----GENDER-----//
                    information = Text.UppercaseFirst(_gamePrisoner.Gender.ToString());
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 24, ConsoleLayout.MenuBoxPositionTop + 11);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(information);

                    #endregion                    
                    break;

                case PlayerAction.LocationsVisited:
                    #region ***COLORS FOR LOCATIONS VISITED SCREEN

                    List<RoomLocation> visitedRoomLocations = new List<RoomLocation>();

                    // add visited locations to a list
                    foreach (int roomLocationID in _gamePrisoner.RoomLocationsVisited)
                    {
                        visitedRoomLocations.Add(_gameUniverse.GetRoomLocationById(roomLocationID));
                    }

                    //----ID----// 
                    cursorPosition = 7;
                    foreach (RoomLocation room in visitedRoomLocations)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(room.RoomLocationID);
                    }

                    //----NAME----//      
                    cursorPosition = 7;
                    foreach (RoomLocation room in visitedRoomLocations)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(room.CommonName);
                    }

                    #endregion
                    break;

                case PlayerAction.ListDestinations:
                    #region ***COLORS FOR LIST DESTINATIONS SCREEN

                    //----ID----// 
                    cursorPosition = 7;
                    foreach (RoomLocation room in _gameUniverse.RoomLocations)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(room.RoomLocationID);
                    }

                    //----NAME----//      
                    cursorPosition = 7;
                    foreach (RoomLocation room in _gameUniverse.RoomLocations)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(room.CommonName);
                    }

                    #endregion
                    break;

                case PlayerAction.Exit:
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// displays game objects in color
        /// </summary>
        public void DisplayColoredObjects(PlayerAction choosenAction, int id)
        {
            int cursorPosition;
            int indexer = 0;
            List<GameObject> gameObjectsInCurrentRoomLocation = _gameUniverse.GetGameObjectsByRoomLocaitonId(_gamePrisoner.RoomLocationId);

            switch (choosenAction)
            {
                case PlayerAction.LookAt:
                    #region ***COLORS FOR LOOK AT SCREEN

                    if (id != 999)
                    {
                        //-----NAME-----//
                        cursorPosition = 7;
                        foreach (var gameObject in gameObjectsInCurrentRoomLocation)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(gameObject.Name);
                        }

                        //-----ID-----//
                        cursorPosition = 7;
                        foreach (var gameObject in gameObjectsInCurrentRoomLocation)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(gameObject.Id);
                        }
                    }
                    else
                    {

                    }
                                                 

                    #region --- display 'no objects in area' notice ---

                    if (_gamePrisoner.IndividualGameObject == 0 && gameObjectsInCurrentRoomLocation.Count == 0)
                    {
                        //-----NOTICE-----//                   
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 3);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("It appears there are no game objects here!");
                    }

                    #endregion

                    #endregion
                    break;

                case PlayerAction.PickUp:
                    #region ***COLORS FOR PICK UP SCREEN

                    //-----NAME-----//
                    cursorPosition = 7;
                    foreach (var gameObject in gameObjectsInCurrentRoomLocation)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(gameObject.Name);
                    }

                    //-----ID-----//
                    cursorPosition = 7;
                    foreach (var gameObject in gameObjectsInCurrentRoomLocation)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(gameObject.Id);
                    }

                    #region --- display 'no objects in area' notice ---

                    if (_gamePrisoner.IndividualGameObject == 0 && gameObjectsInCurrentRoomLocation.Count == 0)
                    {
                        //-----NOTICE-----//                   
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 3);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("It appears there are no game objects here!");
                    }

                    #endregion

                    #endregion
                    break;

                case PlayerAction.PutDown:
                    #region ***COLORS FOR PUT DOWN SCREEN

                    //-----NAME-----//
                    cursorPosition = 7;
                    foreach (var gameObject in _gamePrisoner.Inventory)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(gameObject.Name);
                    }

                    //-----ID-----//
                    cursorPosition = 7;
                    foreach (var gameObject in _gamePrisoner.Inventory)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(gameObject.Id);
                    }

                    #region --- display 'no objects in area' notice ---

                    if (_gamePrisoner.IndividualGameObject == 0 && gameObjectsInCurrentRoomLocation.Count == 0)
                    {
                        //-----NOTICE-----//                   
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 3);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("It appears there are no game objects here!");
                    }

                    #endregion

                    #endregion
                    break;
                case PlayerAction.LookAround:
                    #region ***COLORS FOR LOOK AROUND OBJECTS

                    //-----NAME-----//
                    cursorPosition = 14;
                    foreach (var gameObject in gameObjectsInCurrentRoomLocation)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(gameObject.Name);
                    }

                    //-----ID-----//
                    cursorPosition = 14;
                    foreach (var gameObject in gameObjectsInCurrentRoomLocation)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(gameObject.Id);
                    }

                    //-----GAME OBJECTS-----//                    
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 10);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Game Objects");

                    #endregion
                    break;

                case PlayerAction.TreasureInventory:
                    #region ***COLORS FOR TREASURE INVENTORY

                    //-----NAME-----//
                    cursorPosition = 5;
                    foreach (var gameObject in _gamePrisoner.TreasureInventory)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(gameObject.Name);
                    }

                    //-----ID-----//
                    cursorPosition = 5;
                    foreach (var gameObject in _gamePrisoner.TreasureInventory)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(gameObject.Id);
                    }

                    //-----RARITY
                    cursorPosition = 5;
                    foreach (var gameObject in _gamePrisoner.TreasureInventory)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);

                        if (gameObject.Rarity == Treasure.RarityLevel.Common)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(gameObject.Rarity);
                        }
                        else if (gameObject.Rarity == Treasure.RarityLevel.Unique)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(gameObject.Rarity);
                        }
                        else if (gameObject.Rarity == Treasure.RarityLevel.Rare)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(gameObject.Rarity);
                        }
                        else if (gameObject.Rarity == Treasure.RarityLevel.Legendary)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(gameObject.Rarity);
                        }
                    }

                    #endregion
                    break;

                case PlayerAction.MedicineInventory:
                    #region ***COLORS FOR MEDICINE INVENTORY

                    //-----NAME-----//
                    cursorPosition = 5;

                    foreach (var medicine in _gamePrisoner.MedicinePouch)
                    {
                        if (medicine.HealthPoints == 20)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, 
                            ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write($"{medicine.Name}({indexer})");
                        }
                    }
                    indexer = 0;
                    cursorPosition++;
                    foreach (var medicine in _gamePrisoner.MedicinePouch)
                    {
                        if (medicine.HealthPoints == 40)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write($"{medicine.Name}({indexer})");
                        }
                    }
                    indexer = 0;
                    cursorPosition++;
                    foreach (var medicine in _gamePrisoner.MedicinePouch)
                    {
                        if (medicine.HealthPoints == 60)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write($"{medicine.Name}({indexer})");
                        }
                    }
                    indexer = 0;
                    cursorPosition++;
                    foreach (var medicine in _gamePrisoner.MedicinePouch)
                    {
                        if (medicine.HealthPoints == 80)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write($"{medicine.Name}({indexer})");
                        }
                    }
                    indexer = 0;
                    cursorPosition++;
                    foreach (var medicine in _gamePrisoner.MedicinePouch)
                    {
                        if (medicine.HealthPoints == 100)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write($"{medicine.Name}({indexer})");
                        }
                    }

                    //-----ID-----//
                    cursorPosition = 5;                  
                    foreach (var medicine in _gamePrisoner.MedicinePouch)
                    {                                            
                        if (medicine.HealthPoints == 20)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 5);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(medicine.Id);
                        }
                        else if (medicine.HealthPoints == 40)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 6);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(medicine.Id);
                        }
                        else if (medicine.HealthPoints == 60)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 7);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(medicine.Id);
                        }
                        else if (medicine.HealthPoints == 80)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 8);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(medicine.Id);
                        }
                        else if (medicine.HealthPoints == 100)
                        {
                            indexer++;
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 9);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(medicine.Id);
                        }
                    }

                    //-----HEALING POWER-----//
                    cursorPosition = 5;                    
                    foreach (var gameObject in _gamePrisoner.MedicinePouch)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);

                        if (gameObject.HealthPoints == 20)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + 5);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(gameObject.HealthPoints);
                        }
                        else if (gameObject.HealthPoints == 40)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + 6);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(gameObject.HealthPoints);
                        }
                        else if (gameObject.HealthPoints == 60)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + 7);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(gameObject.HealthPoints);
                        }
                        else if (gameObject.HealthPoints == 80)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + 8);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(gameObject.HealthPoints);
                        }
                        else if (gameObject.HealthPoints == 100)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + 9);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(gameObject.HealthPoints);
                        }
                    }

                    #endregion
                    break;

                case PlayerAction.Inventory:
                    #region ***COLORS FOR INVENTORY

                    //-----NAME-----//
                    cursorPosition = 5;
                    foreach (var gameObject in _gamePrisoner.Inventory)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(gameObject.Name);
                    }

                    //-----ID-----//
                    cursorPosition = 5;
                    foreach (var gameObject in _gamePrisoner.Inventory)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(gameObject.Id);
                    }

                    //-----TYPE-----//
                    cursorPosition = 5;
                    foreach (var gameObject in _gamePrisoner.Inventory)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);

                        if (gameObject.Type == GameObject.ObjectType.Food)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(gameObject.Type);
                        }
                        else if (gameObject.Type == GameObject.ObjectType.Weapon)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(gameObject.Type);
                        }
                        else if (gameObject.Type == GameObject.ObjectType.Key)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(gameObject.Type);
                        }
                    }

                    #endregion
                    break;

                case PlayerAction.ListGameObjects:
                    #region ***COLORS FOR LIST GAME OBJECTS SCREEN
                    // hide cursor
                    Console.CursorVisible = false;
                    
                    // list for objects to go on next page
                    List<GameObject> objectsOnNextPage = new List<GameObject>();
                        
                    #region --- medicine ---

                    //-----NAME, ID, LOCATION-----//
                    cursorPosition = 7;
                    indexer = 0;
                    foreach (var gameObject in _gameUniverse.GameObjects)
                    {
                        if (gameObject is Medicine)
                        {
                            Medicine medicine = gameObject as Medicine;

                            if (indexer <= 0)
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(medicine.Id);

                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write(medicine.RoomLocationId);
                            }

                            if (medicine.HealthPoints == 20)
                            {
                                indexer++;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write($"{medicine.Name}({indexer})");
                            }
                        }
                    }
                    indexer = 0;
                    cursorPosition++;
                    foreach (var gameObject in _gameUniverse.GameObjects)
                    {
                        if (gameObject is Medicine)
                        {
                            Medicine medicine = gameObject as Medicine;

                            if (indexer <= 0)
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(medicine.Id);

                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write(medicine.RoomLocationId);
                            }

                            if (medicine.HealthPoints == 40)
                            {
                                indexer++;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write($"{medicine.Name}({indexer})");
                            }
                        }
                    }
                    indexer = 0;
                    cursorPosition++;
                    foreach (var gameObject in _gameUniverse.GameObjects)
                    {
                        if (gameObject is Medicine)
                        {
                            Medicine medicine = gameObject as Medicine;

                            if (indexer <= 0)
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(medicine.Id);

                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write(medicine.RoomLocationId);
                            }

                            if (medicine.HealthPoints == 60)
                            {
                                indexer++;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write($"{medicine.Name}({indexer})");
                            }
                        }
                    }
                    indexer = 0;
                    cursorPosition++;
                    foreach (var gameObject in _gameUniverse.GameObjects)
                    {
                        if (gameObject is Medicine)
                        {
                            Medicine medicine = gameObject as Medicine;

                            if (indexer <= 0)
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(medicine.Id);

                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write(medicine.RoomLocationId);
                            }

                            if (medicine.HealthPoints == 80)
                            {
                                indexer++;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write($"{medicine.Name}({indexer})");
                            }
                        }
                    }
                    indexer = 0;
                    cursorPosition++;
                    foreach (var gameObject in _gameUniverse.GameObjects)
                    {
                        if (gameObject is Medicine)
                        {
                            Medicine medicine = gameObject as Medicine;

                            if (indexer <= 0)
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(medicine.Id);

                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write(medicine.RoomLocationId);
                            }

                            if (medicine.HealthPoints == 100)
                            {
                                indexer++;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write($"{medicine.Name}({indexer})");
                            }
                        }
                    }

                    #endregion

                    //----ID----// 
                    cursorPosition = 12;
                    foreach (GameObject allGameObjects in _gameUniverse.GameObjects)
                    {
                        if (allGameObjects is Medicine)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("");
                        }
                        else if (cursorPosition > 23)
                        {
                            objectsOnNextPage.Add(allGameObjects);
                        }
                        else
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(allGameObjects.Id);
                        }
                    }

                    //----NAME----//      
                    cursorPosition = 12;
                    foreach (GameObject allGameObjects in _gameUniverse.GameObjects)
                    {
                        #region --- reset cursor position if it extends beyond message box ---

                        if (cursorPosition > 23)
                        {
                            break;
                        }

                        #endregion

                        if (allGameObjects is Medicine)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("");
                        }
                        else
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(allGameObjects.Name);
                        }
                    }

                    //----ROOM LOCATION----//      
                    cursorPosition = 12;
                    foreach (GameObject allGameObjects in _gameUniverse.GameObjects)
                    {
                        #region --- reset cursor position if it extends beyond message box ---

                        if (cursorPosition > 23)
                        {
                            break;
                        }

                        #endregion

                        if (allGameObjects is Medicine)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("");
                        }
                        else
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(allGameObjects.RoomLocationId);
                        }
                    }

                    #endregion

                    #region ***COLORS FOR LIST GAME OBJECTS SCREEN PT. 2

                    GetContinueKey();
                    DisplayGamePlayScreen("List: Game Obejcts - Continued", Text.ListAllGameObjectsPageTwo(), ActionMenu.AdminMenu, "");

                    //----ID----// 
                    cursorPosition = 7;
                    foreach (GameObject allGameObjects in objectsOnNextPage)
                    {
                        #region --- reset cursor position if it extends beyond message box ---

                        if (cursorPosition > 23)
                        {
                            objectsOnNextPage.Add(allGameObjects);
                            break;
                        }

                        #endregion

                        if (allGameObjects is Medicine)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("");
                        }
                        else
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(allGameObjects.Id);
                        }
                    }

                    //----NAME----//      
                    cursorPosition = 7;
                    foreach (GameObject allGameObjects in objectsOnNextPage)
                    {
                        #region --- reset cursor position if it extends beyond message box ---

                        if (cursorPosition > 23)
                        {
                            break;
                        }

                        #endregion

                        if (allGameObjects is Medicine)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("");
                        }
                        else
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(allGameObjects.Name);
                        }
                    }

                    //----ROOM LOCATION----//      
                    cursorPosition = 7;
                    foreach (GameObject allGameObjects in objectsOnNextPage)
                    {
                        #region --- reset cursor position if it extends beyond message box ---

                        if (cursorPosition > 23)
                        {
                            break;
                        }

                        #endregion

                        if (allGameObjects is Medicine)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("");
                        }
                        else
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(allGameObjects.RoomLocationId);
                        }
                    }

                    #endregion
                    break;
                    
                case PlayerAction.TradeWith:
                    #region ***COLORS FOR TRADE WITH SCREENS
                    if (id != 999)
                    {
                        //----ID----// 
                        cursorPosition = 10;
                        foreach (GameObject allGameObjects in _gameUniverse.GameObjects)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(allGameObjects.Id);
                        }

                        //----NAME----//      
                        cursorPosition = 10;
                        foreach (GameObject allGameObjects in _gameUniverse.GameObjects)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(allGameObjects.Name);
                        }
                    }
                    else
                    {

                    }
                    #endregion
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// displays NPCs in color
        /// </summary>
        public void DisplayColoredNpcs(PlayerAction choosenAction, Npc npc)
        {
            int cursorPosition;
            
            //-----list of Civillians-----//
            List<Npc> npcsInCurrentLocation = _gameUniverse.GetNpcsByRoomLocation(_gamePrisoner.RoomLocationId);

            //-----list of Traders-----//
            List<Trader> tradersInCurrentLocation = _gameUniverse.GetTraderNpcsByRoomLocation(_gamePrisoner.RoomLocationId);

            //-----instantiate Trader object for specific use-----//
            Trader trader = npc as Trader;

            switch (choosenAction)
            {
                case PlayerAction.ListNpcs:
                    #region ***COLORS FOR LIST NPCS SCREEN

                    //----ID----// 
                    cursorPosition = 7;
                    foreach (Npc allNpcs in _gameUniverse.Npcs)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(allNpcs.Id);
                    }

                    //----NAME----//      
                    cursorPosition = 7;
                    foreach (Npc allNpcs in _gameUniverse.Npcs)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(allNpcs.Name);
                    }

                    //----ROOM LOCATION----//      
                    cursorPosition = 7;
                    foreach (Npc allNpcs in _gameUniverse.Npcs)
                    {
                        if (allNpcs is Monster)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write("N/A");
                        }
                        else
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 42, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(allNpcs.RoomLocationId);
                        }
                    }

                    #endregion
                    break;

                case PlayerAction.LookAround:
                    #region ***COLORS FOR LOOK AROUND: NPCS

                    //-----NAME-----//
                    cursorPosition = 14;
                    foreach (var specificNpc in npcsInCurrentLocation)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 50, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(specificNpc.Name);
                    }

                    //-----ID-----//
                    cursorPosition = 14;
                    foreach (var specificNpc in npcsInCurrentLocation)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 40, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(specificNpc.Id);
                    }

                    //-----HEADER-----//                    
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 40, ConsoleLayout.MenuBoxPositionTop + 10);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("NPCs");

                    //-----TABLE-----//
                    cursorPosition = 12;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 40, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("ID".PadRight(10) + "Name".PadRight(30) + " \n");

                    //-----TABLE DECORATION-----//  
                    cursorPosition = 13;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 40, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("---".PadRight(10) + "------------------".PadRight(30) + " \n");

                    #endregion
                    break;

                case PlayerAction.TalkTo:
                    #region ***COLORS FOR TALK TO

                    // temporary list of NPCs used to remove NPCs that have been spoken to
                    List<Npc> tempNpcList = _gameUniverse.GetNpcsByRoomLocation(_gamePrisoner.RoomLocationId);

                    foreach (Npc specificNpc in tempNpcList)
                    {
                        if (specificNpc.DialogueExhausted == true)
                        {
                            if (specificNpc.Name == "Gilbert the Brave")
                            {
                                
                            }
                            else
                            {
                                npcsInCurrentLocation.Remove(specificNpc);
                            }
                        }
                    }

                    //-----NAME-----//
                    cursorPosition = 7;
                    foreach (var specificNpc in npcsInCurrentLocation)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(specificNpc.Name);
                    }

                    //-----ID-----//
                    cursorPosition = 7;
                    foreach (var specificNpc in npcsInCurrentLocation)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(specificNpc.Id);
                    }

                    #endregion
                    break;

                case PlayerAction.TradeWith:
                    #region ***COLORS FOR TRADE WITH
                    #region --- display colors for list of Traders ---

                    if (_gamePrisoner.IndividualGameObject != 99)
                    {
                        //-----NAME-----//
                        cursorPosition = 7;
                        foreach (var specificNpc in tradersInCurrentLocation)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(specificNpc.Name);
                        }

                        //-----ID-----//
                        cursorPosition = 7;
                        foreach (var specificNpc in tradersInCurrentLocation)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(specificNpc.Id);
                        }
                    }

                    #endregion

                    #region --- display colors for NPC's inventory ---                    

                    if (_gamePrisoner.IndividualGameObject == 99)
                    {
                        //-----NPC'S NAME-----//
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 3);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{trader.Name}'s Inventory");


                        //-----NPC'S INVENTORY: IDs-----//
                        cursorPosition = 7;
                        foreach (GameObject objectsInNpcInventory in trader.Inventory)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(objectsInNpcInventory.Id);
                        }

                        //-----NPC'S INVENTORY: OBJECTS-----//
                        cursorPosition = 7;
                        foreach (GameObject objectsInNpcInventory in trader.Inventory)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 12, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(objectsInNpcInventory.Name);
                        }
                        _gamePrisoner.IndividualGameObject = 0;
                    }

                    #endregion

                    #endregion
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region ----- get initial player info -----

        /// <summary>
        /// get the player's initial information at the beginning of the game
        /// </summary>
        /// <returns>traveler object with all properties updated</returns>
        public Player GetInitialPrisonerInfo()
        {
            Player prisoner = new Player();

            //
            // intro
            //
            DisplayGamePlayScreen("Quest Setup", Text.InitializeMissionIntro(), ActionMenu.MissionIntro, "");
            GetContinueKey();

            //
            // get Prisoner's age
            //
            DisplayGamePlayScreen("Quest Setup - Age", Text.InitializeMissionGetPrisonerAge(), ActionMenu.MissionIntro, "");
            int gameTravelerAge;
            GetInteger($"Enter your age {prisoner.Name}: ", 0, 1000000, out gameTravelerAge);
            prisoner.Age = gameTravelerAge;

            //
            // get Prisoner's gender
            //
            DisplayGamePlayScreen("Quest Setup - Gender", Text.InitializeMissionGetPrisonerGender, ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt($"Enter your gender {prisoner.Name}: ");
            prisoner.Gender = GetGender();

            //
            // get Prisoner's personality
            //
            bool selectingPersona = true;
            string prompt = $"Enter 'Yes' or 'No' {prisoner.Name}: ";
            DisplayGamePlayScreen("Quest Setup - Personality", Text.InitializeMissionGetPrisonerPersonality(prisoner), ActionMenu.MissionIntro, "");

            // validate
            while (selectingPersona)
            {
                string userResponse;
                DisplayInputBoxPrompt(prompt);
                userResponse = GetString().ToUpper();

                if (userResponse == "YES")
                {
                    prisoner.Personality = true;
                    selectingPersona = false;
                }
                else if (userResponse == "NO")
                {
                    prisoner.Personality = false;
                    selectingPersona = false;
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage("You must enter either 'Yes' or 'No'. Please try again.");
                    DisplayInputBoxPrompt(prompt);
                }
            }
            
            return prisoner;
        }

        /// <summary>
        /// get Prisoner's name
        /// </summary>
        public string GetPrisonerName(Player gamePrisoner, PlayerAction choosenAction, RoomLocation location)
        {
            bool choosingName = true;

            //
            // allows user to enter a custom name
            //
            DisplayGamePlayScreen("Quest Setup - Name", Text.InitializeMissionGetPrisonerName(), ActionMenu.InitializePlayerName, "");
            DisplayInputBoxPrompt("Enter your name: ");
            gamePrisoner.Name = Console.ReadLine();
            

            if (gamePrisoner.Name == "1")
            {
                //
                // this code will generate a random name
                //
                gamePrisoner.Name = GenerateRandomName();

                while (choosingName)
                {
                    string userResponese;
                    string prompt = $"Generate new Name? (Yes or No): ";

                    DisplayGamePlayScreen("Quest Setup - Name", Text.InitializeMissionGetRandomName, ActionMenu.InitializeRandomName, "");
                    DisplayColoredText(gamePrisoner.Name ,choosenAction, location);
                    DisplayInputBoxPrompt(prompt);
                    userResponese = Console.ReadLine().ToUpper();

                    if (userResponese == "YES")
                    {
                        //
                        // this code will generate a random name
                        //
                        gamePrisoner.Name = GenerateRandomName();
                        choosingName = true;
                    }
                    else if (userResponese == "NO")
                    {
                        choosingName = false;

                        // 
                        // change view status to playing game
                        //
                        _viewStatus = ViewStatus.PlayingGame;

                        return gamePrisoner.Name;
                    }
                    else if (userResponese == "1")
                    {
                        //
                        // allows user to return to previous screen and enter a custom name
                        //
                        DisplayGamePlayScreen("Quest Setup - Name", Text.InitializeMissionGetPrisonerName(), ActionMenu.InitializePlayerName, "");
                        DisplayInputBoxPrompt("Enter your name: ");
                        gamePrisoner.Name = Console.ReadLine();

                        if (gamePrisoner.Name == "1")
                        {
                            gamePrisoner.Name = GenerateRandomName();
                            choosingName = true;
                        }
                        else
                        {
                            choosingName = false;

                            // 
                            // change view status to playing game
                            //
                            _viewStatus = ViewStatus.PlayingGame;

                            return gamePrisoner.Name;
                        }
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("You must enter either 'Yes','No', or a menu option. Please try again.");
                        DisplayInputBoxPrompt(prompt);
                        GetContinueKey();
                    }
                }

            }

            // 
            // change view status to playing game
            //
            _viewStatus = ViewStatus.PlayingGame;

            return gamePrisoner.Name;
        }

        /// <summary>
        /// generates a random name string and returns that string
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomName()
        {
            // lists of random names
            List<string> femaleNames = new List<string>()
            {                
                // female names
                "Eugenie 'Crazy' Ethel",
                "Mettie 'The Hawk' Gnash",
                "Teresa 'Temptation' Crowley",
                "Sadye 'Barnacle' Atherton",
                "Blossom 'Shrew' Branson",
                "Adella 'Riot' Griffin",
                "Patsy 'Silver Hair' Watt",
                "Georgie 'The Marked' Arlin",
                "Harriett 'Thoothless' Shurman",
                "Taylor 'The Confident' Achey",
                "Melissa 'Vixen' Stratford",
                "Enola 'Treasure' Bradford",
                "Prudence 'Shark Bait' Merton",
                "Vira 'The Fool' Bing",
                "Vala 'Brown Teeth' Alistair",
                "Claire 'Dawg' Hastings",
                "Dolores 'The Bright' Tindall",
                "Elfrida 'Buccaneer' Smith",
                "Hulda 'Three-Teeth' Cloven",
                "Blanche 'One-Legged' Compton"
            };
            List<string> maleNames = new List<string>()
            {
                "Wayne 'Silver Tooth' Colby",
                "Farr 'Liar' Richmond",
                "Trevin 'Rage' Sweet",
                "Delton 'Double-Crossed' Fischer",
                "Wyatt 'Mutiny' Lucifer",
                "Garton 'Speechless' Brudge",
                "Burton 'Twisting' Ogden",
                "Paley 'Four-Teeth' Smith",
                "Washington 'The Hero' Marlowe",
                "Bramwell 'Rum Lover' Jeronimo",
                "Tomlin 'One-Eared' Shell",
                "Meldrick 'Rough Dog' Griffin",
                "Hawthorne 'One-Eyed' Kimberley",
                "Denver 'Fierce' Swales",
                "Winton 'Shaded' Penney",
                "Radburn 'Roaring' Charles",
                "Filmore 'Poopdeck' Frederik",
                "Harold 'Foxy' Sans",
                "Leland 'Four Fingers' Burling",
                "Sully 'Swine' Ottinger",
            };

            if (_gamePrisoner.Gender == Character.GenderType.MALE)
            {
                string prisonerName;
                Random randomName = new Random();

                int randomNumber = randomName.Next(maleNames.Count); //create a variable that will select a random name from the "names" list                    
                prisonerName = maleNames[randomNumber]; //assigns a random List<string> taken from the "names" list to the pirateName(string) 
                return prisonerName;
            }
            else
            {
                string prisonerName;
                Random randomName = new Random();

                int randomNumber = randomName.Next(maleNames.Count); //create a variable that will select a random name from the "names" list                    
                prisonerName = maleNames[randomNumber]; //assigns a random List<string> taken from the "names" list to the pirateName(string) 
                return prisonerName;
            }
        }

        #endregion

        #region ----- display responses to menu action choices -----

        /// <summary>
        /// displays all player information
        /// </summary>
        public void DisplayPrisonerInfo()
        {
            DisplayGamePlayScreen("Player Information", Text.PrisonerInfo(_gamePrisoner), ActionMenu.PlayerMenu, "");
        }

        /// <summary>
        /// edits player information
        /// </summary>        
        public void DisplayEditPrisonerInformation()
        {
            string userResponse;
            bool editingInfo = true;

            DisplayGamePlayScreen("Edit Player Information", Text.EditPrisonerInfo(), ActionMenu.PlayerMenu, "");
            DisplayColoredText("", PlayerAction.EditPlayerInfo, _gameUniverse.RoomLocations[0]);        

            while (editingInfo)
            {
                DisplayInputBoxPrompt($"Enter your selection {_gamePrisoner.Name}: ");
                userResponse = Console.ReadLine().ToUpper();
                if (userResponse == "A" || userResponse == "B" || userResponse == "C")
                {
                    switch (userResponse)
                    {
                        case "A":
                            DisplayGamePlayScreen("Edit Player Information", $"You have selcted to edit your Name.\n" + "If you change your mind, you may enter 'Back' to be returned to the previous menu.\n", ActionMenu.PlayerMenu, "");
                            DisplayInputBoxPrompt($"Enter your new name: ");
                            _gamePrisoner.Name = Console.ReadLine();
                            editingInfo = false;
                            break;
                           

                        case "B":
                            int prisonerAge = 0;
                            DisplayGamePlayScreen("Edit Player Information", $"You have selcted to edit your Age.\n" + "If you change your mind, you may enter 'Back' to be returned to the previous menu.\n", ActionMenu.PlayerMenu, "");
                            DisplayInputBoxPrompt($"Enter your new age: ");
                            GetInteger($"Enter your age {_gamePrisoner.Name}: ", 0, 1000000, out prisonerAge);
                            _gamePrisoner.Age = prisonerAge;
                            editingInfo = false;
                            break;

                        case "C":
                            DisplayGamePlayScreen("Edit Player Information", $"You have selcted to edit your Gender.\n" + "If you change your mind, you may enter 'Back' to be returned to the previous menu.\n", ActionMenu.PlayerMenu, "");
                            DisplayInputBoxPrompt($"Enter your new gender: ");
                            _gamePrisoner.Gender = GetGender();
                            editingInfo = false;
                            break; ;                 

                        default:
                            break;
                    }
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage("You must enter a correct corresponding letter. Please try again.");
                    DisplayInputBoxPrompt($"Enter your selection, {_gamePrisoner.Name}: ");
                }
            }            
        }

        /// <summary>
        /// displays a closing screen upon exiting
        /// </summary>
        public void DisplayClosingScreen()
        {                        
            Console.BackgroundColor = ConsoleTheme.SplashScreenBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.SplashScreenForegroundColor;
            Console.Clear();
            Console.CursorVisible = false;

            Console.SetCursorPosition(0, 6);
            string tabSpace = new String(' ', 45);
            Console.WriteLine(tabSpace + @" _____ _                    _                                           ");
            Console.WriteLine(tabSpace + @"|_   _| |                  | |  _                                       ");
            Console.WriteLine(tabSpace + @"  | | | |__   ___    ____  | | / /___                                   ");
            Console.WriteLine(tabSpace + @"  | | |  _ \ / _ \  |  _ \ | |/ // __\                                  ");
            Console.WriteLine(tabSpace + @"  | | | | | | (_) | | | | ||  _ \\__ \                                  ");
            Console.WriteLine(tabSpace + @"  \_/ |_| |_|\__/\_\|_| |_||_| \_\___/                                  ");
            Console.WriteLine(tabSpace + @"                  _______                                               ");
            Console.WriteLine(tabSpace + @"                 /  ____/                                               ");
            Console.WriteLine(tabSpace + @"                 | |__  ___  _ __                                       ");
            Console.WriteLine(tabSpace + @"                 |  __|/ _ \| '__/                                      ");
            Console.WriteLine(tabSpace + @"                 | |  | (_) | |                                         ");
            Console.WriteLine(tabSpace + @"                 \_/   \___/|_|                                         ");
            Console.WriteLine(tabSpace + @"                            ______ _               _               _    ");
            Console.WriteLine(tabSpace + @"                            |  __ \ |             (_)             | |   ");
            Console.WriteLine(tabSpace + @"                            | |_/ / | ___ __     _ _  ____   ___  | |   ");
            Console.WriteLine(tabSpace + @"                            |  __/| |/ _ \\ \   / / ||  _ \ / _ \ |_|   ");
            Console.WriteLine(tabSpace + @"                            | |   | | (_) |\ \ / /| || | | | (_) | _    ");
            Console.WriteLine(tabSpace + @"                            \_|   |_|\__/\_\\_  / |_||_| |_|\__  |(_)   ");
            Console.WriteLine(tabSpace + @"                                            _/ /              _/ /      ");
            Console.WriteLine(tabSpace + @"                                           |__/              |__/       ");

            Console.SetCursorPosition(85, 30);
            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }        

        /// <summary>
        /// displays a list of all room locations
        /// </summary>
        public void DisplayListOfRoomLocations()
        {
            DisplayGamePlayScreen("List - Room Locations", Text.ListAllRoomLocations(), ActionMenu.AdminMenu, "");
        }

        /// <summary>
        /// displays a list of all game objects
        /// </summary>
        public void DisplayListOfAllGameObjects()
        {
            DisplayGamePlayScreen("List: Game Objects", Text.ListAllGameObjects(), ActionMenu.AdminMenu, "");
        }

        /// <summary>
        /// displays a list of all NPCs
        /// </summary>        
        public void DisplayListOfAllNpcs()
        {
            DisplayGamePlayScreen("List: NPCs", Text.ListAllNpcs(), ActionMenu.AdminMenu, "");
        }

        /// <summary>
        /// gets the specific NPC the player wants to interact with
        /// </summary>
        /// <returns></returns>
        public int DisplayGetNpcToTalkTo()
        {
            int npcId = 0;
            bool validNpcId = false;

            //
            // get a list of NPCs in the current location
            //
            List<Npc> npcsInCurrentLocation = _gameUniverse.GetNpcsByRoomLocation(_gamePrisoner.RoomLocationId);

            // temporary NPC list to be used to remove NPCs that have been spoken to
            List<Npc> tempNpcList = _gameUniverse.GetNpcsByRoomLocation(_gamePrisoner.RoomLocationId);

            if (npcsInCurrentLocation.Count > 0)
            {
                foreach (Civillian npc in tempNpcList)
                {
                    if (npc.DialogueExhausted == true)
                    {
                        if (npc.Name == "Gilbert the Brave")
                        {

                        }
                        else
                        {
                            npcsInCurrentLocation.Remove(npc);
                        }
                    }
                }

                if (npcsInCurrentLocation.Count <= 0)
                {
                    DisplayGamePlayScreen("Choose Character to Speak With", "It appears there are no NPCs here who want to chat.", ActionMenu.NpcMenu, "");
                    return npcId;
                }

                DisplayGamePlayScreen("Choose Character to Speak With", Text.NpcsChooseList(), ActionMenu.NpcMenu, "");
                DisplayColoredNpcs(PlayerAction.TalkTo, npcsInCurrentLocation[0]);

                while (!validNpcId)
                {
                    //
                    // get an integer fomr the user
                    //
                    GetInteger($"Enter the Id number of the NPC you wish to speak to: ", 0, 0, out npcId);

                    //
                    // validate integer as a valid game object id and in the current location
                    //                    
                    if (_gameUniverse.IsValidNpcByLocationId(npcId, _gamePrisoner.RoomLocationId))
                    {
                        Npc npc = _gameUniverse.GetNpcById(npcId);
                        if (npc is ISpeak)
                        {
                            validNpcId = true;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage("It appears this character has nothing to say. Please try again.");
                        }

                        if (npc.DialogueExhausted)
                        {
                            if (npc.Name == "Gilbert the Brave")
                            {
                                validNpcId = true;
                            }
                            else
                            {
                                ClearInputBox();
                                DisplayInputErrorMessage($" You entered an invalid Id number. Please try again.");
                                validNpcId = false;
                            }                           
                        }

                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("You enetered an invalid Id number. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Choose Character to Speak With", "It appears there are no NPCs here.", ActionMenu.NpcMenu, "");
            }

            return npcId;
        }

        /// <summary>
        /// gets a valid NPC to trade with
        /// </summary>
        public int DisplayGetNpcToTradeWith()
        {
            int traderId = 0;
            Trader trader = new Trader();
            bool validNpcId = false;

            //
            // get a list of NPCs in the current location
            //
            List<Trader> tradersInCurrentLocation = _gameUniverse.GetTraderNpcsByRoomLocation(_gamePrisoner.RoomLocationId);

            if (tradersInCurrentLocation.Count > 0)
            {
                DisplayGamePlayScreen("Choose Character to Trade With", Text.NpcsChooseList(), ActionMenu.NpcMenu, "");
                DisplayColoredNpcs(PlayerAction.TradeWith, tradersInCurrentLocation[0]);

                while (!validNpcId)
                {
                    //
                    // get an integer fomr the user
                    //
                    GetInteger($"Enter the Id number of the NPC you wish to trade with: ", 0, 0, out traderId);

                    //
                    // validate integer as a valid game object id and in the current location
                    //                    
                    if (_gameUniverse.IsValidNpcByLocationId(traderId, _gamePrisoner.RoomLocationId))
                    {
                        Npc npc = _gameUniverse.GetNpcById(traderId);
                        if (npc is ITrade)
                        {
                            validNpcId = true;
                            trader = (Trader)npc;
                            _gamePrisoner.IndividualGameObject = 99;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage("It appears this character has nothing to trade. Please try again.");
                        }

                        if (trader.Inventory.Count < 0)
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage($"{npc.Name}'s is out of stock! Please try again.");
                            validNpcId = false;
                        }

                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears there are no Traders here.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Choose Character to Trade With", "It appears there are no Traders here.", ActionMenu.NpcMenu, "");
            }

            return traderId;
        }

        /// <summary>
        /// gets a valid object to be traded
        /// </summary>
        public int DisplayGetValidObjectIdToTrade(Npc npc)
        {
            ITrade tradingNpc = npc as ITrade;
            int tradingObjectId = 0;
            bool validObjectId = false;

            DisplayGamePlayScreen($"Trading With : {npc.Name}", Text.GameObjectsChooseList(tradingNpc.Inventory), ActionMenu.NpcMenu, "");
            DisplayColoredNpcs(PlayerAction.TradeWith, npc);

            while (!validObjectId)
            {
                GetInteger("Enter the Id number of the object you wish trade for: ", 0, 0, out tradingObjectId);

                if (_gameUniverse.IsValidObjectByNpcInventoryId(tradingObjectId, tradingNpc))
                {
                    validObjectId = true;
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage("Invalid Id number. Please try again.");
                }
            }

            return tradingObjectId;
        }

        /// <summary>
        /// display object to be traded info and initiats a trade is possible
        /// </summary>
        public void DisplayTradeWith(int npcObjectId, int playerObjectId, Npc npc)
        {
            GameObject gameObjectToBeTraded =  _gameUniverse.GetObjectById(npcObjectId, _gamePrisoner);            
            ITrade tradingNpc = npc as ITrade;
            bool trading = true;

            playerObjectId = gameObjectToBeTraded.TradingObjectId;

            ActionMenu.currentMenu = ActionMenu.CurrentMenu.TradeMenu;
            DisplayGamePlayScreen($"Object Info : {gameObjectToBeTraded.Name}", Text.LookAt(gameObjectToBeTraded), ActionMenu.TradeMenu ,"");
            
            DisplayInputBoxPrompt("Would you like to trade for this item? (Yes or No): ");

            while (trading)
            {
                if (GetString() == "YES")
                {
                    tradingNpc.Trade(gameObjectToBeTraded.Id, playerObjectId, _gamePrisoner);

                    if (tradingNpc.InventoryIds.Contains(gameObjectToBeTraded.Id))
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("You lack the required item in order to trade! Please try again.                             ");
                        DisplayInputBoxPrompt("Would you like to trade for this item? (Yes or No): ");
                    }
                    else
                    {
                        if (gameObjectToBeTraded.Id == 14)
                        {                           
                            _gameUniverse.GameObjects[24].RoomLocationId = 0;
                        }
                        else if (gameObjectToBeTraded.Id == 79)
                        {
                            _gameUniverse.GameObjects[40].RoomLocationId = 0;

                        }

                        DisplayConfirmGameObjectAddedToInvetory(gameObjectToBeTraded);
                        GetContinueKey();
                        trading = false;
                    }
                }
                else
                {
                    trading = false;
                }
            }

            DisplayGamePlayScreen("NPC Menu", "Select an operation from the menu.", ActionMenu.NpcMenu, "");
            ActionMenu.currentMenu = ActionMenu.CurrentMenu.NpcMenu;
        }

        /// <summary>
        /// displays the NPC's message to the player
        /// </summary>
        public void DisplayTalkTo(Npc npc)
        {
            ISpeak speakingNpc = npc as ISpeak;
            bool speakingToNpc = true;
            int messageIndex = -1;

            string message = speakingNpc.Speak();

            while (speakingToNpc)
            {
                messageIndex++;
                if (npc.Name == "Gilbert the Brave" && npc.DialogueExhausted)
                {
                    message = speakingNpc.Riddle;
                    DisplayGamePlayScreen($"Speak to Character: {npc.Name}", message, ActionMenu.NpcMenu, "");
                    DisplayInputBoxPrompt("Enter you answer here: ");
                    if (Console.ReadLine().ToUpper() != "CLOUDS" )
                    {
                        DisplayInputErrorMessage("That is not the answer to the riddle. Please try again.");
                        GetContinueKey();
                        DisplayGamePlayScreen("NPC Menu", "Select an operation from the menu.", ActionMenu.NpcMenu, "");
                        break;
                    }
                    else
                    {
                        speakingNpc.Riddle = "Ahhh, I see you're more enlightned than I thought...you may pass. \n";
                        DisplayGamePlayScreen("Riddle Solved", speakingNpc.Riddle, ActionMenu.NpcMenu, "");
                        GetContinueKey();
                        speakingNpc.Riddle = "";
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                        DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_gameUniverse.GetRoomLocationById(_gamePrisoner.RoomLocationId)),
                                              ActionMenu.MainMenu, "");
                        DisplayColoredText("", PlayerAction.LookAround, _gameUniverse.GetRoomLocationById(_gamePrisoner.RoomLocationId));
                        speakingToNpc = false;
                        break;
                    }
                }

                if (messageIndex >= speakingNpc.Messages.Count)
                {
                    npc.DialogueExhausted = true;

                    // add exp points and health
                    if (npc.ExperiencePoints != 0)
                    {
                        npc = npc as Civillian;
                        _gamePrisoner.ExperiencePoints += npc.ExperiencePoints;
                        _gamePrisoner.Health += (int)npc.HealthPoints;
                        npc.ExperiencePoints = 0;
                    }

                    DisplayInputErrorMessage($"{npc.Name} has nothing more to say at this time. Press the Enter key to continue.");
                    GetContinueKey();
                    DisplayGamePlayScreen("NPC Menu", "Select an operation from the menu.", ActionMenu.NpcMenu, "");                    
                    break;
                }

                message = speakingNpc.Messages[messageIndex];

                if (message == "")
                {
                    message = "It appears this NPC has nothing to say. Please try again.";
                    DisplayGamePlayScreen($"Speak to Character: {npc.Name}", message, ActionMenu.NpcMenu, "");
                    speakingToNpc = false;
                }

                DisplayGamePlayScreen($"Speak to Character: {npc.Name}", message, ActionMenu.NpcMenu, "");
                DisplayInputBoxPrompt("Would you like to continue this conversation? (Yes or No): ");

                //
                // determines if the user would like to continue the current conversation
                //
                if (GetString() == "NO")
                {
                    DisplayGamePlayScreen("NPC Menu", "Select an operation from the menu.", ActionMenu.NpcMenu, "");
                    speakingToNpc = false;
                }                
            }
        }

        /// <summary>
        /// gets the specific game object the player wants to look at
        /// </summary>
        public int DisplayGetGameObjectsToLookAt()
        {
            int gameObjectId = 0;
            bool validGameObjectId = false;

            //
            // get a list of game objects in the current room location
            //
            List<GameObject> gameObjectsInRoomLocation = _gameUniverse.GetGameObjectsByRoomLocaitonId(_gamePrisoner.RoomLocationId);

            if (gameObjectsInRoomLocation.Count > 0)
            {
                DisplayGamePlayScreen("Look at a Object", Text.GameObjectsChooseList(gameObjectsInRoomLocation), ActionMenu.ObjectMenu, "");
                DisplayColoredObjects(PlayerAction.LookAt, 0);
                while (!validGameObjectId)
                {
                    //
                    // get an integer from the player
                    //
                    GetInteger($"Enter the Id number of the object you wish to look at: ", 0, 0, out gameObjectId);

                    //
                    // validate integer as a valid game object id and in the current location
                    //
                    if (_gameUniverse.IsValidGameObjectByLocaitonId(gameObjectId, _gamePrisoner.RoomLocationId))
                    {
                        validGameObjectId = true;
                        _gamePrisoner.IndividualGameObject = gameObjectId;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("You have entered an invalid game object id. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Look at a Object", "It appears there are no game objects here.", ActionMenu.ObjectMenu, "");
            }

            return gameObjectId;
        }

        /// <summary>
        /// displays information for a specific game object
        /// </summary>
        public void DisplayGameObjectInfo(GameObject gameObject)
        {
            DisplayGamePlayScreen($"{gameObject.Name} Information", Text.LookAt(gameObject), ActionMenu.ObjectMenu, "");
        }

        /// <summary>
        /// displays information pertaining to the current location
        /// </summary>
        public void DisplayLookAround()
        {
            //
            // get current room location
            //
            RoomLocation currentRoomLocation = _gameUniverse.GetRoomLocationById(_gamePrisoner.RoomLocationId);

            //
            // get list of game objects in current room location
            //
            List<GameObject> gameObjectsInCurrentRoomLocation = _gameUniverse.GetGameObjectsByRoomLocaitonId(_gamePrisoner.RoomLocationId);

            //
            // get list of game objects in current room location
            //
            List<Npc> npcsInCurrentLocation = _gameUniverse.GetNpcsByRoomLocation(_gamePrisoner.RoomLocationId);

            string messageBoxText = Text.LookAround(currentRoomLocation) + Environment.NewLine + Environment.NewLine;
            messageBoxText += Text.GameObjectsChooseList(gameObjectsInCurrentRoomLocation);


            DisplayGamePlayScreen("Current Location - Look Around", messageBoxText, ActionMenu.MainMenu, "");
        }

        /// <summary>
        /// gets a location ID and confirms if it's valid
        /// </summary>        
        public int DisplayGetNextRoomLocation()
        {
            int roomLocationId = 0;
            bool validRoomLocationId = false;

            DisplayGamePlayScreen("Travel to a new Location", Text.Travel(_gamePrisoner), ActionMenu.MainMenu, "");
            DisplayColoredText("", PlayerAction.Travel, _gameUniverse.RoomLocations[1]);

            while (!validRoomLocationId)
            {
                //
                // get and integer from the user
                //
                GetInteger($"Enter your new location {_gamePrisoner.Name}: ", 0, _gameUniverse.GetMaxRoomLocationId(), out roomLocationId);

                //
                // validate choosen integer and determine accessbility level
                //
                if (_gameUniverse.IsValidRoomLocationId(roomLocationId))
                {
                    if (_gameUniverse.IsAccessibleLocation(roomLocationId))
                    {
                        validRoomLocationId = true;
                    }
                    else
                    {
                        // display a specfic error message based on the choosen ID/location
                        switch(roomLocationId)
                        {
                            case 2:
                                #region ***forgotten vault

                                    ClearInputBox();
                                    DisplayInputErrorMessage("You must speak to an NPC before traveling here.               ");
                                    break;

                                #endregion
                            case 3:
                                #region ***sacred den
                                
                                    ClearInputBox();
                                    DisplayInputErrorMessage("You must speak to an NPC before traveling here.               ");
                                    break;
                                
                                #endregion
                            case 4:
                                #region ***dark corridor
                                
                                    ClearInputBox();
                                    DisplayInputErrorMessage("You must have a diamond in order to travel safely here.       ");
                                    break;
                                
                                #endregion 
                            case 5:
                                #region ***secret armory
                                
                                    ClearInputBox();
                                    DisplayInputErrorMessage("You must obtain the Rusty Key to unlock passage through here. ");
                                    break;                                

                                #endregion
                            case 6:
                                #region ***room of silence

                                    ClearInputBox();
                                    DisplayInputErrorMessage("You must speak to an NPC before traveling here.               ");
                                break;                                

                                #endregion
                            case 7:
                                #region ***cave of life

                                    ClearInputBox();
                                    DisplayInputErrorMessage("The area is extremely dangerous. Too unsafe for passage.      ");
                                    break;

                                #endregion
                            case 8:
                                #region ***portal room

                                    ClearInputBox();
                                    DisplayInputErrorMessage("The area is extremely dangerous. Too unsafe for passage.      ");
                                    break;

                                #endregion  
                            case 9:
                                #region ***freedom

                                    ClearInputBox();
                                    DisplayInputErrorMessage("It is impossible for you to find freedom from where you are.   ");
                                    break;

                                #endregion
                            default:
                                #region ***DEFAULT

                                    ClearInputBox();
                                    DisplayInputErrorMessage("It appears you attempted to travel to inaccessible location.   ");

                                #endregion
                                break;
                        }                        
                    }
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage("It appears you entered an invalid Room location ID.           ");
                }
            }

            return roomLocationId;
        }

        /// <summary>
        /// displays all locations that the player has already visited
        /// </summary>
        public void DisplayLocationsVisited()
        {
            List<RoomLocation> visitedRoomLocations = new List<RoomLocation>();

            foreach (int roomLocationID in _gamePrisoner.RoomLocationsVisited)
            {
                visitedRoomLocations.Add(_gameUniverse.GetRoomLocationById(roomLocationID));
            }

            DisplayGamePlayScreen("Rooms Visited", Text.VisitedLocations(), ActionMenu.PlayerMenu, "");
        }

        /// <summary>
        /// displays the player's current treasure inventory
        /// </summary>
        public void DisplayTreasureInventory()
        {
            DisplayGamePlayScreen("Your Treasure", Text.CurrentTreasureInventory(_gamePrisoner.TreasureInventory), ActionMenu.PlayerMenu, "");
        }

        /// <summary>
        /// displays the player's current medicine inventory
        /// </summary>
        public void DisplayMedicineInventory()
        {
            DisplayGamePlayScreen("Your Medicine", Text.CurrentMedicineInventory(_gamePrisoner), ActionMenu.PlayerMenu, "");
        }

        /// <summary>
        /// displays the player's current inventory
        /// </summary>
        public void DisplayInventory()
        {
            DisplayGamePlayScreen("Current Inventory", Text.CurrentInventory(_gamePrisoner.Inventory), ActionMenu.PlayerMenu, "");
            DisplayColoredObjects(PlayerAction.Inventory, 0);
        }

        /// <summary>
        /// allows player to choose an item to add to their invetory, and validates for correct choices
        /// </summary>
        public int DisplayGetGameObjectToPickUp()
        {
            int gameObjectId = 0;
            bool validGameObjectId = false;

            //
            // get a list of game objects located in the current location
            //
            List<GameObject> gameObjectsInRoomLocation = _gameUniverse.GetGameObjectsByRoomLocaitonId(_gamePrisoner.RoomLocationId);

            if (gameObjectsInRoomLocation.Count > 0)
            {
                DisplayGamePlayScreen("Pick Up Object", Text.GameObjectsChooseList(gameObjectsInRoomLocation), ActionMenu.ObjectMenu, "");
                DisplayColoredObjects(PlayerAction.PickUp, 0);

                while (!validGameObjectId)
                {
                    //
                    // get integer from player
                    //
                    GetInteger($"Enter the ID number of the object you wish to add to your inventory: ", 0, 0, out gameObjectId);

                    //
                    // validate chosen integer, make sure its a valid game object id
                    //
                    if (_gameUniverse.IsValidGameObjectByLocaitonId(gameObjectId, _gamePrisoner.RoomLocationId))
                    {
                        GameObject gameObject = _gameUniverse.GetObjectById(gameObjectId, _gamePrisoner);
                        if (gameObject.CanInventory)
                        {
                            validGameObjectId = true;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage("This object may not be added to your inventory. Please try again.");
                        }
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you have entered an invalid game object ID. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Pick Up Object", "It appears that there are no objects in this location.", ActionMenu.ObjectMenu, "");
            }

            return gameObjectId;
        }

        /// <summary>
        /// allows player to choose an item to remove from their invetory, and validates for correct choices
        /// </summary>
        public int DisplayGetGameObjectToPutDown()
        {
            int gameObjectId = 0;
            bool validInventoryObjectId = false;

            if (_gamePrisoner.Inventory.Count > 0)
            {
                DisplayGamePlayScreen("Put Down Object", Text.GameObjectsChooseList(_gamePrisoner.Inventory), ActionMenu.ObjectMenu, "");
                DisplayColoredObjects(PlayerAction.PutDown, 0);

                while (!validInventoryObjectId)
                {
                    //
                    // get integer from player
                    //
                    GetInteger($"Enter the ID number of the object you wish to remove from your inventory: ", 0, 0, out gameObjectId);

                    //
                    // find game object in player's inventory
                    //
                    GameObject objectToPutDown = null; 
                    foreach (GameObject gameObject in _gamePrisoner.Inventory)
                    {
                        if (gameObject.Id == gameObjectId)
                        {
                            objectToPutDown = gameObject;
                        }
                    }

                    //
                    // validate chosen game object
                    //
                    if (objectToPutDown != null)
                    {
                        validInventoryObjectId = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you have entered an ID not in your current inventory. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Put Down Object", "It appears that there are no objects in your inventory.", ActionMenu.ObjectMenu, "");
            }

            return gameObjectId;
        }

        /// <summary>
        /// displays a confirmation that items have been added to player's inventory
        /// </summary>
        public void DisplayConfirmGameObjectAddedToInvetory(GameObject gameObjectAddedToInventory)
        {
            if (gameObjectAddedToInventory is Food)
            {
                Food food = gameObjectAddedToInventory as Food;
                DisplayGamePlayScreen("Pick Up Game Object", food.PickUpMessage, ActionMenu.ObjectMenu, "");
            }
            else
            {
                DisplayGamePlayScreen("Pick Up Game Object", $"The {gameObjectAddedToInventory.Name} has been added to your inventory.", ActionMenu.ObjectMenu, "");
            }
        }

        /// <summary>
        /// displays a confirmation that items have been removed from the player's inventory
        /// </summary>
        public void DisplayConfirmGameObjectRemovedFromInvetory(GameObject gameObjectRemovedFromInventory)
        {
            DisplayGamePlayScreen("Put Down Object", $"The {gameObjectRemovedFromInventory.Name} has been removed from your inventory.", ActionMenu.ObjectMenu, "");
        }   

        /// <summary>
        /// displays an attack against a random monster
        /// </summary>
        public void DisplayAttack(Monster monster, int cursorPosition)
        {
            // random integer to determine attack output
            int actionNumber = _gameUniverse.GetRandomInteger();                                             

            // hit
            if (actionNumber == 1)
            {                                
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Direct Hit! You deal {_gamePrisoner.DamageOutput} points of damage!");
                monster.HealthPoints -= (int)_gamePrisoner.DamageOutput;

                if (monster.HealthPoints < 0)
                {
                    monster.HealthPoints = 0;
                }
            }
            // miss
            else if (actionNumber == 2)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Miss!");                   
            }
            // block
            else if (actionNumber == 3)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{monster.Name} blocked your attack!");
            }
            // deflect
            else if (actionNumber == 4)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{monster.Name} defelcted you attack! Dealing {monster.DamageOutput/2} points of damage!");
                _gamePrisoner.Health -= (monster.DamageOutput / 2);
            }
            // critical hit
            else
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"Critical Hit! You deal {_gamePrisoner.DamageOutput * 2} points of damage!");
                monster.HealthPoints -= (int)_gamePrisoner.DamageOutput * 2;

                if (monster.HealthPoints < 0)
                {
                    monster.HealthPoints = 0;
                }
            }
        }

        /// <summary>
        /// displays an attack from the monster
        /// </summary>
        public void DisplayMonsterAttack(Monster monster, int cursorPosition)
        {
            // random integer to determine attack output
            int actionNumber = _gameUniverse.GetRandomInteger();

            // set cursor position in order to display battle feed properly
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);

            // hit
            if (actionNumber == 1)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{monster.Name} makes contact! Dealing {monster.DamageOutput} points of damage!");
                _gamePrisoner.Health -= monster.DamageOutput;

                if (_gamePrisoner.Health <= 0)
                {
                    _gamePrisoner.Health = 0;
                }
            }
            // miss
            else if (actionNumber == 2)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{monster.Name} misses!");
            }
            // block
            else if (actionNumber == 3)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{monster.Name} attacks, but you blocked it!");
            }
            // deflect
            else if (actionNumber == 4)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"You defelcted {monster.Name}'s attack! Dealing {_gamePrisoner.DamageOutput / 2} points of damage!");
                monster.HealthPoints -= (int)_gamePrisoner.DamageOutput / 2;
               
            }
            // critical hit
            else
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"Critical Hit! {monster.Name} hits you hard, dealing {monster.DamageOutput * 2}!");
                _gamePrisoner.Health -= monster.DamageOutput * 2;

                if (_gamePrisoner.Health <= 0)
                {
                    _gamePrisoner.Health = 0;
                }
            }
        }

        /// <summary>
        /// displays healing from potion use during battle
        /// </summary>
        public void DisplayHeal(Monster monster, int cursorPosition)
        {
            int playerChoice = 0;
            bool healing = true;
            PlayerAction choosenAction = PlayerAction.None;

            // if player has no medicine, display message
            if (_gamePrisoner.MedicinePouch.Count <= 0)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("You don't have any potions to use!");
                healing = false;
            }

            while (healing)
            {
                // display battle screen with potions menu
                DisplayBattleStats(monster);
                DisplayMenuBox(ActionMenu.HealMenu);

                // gets the user's choice on what potion to use
                GetInteger("Please select which potion you want to use from the menu: ", 1, 5, out playerChoice);

                if (playerChoice == 1)
                {
                    choosenAction = PlayerAction.UseMinorPotion;
                }
                else if (playerChoice == 2)
                {
                    choosenAction = PlayerAction.UseRegularPotion;
                }
                else if (playerChoice == 3)
                {
                    choosenAction = PlayerAction.UseMajorPotion;
                }
                else if (playerChoice == 4)
                {
                    choosenAction = PlayerAction.UseSuperiorPotion;
                }
                else if (playerChoice == 5)
                {
                    choosenAction = PlayerAction.UseUltimatePotion;
                }

                // based on user's choice, will add health and dislpay message
                switch (choosenAction)
                {
                    case PlayerAction.UseMinorPotion:
                        #region MINOR POTIONS

                        foreach (var medicine in _gamePrisoner.MedicinePouch)
                        {            
                            _gamePrisoner.RoomLocationId = 6;
                            if (medicine.HealthPoints == 20)
                            {                                
                                _gamePrisoner.Health += medicine.HealthPoints;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($"You drink the potion down, healing {medicine.HealthPoints} points of health!");

                                _gamePrisoner.MedicinePouch.Remove(medicine);
                                ActionMenu.currentMenu = ActionMenu.CurrentMenu.BattleMenu;

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                            else if (!_gamePrisoner.MedicinePouch.Contains(_gameUniverse.GetObjectById(60, _gamePrisoner)))
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("You are currently out of that potion!");

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                        }

                        #endregion
                        break;

                    case PlayerAction.UseRegularPotion:
                        #region REGULAR POTIONS

                        foreach (var medicine in _gamePrisoner.MedicinePouch)
                        {
                            _gamePrisoner.RoomLocationId = 7;
                            if (medicine.HealthPoints == 40)
                            {
                                _gamePrisoner.Health += medicine.HealthPoints;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($"You drink the potion down, healing {medicine.HealthPoints} points of health!");

                                _gamePrisoner.MedicinePouch.Remove(medicine);
                                ActionMenu.currentMenu = ActionMenu.CurrentMenu.BattleMenu;

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }                            
                            else if (!_gamePrisoner.MedicinePouch.Contains(_gameUniverse.GetObjectById(61, _gamePrisoner)))
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("You are currently out of that potion!");

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                        }

                        #endregion
                        break;

                    case PlayerAction.UseMajorPotion:
                        #region MAJOR POTIONS

                        foreach (var medicine in _gamePrisoner.MedicinePouch)
                        {
                            _gamePrisoner.RoomLocationId = 6;
                            if (medicine.HealthPoints == 60)
                            {
                                _gamePrisoner.Health += medicine.HealthPoints;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($"You drink the potion down, healing {medicine.HealthPoints} points of health!");

                                _gamePrisoner.MedicinePouch.Remove(medicine);
                                ActionMenu.currentMenu = ActionMenu.CurrentMenu.BattleMenu;

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                            else if (!_gamePrisoner.MedicinePouch.Contains(_gameUniverse.GetObjectById(62, _gamePrisoner)))
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("You are currently out of that potion!");

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                        }

                        #endregion
                        break;

                    case PlayerAction.UseSuperiorPotion:
                        #region SUPERIOR POTIONS

                        foreach (var medicine in _gamePrisoner.MedicinePouch)
                        {
                            _gamePrisoner.RoomLocationId = 2;
                            if (medicine.HealthPoints == 80)
                            {
                                _gamePrisoner.Health += medicine.HealthPoints;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($"You drink the potion down, healing {medicine.HealthPoints} points of health!");

                                _gamePrisoner.MedicinePouch.Remove(medicine);
                                ActionMenu.currentMenu = ActionMenu.CurrentMenu.BattleMenu;

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                            else if (!_gamePrisoner.MedicinePouch.Contains(_gameUniverse.GetObjectById(63, _gamePrisoner)))
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("You are currently out of that potion!");

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                        }
                        #endregion
                        break;

                    case PlayerAction.UseUltimatePotion:
                        #region ULTIMATE POTIONS

                        foreach (var medicine in _gamePrisoner.MedicinePouch)
                        {
                            _gamePrisoner.RoomLocationId = 6;
                            if (medicine.HealthPoints == 100)
                            {
                                _gamePrisoner.Health += medicine.HealthPoints;
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($"You drink the potion down, healing {medicine.HealthPoints} points of health!");

                                _gamePrisoner.MedicinePouch.Remove(medicine);
                                ActionMenu.currentMenu = ActionMenu.CurrentMenu.BattleMenu;

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                            else if (!_gamePrisoner.MedicinePouch.Contains(_gameUniverse.GetObjectById(64, _gamePrisoner)))
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("You are currently out of that potion!");

                                #region ---reset display for menu box---

                                ClearMenuBox();
                                ClearInputBox();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                DisplayMenuBox(ActionMenu.BattleMenu);

                                #endregion
                                healing = false;
                                break;
                            }
                        }

                        #endregion
                        break;

                    default:
                        break;
                }                
            }
        }

        /// <summary>
        /// displays an attempt to flee from battle
        /// </summary>
        public bool DisplayFlee(Monster monster, int cursorPosition)
        {
            bool escape = false;

            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("You attempt to run away...");

            #region --- stopwatch ---

            // code for stopwatch taken from: https://www.dotnetperls.com/sleep
            Console.CursorVisible = false;
            var stopwatch = Stopwatch.StartNew();
            Thread.Sleep(1000);
            stopwatch.Stop();

            #endregion

            // random integer to determine attack output
            int actionNumber = _gameUniverse.GetRandomInteger();

            // set cursor position in order to display battle feed properly
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);

            // attempt successful
            if (actionNumber == 1)
            {
                #region --- reset cursor position if it extends beyond message box ---

                if (cursorPosition > 23)
                {
                    // this code will clear the "battle feed"
                    for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                        Console.Write($"                                                                                                    ");
                    }

                    cursorPosition = 5;
                }

                #endregion

                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"Success! You managed to escape the {monster.Name}!");
                escape = true;
                return escape;
            }
            // attempt failed
            else if (actionNumber == 2)
            {
                #region --- reset cursor position if it extends beyond message box ---

                if (cursorPosition > 23)
                {
                    // this code will clear the "battle feed"
                    for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                        Console.Write($"                                                                                                    ");
                    }

                    cursorPosition = 5;
                }

                #endregion

                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Failed! The {monster.Name} grabbed you, dealing {monster.DamageOutput/2} points of damage.");
                _gamePrisoner.Health -= monster.DamageOutput / 2;
                escape = false;
                return escape;
            }
            // attempt successful
            else if (actionNumber == 3)
            {
                #region --- reset cursor position if it extends beyond message box ---

                if (cursorPosition > 23)
                {
                    // this code will clear the "battle feed"
                    for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                        Console.Write($"                                                                                                    ");
                    }

                    cursorPosition = 5;
                }

                #endregion

                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"Success! You escaped and retrieved an extra potion!");
                _gamePrisoner.RoomLocationId = 7;
                _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(61, _gamePrisoner));
                escape = true;
                return escape;
            }
            // attempt failed
            else if (actionNumber == 4)
            {
                #region --- reset cursor position if it extends beyond message box ---

                if (cursorPosition > 23)
                {
                    // this code will clear the "battle feed"
                    for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                        Console.Write($"                                                                                                    ");
                    }

                    cursorPosition = 5;
                }

                #endregion

                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Failed! The {monster.Name} struck you as you attempt to flee, dealing {monster.DamageOutput * 2} points of damage.");
                _gamePrisoner.Health -= monster.DamageOutput * 2;
                escape = false;
                return escape;
            }
            // attempt failed
            else
            {
                #region --- reset cursor position if it extends beyond message box ---

                if (cursorPosition > 23)
                {
                    // this code will clear the "battle feed"
                    for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                        Console.Write($"                                                                                                    ");
                    }

                    cursorPosition = 5;
                }

                #endregion

                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition++);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Failed! The {monster.Name} stopped you!");
                escape = false;
                return escape;
            }
        }

        /// <summary>
        /// displays the current health for the player and the monster during a battle
        /// </summary>
        /// <param name="monster"></param>
        public void DisplayBattleStats(Monster monster)
        {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 3);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("--------------------------------------------------------------");
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 4);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("--------------------------------------------------------------");

                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 2);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"Player's Health: {_gamePrisoner.Health}  ");

                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 30, ConsoleLayout.MenuBoxPositionTop + 2);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{monster.Name}'s Health: {monster.HealthPoints}  ");                       
        }

        #endregion

        #endregion
    }
}
