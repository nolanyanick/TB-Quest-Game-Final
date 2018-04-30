using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// static class to hold all objects in the game universe; locations, game objects, npc's, etc.
    /// </summary>
    public static partial class UniverseObjects
    {
        /// <summary>
        /// list of locations with properties
        /// </summary>
        public static List<RoomLocation> RoomLocations = new List<RoomLocation>()
        {
            //damp cell
            new RoomLocation
            {
                CommonName = "Ruined Cell Block",
                RoomLocationID = 1,
                Date = 1252,
                Coordinates = "17.9368° N, 76.8411° W",
                Description = "A semi-large room filled with many cells. Rubble from crumbling walls " +
                "liters the cell block, which seems like it hasn't been used in centuries.",
                GeneralContents = "Amongst the ruins there are roughly 10 jail cells, all destroyed beyond " +
                "their intended use, however some show signs of something or someone living in them. " +
                "As you make your way through the dimly lit cell block you are careful to watch you step, avoiding " + 
                "any rats or rubble.",
                Accessible = true,
                ExperiencePoints = 0
            },
            //forgotten vault
            new RoomLocation
            {
                CommonName = "Forgotten Vault",
                RoomLocationID = 2,
                Date = 1657,
                Coordinates = "44.2312° N, 76.4860° W",
                Description = "A large room made entirely of stone. Numerous iron vaults line the walls, all ranging " +
                "in a variety of sizes and locks. On the back wall of the room is vault door almost the size of " +
                "the wall itself, possibly containing a vast amount of treasure, or even better a way out.",
                GeneralContents = "Walking around you quickly recognize many of locks used on the all the vaults, " +
                "unfortuantely every single vault has been opened and looted, with the exception of the massive one in the back. " +
                "In center of the room is a small camp, smelling of cooked food.",
                Accessible = true,
                ExperiencePoints = 10
            },

            //sacred den
            new RoomLocation
            {
                CommonName = "Sacred Den",
                RoomLocationID = 3,
                Date = 1257,

                Coordinates = "18.1096° N, 77.2975° W",
                Description = "A small room adorned with bookshelfs filled with novels, scorlls, recipes, etc. " +
                "Many candles ligtht the room, making it one of the only places in the dungeonwhere you can see everything.",
                GeneralContents = "In the center lies a large wooden desk with many books stacked on top." +
                "Old papers, scrolls, and books also liter the floor of the room. \n",
                Accessible = true,
                ExperiencePoints = 10
            },
            //

            //dark corridor
            new RoomLocation
            {
                CommonName = "Dark Corridor",
                RoomLocationID = 4,
                Date = 1657,
                Coordinates = "20.0549° N, 72.7925° W",
                Description = "A near pitch black corridor that seems to never end. There are no doors, other than " +
                "the two at either ends of corridor. This is a place feared by all who dwell within the dungeon.",
                GeneralContents = "The hallway is so dark, your vision is limited to only a couple feet in " +
                "in front of you. Along with that, there is an awful stench that never seems to fade. \n",
                Accessible = true,
                ExperiencePoints = 10
            },

            //secret armory
            new RoomLocation
            {
                CommonName = "Secret Armory",
                RoomLocationID = 5,
                Date = 1657,
                Coordinates = "9.1096° N, 64.2975° W",
                Description = "An old armory most likely belonging to former guards stationed in the dungeon." +
                "The armory used to be heavily fortified, but now it has since been reduced to mostly rubble",
                GeneralContents = "Looking around you notice numerous old weapons that have been rendered " +
                "useless due to corrosion. However, you manage to notice a few useful items among the ruins. \n",
                Accessible = true,
                ExperiencePoints = 10
            },

            //monarch bay
            new RoomLocation
            {
                CommonName = "Room of Silence",
                RoomLocationID = 6,
                Date = 1657,
                Coordinates = "18.2208° N, 66.5901°",
                Description = "An ancient room perhaps even older than the dungeon itself. " +
                "Not a single sound can be heard, even as you walk around. Nothing seems to live here" +
                "either, adding to the already omnious atmosphere.",
                GeneralContents = "There is a large, ornate door opposite of where you came in. Hundreds of " +
                "carvings line the walls, floor, and cieling of this room. One carving, positioned on the floor right" +
                " in front of the door. It says: 'The mighty Gold King demands an offering for passage.' HINT: *Drop(Put Down) something here.*",
                Accessible = true,
                ExperiencePoints = 10
            },

            //cavern of life
            new RoomLocation
            {
                CommonName = "Cavern of Life",
                RoomLocationID = 7,
                Date = 1657,
                Coordinates = "18.1096° N, 77.2975° W",
                Description = "A large cavern with waterfall flowing into an underground river. " +
                "The water is crystal clear and the air is as fresh as if you were outside, there are hundreds of glimmeirng " +
                "stones that cover almost ever inch of the cavern.",
                GeneralContents = "It seems that not a lot of people have been this cavern, with only minimal evidence of " +
                "people living here. Many of the stones littering that cavern are amethysts, with the exception" +
                "of some sapphires and emeralds mixed in as well. \n",
                Accessible = true,
                ExperiencePoints = 10
            },
 
            //portal room
            new RoomLocation
            {
                CommonName = "Portal Room",
                RoomLocationID = 8,
                Date = 1657,
                Coordinates = "25.0343° N, 77.3963° W",
                Description = "A large room that seems to be in pristine condition. Not a sinlge crack, " +
                "collapsed pillar, or crumbling in sight. Along with that, the room is constructed entirely " +
                "out of diamond.",
                GeneralContents = "Looking around it's hard to miss the massive portal in the middle of the room. " +
                "The portal glows bright with a blue hue, almost blinding you due to the reflection off the diamond interior. "  +
                "There is also a continous hum being projected from the portal in a soothing tone.",
                Accessible = true,
                ExperiencePoints = 10
            },

            //freedom
            new RoomLocation
            {
                CommonName = "Freedom",
                RoomLocationID = 9,
                Date = 1657,
                Coordinates = "25.0343° N, 77.3963° W",
                Description = "A wide open space filled with endless fields if grass, perhaps on the outskirts of town. " +
                "A tree lies within these fields here and there. *NOTE: You have reached freedom and completed the game, but " +
                "you are allowed to continue playing if you'd like.",
                GeneralContents = "The air is fresh, the sun is out, and birds are chirping; a few things you've come to miss " +
                "since being locked away. You realize you've made it, that you've escaped the unescapable! Feeling a " +
                "rush of euphoria, you quickly start to make your way home promising yourself that you won't end up in that " +
                "dungeon ever agin.",
                Accessible = true,
                ExperiencePoints = 10
            }
        };
    }
}