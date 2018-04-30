using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    interface ISpeak
    {
        List<string> Messages { get; set; }
        string Riddle { get; set; }

        string Speak();
    }
}
