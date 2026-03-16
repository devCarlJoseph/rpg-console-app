using System.Text.Json;

namespace ConsoleRPG.Services
{
    public static class EnemyDataService
    {
        public sealed class EnemyDb
        {
            public EnemyGroups Enemies { get; set; } = new();
        }

        public sealed class EnemyGroups
        {
            public List<EnemyDefinition> Normal { get; set; } = new();
            public List<EnemyDefinition> Elite { get; set; } = new();
            public List<EnemyDefinition> Boss { get; set; } = new();
        }

        public sealed class EnemyDefinition
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Type { get; set; } = "Normal";
            public int EnemyLevel { get; set; }
            public EnemyStats Stats { get; set; } = new();
            public EnemyLoot ExpectedLoot { get; set; } = new();
        }

        public sealed class EnemyStats
        {
            public int MaxHP { get; set; }
            public int Attack { get; set; }
            public int Strength { get; set; }
            public int Defense { get; set; }
        }

        public sealed class EnemyLoot
        {
            public int XP { get; set; }
            public int Gold { get; set; }
        }

        public static EnemyDb Load(string path = "Data/enemies.json")
        {
            var json = File.ReadAllText(path);
            var db = JsonSerializer.Deserialize<EnemyDb>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (db is null)
            {
                throw new InvalidOperationException("Failed to parse enemies.json");
            }

            return db;
        }
    }
}

