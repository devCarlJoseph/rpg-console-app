using System.Text.Json;
using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    public static class SaveLoadService
    {
        private const string DefaultSavePath = "Data/savegame.json";

        private sealed class SaveData
        {
            public string Name { get; set; } = "Jinwoo";
            public int Level { get; set; } = 1;
            public int XP { get; set; } = 0;
            public int Gold { get; set; } = 0;

            public int MaxHP { get; set; } = 100;
            public int HP { get; set; } = 100;
            public int MaxMP { get; set; } = 50;
            public int MP { get; set; } = 50;

            public int Strength { get; set; } = 10;
            public int Agility { get; set; } = 10;
            public int Intelligence { get; set; } = 10;
            public int Defense { get; set; } = 5;

            public List<string> InventoryItemIds { get; set; } = new();
            public List<string> ShadowNames { get; set; } = new();
        }

        public static void Save(Player player, string? path = null)
        {
            path ??= DefaultSavePath;

            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");

            var data = new SaveData
            {
                Name = player.Name,
                Level = player.Level,
                XP = player.XP,
                Gold = player.Gold,
                MaxHP = player.MaxHP,
                HP = player.HP,
                MaxMP = player.MaxMP,
                MP = player.MP,
                Strength = player.Strength,
                Agility = player.Agility,
                Intelligence = player.Intelligence,
                Defense = player.Defense,
                InventoryItemIds = player.Inventory.Items.Select(i => i.Name).ToList(),
                ShadowNames = player.Shadows.Select(s => s.Name).ToList()
            };

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public static Player? Load(string? path = null)
        {
            path ??= DefaultSavePath;
            if (!File.Exists(path))
            {
                return null;
            }

            var json = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<SaveData>(json);
            if (data is null)
            {
                return null;
            }

            var player = new Player(data.Name);

            // Rehydrate by "replaying" level ups using Player's own threshold logic.
            while (player.Level < data.Level)
            {
                var needed = Math.Max(1, player.XPToNextLevel);
                player.GainXP(needed);
            }

            // Clamp/restore vitals & resources:
            player.HP = Math.Clamp(data.HP, 0, data.MaxHP);
            player.MP = Math.Clamp(data.MP, 0, data.MaxMP);

            // Gold:
            player.AddGold(Math.Max(0, data.Gold - player.Gold));

            // Inventory: keep simple for now (names only). You can later map IDs to real items.
            foreach (var itemName in data.InventoryItemIds)
            {
                player.Inventory.Add(new SimpleConsumable(itemName, 10));
            }

            foreach (var shadowName in data.ShadowNames)
            {
                var shadow = new Shadow(shadowName, player.Level, 5 + player.Level, 2 + player.Level);
                player.Shadows.Add(shadow);

                var skillName = $"Shadow: {shadow.Name}";
                var alreadyHas = player.ActiveSkills
                    .OfType<ActiveSkill>()
                    .Any(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
                if (!alreadyHas)
                {
                    player.ActiveSkills.Add(new ShadowStrikeSkill(shadow));
                }
            }

            return player;
        }

        private sealed class SimpleConsumable : Interfaces.IItem
        {
            public string Name { get; }
            public int Value => 5;
            private readonly int _heal;

            public SimpleConsumable(string name, int heal)
            {
                Name = name;
                _heal = heal;
            }

            public void Use(Player player)
            {
                player.Heal(_heal);
            }
        }
    }
}

