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
    /// controller for the MVC pattern in the application
    /// </summary>
    public class Controller
    {
        #region FIELDS

        private ConsoleView _gameConsoleView;
        private Player _gamePrisoner;
        private Universe _gameUniverse;
        private RoomLocation _currentLocation;
        private bool _playingGame;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            //
            // setup all of the objects in the game
            //
            InitializeGame();

            //
            // begins running the application UI
            //
            ManageGameLoop();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize the major game objects
        /// </summary>
        private void InitializeGame()
        {
            _gamePrisoner = new Player();
            _gameUniverse = new Universe();
            _gameConsoleView = new ConsoleView(_gamePrisoner, _gameUniverse);
            _playingGame = true;

            //
            // temp Food for reference when adding event handler
            //
            Food food;

            //           
            // add the event handler for adding/subtracting to/from inventory
            //
            foreach (GameObject gameObject in _gameUniverse.GameObjects)
            {
                if (gameObject is Food)
                {
                    food = gameObject as Food;
                    food.ObjectAddedToInventory += HandleObjectAddedToInventory;
                }
            }

            Console.CursorVisible = false;
        }

        /// <summary>
        /// method to manage the application setup and game loop
        /// </summary>
        private void ManageGameLoop()
        {
            PlayerAction travelerActionChoice = PlayerAction.None;                                             

            //
            // display splash screen
            //
            _playingGame = _gameConsoleView.DisplaySpashScreen();

            //
            // player chooses to quit
            //
            if (!_playingGame)
            {
                Environment.Exit(1);
            }

            //
            // display introductory message
            //
            _gameConsoleView.DisplayGamePlayScreen("Quest Intro", Text.QuestIntro(), ActionMenu.MissionIntro, "");
            _gameConsoleView.GetContinueKey();

            //
            // initialize the mission traveler
            // 
            InitializeMission();

            //
            // prepare game play screen
            //
            _currentLocation = _gameUniverse.GetRoomLocationById(_gamePrisoner.RoomLocationId);
            _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
            _gameConsoleView.DisplayColoredText("", PlayerAction.ReturnToMainMenu, _currentLocation);

            //
            // game loop
            //
            while (_playingGame)
            {
                Console.CursorVisible = false;
                

                //
                // update all game stats/info
                //
                UpdateGameStatus();

                //
                // get game action from player
                //
                travelerActionChoice = GetNextPlayerAction();

                //
                // choose an action based on the user's menu choice
                //
                switch (travelerActionChoice)
                {
                    case PlayerAction.None:
                        break;

                    case PlayerAction.PlayerMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.PlayerMenu;
                        _gameConsoleView.DisplayGamePlayScreen("Player Menu", "Select an operation from the menu.", ActionMenu.PlayerMenu, "");
                        break;

                    case PlayerAction.ObjectMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.ObjectMenu;
                        _gameConsoleView.DisplayGamePlayScreen("Object Menu", "Select an operation from the menu.", ActionMenu.ObjectMenu, "");
                        break;

                    case PlayerAction.NpcMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.NpcMenu;
                        _gameConsoleView.DisplayGamePlayScreen("NPC Menu", "Select an operation from the menu.", ActionMenu.NpcMenu, "");
                        break;

                    case PlayerAction.EditPlayerInfo:                       
                        _gameConsoleView.DisplayEditPrisonerInformation();

                        //
                        // display game play screen with current location info and coordiantes
                        //
                        _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        _gameConsoleView.DisplayColoredText("", PlayerAction.ReturnToMainMenu, _currentLocation);
                        break;

                    case PlayerAction.PlayerInfo:
                        _gameConsoleView.DisplayPrisonerInfo();
                        _gameConsoleView.DisplayColoredText("", travelerActionChoice, _currentLocation);
                        break;

                    case PlayerAction.ListDestinations:
                        _gameConsoleView.DisplayListOfRoomLocations();
                        _gameConsoleView.DisplayColoredText("", travelerActionChoice, _currentLocation);
                        break;

                    case PlayerAction.ListGameObjects:
                        _gameConsoleView.DisplayListOfAllGameObjects();
                        _gameConsoleView.DisplayColoredObjects(travelerActionChoice, 0);
                        break;

                    case PlayerAction.ListNpcs:
                        _gameConsoleView.DisplayListOfAllNpcs();
                        _gameConsoleView.DisplayColoredNpcs(travelerActionChoice, _gameUniverse.GetNpcById(1));
                        break;

                    case PlayerAction.LookAround:
                        _gameConsoleView.DisplayLookAround();
                        _gameConsoleView.DisplayColoredText("", travelerActionChoice, _currentLocation);
                        _gameConsoleView.DisplayColoredObjects(travelerActionChoice, 0);
                        _gameConsoleView.DisplayColoredNpcs(travelerActionChoice, _gameUniverse.GetNpcById(1));
                        break;

                    case PlayerAction.LookAt:
                        LookAtAction();
                        break;

                    case PlayerAction.Travel:
                        int currentLocationId = _gamePrisoner.RoomLocationId;
                        int initiateBattle = _gameUniverse.GetRandomInteger();                       

                        //
                        // get new location choice
                        //
                        _gamePrisoner.RoomLocationId = _gameConsoleView.DisplayGetNextRoomLocation();

                        //
                        // determines if the player undergoes a battle (random)
                        //
                        if (!_gamePrisoner.HasVisited(_gamePrisoner.RoomLocationId))
                        {
                            if (_gamePrisoner.RoomLocationId == 9)
                            {
                                _gameConsoleView.DisplayGamePlayScreen("FREEDOM", "CONGRATULATIONS!!! You've done it! You survived the dungeon and managed to see the light of day again! \n" +
                                    "Press the Enter key to continue", ActionMenu.MainMenu, "");
                                _gameConsoleView.GetContinueKey();
                            }
                            else
                            {
                                _gameConsoleView.DisplayGamePlayScreen("Battle!", "During your travels you've encoutnered a monster! Please select an operation form the menu.", ActionMenu.BattleMenu, "");
                                ActionMenu.currentMenu = ActionMenu.CurrentMenu.BattleMenu;
                                travelerActionChoice = GetNextPlayerAction();
                                BattleAction(travelerActionChoice);

                                if (_gamePrisoner.Health <= 0)
                                {
                                    _gamePrisoner.RoomLocationId = currentLocationId;                                    
                                }
                            }
                            
                        }
                        else if (initiateBattle == 4 || initiateBattle == 2 || initiateBattle == 0)
                        {
                            if (_gamePrisoner.RoomLocationId == currentLocationId)
                            {

                            }
                            else
                            {
                                _gameConsoleView.DisplayGamePlayScreen("Battle!", "During your travels you've encoutnered a monster! Please select an operation form the menu.", ActionMenu.BattleMenu, "");
                                ActionMenu.currentMenu = ActionMenu.CurrentMenu.BattleMenu;
                                travelerActionChoice = GetNextPlayerAction();
                                BattleAction(travelerActionChoice);

                                if (_gamePrisoner.Health <= 0)
                                {
                                    _gamePrisoner.RoomLocationId = currentLocationId;
                                    _gamePrisoner.Health = 100;
                                }
                            }                         
                        }

                        //
                        // update current location
                        //
                        _currentLocation = _gameUniverse.GetRoomLocationById(_gamePrisoner.RoomLocationId);                        

                        //
                        // display game play screen with current location info and coordiantes
                        //
                        _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        _gameConsoleView.DisplayColoredText("", PlayerAction.ReturnToMainMenu, _currentLocation);
                        break;

                    case PlayerAction.LocationsVisited:
                        _gameConsoleView.DisplayLocationsVisited();
                        _gameConsoleView.DisplayColoredText("", travelerActionChoice, _currentLocation);                    
                        break;

                    case PlayerAction.AdminMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.AdminMenu;

                        _gameConsoleView.DisplayGamePlayScreen("Admin Menu", "Select an operation from the menu.", ActionMenu.AdminMenu, "");                        ;
                        break;

                    case PlayerAction.ReturnToMainMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;

                        _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        _gameConsoleView.DisplayColoredText("", travelerActionChoice, _currentLocation);
                        break;

                    case PlayerAction.Inventory:
                        _gameConsoleView.DisplayInventory();                        
                        break;

                    case PlayerAction.TreasureInventory:
                        _gameConsoleView.DisplayTreasureInventory();
                        _gameConsoleView.DisplayColoredObjects(travelerActionChoice, 0);
                        break;

                    case PlayerAction.MedicineInventory:
                        _gameConsoleView.DisplayMedicineInventory();
                        _gameConsoleView.DisplayColoredObjects(travelerActionChoice, 0);
                        break;

                    case PlayerAction.PickUp:
                        PickUpAction();                    
                        break;

                    case PlayerAction.PutDown:
                        PutDownAction();
                        break;

                    case PlayerAction.TalkTo:                       
                        TalkToAction();
                        break;

                    case PlayerAction.TradeWith:
                        TradeWithAction();
                        break;

                    case PlayerAction.Attack:
                        BattleAction(travelerActionChoice);
                        break;

                    case PlayerAction.Heal:
                        BattleAction(travelerActionChoice);
                        break;

                    case PlayerAction.Flee:
                        BattleAction(travelerActionChoice);
                        break;

                    case PlayerAction.Exit:
                        _gameConsoleView.DisplayClosingScreen();
                        _playingGame = false;
                        break;

                    default:
                        break;
                }
            }

            //
            // close the application
            //
            Environment.Exit(1);
        }

        /// <summary>
        /// initialize the player info
        /// </summary>
        private void InitializeMission()
        {
            Player prisoner = _gameConsoleView.GetInitialPrisonerInfo();

            //player information
            _gamePrisoner.Age = prisoner.Age;
            _gamePrisoner.Gender = prisoner.Gender;
            _gamePrisoner.Personality = prisoner.Personality;
            _gamePrisoner.Name = _gameConsoleView.GetPrisonerName(prisoner, PlayerAction.None, _currentLocation);
            _gamePrisoner.Weapon = (Weapon)_gameUniverse.GetObjectById(30, _gamePrisoner);
            _gamePrisoner.DamageOutput = 20 + _gamePrisoner.Weapon.Damage;

            #region ***STARTING INVENTORY

            #region --- potions(medicine) ---
            _gamePrisoner.RoomLocationId = 6;
            _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(60, _gamePrisoner));
            _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(60, _gamePrisoner));
            _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(60, _gamePrisoner));
            _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(64, _gamePrisoner));
            _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(62, _gamePrisoner));

            _gamePrisoner.RoomLocationId = 7;
            _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(61, _gamePrisoner));

            _gamePrisoner.RoomLocationId = 2;
            _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(63, _gamePrisoner));

            #endregion

            #endregion

            #region ***STARTING STATS

            _gamePrisoner.ExperiencePoints = 0;
            _gamePrisoner.Health = 1;
            _gamePrisoner.Lives = 3;
            _gamePrisoner.Level = 1;
            _gamePrisoner.ExpCap = 200;

            #endregion          

            // starting location
            _gamePrisoner.RoomLocationId = 1;

            //
            // echo the prisoner's info
            //
            _gameConsoleView.DisplayGamePlayScreen("Quest Setup - Complete", Text.InitializeMissionEchoPrisonerInfo(_gamePrisoner), ActionMenu.MissionIntro, "");
            _gameConsoleView.DisplayColoredText("", PlayerAction.None, _currentLocation);
            _gameConsoleView.GetContinueKey();
        }

        /// <summary>
        /// updates all the game's info/stats
        /// </summary>
        private void UpdateGameStatus()
        {
            bool levelUp = false;

            // chec to see if player picked up new weapon
            // re-calculate damage output based on player's weapon            
            _gamePrisoner.DamageOutput = 40 + _gamePrisoner.Weapon.Damage;
            
            //
            // reset all monster health points if necessary
            //
            foreach (var npc in _gameUniverse.Npcs)
            {
                if (npc is Monster)
                {
                    Monster monster = npc as Monster;
                    if (monster.DamageOutput == 10 || monster.DamageOutput == 15)
                    {
                        if (monster.HealthPoints < 100)
                        {
                            monster.HealthPoints = 100;
                        }
                    }
                    if (monster.DamageOutput == 20)
                    {
                        if (monster.HealthPoints < 125)
                        {
                            monster.HealthPoints = 125;
                        }
                    }
                    if (monster.DamageOutput == 20)
                    {
                        monster.HealthPoints = 250;
                    }
                    if (monster.DamageOutput == 35)
                    {
                        monster.HealthPoints = 350;
                    }
                    if (monster.DamageOutput == 45)
                    {
                        monster.HealthPoints = 450;
                    }
                }
            }

            //
            // if player runs out of lives, exit game
            //
            if (_gamePrisoner.Lives == 0)
            {
                _gameConsoleView.DisplayGamePlayScreen("GAME OVER", "You have ran out lives, you lose! The game will now close. " +
                    "Press the Enter key to continue.", ActionMenu.MissionIntro, "");
                _gameConsoleView.GetContinueKey();
                Environment.Exit(1);                
            }

            #region ----- update visited locations list -----

            if (!_gamePrisoner.HasVisited(_currentLocation.RoomLocationID))
            {
                //
                // add a new location to visited list
                //
                _gamePrisoner.RoomLocationsVisited.Add(_currentLocation.RoomLocationID);

                //
                // add experience points for visiting locations
                //
                _gamePrisoner.ExperiencePoints += _currentLocation.ExperiencePoints;
            }

            #endregion

            #region ----- update room accessibility -----

            #region ***FORGOTTEN VAULT

            if (_gameUniverse.Npcs[0].DialogueExhausted)
            {
                _gameUniverse.RoomLocations[1].Accessible = true;
            }
            else
            {
                _gameUniverse.RoomLocations[1].Accessible = false;

            }

            #endregion

            #region ***SACRED DEN

            if (_gameUniverse.GetNpcById(3).DialogueExhausted)
            {
                _gameUniverse.RoomLocations[2].Accessible = true;
            }
            else
            {
                _gameUniverse.RoomLocations[2].Accessible = false;

            }

            #endregion

            #region ***DARK CORRIDOR            

            if (_gameUniverse.GameObjects[24].RoomLocationId == 0)
            {
                _gameUniverse.RoomLocations[3].Accessible = true;
            }
            else
            {
                _gameUniverse.RoomLocations[3].Accessible = false;
            }

            #endregion

            #region ***SECRET ARMORY

            if (_gameUniverse.GameObjects[40].RoomLocationId == 0)
            {
                _gameUniverse.RoomLocations[4].Accessible = true;
            }
            else
            {
                _gameUniverse.RoomLocations[4].Accessible = false;
            }

            #endregion

            #region ***CAVERN OF LIFE

            if (_gameUniverse.RoomLocations[4].Accessible)
            {
                _gameUniverse.RoomLocations[6].Accessible = true;
            }
            else
            {
                _gameUniverse.RoomLocations[6].Accessible = false;
            }

            #endregion

            #region ***ROOM OF SILIENCE

            ISpeak gilbertThebrave;
            gilbertThebrave = _gameUniverse.GetNpcById(10) as ISpeak;

            if (gilbertThebrave.Riddle == "")
            {
                _gameUniverse.RoomLocations[5].Accessible = true;
            }
            else
            {
                _gameUniverse.RoomLocations[5].Accessible = false;
            }

            #endregion

            #region ***PORTAL ROOM

            Key treasure = (Key)_gameUniverse.GetObjectById(45, _gamePrisoner);

            if (treasure.RoomLocationId == 6)
            {
                _gameUniverse.RoomLocations[7].Accessible = true;
            }
            else
            {
                _gameUniverse.RoomLocations[7].Accessible = false;

            }

            #endregion

            #region ***FREEDOM

            if (_gameUniverse.GetNpcById(98).HealthPoints == 0 || _gamePrisoner.HasVisited(8))
            {
                _gameUniverse.RoomLocations[8].Accessible = true;
            }
            else
            {
                _gameUniverse.RoomLocations[8].Accessible = false;
            }

            #endregion

            #endregion

            #region ----- update experience points -----

            #region ***EXP FOR OBJECTS

            //
            // add exp. points when the player picks up a specific game object
            //
            foreach (GameObject gameObject in _gamePrisoner.TreasureInventory)
            {
                #region ***TREASURE

                if (gameObject.Id == 11 && gameObject.HasBeenPickedUp == false)
                {
                    _gamePrisoner.ExperiencePoints += 25;
                    gameObject.HasBeenPickedUp = true;
                }

                if (gameObject.Id == 12 && gameObject.HasBeenPickedUp == false)
                {
                    _gamePrisoner.ExperiencePoints += 50;
                    gameObject.HasBeenPickedUp = true;
                }

                if (gameObject.Id == 13 && gameObject.HasBeenPickedUp == false)
                {
                    _gamePrisoner.ExperiencePoints += 75;
                    gameObject.HasBeenPickedUp = true;
                }

                if (gameObject.Id == 14 && gameObject.HasBeenPickedUp == false)
                {
                    _gamePrisoner.ExperiencePoints += 100;
                    gameObject.HasBeenPickedUp = true;
                }

                #endregion                
            }

            #endregion

            #endregion

            #region ----- update player stats/level up -----            

            if (_gamePrisoner.Health == 0)
            {
                _gamePrisoner.Lives -= 1;
                _gamePrisoner.Health = 100;
            }

            if (_gamePrisoner.ExperiencePoints > _gamePrisoner.ExpCap)
            {
                levelUp = true;
                _gamePrisoner.Health = 100;
                _gamePrisoner.DamageOutput += 2;
                _gamePrisoner.Level += 1;
                _gamePrisoner.Lives += 1;
                _gamePrisoner.ExpCap += _gamePrisoner.ExpCap * 2;

                _gamePrisoner.RoomLocationId = 6;
                _gamePrisoner.MedicinePouch.Add((Medicine)_gameUniverse.GetObjectById(64, _gamePrisoner));
                _gamePrisoner.RoomLocationId = _currentLocation.RoomLocationID;
            }

            if (levelUp)
            {
                _gameConsoleView.DisplayGamePlayScreen("LEVEL UP!", "You've leveled up! You are now permanently stronger, faster, and more resilient!" + 
                    " \n\tPress the Enter key to continue.", ActionMenu.MainMenu, "");
                _gameConsoleView.GetContinueKey();
                ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                _gameConsoleView.DisplayColoredText("", PlayerAction.LookAround, _currentLocation);
            }     

            #endregion
        }

        /// <summary>
        /// allows player to look at specific game objects
        /// </summary>
        private void LookAtAction()
        {
            //
            // display a list of game objects in islad location and player choice
            //
            int gameObjectToLookAtId = _gameConsoleView.DisplayGetGameObjectsToLookAt();
            _gameConsoleView.DisplayColoredObjects(PlayerAction.LookAt, 0);

            //
            // display game object info
            //
            if (gameObjectToLookAtId != 0)
            {
                //
                // get the game object form the universe
                //
                GameObject gameObject = _gameUniverse.GetObjectById(gameObjectToLookAtId, _gamePrisoner);

                //
                // display information for the chosen object
                //
                _gameConsoleView.DisplayGameObjectInfo(gameObject);
                _gameConsoleView.DisplayColoredObjects(PlayerAction.LookAt, 999);
            }
        }

        /// <summary>
        /// allows player to pick up a game object and add it to their inventory
        /// </summary>
        private void PickUpAction()
        {
            //
            // display list of game objects and gets the player's choice
            //
            int gameObjectToPickUpId = _gameConsoleView.DisplayGetGameObjectToPickUp();

            //
            // add the game object to the player's inventory
            //
            if (gameObjectToPickUpId != 0)
            {
                //
                // get game object from the universe
                //
                GameObject gameObject = _gameUniverse.GetObjectById(gameObjectToPickUpId, _gamePrisoner);

                //
                // add game object to player's inventory or treasure inventory or med pouch
                // and set location Id to 0
                //
                if (gameObject is Treasure)
                {
                    _gamePrisoner.TreasureInventory.Add(gameObject as Treasure);
                    gameObject.RoomLocationId = 0;
                }
                else if (gameObject is Medicine)
                {
                    _gamePrisoner.MedicinePouch.Add(gameObject as Medicine);
                    gameObject.RoomLocationId = 0;
                }
                else if (gameObject is Weapon)
                {                    
                    _gamePrisoner.Weapon = gameObject as Weapon;
                    _gamePrisoner.Inventory.Add(gameObject);
                    gameObject.RoomLocationId = 0;
                }
                else if (gameObject is Food)
                {
                    _gamePrisoner.Inventory.Add(gameObject);
                    gameObject.RoomLocationId = 0;
                }
                else
                {
                    _gamePrisoner.Inventory.Add(gameObject);
                    gameObject.RoomLocationId = 0;
                }

                //
                // display confirmation message
                //
                _gameConsoleView.DisplayConfirmGameObjectAddedToInvetory(gameObject);
            }
        }

        /// <summary>
        /// allows player to put down a game object and remove it from their inventory
        /// </summary>
        private void PutDownAction()
        {
            //
            // display list of game objects in the player's inventory and gets the player's choice
            //
            int inventoryObjectToRemoveId = _gameConsoleView.DisplayGetGameObjectToPutDown();

            //
            // get game object from the universe
            //
            GameObject gameObject = _gameUniverse.GetObjectById(inventoryObjectToRemoveId, _gamePrisoner);

            //
            // remove game object from player's inventory
            // and set location Id to current location
            //
            if (gameObject is Treasure)
            {
                _gamePrisoner.TreasureInventory.Remove(gameObject as Treasure);
                gameObject.RoomLocationId = _gamePrisoner.RoomLocationId;
            }
            else if (gameObject is Medicine)
            {
                _gamePrisoner.MedicinePouch.Remove(gameObject as Medicine);
                gameObject.RoomLocationId = _gamePrisoner.RoomLocationId;
            }
            else if (gameObject is Weapon)
            {
                _gamePrisoner.Inventory.Remove(gameObject);
                gameObject.RoomLocationId = _gamePrisoner.RoomLocationId;
            }
            else
            {
                _gamePrisoner.Inventory.Remove(gameObject);
                gameObject.RoomLocationId = _gamePrisoner.RoomLocationId;
            }

            //
            // display confirmation message
            //
            _gameConsoleView.DisplayConfirmGameObjectRemovedFromInvetory(gameObject);            
        }

        /// <summary>
        /// allows player to talk to a specific NPC
        /// </summary>
        private void TalkToAction()
        {
            //
            // display a list of NPCs in the curren location
            // and get the player's choice
            //
            int npcToTalkToId = _gameConsoleView.DisplayGetNpcToTalkTo();

            //
            // display NPC's message
            //
            if (npcToTalkToId != 0)
            {
                //
                // get the NPC from the universe
                //
                Npc npc = _gameUniverse.GetNpcById(npcToTalkToId);

                //
                // display information for the object chosen
                //
                _gameConsoleView.DisplayTalkTo(npc);
            }
        }

        /// <summary>
        /// allows player to trade with specific NPCs
        /// </summary>
        private void TradeWithAction()
        {
            //
            // display a list of NPCs in the current location
            // and get the player's choice
            //
            int npcToTradeWithId = _gameConsoleView.DisplayGetNpcToTradeWith();

            //
            // display NPC's message
            //
            if (npcToTradeWithId != 0)
            {
                //
                // get the NPC from the universe
                //
                Npc npc = _gameUniverse.GetNpcById(npcToTradeWithId);

                //
                // get valid object id in the NPC's inventory
                //
                _gameConsoleView.DisplayTradeWith(_gameConsoleView.DisplayGetValidObjectIdToTrade(npc), 0, npc);
            }
        }

        /// <summary>
        /// allows player to battle random enemies
        /// </summary>
        /// <param name="choosenAction"></param>
        private void BattleAction(PlayerAction choosenAction)
        {
            bool escapeAttempt = false;
            bool battling = true;
            int cursorPosition = 4;
            Monster monster = new Monster();

            if (_gamePrisoner.RoomLocationId == 3 && !_gamePrisoner.HasVisited(3))
            {
                monster = (Monster)_gameUniverse.GetNpcById(96);
            }
            else if (_gamePrisoner.RoomLocationId == 7 && !_gamePrisoner.HasVisited(7))
            {
                monster = (Monster)_gameUniverse.GetNpcById(97);
            }
            else if (_gamePrisoner.RoomLocationId == 8 && !_gamePrisoner.HasVisited(8))
            {
                monster = (Monster)_gameUniverse.GetNpcById(98);
            }
            else
            {
            monster = _gameUniverse.GetMonsterToBattle(_gamePrisoner);
            }

            _gameConsoleView.DisplayGamePlayScreen($"Battling {monster.Name}", "", ActionMenu.BattleMenu, "");
            _gameConsoleView.ClearStatusBox();

            while (battling)
            {
                cursorPosition++;                

                if (cursorPosition > 23)
                {
                    for (cursorPosition = 5; cursorPosition  <= 23; cursorPosition++)
                    {
                        Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                        Console.Write($"                                                                                                    ");
                    }

                    cursorPosition = 5;
                }

                switch (choosenAction)
                {
                    case PlayerAction.Attack:

                        _gameConsoleView.DisplayAttack(monster, cursorPosition);
                        _gameConsoleView.DisplayBattleStats(monster);

                        #region --- check if monster health = 0 ---

                        if (monster.HealthPoints <= 0)
                        {
                            break;
                        }

                        #endregion

                        #region --- stopwatch ---

                        // code for stopwatch taken from: https://www.dotnetperls.com/sleep
                        Console.CursorVisible = false;
                        var stopwatch = Stopwatch.StartNew();
                        Thread.Sleep(1000);
                        stopwatch.Stop();

                        #endregion

                        cursorPosition++;
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

                        _gameConsoleView.DisplayMonsterAttack(monster, cursorPosition);
                        _gameConsoleView.DisplayBattleStats(monster);
                        break;

                    case PlayerAction.Heal:

                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.HealMenu;
                        _gameConsoleView.DisplayHeal(monster, cursorPosition);
                        _gameConsoleView.DisplayBattleStats(monster);

                        #region --- stopwatch ---

                        // code for stopwatch taken from: https://www.dotnetperls.com/sleep
                        Console.CursorVisible = false;
                        stopwatch = Stopwatch.StartNew();
                        Thread.Sleep(1000);
                        stopwatch.Stop();

                        #endregion

                        cursorPosition++;
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

                        _gameConsoleView.DisplayMonsterAttack(monster, cursorPosition);
                        _gameConsoleView.DisplayBattleStats(monster);
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.BattleMenu;
                        _gamePrisoner.RoomLocationId = _currentLocation.RoomLocationID;
                        break;
                    case PlayerAction.Flee:

                        // monster is a boss prevent flee attmept
                        if (monster is Boss)
                        {                  
                            _gameConsoleView.DisplayBattleStats(monster);
                            #region --- reset cursor position if it extend beyon message box ---

                            if (cursorPosition > 23)
                            {
                                for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                                {
                                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                    Console.Write($"                                                                                                    ");
                                }

                                cursorPosition = 5;
                            }

                            #endregion
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"You cannot flee from this battle!");
                            #region --- stopwatch ---

                            // code for stopwatch taken from: https://www.dotnetperls.com/sleep
                            Console.CursorVisible = false;
                            stopwatch = Stopwatch.StartNew();
                            Thread.Sleep(1000);
                            stopwatch.Stop();

                            #endregion

                            cursorPosition++;
                            #region --- reset cursor position if it extends beyond message box ---

                            if (cursorPosition > 23)
                            {
                                // this code will clear the "battle feed"
                                for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                                {
                                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, 
                                    ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                    Console.Write($"                                                                                                    ");
                                }

                                cursorPosition = 5;
                            }

                            #endregion

                            _gameConsoleView.DisplayMonsterAttack(monster, cursorPosition);
                            _gameConsoleView.DisplayBattleStats(monster);
                            break;
                        }                        
                        // temp location id used to re-assign player's location id
                        int tempLocaitonId = _gamePrisoner.RoomLocationId;
                        
                        // display battle stats
                        _gameConsoleView.DisplayBattleStats(monster);

                        // determine if the outomce of the flee attmept and return bool
                        escapeAttempt = _gameConsoleView.DisplayFlee(monster, cursorPosition);                        
                        if (escapeAttempt)
                        {
                            _gameConsoleView.DisplayBattleStats(monster);

                            #region --- stopwatch ---

                            // code for stopwatch taken from: https://www.dotnetperls.com/sleep
                            Console.CursorVisible = false;
                            stopwatch = Stopwatch.StartNew();
                            Thread.Sleep(1000);
                            stopwatch.Stop();

                            #endregion

                            cursorPosition++;
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

                            battling = false;
                        }
                        else
                        {
                            #region --- stopwatch ---

                            // code for stopwatch taken from: https://www.dotnetperls.com/sleep
                            Console.CursorVisible = false;
                            stopwatch = Stopwatch.StartNew();
                            Thread.Sleep(1000);
                            stopwatch.Stop();

                            #endregion

                            cursorPosition++;
                            cursorPosition++;
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

                            _gameConsoleView.DisplayMonsterAttack(monster, cursorPosition);
                            _gameConsoleView.DisplayBattleStats(monster);
                        }

                        // re-assign player's locatio id using temporary int
                        _gamePrisoner.RoomLocationId = tempLocaitonId;
                        break;

                    default:
                        break;
                }

                // determine outcome of battle if valid
                if (_gamePrisoner.Health <= 0)
                {
                    cursorPosition++;
                    #region --- reset cursor position if it extend beyon message box ---

                        if (cursorPosition > 23)
                        {
                            for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                            {
                                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                                Console.Write($"                                                                                                    ");
                            }

                            cursorPosition = 5;
                        }

                        #endregion
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"Oh no! You were vanquished by the mighty {monster.Name}! You have been returned to your previous location.");

                    cursorPosition++;
                    #region --- reset cursor position if it extend beyon message box ---

                    if (cursorPosition > 23)
                    {
                        for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.Write($"                                                                                                        ");
                        }

                        cursorPosition = 5;
                    }

                    #endregion
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                    Console.Write("Press the Enter key to continue.");
                    _gameConsoleView.GetContinueKey();
                    ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                    _gamePrisoner.Lives -= 1;
                    battling = false;
                    break;
                }
                else if (monster.HealthPoints <= 0)
                {
                    _gamePrisoner.ExperiencePoints += monster.ExperiencePoints;

                    if (monster is Boss && monster.Name == "Crystal Troll")
                    {
                        _gamePrisoner.TreasureInventory.Add((Treasure)_gameUniverse.GetObjectById(13, _gamePrisoner));
                    }

                    cursorPosition++;
                    #region --- reset cursor position if it extend beyon message box ---

                    if (cursorPosition > 23)
                    {
                        for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.Write($"                                                                                                    ");
                        }

                        cursorPosition = 5;
                    }

                    #endregion
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"Victory! You have defeated the {monster.Name}! You keep moving towards your next destination.");

                    cursorPosition++;
                    #region --- reset cursor position if it extend beyon message box ---

                    if (cursorPosition > 23)
                    {
                        for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.Write($"                                                                                                    ");
                        }

                        cursorPosition = 5;
                    }

                    #endregion
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                    Console.Write("Press the Enter key to continue.");
                    _gameConsoleView.GetContinueKey();
                    ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                    battling = false;
                    break;
                }
                else if (escapeAttempt)
                {
                    _gamePrisoner.ExperiencePoints += monster.ExperiencePoints / 2;

                    cursorPosition++;
                    #region --- reset cursor position if it extend beyon message box ---

                    if (cursorPosition > 23)
                    {
                        for (cursorPosition = 5; cursorPosition <= 23; cursorPosition++)
                        {
                            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                            Console.Write($"                                                                                                    ");
                        }

                        cursorPosition = 5;
                    }

                    #endregion
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + cursorPosition);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Press the Enter key to continue.");
                    _gameConsoleView.GetContinueKey();
                    ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                    battling = false;
                    break;
                }

                // get next move from player
                Console.ForegroundColor = ConsoleColor.Black;
                choosenAction = GetNextPlayerAction();
            }
        }

        /// <summary>
        /// gets the action from the player
        /// </summary>
        private PlayerAction GetNextPlayerAction()
        {
            PlayerAction playerActionChoice = new PlayerAction();

            switch (ActionMenu.currentMenu)
            {
                case ActionMenu.CurrentMenu.MainMenu:
                    playerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.MainMenu);
                    break;

                case ActionMenu.CurrentMenu.ObjectMenu:
                    playerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.ObjectMenu);
                    break;

                case ActionMenu.CurrentMenu.NpcMenu:
                    playerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.NpcMenu);
                    break;

                case ActionMenu.CurrentMenu.PlayerMenu:
                    playerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.PlayerMenu);
                    break;

                case ActionMenu.CurrentMenu.AdminMenu:
                    playerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.AdminMenu);
                    break;

                case ActionMenu.CurrentMenu.TradeMenu:
                    playerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.TradeMenu);
                    break;

                case ActionMenu.CurrentMenu.BattleMenu:
                    playerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.BattleMenu);
                    break;

                default:
                    break;
            }

            return playerActionChoice;
        }

        /// <summary>
        /// event handler used when player picks up food objects
        /// </summary>
        private void HandleObjectAddedToInventory(object gameObject, EventArgs e)
        {
            GameObject objectPickedUp = gameObject as GameObject;

            switch (objectPickedUp.Type)
            {
                case GameObject.ObjectType.Food:                   
                    _gamePrisoner.Health += (int)objectPickedUp.Value;

                    //
                    // add life if health greater than 100
                    //
                    if (_gamePrisoner.Health > 100)
                    {
                        _gamePrisoner.Health = 100;
                        _gamePrisoner.Lives += 1;
                    }

                    //
                    // remove from the game
                    //
                    if (objectPickedUp.IsConsumable)
                    {
                        objectPickedUp.RoomLocationId = -1;
                    }
                    break;

                default:
                    break;
            }            
        }
    }
}
#endregion