using System.Text.Json;

namespace ConsoleRPG.Services
{

    // Loads dungeon definitions and rewards from JSON.

    public static class DungeonDataService
    {

        // Root container for dungeon.json.

        public sealed class DungeonDb
        {
            public Dictionary<string, List<DungeonDefinition>> Dungeons { get; set; } = new();
        }


        // Serializable dungeon definition including waves, boss, and rewards.

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


        // Specifies how many of a given enemy id to spawn.

        public sealed class EnemyCount
        {
            public string EnemyId { get; set; } = string.Empty;
            public int Count { get; set; }
        }


        // Rewards granted after clearing a dungeon.

        public sealed class DungeonRewards
        {
            public int XP { get; set; }
            public int Gold { get; set; }
            public List<string> ItemIds { get; set; } = new();
        }


        // Reads dungeon.json and returns the parsed database.

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

