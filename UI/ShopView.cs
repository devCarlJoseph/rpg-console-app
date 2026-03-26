using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.UI
{
    /// <summary>
    /// Renders the shop screen, allowing filtering and purchasing items.
    /// </summary>
    public static class ShopView
    {
        private const int WIndex = 3;
        private const int WItem = 44;
        private const int WPrice = 8;
        private const int WInfo = 18;

        /// <summary>
        /// Main shop loop; lets players filter categories and buy items.
        /// </summary>
        public static void Show(ShopService shop, Player player)
        {
            while (true)
            {
                ConsoleUi.SafeClear();
                ConsoleUi.Hud(player, "Shop");
                RenderCategoryMenu(player.Gold);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("> ");
                Console.ResetColor();

                var catInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(catInput))
                    continue;

                if (catInput.Equals("b", StringComparison.OrdinalIgnoreCase))
                    return;

                Func<Interfaces.IItem, bool> filter = catInput switch
                {
                    "1" => i => i is Weapon,
                    "2" => i => i is Armor,
                    "3" => i => i is Accessory,
                    "4" => i => i is Consumable,
                    "5" => _ => true,
                    _ => _ => false
                };

                var visible = new List<(int stockIndex, Interfaces.IItem item)>();

                for (int i = 0; i < shop.Stock.Count; i++)
                {
                    var item = shop.Stock[i];
                    if (filter(item))
                        visible.Add((i, item));
                }

                ConsoleUi.SafeClear();
                RenderItemsTableHeader();

                if (visible.Count == 0)
                {
                    RenderEmptyItemsRow();
                    RenderItemsTableFooter();
                    ConsoleUi.Hint("\nPress any key to continue...");
                    Console.ReadKey(true);
                    continue;
                }

                for (int i = 0; i < visible.Count; i++)
                {
                    var item = visible[i].item;

                    string rarity = item is Item it ? it.Rarity : string.Empty;
                    string info = item is Item it2 ? $"Lv {it2.LevelRequirement}" : string.Empty;

                    string icon =
                        item is Weapon ? "⚔" :
                        item is Armor ? "🛡" :
                        item is Accessory ? "💍" :
                        item is Consumable ? "🧪" :
                        "📦";

                    RenderItemRow(
                        idx: i + 1,
                        icon: icon,
                        name: item.Name,
                        value: item.Value,
                        rarity: rarity,
                        info: info);
                }

                RenderItemsTableFooter();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Commands");
                Console.ResetColor();
                Console.WriteLine("  Buy <#>   - Purchase item");
                Console.WriteLine("  B         - Back");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("> ");
                Console.ResetColor();

                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (input.StartsWith("buy", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length < 2 || !int.TryParse(parts[1], out var idx))
                    {
                        SystemMessageService.Hint("Usage: Buy <#>");
                        continue;
                    }

                    if (idx < 1 || idx > visible.Count)
                    {
                        SystemMessageService.Hint("Invalid item number.");
                        continue;
                    }

                    var stockIndex = visible[idx - 1].stockIndex;

                    var ok = shop.TryBuy(player, stockIndex);

                    if (!ok)
                    {
                        SystemMessageService.Warning("Purchase failed (invalid item or not enough gold).");
                        continue;
                    }

                    SystemMessageService.Success("Purchased!");
                }
            }
        }

        /// <summary>
        /// Shows the category selector and current gold.
        /// </summary>
        private static void RenderCategoryMenu(int gold)
        {
            const int innerWidth = 46;
            WriteBoxLineTop(innerWidth);
            WriteBoxTitle(innerWidth, "ADVENTURER SHOP", ConsoleColor.Cyan);
            WriteBoxLineSep(innerWidth);

            Console.Write("║ ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Gold");
            Console.ResetColor();
            Console.Write($": {gold}");
            Console.Write(new string(' ', Math.Max(0, innerWidth - ($"Gold: {gold}".Length))));
            Console.WriteLine(" ║");

            WriteBoxLineSep(innerWidth);
            WriteBoxText(innerWidth, "Select Category", ConsoleColor.Green);
            WriteBoxText(innerWidth, "[1] Weapons", ConsoleColor.White);
            WriteBoxText(innerWidth, "[2] Armor", ConsoleColor.White);
            WriteBoxText(innerWidth, "[3] Accessories", ConsoleColor.White);
            WriteBoxText(innerWidth, "[4] Consumables", ConsoleColor.White);
            WriteBoxText(innerWidth, "[5] All Items", ConsoleColor.White);
            WriteBoxBlank(innerWidth);
            WriteBoxText(innerWidth, "[B] Back", ConsoleColor.DarkGray);
            WriteBoxLineBottom(innerWidth);
        }

        /// <summary>
        /// Writes the table header for the visible stock listing.
        /// </summary>
        private static void RenderItemsTableHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╔" + new string('═', WIndex) + "╦" + new string('═', WItem) + "╦" + new string('═', WPrice) + "╦" + new string('═', WInfo) + "╗");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("║");
            Console.Write(Pad(" #", WIndex));
            Console.Write("║");
            Console.Write(Pad(" Item", WItem));
            Console.Write("║");
            Console.Write(Pad(" Price", WPrice));
            Console.Write("║");
            Console.Write(Pad(" Info", WInfo));
            Console.WriteLine("║");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╠" + new string('═', WIndex) + "╬" + new string('═', WItem) + "╬" + new string('═', WPrice) + "╬" + new string('═', WInfo) + "╣");
            Console.ResetColor();
        }

        /// <summary>
        /// Prints a placeholder row when no items match the filter.
        /// </summary>
        private static void RenderEmptyItemsRow()
        {
            var msg = "(No items in this category)";
            var contentWidth = WIndex + WItem + WPrice + WInfo + 3; // 3 inner column separators
            Console.Write("║");
            Console.Write(Pad(" " + msg, contentWidth));
            Console.WriteLine("║");
        }

        /// <summary>
        /// Renders one item row with icon, name, price, and rarity/info.
        /// </summary>
        private static void RenderItemRow(int idx, string icon, string name, int value, string rarity, string info)
        {
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Pad($" {idx}", WIndex));
            Console.ResetColor();
            Console.Write("║");

            var itemText = $" {icon} {name}";
            Console.Write(Pad(Truncate(itemText, WItem), WItem));
            Console.Write("║");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Pad($" {value}g", WPrice));
            Console.ResetColor();
            Console.Write("║");

            var rarityText = string.IsNullOrWhiteSpace(rarity) ? string.Empty : rarity;
            var infoText = string.IsNullOrWhiteSpace(info) ? string.Empty : info;
            var combined = string.IsNullOrWhiteSpace(rarityText) ? infoText : $"{rarityText} {infoText}".Trim();

            WriteRarity(combined, rarity, WInfo);
            Console.WriteLine("║");
        }

        /// <summary>
        /// Writes the bottom border of the stock table.
        /// </summary>
        private static void RenderItemsTableFooter()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╚" + new string('═', WIndex) + "╩" + new string('═', WItem) + "╩" + new string('═', WPrice) + "╩" + new string('═', WInfo) + "╝");
            Console.ResetColor();
        }

        /// <summary>
        /// Colors rarity text and pads it to the requested width.
        /// </summary>
        private static void WriteRarity(string text, string rarity, int width)
        {
            Console.Write(" ");
            var color = rarity.ToLowerInvariant() switch
            {
                "common" => ConsoleColor.Gray,
                "uncommon" => ConsoleColor.Green,
                "rare" => ConsoleColor.Cyan,
                "epic" => ConsoleColor.Magenta,
                "legendary" => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
            Console.ForegroundColor = color;
            Console.Write(Pad(Truncate(text, width - 1), width - 1));
            Console.ResetColor();
        }

        /// <summary>
        /// Pads text to a fixed width for aligned columns.
        /// </summary>
        private static string Pad(string s, int width) => s.Length >= width ? s : s + new string(' ', width - s.Length);

        /// <summary>
        /// Truncates text and appends an ellipsis when it exceeds the width.
        /// </summary>
        private static string Truncate(string s, int width)
        {
            if (width <= 0)
            {
                return string.Empty;
            }

            if (s.Length <= width)
            {
                return s;
            }

            return width <= 1 ? s[..width] : s[..(width - 1)] + "…";
        }

        /// <summary>
        /// Draws the top border for a boxed block.
        /// </summary>
        private static void WriteBoxLineTop(int innerWidth)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╔" + new string('═', innerWidth + 2) + "╗");
            Console.ResetColor();
        }

        /// <summary>
        /// Draws a separator line inside a boxed block.
        /// </summary>
        private static void WriteBoxLineSep(int innerWidth)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╠" + new string('═', innerWidth + 2) + "╣");
            Console.ResetColor();
        }

        /// <summary>
        /// Draws the bottom border for a boxed block.
        /// </summary>
        private static void WriteBoxLineBottom(int innerWidth)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("╚" + new string('═', innerWidth + 2) + "╝");
            Console.ResetColor();
        }

        /// <summary>
        /// Prints an empty interior line for boxed blocks.
        /// </summary>
        private static void WriteBoxBlank(int innerWidth)
        {
            Console.WriteLine("║ " + new string(' ', innerWidth) + " ║");
        }

        /// <summary>
        /// Centers a title inside a boxed header line.
        /// </summary>
        private static void WriteBoxTitle(int innerWidth, string title, ConsoleColor titleColor)
        {
            var content = $" {title} ";
            if (content.Length > innerWidth)
            {
                content = Truncate(content, innerWidth);
            }

            var left = Math.Max(0, (innerWidth - content.Length) / 2);
            var right = Math.Max(0, innerWidth - content.Length - left);

            Console.Write("║ ");
            Console.Write(new string(' ', left));
            Console.ForegroundColor = titleColor;
            Console.Write(content.TrimEnd());
            Console.ResetColor();
            Console.Write(new string(' ', right));
            Console.WriteLine(" ║");
        }

        /// <summary>
        /// Writes a colored text line inside a boxed block.
        /// </summary>
        private static void WriteBoxText(int innerWidth, string text, ConsoleColor color)
        {
            text = text.Length > innerWidth ? Truncate(text, innerWidth) : text;
            Console.Write("║ ");
            Console.ForegroundColor = color;
            Console.Write(Pad(text, innerWidth));
            Console.ResetColor();
            Console.WriteLine(" ║");
        }
    }
}
