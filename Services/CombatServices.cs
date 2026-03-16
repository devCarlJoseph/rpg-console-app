using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    /// <summary>
    /// Central combat math entry point.
    /// Uses <see cref="CombatResult" /> so the game loop can know
    /// not only how much damage was dealt, but also whether the
    /// attack hit, crit, was evaded, or blocked, and who the source was.
    /// </summary>
    public static class CombatService
    {
        private static readonly Random Rng = new Random();

        private const double BaseHitChance = 0.95;
        private const double BaseCritChance = 0.10;
        private const double CritMultiplier = 1.5;

        /// <summary>
        /// Resolves a basic physical attack from the Player to an Enemy.
        /// Includes hit chance, crit chance, defense reduction, and fills a CombatResult.
        /// </summary>
        public static CombatResult CalculatePlayerPhysicalDamage(Player attacker, Enemy defender, int flatBonus = 0)
        {
            var result = new CombatResult
            {
                Source = "Player"
            };

            // HIT CHECK (Player Agility vs Enemy Level as a simple proxy for enemy speed)
            double hitChance = BaseHitChance + (attacker.Agility - defender.Level) * 0.01;
            double hitRoll = Rng.NextDouble();

            if (hitRoll > hitChance)
            {
                result.IsHit = false;
                result.WasEvaded = true;
                result.DamageDealt = 0;
                return result;
            }

            result.IsHit = true;

            // BASE DAMAGE (weapon already contributes to Player.Strength when equipped)
            int rawDamage = attacker.Strength + flatBonus;

            // CRIT CHECK
            double critRoll = Rng.NextDouble();
            if (critRoll < BaseCritChance)
            {
                rawDamage = (int)(rawDamage * CritMultiplier);
                result.IsCritical = true;
            }

            // DEFENSE REDUCTION
            int finalDamage = rawDamage - defender.Defense;

            if (finalDamage < 0)
            {
                finalDamage = 0;
            }

            result.DamageDealt = finalDamage;

            return result;
        }

        /// <summary>
        /// Resolves a basic physical attack from an Enemy to the Player.
        /// Mirrors the same rules as the Player attack for consistency.
        /// </summary>
        public static CombatResult CalculateEnemyPhysicalDamage(Enemy attacker, Player defender, int flatBonus = 0)
        {
            var result = new CombatResult
            {
                Source = "Enemy"
            };

            // HIT CHECK (Enemy Level vs Player Agility)
            double hitChance = BaseHitChance + (attacker.Level - defender.Agility) * 0.01;
            double hitRoll = Rng.NextDouble();

            if (hitRoll > hitChance)
            {
                result.IsHit = false;
                result.WasEvaded = true;
                result.DamageDealt = 0;
                return result;
            }

            result.IsHit = true;

            int rawDamage = attacker.Strength + flatBonus;

            double critRoll = Rng.NextDouble();
            if (critRoll < BaseCritChance)
            {
                rawDamage = (int)(rawDamage * CritMultiplier);
                result.IsCritical = true;
            }

            int finalDamage = rawDamage - defender.Defense;

            if (finalDamage < 0)
            {
                finalDamage = 0;
            }

            result.DamageDealt = finalDamage;

            return result;
        }
    }
}