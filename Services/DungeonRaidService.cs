using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    /// <summary>
    /// Translates dungeon definitions into concrete raid plans with instantiated enemies.
    /// </summary>
    public static class DungeonRaidService
    {
        /// <summary>
        /// Aggregates all details needed to run a dungeon raid.
        /// </summary>
        public sealed class RaidPlan
        {
            public string DungeonId { get; init; } = string.Empty;
            public string Name { get; init; } = string.Empty;
            public int RecommendedLevel { get; init; }
            public List<Enemy> Waves { get; init; } = new();
            public Enemy Boss { get; init; } = new Enemy("Boss", 1);
            public DungeonDataService.DungeonRewards Rewards { get; init; } = new();
            public List<string> QuestIds { get; init; } = new();
        }

        /// <summary>
        /// Builds a raid plan from a dungeon definition, instantiating wave enemies and boss.
        /// </summary>
        public static RaidPlan BuildRaid(DungeonDataService.DungeonDefinition def)
        {
            var enemyDb = EnemyDataService.Load();
            var enemyById = enemyDb.Enemies.Normal
                .Concat(enemyDb.Enemies.Elite)
                .Concat(enemyDb.Enemies.Boss)
                .GroupBy(e => e.Id, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

            var waves = new List<Enemy>();
            foreach (var e in def.Enemies)
            {
                if (!enemyById.TryGetValue(e.EnemyId, out var enemyDef))
                {
                    continue;
                }

                var count = Math.Max(0, e.Count);
                for (int i = 0; i < count; i++)
                {
                    waves.Add(CreateEnemyFromDef(enemyDef));
                }
            }

            Enemy boss = enemyById.TryGetValue(def.Boss.EnemyId, out var bossDef)
                ? CreateEnemyFromDef(bossDef)
                : new Enemy("Unknown Boss", 1);

            var recommended = Math.Max(1, boss.Level);

            return new RaidPlan
            {
                DungeonId = def.Id,
                Name = def.Name,
                RecommendedLevel = recommended,
                Waves = waves,
                Boss = boss,
                Rewards = def.Rewards,
                QuestIds = def.QuestIds
            };
        }

        /// <summary>
        /// Instantiates an enemy from its data definition.
        /// </summary>
        private static Enemy CreateEnemyFromDef(EnemyDataService.EnemyDefinition def)
        {
            return new JsonEnemy(def.Name, Math.Max(1, def.EnemyLevel), def.Stats.MaxHP, def.Stats.Strength, def.Stats.Defense);
        }

        /// <summary>
        /// Helper enemy type used to hydrate stats directly from JSON.
        /// </summary>
        private sealed class JsonEnemy : Enemy
        {
            /// <summary>
            /// Constructs an enemy with exact stats from data.
            /// </summary>
            public JsonEnemy(string name, int level, int maxHp, int strength, int defense) : base(name, level)
            {
                MaxHP = maxHp;
                HP = maxHp;
                Strength = strength;
                Defense = defense;
            }
        }
    }
}

