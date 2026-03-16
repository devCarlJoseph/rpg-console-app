using System.Text.Json;
using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    public static class ItemDataService
    {
        public sealed class ItemDb
        {
            public ItemGroups Items { get; set; } = new();
        }

        public sealed class ItemGroups
        {
            public List<WeaponDef> Weapons { get; set; } = new();
            public List<ArmorDef> Armor { get; set; } = new();
            public List<AccessoryDef> Accessories { get; set; } = new();
            public List<ConsumableDef> Consumables { get; set; } = new();
        }

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

        public sealed class WeaponStats
        {
            public int AttackBonus { get; set; }
        }

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

        public sealed class ArmorStats
        {
            public int DefenseBonus { get; set; }
        }

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

        public sealed class AccessoryStats
        {
            public int AttackBonus { get; set; }
            public int DefenseBonus { get; set; }
        }

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

        public sealed class ConsumableStats
        {
            public int RestoreHP { get; set; }
            public int RestoreMP { get; set; }
        }

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

        private sealed class JsonWeapon : Weapon
        {
            public JsonWeapon(WeaponDef def)
                : base(def.Name, def.Description, def.Value, def.Weight, def.Rarity, def.LevelRequirement, def.Stats.AttackBonus)
            {
            }
        }

        private sealed class JsonConsumable : Consumable
        {
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

