using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// enum of all possible player actions
    /// </summary>
    public enum PlayerAction
    {
        None,
        MissionSetup,
        RandomName,
        LookAround,
        Travel,

        PlayerMenu,
        PlayerInfo,
        EditPlayerInfo,
        Inventory,
        TreasureInventory,
        MedicineInventory,
        LocationsVisited,

        ObjectMenu,
        LookAt,
        PickUp,
        PutDown,

        NpcMenu,
        TalkTo,
        TradeWith,

        TradeMenu,
        Trade,

        BattleMenu,
        Attack,
        Heal,
        Flee,

        HealMenu,
        UseMinorPotion,
        UseRegularPotion,
        UseMajorPotion,
        UseSuperiorPotion,
        UseUltimatePotion,

        AdminMenu,
        ListDestinations,
        ListNpcs,
        ListGameObjects,
        ListTreasures,
    
        ReturnToMainMenu,  
        Return,
        Exit
    }
}
