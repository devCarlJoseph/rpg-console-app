using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    // Handles RNG-based extraction of shadows from defeated enemies.
    public static class ShadowExtractionService
    {
        private static readonly Random Rng = new();


        // Attempts a single extraction roll and returns a shadow if successful.
        // Does not mutate player state; caller must add the shadow/skill.
        public static bool TryExtract(Player player, Enemy defeatedEnemy, out Shadow? shadow)
        {
            shadow = null;

            // Base chance + INT scaling.
            var chance = 0.20 + player.Intelligence * 0.01;
            chance = Math.Clamp(chance, 0.20, 0.80);

            if (Rng.NextDouble() > chance)
            {
                return false;
            }

            shadow = new Shadow(
                name: defeatedEnemy.Name,
                level: defeatedEnemy.Level,
                strength: Math.Max(1, defeatedEnemy.Strength / 2),
                defense: Math.Max(0, defeatedEnemy.Defense / 2));

            return true;
        }
    }
}

