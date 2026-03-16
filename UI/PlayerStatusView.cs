using ConsoleRPG.Model;

namespace ConsoleRPG.UI
{
    public static class PlayerStatusView
    {
        private const int InnerWidth = 56;

        public static void Show(Player player)
        {
            ConsoleUi.SafeClear();
            ConsoleUi.Hud(player, "Status");
            ConsoleUi.BoxHeader("CHARACTER STATUS", InnerWidth);
            ConsoleUi.BoxKeyValue(InnerWidth, "Name", player.Name, ConsoleUi.Theme.Label, ConsoleUi.Theme.HeaderColor);
            ConsoleUi.BoxKeyValue(InnerWidth, "Level", $"{player.Level}  (XP: {player.XP})");
            ConsoleUi.BoxKeyValue(InnerWidth, "Gold", $"{player.Gold}g");
            ConsoleUi.BoxLineSep(InnerWidth);
            ConsoleUi.BoxKeyValue(InnerWidth, "HP", $"{Bar(player.HP, player.MaxHP)}  {player.HP}/{player.MaxHP}", ConsoleUi.Theme.Label, ConsoleUi.Theme.Success);
            ConsoleUi.BoxKeyValue(InnerWidth, "MP", $"{Bar(player.MP, player.MaxMP)}  {player.MP}/{player.MaxMP}", ConsoleUi.Theme.Label, ConsoleUi.Theme.HeaderColor);
            ConsoleUi.BoxLineSep(InnerWidth);
            ConsoleUi.BoxKeyValue(InnerWidth, "STR", player.Strength.ToString());
            ConsoleUi.BoxKeyValue(InnerWidth, "AGI", player.Agility.ToString());
            ConsoleUi.BoxKeyValue(InnerWidth, "INT", player.Intelligence.ToString());
            ConsoleUi.BoxKeyValue(InnerWidth, "DEF", player.Defense.ToString());
            ConsoleUi.BoxKeyValue(InnerWidth, "Shadows", player.Shadows.Count.ToString());
            ConsoleUi.BoxLineBottom(InnerWidth);

            ConsoleUi.Hint("\nPress Enter to go back...");
            Console.ReadLine();
        }

        private static string Bar(int current, int max, int width = 20)
        {
            if (max <= 0)
            {
                return "[--------------------]";
            }

            current = Math.Clamp(current, 0, max);
            var filled = (int)Math.Round((double)current / max * width);
            filled = Math.Clamp(filled, 0, width);
            return "[" + new string('█', filled) + new string('-', width - filled) + "]";
        }
    }
}

