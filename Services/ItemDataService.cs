using System.Text.Json;
using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{

    // Loads item definitions from JSON and converts them into runtime items.

    public static class ItemDataService
    {

        // Root container for items.json.

        public sealed class ItemDb
        {
            public ItemGroups Items { get; set; } = new();
        }


        // Groups of item records by type.

        public sealed class ItemGroups
        {
            public List<WeaponDef> Weapons { get; set; } = new();
            public List<ArmorDef> Armor { get; set; } = new();
            public List<AccessoryDef> Accessories { get; set; } = new();
            public List<ConsumableDef> Consumables { get; set; } = new();
        }


        // Serializable weapon definition.

        public sealed class WeaponDef
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int Value { get; set; }
            public int Weight { get; set; }
            public string Rarity { get; set; } = "Common";
            public int LevelRequirement { get; set; }
            public WeaponStats Stats { get; set; } = new();
        }


        // Attack bonus payload for a weapon.

        public sealed class WeaponStats
        {
            public int AttackBonus { get; set; }
        }


        // Serializable armor definition.

        public sealed class ArmorDef
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int Value { get; set; }
            public int Weight { get; set; }
            public string Rarity { get; set; } = "Common";
            public int LevelRequirement { get; set; }
            public ArmorStats Stats { get; set; } = new();
        }


        // Defense bonus payload for armor.

        public sealed class ArmorStats
        {
            public int DefenseBonus { get; set; }
        }


        // Serializable accessory definition.

        public sealed class AccessoryDef
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int Value { get; set; }
            public int Weight { get; set; }
            public string Rarity { get; set; } = "Common";
            public int LevelRequirement { get; set; }
            public AccessoryStats Stats { get; set; } = new();
        }


        // Attack/defense bonuses granted by an accessory.

        public sealed class AccessoryStats
        {
            public int AttackBonus { get; set; }
            public int DefenseBonus { get; set; }
        }


        // Serializable consumable definition.

        public sealed class ConsumableDef
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int Value { get; set; }
            public int Weight { get; set; }
            public string Rarity { get; set; } = "Common";
            public int LevelRequirement { get; set; }
            public ConsumableStats Stats { get; set; } = new();
        }


        // Healing/resource restoration payload for a consumable.

        public sealed class ConsumableStats
        {
            public int RestoreHP { get; set; }
            public int RestoreMP { get; set; }
        }


        // Reads items.json and returns the parsed item database.

        public static ItemDb Load(string path = "Data/items.json")
        {
            var json = File.ReadAllText(path);
            var db = JsonSerializer.Deserialize<ItemDb>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (db is null)
            {
                throw new InvalidOperationException("Failed to parse items.json");
            }

            return db;
        }


        // Materializes all items into concrete instances for shop stock.

        public static List<IItem> LoadAllItems(string path = "Data/items.json")
        {
            var db = Load(path);
            var items = new List<IItem>();

            items.AddRange(db.Items.Weapons.Select(w => (IItem)new JsonWeapon(w)));
            items.AddRange(db.Items.Armor.Select(a => (IItem)new Armor(
                name: a.Name,
                description: a.Description,
                value: a.Value,
                weight: a.Weight,
                rarity: a.Rarity,
                levelRequirement: a.LevelRequirement,
                defenseBonus: a.Stats.DefenseBonus)));
            items.AddRange(db.Items.Accessories.Select(a => (IItem)new Accessory(
                name: a.Name,
                description: a.Description,
                value: a.Value,
                weight: a.Weight,
                rarity: a.Rarity,
                levelRequirement: a.LevelRequirement,
                attackBonus: a.Stats.AttackBonus,
                defenseBonus: a.Stats.DefenseBonus)));
            items.AddRange(db.Items.Consumables.Select(c => (IItem)new JsonConsumable(c)));

            return items;
        }


        // Returns a lookup from item id to instantiated item.

        public static Dictionary<string, IItem> LoadItemsById(string path = "Data/items.json")
        {
            var db = Load(path);
            var map = new Dictionary<string, IItem>(StringComparer.OrdinalIgnoreCase);

            foreach (var w in db.Items.Weapons)
            {
                map[w.Id] = new JsonWeapon(w);
            }

            foreach (var a in db.Items.Armor)
            {
                map[a.Id] = new Armor(a.Name, a.Description, a.Value, a.Weight, a.Rarity, a.LevelRequirement, a.Stats.DefenseBonus);
            }

            foreach (var a in db.Items.Accessories)
            {
                map[a.Id] = new Accessory(a.Name, a.Description, a.Value, a.Weight, a.Rarity, a.LevelRequirement, a.Stats.AttackBonus, a.Stats.DefenseBonus);
            }

            foreach (var c in db.Items.Consumables)
            {
                map[c.Id] = new JsonConsumable(c);
            }

            return map;
        }


        // Concrete weapon type built from JSON data.

        private sealed class JsonWeapon : Weapon
        {

            // Maps a JSON weapon definition to a runtime weapon.

            public JsonWeapon(WeaponDef def)
                : base(def.Name, def.Description, def.Value, def.Weight, def.Rarity, def.LevelRequirement, def.Stats.AttackBonus)
            {
            }
        }


        // Concrete consumable type built from JSON data.

        private sealed class JsonConsumable : Consumable
        {

            // Maps a JSON consumable definition to a runtime consumable item.

            public JsonConsumable(ConsumableDef def)
                : base(
                    name: def.Name,
                    description: def.Description,
                    value: def.Value,
                    weight: def.Weight,
                    rarity: def.Rarity,
                    levelRequirement: def.LevelRequirement,
                    healAmount: def.Stats.RestoreHP > 0 ? def.Stats.RestoreHP : 0)
            {
            }
        }
    }
}

