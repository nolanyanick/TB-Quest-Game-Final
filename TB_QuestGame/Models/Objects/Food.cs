using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class  Food : GameObject
    {

        #region ENUMS




        #endregion

        #region FIELDS

        private bool _isSpoiled;
        private int _healthPoints;
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override string Description { get; set; }       
        public override bool CanInventory { get; set; }
        public override bool IsConsumable { get; set; }
        public override bool IsVisible { get; set; }
        public override double Value { get; set; }
        public override bool HasBeenPickedUp { get; set; }
        public override bool Consumed { get; set; }
        public override ObjectType Type { get; set; }
        public override int TradingObjectId { get; set; }
        public string PickUpMessage { get; set; }

        private int _RoomLocationId;

        public override int RoomLocationId
        {
            get
            {
                return _RoomLocationId;
            }
            set
            {
                _RoomLocationId = value;
                if (value == 0)
                {
                    OnObjectAddedToIneventory();
                }
            }
        }
        #endregion

        #region PROPERTIES

        public bool IsSpoiled
        {
            get { return _isSpoiled; }
            set { _isSpoiled = value; }
        }

        public int HealthPoints
        {
            get { return _healthPoints; }
            set { _healthPoints = value; }
        }

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        #endregion

        public event EventHandler ObjectAddedToInventory;

        public void OnObjectAddedToIneventory()
        {
            if (ObjectAddedToInventory != null)
            {
                ObjectAddedToInventory(this, EventArgs.Empty);
            }
        }





    }
}
