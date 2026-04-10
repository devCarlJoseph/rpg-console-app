using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.UI
{
    /// <summary>
    /// Lists available quests and lets the player accept them.
    /// </summary>
    public static class QuestBoardView
    {
        private const int InnerWidth = 78;

        /// <summary>
        /// Displays quest definitions with status indicators and handles accept input.
        /// </summary>
        public static void Show(QuestManager quests, Player player)
        {
            ConsoleUi.SafeClear();
            ConsoleUi.Hud(player, "Quest Board");
            ConsoleUi.BoxHeader("QUEST BOARD", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "Available Quests:", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineSep(InnerWidth);
            var defs = quests.GetAllQuestDefinitions();
            if (defs.Count == 0)
            {
                ConsoleUi.BoxText(InnerWidth, "(no quests found in quests.json)", ConsoleUi.Theme.HintColor);
            }
            else
            {
                for (int i = 0; i < defs.Count; i++)
                {
                    var d = defs[i];
                    var isActive = player.ActiveQuests.Any(q => q.Id.Equals(d.Id, StringComparison.OrdinalIgnoreCase));
                    var isDone = player.CompletedQuests.Any(q => q.Id.Equals(d.Id, StringComparison.OrdinalIgnoreCase));
                    var status = isDone ? "COMPLETED" : (isActive ? "ACTIVE" : "AVAILABLE");
                    var line = $"[{i + 1}] [{status}] {d.Title}  (+{d.RewardXP} XP, +{d.RewardGold}g)";
                    ConsoleUi.BoxText(InnerWidth, ConsoleUi.Truncate(line, InnerWidth));
                }

                ConsoleUi.BoxLineSep(InnerWidth);
                ConsoleUi.BoxText(InnerWidth, "A <#>  - Accept quest");
                ConsoleUi.BoxText(InnerWidth, "Enter  - Back", ConsoleUi.Theme.HintColor);
                ConsoleUi.BoxLineBottom(InnerWidth);

                Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
                Console.Write("> ");
                Console.ResetColor();
                var input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(input) && input.StartsWith("a", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2 && int.TryParse(parts[1], out var idx) && idx >= 1 && idx <= defs.Count)
                    {
                        var ok = quests.TryAcceptQuest(player, defs[idx - 1].Id);
                        if (!ok)
                        {
                            SystemMessageService.Hint("Quest could not be accepted (already active/completed or invalid).");
                        }
                    }
                    else
                    {
                        ConsoleUi.ErrorMessage("Usage: A <#> (e.g., A 1)");
                    }
                }

                return;
            }

            ConsoleUi.BoxLineSep(InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "Completed:");
            if (player.CompletedQuests.Count == 0)
            {
                ConsoleUi.BoxText(InnerWidth, "(none)", ConsoleUi.Theme.HintColor);
            }
            else
            {
                foreach (var q in player.CompletedQuests)
                {
                    ConsoleUi.BoxText(InnerWidth, "- " + ConsoleUi.Truncate(q.Title, InnerWidth - 2));
                }
            }

            ConsoleUi.BoxLineBottom(InnerWidth);
            ConsoleUi.Hint("\nPress Enter to go back...");
            Console.ReadLine();
        }
    }
}

