using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;

namespace ConsoleRPG.View
{
    /// Simple UI to confirm when an item has been used, showing current HP/MP.
    public static class ItemUseView
    {
        private const int InnerWidth = 56;

        
        /// Renders an item-used confirmation screen.
        public static void Show(string title, Player player, IItem item, string? note = null)
        {
            ConsoleUi.SafeClear();
            ConsoleUi.Hud(player, title);
            ConsoleUi.BoxHeader("ITEM USED", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, item.Name, ConsoleColor.Green);
            if (!string.IsNullOrWhiteSpace(note))
            {
                ConsoleUi.BoxText(InnerWidth, ConsoleUi.Truncate(note, InnerWidth), ConsoleUi.Theme.HintColor);
            }
            ConsoleUi.BoxLineSep(InnerWidth);
            ConsoleUi.BoxKeyValue(InnerWidth, "HP", $"{player.HP}/{player.MaxHP}", ConsoleUi.Theme.Label, ConsoleUi.Theme.Success);
            ConsoleUi.BoxKeyValue(InnerWidth, "MP", $"{player.MP}/{player.MaxMP}", ConsoleUi.Theme.Label, ConsoleUi.Theme.HeaderColor);
            ConsoleUi.BoxLineBottom(InnerWidth);
            ConsoleUi.Hint("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
