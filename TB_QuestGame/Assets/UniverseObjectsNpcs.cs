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
                ExperiencePoints = 50,
                HealthPoints = 10,
                Description = "A man dressed in fine clothing; seems like he comes from nobility.",
                Messages = new List<string>()
                {
                    "Oh, hello there. It's clear that you're not nobility...quite a shame indeed. " +
                    "My name is Bernard Ebrhard, heir to the Ebrhard fortune...at least I was.",

                    "If you're wondering why someone like me is here, keep guessing, you'll " +
                    "never find out.",

                    "Even though we may be in here together, that doesn't mean we're equals. Now buzz off! ",

                    "Ugh, this place is crawling with deviants and horrors of the worst kind. " +
                    "And you have got ot be the most annoying. Congratulations.",

                    "If I am to spend the rest of my days in this place, the last thing I want to do " +
                    "is spend my time talking to you.",

                    "Must you insist on continuing to annoy me!",

                    "Oh, I suppose you want to get out of here...well join the club. " +
                    "But if you promise to leave me alone for the rest of eternity, " +
                    "I can tell you what I know.",

                    "Seek out the dwarf by the name of Silas, the man's 342 years old and has been here as long as I can remember " +
                    "he's bound to have information on breaking out of this cesspit. However, allow " +
                    "me to give a formal warning; there is no exit. Once you're here, you're here for life.",

                    "Ah yes, my apologies. You can find Mr. Silas a few cell blocks from here in an area " +
                    "know as the 'Forgotten Vault', it's where he spends most of his time."
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
                RoomLocationId = 3,
                ExperiencePoints = 10,
                HealthPoints = 10,
                Description = "A man wearing dirty ragged clothes, speaks only one word.",
                Messages = new List<string>()
                {
                    "...WHAT..."
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
                    "If havent already noticed, I'm a cleric. One of the best in all the land.",
                    "Let us skip the small talk, dearie. I'm far too old to waste time on trivial things. " +
                    "But if you ever need healing you know where to find me",
                    "I see. By the power of the light and prosperity, I heal thee!"
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
                ExperiencePoints = 50,
                Description = "A wise old dwarf with a long white beard.",
                Messages = new List<string>()
                {
                    "Hello stranger! My name is Silas.",

                    "Ahhh, so good ole Bernard sent ya? Well, I'd be more than happy to provide you with any assistance you need!",

                    "HA! You're looking for a way out, I should've guessed!",

                    "Well, there is a passage leading to a room of unknown origin...a room that holds doors leading " +
                    "to other worlds, at least according to the rumors. Now I have been to this perculiar room before " +
                    "and I can tell ya this, there are doors in there but noone nor do I have any idea as to where they lead." +
                    " \n" +
                    "I would suggest heading to that room, it's certainly a possibility that one of those doors is the way out. " +
                    "However getting there will cost your life.....unless you have this flawless diamond to gain access.",

                    "There is very deadly and dangerous troll who patrols the halls leading to the room. " +
                    "The troll wont hesitate to rip you to shreds, unless ofcourse you offer him a diamond for safe passage.",

                    "Now ofcourse I can help ya break outta here, kid....but I require a little payment of my own first....",

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
                        TradingObjectId = 5,
                        Description = "Ornate necklace won by royalty. \n\n" +
                        "This item can be traded for some Crab Legs.",
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
                ExperiencePoints = 50,
                Description = "A large troll with a fascination for shiny rocks.",
                Messages = new List<string>()
                {
                    "You there! where do you think you're going?",

                    "Bwahahaha! You want passgage through MY corridor?! Well it's gonna cost you!",

                    "I see that you have a gem there and not just any old gem but a diamond, a very " +
                    "beautiful one at that.",

                    "Tell you what, you hand over that there jewell annd I'll let you go on your merry way....alive.",
                },
                Inventory = new List<GameObject>()
                {
                    new Food
                    {
                        Id = 3,
                        HealthPoints = -10,
                        IsSpoiled = false,
                        Name = "Moldy Bread",
                        Type = GameObject.ObjectType.Food,
                        RoomLocationId = 7,
                        TradingObjectId = 7,
                        Description = "Bread covered in repulsive mold. \n\n" +
                        "This item can be tradeed for some Bread.",
                        Value = 0,
                        CanInventory = false,
                        IsConsumable = true,
                        IsVisible = true
                    },

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
                    3, 79
                }
            },
	        #endregion

            #region MONSTERS

            new Monster
            {
                Id = 90,
                Name = "Skeleton",
                RoomLocationId = -1,
                ExperiencePoints = 50,
                HealthPoints = 100,
                DamageOutput = 10,
                Description = "A walking skeleton holding a sword, cetainly not friendly.",
            },

            new Monster
            {
                Id = 91,
                Name = "Swarm of Bats",
                RoomLocationId = -1,
                ExperiencePoints = 50,
                HealthPoints = 100,
                DamageOutput = 10,
                Description = "A swarm of angry bats.",
            },

            new Monster
            {
                Id = 92,
                Name = "Corrupted Soul",
                RoomLocationId = -1,
                ExperiencePoints = 75,
                HealthPoints = 100,
                DamageOutput = 15,
                Description = "A lost soul gone mad.",
            },

            new Monster
            {
                Id = 93,
                Name = "Zombie",
                RoomLocationId = -1,
                ExperiencePoints = 75,
                HealthPoints = 100,
                DamageOutput = 15,
                Description = "A living undead looking for food.",
            },

            new Monster
            {
                Id = 94,
                Name = "Evil Warlock",
                RoomLocationId = -1,
                ExperiencePoints = 100,
                HealthPoints = 125,
                DamageOutput = 20,
                Description = "A powerful warlock with malicious intent.",
            },
            new Monster
            {
                Id = 95,
                Name = "Rock Troll",
                RoomLocationId = -1,
                ExperiencePoints = 100,
                HealthPoints = 125,
                DamageOutput = 20,
                Description = "A swarm of angry bats.",
            },

            #endregion

            #region BOSSES

            new Boss
            {
                Id = 96,
                Name = "Crystal Troll",
                RoomLocationId = -1,
                ExperiencePoints = 250,
                HealthPoints = 250,
                DamageOutput = 25,
                Description = "A giant crystal troll.",
            },
            new Boss
            {
                Id = 97,
                Name = "Undead Knight",
                RoomLocationId = -1,
                ExperiencePoints = 500,
                HealthPoints = 350,
                DamageOutput = 35,
                Description = "A powerful undead knight.",
            },
            new Boss
            {
                Id = 98,
                Name = "Firey Demon",
                RoomLocationId = -1,
                ExperiencePoints = 1000,
                HealthPoints = 450,
                DamageOutput = 45,
                Description = "A mailcious demon, capable of mass destruction.",
            },

	        #endregion
        };
    }
}
