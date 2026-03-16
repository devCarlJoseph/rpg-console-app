using System.Text.Json;

namespace ConsoleRPG.Services
{
    public static class DungeonDataService
    {
        public sealed class DungeonDb
        {
            public Dictionary<string, List<DungeonDefinition>> Dungeons { get; set; } = new();
        }

        public sealed class DungeonDefinition
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Difficulty { get; set; } = string.Empty;

            public List<string> QuestIds { get; set; } = new();

            public List<EnemyCount> Enemies { get; set; } = new();
            public EnemyCount Boss { get; set; } = new();
            public DungeonRewards Rewards { get; set; } = new();
        }

        public sealed class EnemyCount
        {
            public string EnemyId { get; set; } = string.Empty;
            public int Count { get; set; }
        }

        public sealed class DungeonRewards
        {
            public int XP { get; set; }
            public int Gold { get; set; }
            public List<string> ItemIds { get; set; } = new();
        }

        public static DungeonDb Load(string path = "Data/dungeon.json")
        {
            var json = File.ReadAllText(path);
            var db = JsonSerializer.Deserialize<DungeonDb>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (db is null)
            {
                throw new InvalidOperationException("Failed to parse dungeon.json");
            }

            return db;
        }
    }
}

