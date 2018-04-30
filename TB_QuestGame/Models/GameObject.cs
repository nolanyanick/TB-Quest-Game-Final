using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public abstract class GameObject
    {
        public enum ObjectType
        {
            Food,
            Treasure,
            Weapon,
            Key,
            Medicine
        }

        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public abstract int RoomLocationId { get; set; }
        public abstract bool CanInventory { get; set; }
        public abstract bool IsConsumable { get; set; }
        public abstract bool IsVisible { get; set; }
        public abstract double Value { get; set; }
        public abstract bool HasBeenPickedUp { get; set; }
        public abstract ObjectType Type { get; set; }
        public abstract bool Consumed { get; set; }

        public abstract int TradingObjectId { get; set;}
    }
}
