using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    class Boss : Monster
    {
        public override int Id { get; set; }
        public override string Description { get; set; }
        public override int ExperiencePoints { get; set; }
        public override double HealthPoints { get; set; }
    }
}
