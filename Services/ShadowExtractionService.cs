using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    public static class ShadowExtractionService
    {
        private static readonly Random Rng = new();

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

            player.Shadows.Add(shadow);

            // Also grant a usable combat skill for this shadow.
            // Prevent duplicates by name.
            var skillName = $"Shadow: {shadow.Name}";
            var alreadyHas = player.ActiveSkills
                .OfType<ActiveSkill>()
                .Any(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
            if (!alreadyHas)
            {
                player.ActiveSkills.Add(new ShadowStrikeSkill(shadow));
            }

            return true;
        }
    }
}

