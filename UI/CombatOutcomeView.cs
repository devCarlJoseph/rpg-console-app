using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.UI
{
    /// <summary>
    /// Small UI helpers for post-combat outcomes (victory, extraction, defeat).
    /// </summary>
    public static class CombatOutcomeView
    {
        private const int InnerWidth = 64;

        /// <summary>
        /// Shows a victory screen with rewards, last combat log, and current HP/MP.
        /// </summary>
        public static void ShowVictory(Player player, Enemy enemy, int xp, int gold, string? lastLog)
        {
            ConsoleUi.SafeClear();
            ConsoleUi.Hud(player, "Victory");
            ConsoleUi.BoxHeader("ENEMY DEFEATED", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, $"{enemy.Name} was defeated!", ConsoleColor.Green);
            if (!string.IsNullOrWhiteSpace(lastLog))
            {
                ConsoleUi.BoxText(InnerWidth, ConsoleUi.Truncate(lastLog, InnerWidth));
            }
            ConsoleUi.BoxKeyValue(InnerWidth, "Rewards", $"+{xp} XP, +{gold}g");
            ConsoleUi.BoxKeyValue(InnerWidth, "HP", $"{player.HP}/{player.MaxHP}");
            ConsoleUi.BoxKeyValue(InnerWidth, "MP", $"{player.MP}/{player.MaxMP}");
            ConsoleUi.BoxLineBottom(InnerWidth);
            ConsoleUi.Hint("\nPress Enter to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Shows a prompt asking whether to attempt shadow extraction.
        /// </summary>
        public static bool PromptExtraction()
        {
            ConsoleUi.SafeClear();
            ConsoleUi.BoxHeader("ARISE?", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "Attempt to extract this shadow?");
            ConsoleUi.BoxText(InnerWidth, "[A] Arise / Extract");
            ConsoleUi.BoxText(InnerWidth, "[Enter] Skip", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineBottom(InnerWidth);
            Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
            Console.Write("> ");
            Console.ResetColor();
            var input = Console.ReadLine()?.Trim();
            return string.Equals(input, "a", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(input, "arise", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Displays the result of an extraction attempt.
        /// </summary>
        public static void ShowExtractionResult(bool success, int attempt, int maxAttempts, Shadow? shadow)
        {
            ConsoleUi.SafeClear();
            ConsoleUi.BoxHeader("EXTRACTION RESULT", InnerWidth);
            if (success && shadow is not null)
            {
                ConsoleUi.BoxText(InnerWidth, $"Success! {shadow.Name} joined your shadows.", ConsoleColor.Green);
                ConsoleUi.BoxText(InnerWidth, $"Level {shadow.Level}  STR {shadow.Strength}  DEF {shadow.Defense}", ConsoleColor.Green);
            }
            else
            {
                var remaining = Math.Max(0, maxAttempts - attempt);
                ConsoleUi.BoxText(InnerWidth, $"Extraction failed. Attempt {attempt}/{maxAttempts}.", ConsoleUi.Theme.Warning);
                if (remaining > 0)
                {
                    ConsoleUi.BoxText(InnerWidth, $"{remaining} attempt(s) left.", ConsoleUi.Theme.HintColor);
                }
            }

            ConsoleUi.BoxLineBottom(InnerWidth);
            ConsoleUi.Hint("\nPress Enter to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Shows a defeat screen when the player falls in battle.
        /// </summary>
        public static void ShowDefeat(Player player, Enemy enemy)
        {
            ConsoleUi.SafeClear();
            ConsoleUi.BoxHeader("YOU WERE DEFEATED", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, $"{enemy.Name} struck the final blow.", ConsoleColor.Red);
            ConsoleUi.BoxKeyValue(InnerWidth, "HP", $"{player.HP}/{player.MaxHP}");
            ConsoleUi.BoxKeyValue(InnerWidth, "MP", $"{player.MP}/{player.MaxMP}");
            ConsoleUi.BoxLineBottom(InnerWidth);
            ConsoleUi.Hint("\nPress Enter to return...");
            Console.ReadLine();
        }
    }
}
