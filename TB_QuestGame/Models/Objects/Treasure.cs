using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Treasure : GameObject
    {
        #region ENUMS

        public enum RarityLevel
        {
            Common,
            Unique,
            Rare,
            Legendary
        }

        #endregion

        #region FIELDS

        
        private RarityLevel _rarityLevel;

        public override int Id { get; set; }
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override int RoomLocationId { get; set; }
        public override bool CanInventory { get; set; }
        public override bool IsConsumable { get; set; }
        public override bool IsVisible { get; set; }
        public override double Value { get; set; }
        public override bool HasBeenPickedUp { get; set; }
        public override ObjectType Type { get; set; }
        public override bool Consumed { get; set; }
        public override int TradingObjectId { get; set; }

        #endregion

        #region PROPERTIES

        public RarityLevel Rarity
        {
            get { return _rarityLevel; }
            set { _rarityLevel = value; }
        }

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        #endregion
    }
}
