using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.UI
{
    public static class InventoryView
    {
        private const int InnerWidth = 66;

        public static void Show(Player player)
        {
            while (true)
            {
                ConsoleUi.SafeClear();
                ConsoleUi.Hud(player, "Inventory");
                ConsoleUi.BoxHeader("INVENTORY", InnerWidth);
                ConsoleUi.BoxKeyValue(InnerWidth, "Gold", $"{player.Gold}g");
                ConsoleUi.BoxLineSep(InnerWidth);

                if (player.Inventory.Items.Count == 0)
                {
                    ConsoleUi.BoxText(InnerWidth, "(empty)", ConsoleUi.Theme.HintColor);
                }
                else
                {
                    // Simple aligned list inside the box.
                    for (int i = 0; i < player.Inventory.Items.Count; i++)
                    {
                        var item = player.Inventory.Items[i];
                        var line = $"[{i + 1}] {item.Name}  ({item.Value}g)";
                        ConsoleUi.BoxText(InnerWidth, ConsoleUi.Truncate(line, InnerWidth));
                    }
                }

                ConsoleUi.BoxLineSep(InnerWidth);
                ConsoleUi.BoxText(InnerWidth, "U <#>  - Use item");
                ConsoleUi.BoxText(InnerWidth, "B      - Back", ConsoleUi.Theme.HintColor);
                ConsoleUi.BoxLineBottom(InnerWidth);

                Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
                Console.Write("> ");
                Console.ResetColor();

                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (input.StartsWith("u", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2 || !int.TryParse(parts[1], out var idx))
                    {
                        SystemMessageService.Hint("Usage: U <#>");
                        continue;
                    }

                    if (!player.Inventory.UseItem(idx - 1, player))
                    {
                        SystemMessageService.Hint("Invalid item number.");
                        continue;
                    }

                    SystemMessageService.Success("Item used.");
                }
            }
        }
    }
}

