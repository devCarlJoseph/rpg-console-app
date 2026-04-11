using ConsoleRPG.Services;

namespace ConsoleRPG.View
{
    /// <summary>
    /// Provides the gate/dungeon rank selection flow.
    /// </summary>
    public static class DungeonSelectView
    {
        private const int InnerWidth = 56;

        /// <summary>
        /// Lets the player choose a dungeon rank and specific dungeon, returning the chosen definition.
        /// </summary>
        public static DungeonDataService.DungeonDefinition? ChooseDungeon(DungeonDataService.DungeonDb db)
        {
            ConsoleUi.SafeClear();
            // HUD isn't passed a Player here; keep this screen simple and consistent.
            ConsoleUi.BoxHeader("CHOOSE DUNGEON RANK", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "[1] E-Rank");
            ConsoleUi.BoxText(InnerWidth, "[2] D-Rank");
            ConsoleUi.BoxText(InnerWidth, "[3] C-Rank");
            ConsoleUi.BoxText(InnerWidth, "[4] B-Rank");
            ConsoleUi.BoxText(InnerWidth, "[5] A-Rank");
            ConsoleUi.BoxBlank(InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "[B] Back", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineBottom(InnerWidth);

            Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
            Console.Write("> ");
            Console.ResetColor();
            var rankChoice = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(rankChoice) || rankChoice.Equals("b", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var rankKey = rankChoice switch
            {
                "1" => "E-Rank",
                "2" => "D-Rank",
                "3" => "C-Rank",
                "4" => "B-Rank",
                "5" => "A-Rank",
                _ => string.Empty
            };

            if (string.IsNullOrWhiteSpace(rankKey) || !db.Dungeons.TryGetValue(rankKey, out var list) || list.Count == 0)
            {
                ConsoleUi.ErrorMessage("No dungeons found for that rank.");
                return null;
            }

            ConsoleUi.SafeClear();
            ConsoleUi.BoxHeader($"{rankKey} DUNGEONS", InnerWidth);
            for (int i = 0; i < list.Count; i++)
            {
                var d = list[i];
                var line = $"[{i + 1}] {d.Name}";
                ConsoleUi.BoxText(InnerWidth, ConsoleUi.Truncate(line, InnerWidth));
            }
            ConsoleUi.BoxLineSep(InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "Select a dungeon number, or B to go back.", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineBottom(InnerWidth);

            Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
            Console.Write("> ");
            Console.ResetColor();
            var pick = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(pick) || pick.Equals("b", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            if (!int.TryParse(pick, out var dungeonIdx) || dungeonIdx < 1 || dungeonIdx > list.Count)
            {
                ConsoleUi.ErrorMessage("Invalid selection.");
                return null;
            }

            return list[dungeonIdx - 1];
        }
    }
}

