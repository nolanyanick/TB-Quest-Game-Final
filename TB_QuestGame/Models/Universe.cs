using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// class of the game map
    /// </summary>
    public class Universe
    {
        #region lists to be maintianed by the Universe Class
        
        private List<RoomLocation> _roomLocations;
        private List<GameObject> _gameObjects;
        private List<Npc> _npcs;

        #endregion

        #region properties of lists

        public List<RoomLocation> RoomLocations
        {
            get { return _roomLocations; }
            set { _roomLocations = value; }
        }

        public List<GameObject> GameObjects
        {
            get { return _gameObjects; }
            set { _gameObjects = value; }
        }

        public List<Npc> Npcs
        {
            get { return _npcs; }
            set { _npcs = value; }
        }
    
        #endregion

        #region CONSTRUCTORS
        //
        // default Universe constructor
        //
        public Universe()
        {
            //
            // add all of the universe objects to the game
            // 
            InitializeUniverse();
        }
        #endregion

        #region METHODS

        /// <summary>
        /// initialize the universe with all of the room locations
        /// </summary>
        private void InitializeUniverse()
        {
            _roomLocations = UniverseObjects.RoomLocations;
            _gameObjects = UniverseObjects.GameObjects;
            _npcs = UniverseObjects.Npcs;
        }

        /// <summary>
        /// determines if the user selected a valid location ID
        /// </summary>
        public bool IsValidRoomLocationId(int roomLocationId)
        {
            List<int> roomLocationIds = new List<int>();

            //
            // create a list of room ids
            // 
            foreach (RoomLocation room in _roomLocations)
            {
                roomLocationIds.Add(room.RoomLocationID);
            }

            //
            // determine if the room id is valid and return that value
            //
            if (roomLocationIds.Contains(roomLocationId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// determines if the object in question is valid
        /// </summary>        
        public bool IsValidGameObjectByLocaitonId(int gameObjectId, int currentRoomLocation)
        {
            List<int> gameObjectIds = new List<int>();

            //
            // creates a list of game object ids in current location
            //
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.RoomLocationId == currentRoomLocation)
                {
                    gameObjectIds.Add(gameObject.Id);
                }
            }

            //
            // determines if the game object id is valid and returns the result
            //
            if (gameObjectIds.Contains(gameObjectId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// determines if the selected object is in the NPC's inventory
        /// </summary>
        public bool IsValidObjectByNpcInventoryId(int gameObjectId, ITrade trader)
        {        
            //
            // determines if the game object id is valid and returns the result
            //
            if ( trader.InventoryIds.Contains(gameObjectId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// determines if npc in question is valid
        /// </summary>
        public bool IsValidNpcByLocationId(int npcId, int currentSpacetiemLocation)
        {
            List<int> npcIds = new List<int>();

            //
            // create a list of npcs in the current location
            //
            foreach (var npc in _npcs)
            {
                if (npc.RoomLocationId == currentSpacetiemLocation)
                {
                    npcIds.Add(npc.Id);
                }
            }

            //
            // determine if the game object id is a valid id and return the reslult
            //
            if (npcIds.Contains(npcId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// gets an NPC via their Id
        /// </summary>
        public Npc GetNpcById(int Id)
        {
            Npc npcToReturn = null;

            //
            // run through the NPC object list and grab the correct one
            //
            foreach (var npc in _npcs)
            {
                if (npc.Id == Id)
                {
                    npcToReturn = npc;
                }
            }

            //
            // the specified Id was notfound in the universe
            // thow an exception
            //
            if (npcToReturn == null)
            {
                string feedbackMessage = $"The NPC ID {Id} does not exsist in the current universe.";
                throw new ArgumentException(feedbackMessage, Id.ToString());
            }

            return npcToReturn;
        }

        /// <summary>
        /// gets a list of NPCs in the current location
        /// </summary>
        public List<Npc> GetNpcsByRoomLocation(int isandLocationId)
        {
            List<Npc> npcs = new List<Npc>();

            //
            // run through the NPC object list and grab all that are in the current location
            //
            foreach (var npc in _npcs)
            {
                if (npc.RoomLocationId == isandLocationId)
                {
                    npcs.Add(npc);
                }
            }

            return npcs;
        }

        /// <summary>
        /// gets a list of traders in the current location
        /// </summary>
        public List<Trader> GetTraderNpcsByRoomLocation(int isandLocationId)
        {
            List<Trader> npcs = new List<Trader>();

            //
            // run through the NPC object list and grab all Traders that are in the current location
            //
            foreach (Npc npc in _npcs)
            {
                if (npc.RoomLocationId == isandLocationId && npc is Trader)
                {
                    npcs.Add((Trader)npc);
                }
            }

            return npcs;
        }

        /// <summary>
        /// gets an object via its id
        /// </summary>
        public GameObject GetObjectById(int id, Player prisoner)
        {
            GameObject gameObjectToReturn = null;
            int indexer = 0;

            //
            // run through the game object list and grab the correct one
            //
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject is Medicine)
                {
                    if (gameObject.Id == id && indexer <= 0 && gameObject.RoomLocationId == prisoner.RoomLocationId)
                    {
                        indexer++;
                        gameObjectToReturn = gameObject;
                    }
                }
                else if (gameObject.Id == id)
                {
                    gameObjectToReturn = gameObject;
                }
            }

            //
            // if the specified id was not found in the universe
            // throw an exception
            //
            if (gameObjectToReturn == null)
            {
                string feedbackMessage = $"The Game Object ID, {id}, does not exsist in the current universe.";
                throw new ArgumentException(feedbackMessage, id.ToString());
            }

            return gameObjectToReturn;
        }

        /// <summary>
        /// gets all game objects in the current location
        /// </summary>
        public List<GameObject> GetGameObjectsByRoomLocaitonId(int roomLocationId)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            //
            // shift throught the game object list
            // and grab all that are in the current location
            //
            foreach (GameObject gameObject in _gameObjects)            
            {
                if (gameObject.RoomLocationId == roomLocationId)
                {
                    gameObjects.Add(gameObject);
                }
            }

            return gameObjects;
        }

        /// <summary>
        /// gets a room location ID choosen from the user
        /// </summary>
        public RoomLocation GetRoomLocationById(int id)
        {
            RoomLocation roomLocation = null;

            //
            // shift through roomLocation list and select correct one
            //
            foreach (RoomLocation location in _roomLocations)
            {
                if (location.RoomLocationID == id)
                {
                    roomLocation = location;
                }            
            }

            //
            // if the ID is not found wihtin the universe,
            // throw an exception
            //
            if (roomLocation == null)
            {
                string feedbackMessage = $"The Room Location ID, {id}, does not exist on the current Map.";
                throw new ArgumentException(id.ToString(), feedbackMessage);
            }

            return roomLocation;
        }

        /// <summary>
        /// determines if the choosen location is accessible or not
        /// </summary>
        public bool IsAccessibleLocation(int roomLocationId)
        {
            RoomLocation roomLocation = GetRoomLocationById(roomLocationId);

            if (roomLocation.Accessible == true)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        /// <summary>
        /// gets the maximum location ID
        /// </summary>
        public int GetMaxRoomLocationId()
        {
            int MaxId = 0;

            foreach (RoomLocation roomLocation in RoomLocations)
            {
                if (roomLocation.RoomLocationID > MaxId)
                {
                    MaxId = roomLocation.RoomLocationID;
                }
            }

            return MaxId;
        }

        /// <summary>
        /// gets an appropriate monster to battle
        /// </summary>
        public Monster GetMonsterToBattle(Player prisoner)
        {
            List<Monster> listOfAllMonsters = new List<Monster>();
            Monster monsterToDoBattle = null;

            //
            // get a list of monsters from the Npcs list
            //
            foreach (var npc in Npcs)
            {
                if (npc is Boss)
                {

                }
                else if (npc is Monster)
                {
                    listOfAllMonsters.Add(npc as Monster);                    
                }
            }


            Random random = new Random();
            int randomNumber = random.Next(listOfAllMonsters.Count);
            monsterToDoBattle = listOfAllMonsters[randomNumber];

            return monsterToDoBattle;








            //if (prisoner.DamageOutput <= 15)
            //{
            //    //temp list of Monsters to be used for random selection
            //    List<Monster> tempMonsterList = new List<Monster>();

            //    foreach (var monster in listOfAllMonsters)
            //    {
            //        if (monster.DamageOutput == 10)
            //        {
            //            tempMonsterList.Add(monster);
            //        }
            //    }

            //}
            //else if (prisoner.DamageOutput <= 25)
            //{
            //    //temp list of Monsters to be used for random selection
            //    List<Monster> tempMonsterList = new List<Monster>();

            //    foreach (var monster in listOfAllMonsters)
            //    {
            //        if (monster.DamageOutput == 15)
            //        {
            //            tempMonsterList.Add(monster);
            //        }
            //    }

            //    Random random = new Random();
            //    int randomNumber = random.Next(tempMonsterList.Count);
            //    monsterToDoBattle = tempMonsterList[randomNumber];
            //}
            //else if (prisoner.DamageOutput <= 40)
            //{
            //    //temp list of Monsters to be used for random selection
            //    List<Monster> tempMonsterList = new List<Monster>();

            //    foreach (var monster in listOfAllMonsters)
            //    {
            //        if (monster.DamageOutput == 20)
            //        {
            //            tempMonsterList.Add(monster);
            //        }
            //    }

            //    Random random = new Random();
            //    int randomNumber = random.Next(tempMonsterList.Count);
            //    monsterToDoBattle = tempMonsterList[randomNumber];
            //}
            //else
            //{            
            //    Random random = new Random();
            //    int randomNumber = random.Next(listOfAllMonsters.Count);
            //    monsterToDoBattle = listOfAllMonsters[randomNumber];
            //}        
        }

        /// <summary>
        /// gets a random integer from a list
        /// </summary>
        public int GetRandomInteger()
        {
            Random random = new Random();
            List<int> listOfIntegers = new List<int>()
            {
                1,
                2,
                3,
                4,
                5
            };

            int randomNumber = random.Next(listOfIntegers.Count); //create a variable that will select a random name from the "names" list                    
            
            return randomNumber;
        }
        
        #endregion
    }
}

