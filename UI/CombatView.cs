using ConsoleRPG.Model;

namespace ConsoleRPG.UI
{
    /// <summary>
    /// Handles combat-specific prompts and layouts during encounters.
    /// </summary>
    public static class CombatView
    {
        private const int InnerWidth = 78;

        public enum CombatAction
        {
            Attack,
            UseItem,
            Regenerate,
            Retreat,
            Invalid
        }

        public enum AttackType
        {
            Physical,
            Magic,
            Skill,
            Back
        }

        /// <summary>
        /// Renders the enemy panel and optional combat log line.
        /// </summary>
        public static void RenderEncounter(Player player, Enemy enemy, string encounterTitle, string? logLine)
        {
            ConsoleUi.SafeClear();
            ConsoleUi.Hud(player, encounterTitle);

            ConsoleUi.BoxHeader("ENEMY", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, $"{enemy.Name}  (Lv {enemy.Level})", ConsoleUi.Theme.Error);
            ConsoleUi.BoxKeyValue(InnerWidth, "HP", $"{Bar(enemy.HP, enemy.MaxHP, 18)}  {enemy.HP}/{enemy.MaxHP}", ConsoleUi.Theme.Label, ConsoleUi.Theme.Error);
            ConsoleUi.BoxLineBottom(InnerWidth);

            if (!string.IsNullOrWhiteSpace(logLine))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleUi.Theme.HintColor;
                Console.WriteLine(logLine);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Shows available combat actions and returns the selected choice.
        /// </summary>
        public static CombatAction PromptAction(Player player)
        {
            Console.WriteLine();
            ConsoleUi.BoxHeader("COMBAT ACTIONS", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "[1] Attack");
            ConsoleUi.BoxText(InnerWidth, "[2] Use Item");
            ConsoleUi.BoxText(InnerWidth, "[3] Regenerate (small HP/MP)");
            ConsoleUi.BoxText(InnerWidth, "[4] Retreat", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineBottom(InnerWidth);

            Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
            Console.Write("> ");
            Console.ResetColor();
            var input = Console.ReadLine()?.Trim();
            return input switch
            {
                "1" => CombatAction.Attack,
                "2" => CombatAction.UseItem,
                "3" => CombatAction.Regenerate,
                "4" => CombatAction.Retreat,
                _ => CombatAction.Invalid
            };
        }

        /// <summary>
        /// Presents attack types (physical, magic, skill) and returns the selection.
        /// </summary>
        public static AttackType PromptAttackType(Player player)
        {
            Console.WriteLine();
            ConsoleUi.BoxHeader("CHOOSE ATTACK TYPE", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, $"[1] Physical (STR {player.Strength}, AGI {player.Agility})");
            ConsoleUi.BoxText(InnerWidth, $"[2] Magic (INT {player.Intelligence}, MP {player.MP}/{player.MaxMP})");
            ConsoleUi.BoxText(InnerWidth, $"[3] Skill ({player.ActiveSkills.OfType<ActiveSkill>().Count()} available)");
            ConsoleUi.BoxText(InnerWidth, "[B] Back", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineBottom(InnerWidth);

            Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
            Console.Write("> ");
            Console.ResetColor();
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input) || input.Equals("b", StringComparison.OrdinalIgnoreCase))
            {
                return AttackType.Back;
            }

            return input switch
            {
                "1" => AttackType.Physical,
                "2" => AttackType.Magic,
                "3" => AttackType.Skill,
                _ => AttackType.Back
            };
        }

        /// <summary>
        /// Lists usable active skills and returns the chosen one, or null to cancel.
        /// </summary>
        public static ActiveSkill? PromptSkill(Player player)
        {
            var skills = player.ActiveSkills.OfType<ActiveSkill>()
                .Where(s => player.Level >= s.RequiredLevel)
                .ToList();

            Console.WriteLine();
            ConsoleUi.BoxHeader("SKILLS", InnerWidth);
            if (skills.Count == 0)
            {
                ConsoleUi.BoxText(InnerWidth, "(no usable skills)", ConsoleUi.Theme.HintColor);
                ConsoleUi.BoxText(InnerWidth, "Press Enter to go back.", ConsoleUi.Theme.HintColor);
                ConsoleUi.BoxLineBottom(InnerWidth);
                Console.ReadLine();
                return null;
            }

            for (int i = 0; i < skills.Count; i++)
            {
                var s = skills[i];
                var line = $"[{i + 1}] {s.Name}  (MP {s.ManaCost})  - {s.Description}";
                ConsoleUi.BoxText(InnerWidth, ConsoleUi.Truncate(line, InnerWidth));
            }
            ConsoleUi.BoxLineSep(InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "Enter a skill number, or B to go back.", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineBottom(InnerWidth);

            Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
            Console.Write("> ");
            Console.ResetColor();
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input) || input.Equals("b", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            if (!int.TryParse(input, out var idx) || idx < 1 || idx > skills.Count)
            {
                return null;
            }
            return skills[idx - 1];
        }

        /// <summary>
        /// Displays inventory and returns the selected item index for use, or null to cancel.
        /// </summary>
        public static int? PromptItemIndex(Player player)
        {
            Console.WriteLine();
            ConsoleUi.BoxHeader("USE ITEM", InnerWidth);
            if (player.Inventory.Items.Count == 0)
            {
                ConsoleUi.BoxText(InnerWidth, "(no items)", ConsoleUi.Theme.HintColor);
                ConsoleUi.BoxText(InnerWidth, "Press Enter to go back.", ConsoleUi.Theme.HintColor);
                ConsoleUi.BoxLineBottom(InnerWidth);
                Console.ReadLine();
                return null;
            }

            for (int i = 0; i < player.Inventory.Items.Count; i++)
            {
                var it = player.Inventory.Items[i];
                var stats = ConsoleUi.GetItemStats(it);
                var line = string.IsNullOrWhiteSpace(stats) ? $"[{i + 1}] {it.Name}" : $"[{i + 1}] {it.Name}  ({stats})";
                ConsoleUi.BoxText(InnerWidth, ConsoleUi.Truncate(line, InnerWidth));
            }
            ConsoleUi.BoxLineSep(InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "Enter an item number, or B to go back.", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineBottom(InnerWidth);

            Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
            Console.Write("> ");
            Console.ResetColor();
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input) || input.Equals("b", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            if (!int.TryParse(input, out var idx) || idx < 1 || idx > player.Inventory.Items.Count)
            {
                return null;
            }
            return idx - 1;
        }

        /// <summary>
        /// Builds a progress bar string for HP/MP display inside combat frames.
        /// </summary>
        private static string Bar(int current, int max, int width)
        {
            if (max <= 0)
            {
                return "[" + new string('-', width) + "]";
            }

            current = Math.Clamp(current, 0, max);
            var filled = (int)Math.Round((double)current / max * width);
            filled = Math.Clamp(filled, 0, width);
            return "[" + new string('█', filled) + new string('-', width - filled) + "]";
        }
    }
}

