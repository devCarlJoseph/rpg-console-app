using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    
    // Builds procedurally generated gates/dungeons based on player level.
    public static class GateGenerationService
    {
        private static readonly Random Rng = new();

        
        // Difficulty tiers used to select enemies.

        public enum GateRank
        {
            E,
            D,
            C,
            B,
            A,
            S
        }

        
        // Creates a new dungeon with waves and boss chosen for the given rank.

        public static Dungeon Generate(Player player, GateRank? forcedRank = null)
        {
            var db = EnemyDataService.Load();
            var rank = forcedRank ?? RollRank(player.Level);

            var (waves, bossDef) = PickEnemiesForRank(db, rank);

            var waveEnemies = waves.Select(d => CreateEnemyFromDef(d)).ToList();
            var boss = CreateEnemyFromDef(bossDef);

            var name = $"{rank}-Rank Gate";
            var recommended = Math.Max(1, boss.Level);
            return new Dungeon(name, recommended, waveEnemies, boss);
        }

        
        // Picks enemy definitions for the wave list and boss slot based on rank.

        private static (List<EnemyDataService.EnemyDefinition> waves, EnemyDataService.EnemyDefinition boss) PickEnemiesForRank(
            EnemyDataService.EnemyDb db,
            GateRank rank)
        {
            // Simple mapping: higher ranks pull from higher tiers.
            var waves = new List<EnemyDataService.EnemyDefinition>();

            int waveCount = rank switch
            {
                GateRank.E => 2,
                GateRank.D => 3,
                GateRank.C => 3,
                GateRank.B => 4,
                GateRank.A => 4,
                _ => 5
            };

            var normalPool = db.Enemies.Normal;
            var elitePool = db.Enemies.Elite;
            var bossPool = db.Enemies.Boss;

            for (int i = 0; i < waveCount; i++)
            {
                var useElite = rank >= GateRank.B && Rng.NextDouble() < 0.35;
                var pool = useElite && elitePool.Count > 0 ? elitePool : normalPool;
                waves.Add(pool[Rng.Next(pool.Count)]);
            }

            // Boss: prefer boss pool; fallback to elite/normal.
            EnemyDataService.EnemyDefinition boss = bossPool.Count > 0
                ? bossPool[Rng.Next(bossPool.Count)]
                : (elitePool.Count > 0 ? elitePool[Rng.Next(elitePool.Count)] : normalPool[Rng.Next(normalPool.Count)]);

            return (waves, boss);
        }

        
        // Rolls a gate rank weighted by player level.

        private static GateRank RollRank(int playerLevel)
        {
            // Lightweight scaling: as level rises, better odds at higher ranks.
            var roll = Rng.Next(100);
            if (playerLevel < 5)
            {
                return roll < 80 ? GateRank.E : GateRank.D;
            }

            if (playerLevel < 10)
            {
                return roll < 60 ? GateRank.D : GateRank.C;
            }

            if (playerLevel < 20)
            {
                return roll < 45 ? GateRank.C : (roll < 85 ? GateRank.B : GateRank.A);
            }

            return roll < 50 ? GateRank.B : (roll < 85 ? GateRank.A : GateRank.S);
        }

        
        // Instantiates an enemy from its data definition.

        private static Enemy CreateEnemyFromDef(EnemyDataService.EnemyDefinition def)
        {
            // Enemy has protected setters; use a small derived type to set values.
            return new JsonEnemy(def.Name, Math.Max(1, def.EnemyLevel), def.Stats.MaxHP, def.Stats.Strength, def.Stats.Defense);
        }

        
        // Lightweight enemy that sets stats directly from JSON.

        private sealed class JsonEnemy : Enemy
        {
            
            // Constructs an enemy with exact stats from data.
    
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

