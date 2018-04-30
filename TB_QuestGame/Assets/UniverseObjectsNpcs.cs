using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static partial class UniverseObjects
    {
        public static List<Npc> Npcs = new List<Npc>()
        {
            #region CIVILLIANS

            //bernard
            new Civillian
            {
                Id = 1,
                Name = "Bernard Ebrhard",
                RoomLocationId = 1,
                ExperiencePoints = 25,
                HealthPoints = 10,
                Description = "A man dressed in fine clothing; seems like he comes from nobility.",
                Messages = new List<string>()
                {
                    "Oh, hello there. I my name is Bernard.",

                    "Oh, I suppose you want to get out of here...well join the club. " +
                    "But if you promise to leave me alone for the rest of eternity, " +
                    "I can tell you what I know.",

                    "Seek out the dwarf by the name of Silas, he's bound to have the information youre looking for. " +
                    "You can find him a few cell blocks from here in an area known as the Forgotten Vault.",                   
                }
            },

            //prisoner
            new Civillian
            {
                Id = 2,
                Name = "Prisoner",
                RoomLocationId = 1,
                ExperiencePoints = 25,
                HealthPoints = 10,
                Description = "A man wearing dirty ragged clothes, who looks like he's been through alot.",
                Messages = new List<string>()
                {
                    "Nice to meet you, friend. Hopefully you wont be here too long.",
                    "I've been down here so long....can hardly remember who I am.",
                    "I was imprisoned long ago, not much else to say about me.",
                    "There's not really much to do down here, being a dungeon and all.",
                    "If you're looking for a way out, goodluck, I've searched everywhere."
                }
            },

            //insane man
            new Civillian
            {
                Id = 4,
                Name = "Insane Man",
                RoomLocationId = 7,
                ExperiencePoints = 10,
                HealthPoints = 10,
                Description = "A man wearing dirty ragged clothes, speaks only one word.",
                Messages = new List<string>()
                {
                    "...CLOUDS..."
                }
            },

            //old cleric
            new Civillian
            {
                Id = 5,
                Name = "Old Cleric",
                RoomLocationId = 3,
                ExperiencePoints = 10,
                HealthPoints = 100,
                Description = "An old woman wearing white robes.",
                Messages = new List<string>()
                {
                    "Hello fellow prisoner, nice to see a new face down here.",
                    "If you haven't already noticed, I'm a cleric. One of the best in all the land.",
                    "Goodbye",
                }
            },

            //prisoner
            new Civillian
            {
                Id = 10,
                Name = "Gilbert the Brave",
                RoomLocationId = 7,
                ExperiencePoints = 25,
                HealthPoints = 10,
                Description = ".",
                Messages = new List<string>()
                {
                    "I know what you seek.",
                    "You seek a way out, a way to freedom. I can help you, but first you must answer me this...",                    
                },
                Riddle = "...I fly without wings. I cry without eyes. What am I?",                                                  
            },

	        #endregion

            #region TRADERS

            //silas
            new Trader
            {
                Id = 3,
                Name = "Silas",
                RoomLocationId = 2,
                ExperiencePoints = 25,
                Description = "A wise old dwarf with a long white beard.",
                Messages = new List<string>()
                {
                    "Hello stranger! My name is Silas.",

                    "Well, I'd be more than happy to provide you with any assistance you need!",                    

                    "Well, there is a passage leading to a room of unknown origin...a room that holds a door leading " +
                    "out of this place.", 

                    "There is very deadly and dangerous troll who patrols the halls leading to the room. " +
                    "The troll wont be too friendly unless you offer him a diamond for safe passage.",

                    "Now I can help you break outta here....but I require a little payment of my own first....",

                    "Find me a beautiful shiny emerald for my collection and I'll see that you get what you need."
                },
                Inventory = new List<GameObject>()
                {
                    new Treasure()
                    {
                        Id = 14,
                        Name = "Diamond",
                        Type = GameObject.ObjectType.Treasure,
                        RoomLocationId = 4,
                        TradingObjectId = 13,
                        Description = "Flawless diamond. \n\n" +
                        "This item can be traded for an Emerald.",
                        Value = 500,
                        Rarity = Treasure.RarityLevel.Legendary,
                        CanInventory = true,
                        IsConsumable = false,
                        IsVisible = true
                    },

                    new Treasure()
                    {
                        Id = 15,
                        Name = "Royal Necklace",
                        Type = GameObject.ObjectType.Treasure,
                        RoomLocationId = 2,
                        TradingObjectId = 6,
                        Description = "Ornate necklace won by royalty. \n\n" +
                        "This item can be traded for a Ruby.",
                        Value = 75,
                        Rarity = Treasure.RarityLevel.Unique,
                        CanInventory = true,
                        IsConsumable = false,
                        IsVisible = true
                    },

                    new Treasure()
                    {
                        Id = 16,
                        Name = "Jewel Studded Rings",
                        Type = GameObject.ObjectType.Treasure,
                        RoomLocationId = 3,
                        TradingObjectId = 10,
                        Description = "Gold rings decorated with numerous gems. \n\n" +
                        "This item can be traded for an Amethyst.",
                        Value = 100,
                        Rarity = Treasure.RarityLevel.Unique,
                        CanInventory = true,
                        IsConsumable = false,
                        IsVisible = true
                    },
                },
                InventoryIds = new List<int>()
                {
                    14,
                    15,
                    16
                }
            },

            //mr. bones
            new Trader
            {
                Id = 6,
                Name = "Mr. Bones",
                RoomLocationId = 4,
                ExperiencePoints = 25,
                Description = "A large troll with a fascination for shiny rocks.",
                Messages = new List<string>()
                {
                    "You there! Where do you think you're going?",

                    "You want passgage through MY corridor?! Well it's gonna cost you!",

                    "I desire a diamond, fool!",

                    "Get that jewell and I'll let you go on your merry way....alive.",
                },
                Inventory = new List<GameObject>()
                {
                    new Key
                    {
                    Id = 79,
                    Name = "Rusty Key",
                    Type = GameObject.ObjectType.Key,
                    RoomLocationId = 1,
                    TradingObjectId = 14,
                    Description = "A rusty old key belonging to Mr.Bones. \n\n" +
                        "This item can be traded for a Diamond.",
                    Value = 0,
                    CanInventory = true,
                    IsConsumable = false,
                    IsVisible = true
                    },
                },

                InventoryIds = new List<int>()
                {
                    79
                }
            },
	        #endregion

            #region MONSTERS

            new Monster
            {
                Id = 90,
                Name = "Skeleton",
                RoomLocationId = -1,
                ExperiencePoints = 25,
                HealthPoints = 100,
                DamageOutput = 5,
                Description = "A walking skeleton holding a sword, cetainly not friendly.",
            },

            new Monster
            {
                Id = 91,
                Name = "Swarm of Bats",
                RoomLocationId = -1,
                ExperiencePoints = 25,
                HealthPoints = 100,
                DamageOutput = 5,
                Description = "A swarm of angry bats.",
            },

            new Monster
            {
                Id = 92,
                Name = "Corrupted Soul",
                RoomLocationId = -1,
                ExperiencePoints = 50,
                HealthPoints = 100,
                DamageOutput = 9,
                Description = "A lost soul gone mad.",
            },

            new Monster
            {
                Id = 93,
                Name = "Zombie",
                RoomLocationId = -1,
                ExperiencePoints = 50,
                HealthPoints = 100,
                DamageOutput = 9,
                Description = "A living undead looking for food.",
            },

            new Monster
            {
                Id = 94,
                Name = "Evil Warlock",
                RoomLocationId = -1,
                ExperiencePoints = 75,
                HealthPoints = 100,
                DamageOutput = 11,
                Description = "A powerful warlock with malicious intent.",
            },
            new Monster
            {
                Id = 95,
                Name = "Rock Troll",
                RoomLocationId = -1,
                ExperiencePoints = 75,
                HealthPoints = 100,
                DamageOutput = 11,
                Description = "An anrgy troll.",
            },

            #endregion

            #region BOSSES

            new Boss
            {
                Id = 96,
                Name = "Crystal Troll",
                RoomLocationId = -1,
                ExperiencePoints = 100,
                HealthPoints = 125,
                DamageOutput = 12,
                Description = "A giant crystal troll.",
            },
            new Boss
            {
                Id = 97,
                Name = "Undead Knight",
                RoomLocationId = -1,
                ExperiencePoints = 200,
                HealthPoints = 150,
                DamageOutput = 14,
                Description = "A powerful undead knight.",
            },
            new Boss
            {
                Id = 98,
                Name = "Firey Demon",
                RoomLocationId = -1,
                ExperiencePoints = 300,
                HealthPoints = 200,
                DamageOutput = 16,
                Description = "A mailcious demon, capable of mass destruction.",
            },

	        #endregion
        };
    }
}
