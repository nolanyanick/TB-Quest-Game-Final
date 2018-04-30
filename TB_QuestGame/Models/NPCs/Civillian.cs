using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Civillian : Npc, ISpeak
    {
        //////////////////////////////////
        // CLASS FOR DIALOGUE ONLY NPCs //
        //////////////////////////////////

        public override int Id { get; set; }
        public override string Description { get; set; }
        public override int ExperiencePoints { get; set; }
        public List<string> Messages { get; set; }
        public string Riddle { get; set; }
        public override double HealthPoints { get; set; }
        public override NpcType Type { get; set; }

        #region METHODS

        public string Speak()
        {
            if (this.Messages != null)
            {
                return GetMessage();
            }
            else
            {
                return $"My name is {base.Name}, have a nice day.";
            }
        }

        private string GetMessage()
        {
            if (this.Name == "Prisoner")
            {
                Random random = new Random();
                int messageIndex = random.Next(0, Messages.Count);
                return Messages[messageIndex];
            }

            if (this.Name == "Bernard Ebrhard" && this.DialogueExhausted)
            {
                return "Tread carefully. I wouldn't want yet another corpse acting as a tripping hazard for me.";
            }

            return Messages[0];
        }

        #endregion
    }
}
