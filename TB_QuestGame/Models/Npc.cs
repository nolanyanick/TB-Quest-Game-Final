using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public abstract class Npc : Character
    {
        public enum NpcType
        {
            Civillian,
            Trader,
            Monster,
            Boss
        }
        public abstract int Id { get; set; }
        public abstract string Description { get; set; }
        public bool DialogueExhausted { get; set; }
        public abstract int ExperiencePoints { get; set; }
        public abstract double HealthPoints { get; set; }
        public abstract NpcType Type { get; set; }
    }
}
