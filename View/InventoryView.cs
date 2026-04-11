using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.View
{
    /// <summary>
    /// Screen for viewing and consuming items in the player's inventory.
    /// </summary>
    public static class InventoryView
    {
        private const int WIndex = 3;
        private const int WItem = 30;
        private const int WStats = 25;
        private const int WValue = 8;

        /// <summary>
        /// Loops over inventory interactions until the player chooses to leave.
        /// </summary>
        public static void Show(Player player)
        {
            while (true)
            {
                ConsoleUi.SafeClear();
                ConsoleUi.Hud(player, "Inventory");
                
                RenderInventoryHeader();

                if (player.Inventory.Items.Count == 0)
                {
                    RenderEmptyInventoryRow();
                }
                else
                {
                    for (int i = 0; i < player.Inventory.Items.Count; i++)
                    {
                        var item = player.Inventory.Items[i];
                        var stats = ConsoleUi.GetItemStats(item);
                        RenderInventoryRow(i + 1, item.Name, stats, item.Value);
                    }
                }

                RenderInventoryFooter();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Commands");
                Console.ResetColor();
                Console.WriteLine("  U <#>  - Use/Equip item");
                Console.WriteLine("  B      - Back");
                Console.ForegroundColor = ConsoleColor.Yellow;
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
                        ConsoleUi.ErrorMessage("Usage: U <#>");
                        continue;
                    }

                    var itemIndex = idx - 1;
                    if (itemIndex < 0 || itemIndex >= player.Inventory.Items.Count)
                    {
                        ConsoleUi.ErrorMessage("Invalid item number.");
                        continue;
                    }

                    var item = player.Inventory.Items[itemIndex];
                    if (!player.Inventory.UseItem(itemIndex, player))
                    {
                        ConsoleUi.ErrorMessage("This item cannot be used right now.");
                        continue;
                    }

                    ItemUseView.Show("Item Used", player, item);
                }
            }
        }

        private static void RenderInventoryHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╔" + new string('═', WIndex) + "╦" + new string('═', WItem) + "╦" + new string('═', WStats) + "╦" + new string('═', WValue) + "╗");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("║");
            Console.Write(ConsoleUi.Pad(" #", WIndex));
            Console.Write("║");
            Console.Write(ConsoleUi.Pad(" Item Name", WItem));
            Console.Write("║");
            Console.Write(ConsoleUi.Pad(" Stats", WStats));
            Console.Write("║");
            Console.Write(ConsoleUi.Pad(" Value", WValue));
            Console.WriteLine("║");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╠" + new string('═', WIndex) + "╬" + new string('═', WItem) + "╬" + new string('═', WStats) + "╬" + new string('═', WValue) + "╣");
            Console.ResetColor();
        }

        private static void RenderInventoryRow(int idx, string name, string stats, int value)
        {
            Console.Write("║");
            Console.Write(ConsoleUi.Pad($" {idx}", WIndex));
            Console.Write("║");
            Console.Write(ConsoleUi.Pad(ConsoleUi.Truncate($" {name}", WItem), WItem));
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(ConsoleUi.Pad($" {stats}", WStats));
            Console.ResetColor();
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(ConsoleUi.Pad($" {value}g", WValue));
            Console.ResetColor();
            Console.WriteLine("║");
        }

        private static void RenderEmptyInventoryRow()
        {
            var contentWidth = WIndex + WItem + WStats + WValue + 3;
            Console.Write("║");
            Console.Write(ConsoleUi.Pad(" (Inventory is empty)", contentWidth));
            Console.WriteLine("║");
        }

        private static void RenderInventoryFooter()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╚" + new string('═', WIndex) + "╩" + new string('═', WItem) + "╩" + new string('═', WStats) + "╩" + new string('═', WValue) + "╝");
            Console.ResetColor();
        }
    }
}
