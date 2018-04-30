using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// the character class the player uses in the game
    /// </summary>
    public class Player : Character
    {
        #region ENUMERABLES



        #endregion

        #region FIELDS

        private int _age;
        private int _health;
        private int _lives;
        private int _experiencePoints;
        private double _damageOutput;  
        private int _level;
        private int _expCap;
        private Weapon _weapon;
        private List<int> _roomLocationsVisited;
        private List<GameObject> _inventory;
        private List<Medicine> _medicinePouch;
        private List<Treasure> _treasureInventory;

        // *NOTE: field, property pair(InidividualGameObject), used to help with game flow
        private int _individualGameObject;

        #endregion

        #region PROPERTIES

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = value; }
        }

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public List<int> RoomLocationsVisited
        {
            get { return _roomLocationsVisited; }
            set { _roomLocationsVisited = value; }
        }

        public List<GameObject> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public List<Medicine> MedicinePouch
        {
            get { return _medicinePouch; }
            set { _medicinePouch = value; }
        }

        public int IndividualGameObject
        {
            get { return _individualGameObject; }
            set { _individualGameObject = value; }
        }

        public List<Treasure> TreasureInventory
        {
            get { return _treasureInventory; }
            set { _treasureInventory = value; }
        }

        public double DamageOutput
        {
            get { return _damageOutput; }
            set { _damageOutput = value; }
        }

        public int ExpCap
        {
            get { return _expCap; }
            set { _expCap = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public Weapon Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Player()
        {
            _roomLocationsVisited = new List<int>();
            _inventory = new List<GameObject>();
            _treasureInventory = new List<Treasure>();
            _medicinePouch = new List<Medicine>();
        }

        public Player(string name, GenderType gender, int roomLocationID) : base(name, gender, roomLocationID)
        {
            _roomLocationsVisited = new List<int>();
            _inventory = new List<GameObject>();
            _treasureInventory = new List<Treasure>();
            _medicinePouch = new List<Medicine>();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// sets player's greeting based on gender and personlity type
        /// </summary>        
        public override string Greeting()
        {
            if (base.Personality)
            {
                return $"Hello! my name is {base.Name}, nice to meet you!";
            }
            else
            {
                if (base.Gender == GenderType.MALE)
                {
                    return "You're a waste of space, but lets talk.";
                }
                else if (base.Gender == GenderType.FEMALE)
                {
                    return "We need to speak, but don't waste my time.";
                }
                else
                {
                    return "As much as I don't want to, we gotta talk.";
                }
            }
        }
               
        /// <summary>
        /// determines if the player has visited the room location
        /// </summary>     
        public bool HasVisited(int _roomLocationID)
        {
            if (RoomLocationsVisited.Contains(_roomLocationID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        #endregion
    }
}
            
