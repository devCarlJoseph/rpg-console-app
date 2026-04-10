using System.Text.Json;

namespace ConsoleRPG.Services
{

    // Loads quest definitions from JSON and provides simple DTOs.

    public static class QuestDataService
    {

        // Types of quests supported by the data file.

        public enum QuestType
        {
            Kill,
            ClearDungeon
        }


        // Root container for quest data.

        public sealed class QuestDb
        {
            public List<QuestDefinition> Quests { get; set; } = new();
        }


        // Serializable quest definition used to instantiate runtime quests.

        public sealed class QuestDefinition
        {
            public string Id { get; set; } = string.Empty;
            public string Type { get; set; } = "Kill";

            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;

            // Link to dungeon.json
            public string? DungeonId { get; set; }

            // Kill quest fields
            public string? TargetEnemyId { get; set; }
            public int RequiredKills { get; set; }

            // Rewards (aligned with IQuest fields)
            public int RewardXP { get; set; }
            public int RewardGold { get; set; }
        }


        // Reads quests.json and returns the parsed quest database.

        public static QuestDb Load(string path = "Data/quests.json")
        {
            var json = File.ReadAllText(path);
            var db = JsonSerializer.Deserialize<QuestDb>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (db is null)
            {
                throw new InvalidOperationException("Failed to parse quests.json");
            }

            return db;
        }
    }
}

