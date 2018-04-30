using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public interface ITrade
    {
        List<GameObject> Inventory { get; set; }
        List<int> InventoryIds { get; set; }       

        /// <summary>
        /// trade
        /// </summary>
        void Trade(int objectIdInNpcInventory, int objectIdInPlayerInventory, Player prisoner);
    }
}
