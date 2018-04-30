using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    class Program
    {
        /// <summary>
        /// instantiate the game controller, passing all control to the new Controller object
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Controller gameController = new Controller();

            Console.ReadKey();
        }


    }
}
