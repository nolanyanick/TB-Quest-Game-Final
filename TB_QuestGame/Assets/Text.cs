using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// class to store static and to generate dynamic text for the message and input boxes
    /// </summary>
    public static class Text
    {
        public static List<string> HeaderText = new List<string>() { "The Dungeon" };
        public static List<string> FooterText = new List<string>() { "Nolan B. Yanick Productions, 2018" };   

        #region INTITIAL GAME SETUP

        public static string QuestIntro()
        {
            string messageBoxText =
            "The year is 1252 and you are a common theif stealing anything you can find that'll put money in your pocket. " +
            "As long as you can remeber you've had success stealing valuables for your own gain. \n" +
            " \n" +
            "One day during a normal burglary job, you're apprehended by guards. " +
            "With all the crimes you've committed over the years, you're sentenced to spend the rest of your days in a deep and vast " +
            "dungeon below the city." +
            "According to legend this place is filled with dangerous criminals, creatures, and demons. " +
            "Along with that, you've heard rumors that any attempt to escpape from the dungeon is futile, as there " +
            "has never been any evidence of anyone breaking out. \n" +
            " \n" +
            "Coming to grips with your fate you decide that instead of wasting away for years and years, you'd rather " +
            "spend your time trying to escape and spend the rest of your life abive ground. \n" +
            " \n" +
            " \n" +
            "Press the Esc key to exit the game at any point.\n" +
            " \n" +
            "Your quest begins now.\n" +
            " \n" +
            "\tFor your first task, you must fill out some essential information for this quest.\n" +
            " \n" +
            "\tPress any key to begin the Quest Initialization Process.\n";

            return messageBoxText;
        }

        public static List<string> StatusBox(Player gamePrisoner) 
        {
            List<string> statusBoxText = new List<string>();

            statusBoxText.Add($"Health: {gamePrisoner.Health}\n");
            statusBoxText.Add($"Lives: {gamePrisoner.Lives}\n");
            statusBoxText.Add($"Level: {gamePrisoner.Level}\n");
            statusBoxText.Add($"Exp To Next Level: {gamePrisoner.ExpCap - gamePrisoner.ExperiencePoints}\n");

            return statusBoxText;
        }

        #region Initialize Mission Text

        public static string InitializeMissionIntro()
        {
            string messageBoxText =
                "Before you begin your quest we must gather some basic information.\n" +
                " \n" +
                "You will be prompted for the required information. Please enter the information below.\n" +
                " \n" +
                "\tPress any key to begin.";

            return messageBoxText;
        }

        public static string InitializeMissionGetPrisonerName()
        {
            string messageBoxText =
                "Enter your name.\n" +
                " \n" +
                "Please use the name you wish to be referred to during your quest.\n" +
                " \n" +
                "If you would like to generate a random name for you to use, enter '1'.";

            return messageBoxText;
        }

        public static string InitializeMissionGetRandomName
        {
            get
            {
                string messageBoxText =
                    "Random Name:" +
                    " \n" +
                    " \n" +
                    "If you would like to generate a new name, enter 'Yes'.\n" +
                    "If, however, you are satisfied with this name you may continue on with the setup process by entering 'No'.\n" +
                    "Also, if you change your mind you may enter '1' to be returned to previous screen.";

                return messageBoxText;
            }
        }

        public static string InitializeMissionGetPrisonerAge()
        {
            string messageBoxText =
                "First we'll start off with your age.\n" +
                " \n" +
                "Enter your age below.\n" +
                " \n";                

            return messageBoxText;
        }

        public static string InitializeMissionGetPrisonerGender
        {
            get
            {
                string messageBoxText =
                    "Next we  must record your gender.\n" +
                    " \n" +
                    "Please use the universal gender classifications listed below.\n" +
                    " \n";

                string genderList = null;

                foreach (Character.GenderType gender in Enum.GetValues(typeof(Character.GenderType)))
                {
                    if (gender != Character.GenderType.OTHER)
                    {
                        genderList += UppercaseFirst($"{gender}\n");
                    }
                }

                messageBoxText += genderList;

                return messageBoxText;
            }
        }

        public static string InitializeMissionGetPrisonerPersonality(Player gamePrisoner)
        {
            string messageBoxText =
                $"Next we must know a little about who you are.\n" +
                " \n" +
                "Do consider yourself to be friendly around others, or not.\n" +
                "A simple 'Yes' or 'No' will suffice.\n" +
                " \n" +
                "Enter your answer below.\n";

            return messageBoxText;
        }

        public static string InitializeMissionEchoPrisonerInfo(Player gamePrisoner)
        {
            string messageBoxText =
                $"Very good then {gamePrisoner.Name}.\n" +
                " \n" +
                "It appears we have all the necessary information to begin your game! You will find it" +
                " listed below.\n" +
                " \n" +
                $"\tPrisoner Name: \n" +
                $"\tPrisoner Age: \n" +
                $"\tPrisoner Gender: \n" +
                $"\tPrisoner Personality: \n" +
                " \n" +
                "Press any key to begin your quest.";

            return messageBoxText;
        }

        #endregion

        #endregion

        #region MAIN MENU ACTION SCREENS

        /// <summary>
        /// displays all player info
        /// </summary>
        public static string PrisonerInfo(Player gamePrisoner)
        {            
            string messageBoxText =
                $"\tPrisoner Name: \n" +
                $"\tPrisoner Age: \n" +
                $"\tPrisoner Gender: \n" +
                $"\tPrisoner's Weapon: \n" +
                $"\tDamage Output: \n" +
                $"\tExperience Points: \n" +
                " \n";               

            return messageBoxText;
        }

        /// <summary>
        /// allows player to edit parts of their exsisting info
        /// </summary>
        public static string EditPrisonerInfo()
        {
            string messageBoxText =
               "Please check over you current account information, and select a corresponding \n" +
               "letter to change that part of yout account.\n" +
               " \n" +
               " \n" +
               $"\tCurrent Information for: \n" +
               " \n" +             
               $"\tPrisoner Name: \n" +
               $"\tPrisoner Age: \n" +
               $"\tPrisoner Gender: \n" +
               " \n" +
               " \n" +
               "\tOptions:\n" +               
               " \n" +
               "\tA) Prisoner Name | B) Prisoner Age | C) Prisoner Gender\n" +            
               " \n";

            return messageBoxText;
        }

        /// <summary>
        /// displays all locations
        /// </summary>        
        public static string ListAllRoomLocations()
        {
            string messageBox =
            "Room Locations\n" +
            " \n" +

            //
            // display table header
            //
            "ID".PadRight(10) + "Name".PadRight(30) + " \n" +
            "---".PadRight(10) + "-----------------------".PadRight(30) + " \n";

            return messageBox;
        }

        /// <summary>
        /// displays all game objects
        /// </summary>
        public static string ListAllGameObjects()
        {
            //
            // display table name and column headers
            //             
            string messageBoxText =
                "Game Objects - NOTE: To view more objects press the Enter key.\n" +
                " \n" +

                //
                // display table header
                //
                "ID".PadRight(10) +
                "Name".PadRight(30) +
                "Room Location Id".PadRight(10) + " \n" +
                "---".PadRight(10) +
                "------------------".PadRight(30) +
                "------------------".PadRight(10) + " \n";

            return messageBoxText;
        }

        /// <summary>
        /// displays a list of all NPCs
        /// </summary>
        public static string ListAllNpcs()
        {
            //
            // display table name and column headers
            //             
            string messageBoxText =
                "NPCs\n" +
                " \n" +

                //
                // display table header
                //
                "ID".PadRight(10) +
                "Name".PadRight(30) +
                "Room Location Id".PadRight(10) + " \n" +
                "---".PadRight(10) +
                "------------------".PadRight(30) +
                "------------------".PadRight(10) + " \n";

            return messageBoxText;
        }

        /// <summary>
        /// displays all NPCs in current location
        /// </summary>
        public static string NpcsChooseList()
        {
            //
            // display table name and column headers
            //             
            string messageBoxText =
                "NPCs\n" +
                " \n" +

                //
                // display table header
                //
                "ID".PadRight(10) +
                "Name".PadRight(30) + " \n" +
                "---".PadRight(10) +
                "------------------".PadRight(30) + " \n";

            return messageBoxText;
        }

        /// <summary>
        /// displays all game objects in current location
        /// </summary>
        public static string GameObjectsChooseList(IEnumerable<GameObject> gameObjects)
        {
            //
            // display table name and column headers
            //             
            string messageBoxText =
                "Game Objects\n" +
                " \n" +

                //
                // display table header
                //
                "ID".PadRight(10) +
                "Name".PadRight(30) + " \n" +          
                "---".PadRight(10) +               
                "------------------".PadRight(30) + " \n";

            //
            // display all game objects in rows
            //
            string gameObjectRows = null;
            foreach (GameObject gameObject in gameObjects)
            {
                gameObjectRows +=
                    $"{gameObject.Id}".PadRight(10) +
                    $"{gameObject.Name}".PadRight(30) +                    
                    Environment.NewLine;
            }

            messageBoxText += gameObjectRows;

            return messageBoxText;
        }

        /// <summary>
        /// allows user to "look around" their current location, and
        /// displays location's general contents
        /// </summary>      
        public static string LookAround(RoomLocation roomLocation)
        {
            string messageBox = 
                $"Current Location: \n" +
                "Coordinates: \n" +
                " \n" +                           
                
                roomLocation.GeneralContents;

            return messageBox;
        }

        /// <summary>
        /// displays specific game object information
        /// </summary>
        public static string LookAt(GameObject gameObject)
        {
            string meassageTextBox = "";

            meassageTextBox =
                $"{gameObject.Name}\n" +
                " \n" +
                gameObject.Description + " \n" +
                " \n" +

                $"The {gameObject.Name} has a value of {gameObject.Value} and ";

            if (gameObject.CanInventory)
            {
                meassageTextBox += "can be added to your inventory.";
            }
            else
            {
                meassageTextBox += "cannot be added to your inventory.";

            }

            return meassageTextBox;
        }

        /// <summary>
        /// displays the current treasure inventory
        /// </summary>
        public static string CurrentTreasureInventory(IEnumerable<Treasure> treasureInventory)
        {
            string messageBoxText = "";

            //
            // display table header
            //             
            messageBoxText =
                "ID".PadRight(10) +
                "Name".PadRight(30) +
                "Rarity".PadRight(10) +
                " \n" + 
                "---".PadRight(10) +
                "-------------------------".PadRight(30) +
                "-------------------".PadRight(30) + 
                " \n";

            return messageBoxText;
        }

        /// <summary>
        /// displays the current inventory
        /// </summary>
        public static string CurrentInventory(IEnumerable<GameObject> inventory)
        {
            string messageBoxText = "";

            //
            // display table header
            //             
            messageBoxText =
                "ID".PadRight(10) +
                "Name".PadRight(30) +
                "Type".PadRight(10) +
                " \n" +
                "---".PadRight(10) +
                "-------------------------".PadRight(30) +
                "-------------------".PadRight(30) +
                " \n";

            //
            // display all game objects in rows
            //
            string inventoryObjectRows = null;
            foreach (GameObject inventoryObject in inventory)
            {
                inventoryObjectRows +=
                    $"{inventoryObject.Id}".PadRight(10) +
                    $"{inventoryObject.Name}".PadRight(30) +
                    $"{inventoryObject.Type}".PadRight(30) +

                    Environment.NewLine;
            }

            messageBoxText += inventoryObjectRows;

            return messageBoxText;
        }

        /// <summary>
        /// displays travel information
        /// </summary>
        public static string Travel(Player gamePrisoner)
        {
            string messageBox =
                $"{gamePrisoner.Name}, enter the ID number of your desired location from the table below. \n" +
                "To return to your current location, enter that ID for the location. \n" +
                " \n" +                

                //
                // display table header
                //
                "ID".PadRight(10) + "Name".PadRight(30) + "Accessible".PadRight(10) + "\n" +
                "---".PadRight(10) + "----------------------".PadRight(30) + "-------".PadRight(10) + "\n";

            return messageBox;
        }

        /// <summary>
        /// displays current location info
        /// </summary>
        public static string CurrentLocationInfo(RoomLocation roomLocation)
        {            
            string messageBox =
                "Current Location: \n" +
                "Coordinates: \n" +
                " \n" +
                " \n" +                        

                roomLocation.Description +

                " \n" +
                " \n" +
                "\tChoose from the menu options to proceed.\n";

            return messageBox;
        }

        /// <summary>
        /// displays all locations that have been visited by the player
        /// </summary>        
        public static string VisitedLocations()
        {
            string meassageBox = 
                "Rooms you've Visited\n" +
                " \n" +

                //
                // display table header
                //
                "ID".PadRight(10) + "Name".PadRight(30) + " \n" +
                "---".PadRight(10) + "--------------".PadRight(30) + " \n";

            return meassageBox;
        }

        /// <summary>
        /// displays the current medicine inventory
        /// </summary>
        public static string CurrentMedicineInventory(Player prisoner)
        {
            string messageBoxText = "";

            //
            // display table header
            //             
            messageBoxText =
                "ID".PadRight(10) +
                "Name".PadRight(30) +
                "Healing Power".PadRight(10) +
                " \n" +
                "---".PadRight(10) +
                "-------------------------".PadRight(30) +
                "-------------------".PadRight(30) +
                " \n";

            return messageBoxText;
        }

        #endregion

        #region TEXT UTILITIES

        /// <summary>
        /// changes string to lowercase with first letter uppercase
        /// adapted from: https://www.dotnetperls.com/uppercase-first-letter
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concatenation substring.
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        #endregion
    }
}
