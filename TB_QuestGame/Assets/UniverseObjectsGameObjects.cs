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
        /// list of all game objects
        /// </summary>
        public static List<GameObject> GameObjects = new List<GameObject>()
        {
            #region ***MEDICINE***

            #region --- minor potions x 6 ---

            new Medicine
            {
                Id = 60,
                Name = "Minor Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 1,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 20,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 60,
                Name = "Minor Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 2,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 20,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 60,
                Name = "Minor Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 3,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 20,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 60,
                Name = "Minor Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 4,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 20,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 60,
                Name = "Minor Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 5,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 20,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 60,
                Name = "Minor Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 6,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 20,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            #endregion

            #region --- regular x 4 ---

            new Medicine
            {
                Id = 61,
                Name = "Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 1,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 40,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 61,
                Name = "Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 3,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 40,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 61,
                Name = "Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 5,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 40,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 61,
                Name = "Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 7,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 40,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            #endregion

            #region --- major potions x 4 ---

            new Medicine
            {
                Id = 62,
                Name = "Major Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 5,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 60,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

             new Medicine
            {
                Id = 62,
                Name = "Major Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 4,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 60,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 62,
                Name = "Major Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 8,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 60,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 62,
                Name = "Major Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 6,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 60,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            #endregion

            #region ---- superior potions x 3 ---

            new Medicine
            {
                Id = 63,
                Name = "Superior Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 4,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 80,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 63,
                Name = "Superior Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 3,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 80,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },
            new Medicine
            {
                Id = 63,
                Name = "Superior Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 2,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 80,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            #endregion

            #region --- ultimate potions x 3 ---

            new Medicine
            {
                Id = 64,
                Name = "Ultimate Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 8,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 100,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 64,
                Name = "Ultimate Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 7,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 100,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Medicine
            {
                Id = 64,
                Name = "Ultimate Healing Potion",
                Type = GameObject.ObjectType.Medicine,
                RoomLocationId = 6,
                Description = "A small vial of red liquid, feels warm to the touch.",
                Value = 0,
                HealthPoints = 100,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

	        #endregion

	        #endregion

            #region ***TREASURES***

            new Treasure()
            {
                Id = 10,
                Name = "Amethyst",
                Type = GameObject.ObjectType.Treasure,
                RoomLocationId = 7,                
                Description = "A nice lookin amethyst.",
                Value = 100,
                Rarity = Treasure.RarityLevel.Unique,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Treasure()
            {
                Id = 11,
                Name = "Ruby",
                Type = GameObject.ObjectType.Treasure,
                RoomLocationId = 6,
                Description = "Flawless ruby.",
                Value = 400,
                Rarity = Treasure.RarityLevel.Rare,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Treasure()
            {
                Id = 12,
                Name = "Sapphire",
                Type = GameObject.ObjectType.Treasure,
                RoomLocationId = 7,
                Description = "Flawless sapphire.",
                Value = 300,
                Rarity = Treasure.RarityLevel.Rare,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Treasure()
            {
                Id = 13,
                Name = "Emerald",
                Type = GameObject.ObjectType.Treasure,
                RoomLocationId = 5,
                Description = "Flawless emerald shines bright green in the light.",
                Value = 200,
                Rarity = Treasure.RarityLevel.Rare,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Treasure()
            {
                Id = 14,
                Name = "Diamond",
                Type = GameObject.ObjectType.Treasure,
                RoomLocationId = 8,
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
                RoomLocationId = 4,
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
                RoomLocationId = 4,
                TradingObjectId = 10,
                Description = "Gold rings decorated with numerous gems. \n\n" +
                "This item can be traded for an Amethyst.",       
                Value = 100,
                Rarity = Treasure.RarityLevel.Unique,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Key()
            {
                Id = 45,
                Name = "Gold Bar",
                Type = GameObject.ObjectType.Key,
                RoomLocationId = 3,
                Description = "A solid gold bar.",
                Value = 100,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            #endregion

            #region ***FOOD***

            new Food
            {
                Id = 1,
                HealthPoints = 25,
                IsSpoiled = false,
                Name = "Fish",
                Type = GameObject.ObjectType.Food,
                RoomLocationId = 1,
                Description = "Tasty fish, packed with nutrients.",
                PickUpMessage = "You devour the fish.",
                Value = 5,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Food
            {
                Id = 2,
                HealthPoints = 10,
                IsSpoiled = false,
                Name = "Crackers",
                Type = GameObject.ObjectType.Food,
                RoomLocationId = 2,
                Description = "Yummy crackers. Extra salty.",
                PickUpMessage = "You eat the crackers.",
                Value = 1,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

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
                PickUpMessage = "You eat the moldy bread and start to feel sick.",
                Value = 0,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Food
            {
                Id = 4,
                HealthPoints = 35,
                IsSpoiled = false,
                Name = "Rum",
                Type = GameObject.ObjectType.Food,
                RoomLocationId = 6,
                Description = "Rum, everybody's favorite beverage.",
                PickUpMessage = "You drink the old rum.",
                Value = 25,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Food
            {
                Id = 5,
                Name = "Crab Legs",
                Type = GameObject.ObjectType.Food,
                RoomLocationId = 3,
                Description = "Juicy crab legs that taste better than they look.",
                PickUpMessage = "You eat the crab legs savoring the taste.",
                Value = 30,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Food
            {
                Id = 6,
                HealthPoints = 35,
                IsSpoiled = false,
                Name = "Red Wine",
                Type = GameObject.ObjectType.Food,
                RoomLocationId = 5,
                Description = "Delicous red wine.",
                PickUpMessage = "You drink the old wine.",
                Value = 35,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new Food
            {
                Id = 7,
                Name = "Bread",
                Type = GameObject.ObjectType.Food,
                RoomLocationId = 1,
                Description = "Standard bread, looks safe to eat.",
                PickUpMessage = "You eat the stale bread.",
                Value = 2,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            #endregion

            #region ***WEAPONS***

            new Weapon
            {
                Id = 30,
                Damage = 5,
                Name = "Dagger",
                Type = GameObject.ObjectType.Weapon,
                RoomLocationId = 0,
                Description = "A rusty old dagger.",
                Value = 100,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Weapon
            {
                Id = 35,
                Damage = 10,
                Name = "Broad Sword",
                Type = GameObject.ObjectType.Weapon,
                RoomLocationId = 5,
                TradingObjectId = 15,
                Description = "Standard sword prefered by many on the battlefield.",
                Value = 100,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Weapon
            {
                Id = 31,
                Damage = 40,
                Name = "Warhammer",
                Type = GameObject.ObjectType.Weapon,
                RoomLocationId = 6,
                Description = "A giant hammer capable of delivering devastating blows.",
                Value = 250,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Weapon
            {
                Id = 32,
                Damage = 20,
                Name = "Bow and Arrow",
                Type = GameObject.ObjectType.Weapon,
                RoomLocationId = 3,
                Description = "A bow made of oak, equipped with steel tipped arrows.",
                Value = 500,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new Weapon
            {
                Id = 33,
                Damage = 30,
                Name = "Battle Axe",
                Type = GameObject.ObjectType.Weapon,
                RoomLocationId = 5,
                Description = "A razor sharp axe designed for deadly conflicts.",
                Value = 1000,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            #endregion

            #region ***KEYS***

            new Key
            {
                Id = 79,               
                Name = "Rusty Key",
                Type = GameObject.ObjectType.Key,
                RoomLocationId = 8,
                TradingObjectId = 14,
                Description = "A rusty old key belonging to Mr. Bones. \n\n" +
                        "This item can be traded for a Diamond.",
                Value = 0,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            #endregion               

        };
    }
}
