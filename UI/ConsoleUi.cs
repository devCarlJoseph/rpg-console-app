namespace ConsoleRPG.UI
{
    /// <summary>
    /// Shared console helpers for consistent colors, headers, and framed text blocks.
    /// </summary>
    public static class ConsoleUi
    {
        /// <summary>
        /// Theme palette used across UI helpers.
        /// </summary>
        public static class Theme
        {
            public const ConsoleColor Border = ConsoleColor.DarkGray;
            public const ConsoleColor HeaderColor = ConsoleColor.Cyan;
            public const ConsoleColor PromptColor = ConsoleColor.Yellow;
            public const ConsoleColor Label = ConsoleColor.White;
            public const ConsoleColor HintColor = ConsoleColor.DarkGray;
            public const ConsoleColor Success = ConsoleColor.Green;
            public const ConsoleColor Warning = ConsoleColor.Yellow;
            public const ConsoleColor Error = ConsoleColor.Red;
        }

        /// <summary>
        /// Clears the console, swallowing errors in environments that disallow clear.
        /// </summary>
        public static void SafeClear()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
            }
        }

        /// <summary>
        /// Writes a colored header line.
        /// </summary>
        public static void Header(string text)
        {
            Console.ForegroundColor = Theme.HeaderColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes hint text in a muted color.
        /// </summary>
        public static void Hint(string text)
        {
            Console.ForegroundColor = Theme.HintColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Prompts for input using a colored prefix and returns the raw line.
        /// </summary>
        public static string? Prompt(string text, ConsoleColor color = Theme.PromptColor)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
            return Console.ReadLine();
        }

        /// <summary>
        /// Writes a framed header block with title and separator.
        /// </summary>
        public static void BoxHeader(string title, int innerWidth)
        {
            BoxLineTop(innerWidth);
            BoxTitle(innerWidth, title, Theme.HeaderColor);
            BoxLineSep(innerWidth);
        }

        /// <summary>
        /// Renders the HUD showing player name, HP/MP bars, gold and optional location.
        /// </summary>
        public static void Hud(ConsoleRPG.Model.Player player, string? location = null)
        {
            const int innerWidth = 79;
            BoxLineTop(innerWidth);

            // Title line
            Console.Write("║ ");
            Console.ForegroundColor = Theme.HeaderColor;
            var title = "Jerreh: Leveling Beyon Limits";
            Console.Write(title);
            Console.ResetColor();
            var loc = string.IsNullOrWhiteSpace(location) ? string.Empty : $"  |  {location}";
            var right = $"Gold: {player.Gold}g";
            var mid = Truncate(title + loc, innerWidth - right.Length - 1);
            var pad = Math.Max(0, innerWidth - mid.Length - right.Length);
            Console.Write(Truncate(loc, Math.Max(0, innerWidth - title.Length - right.Length - 1)));
            Console.Write(new string(' ', Math.Max(1, pad)));
            Console.ForegroundColor = Theme.PromptColor;
            Console.Write(right);
            Console.ResetColor();
            Console.WriteLine(" ║");

            // Stats line
            var hpBar = Bar(player.HP, player.MaxHP, 12);
            var mpBar = Bar(player.MP, player.MaxMP, 12);
            var stats = $"{player.Name}  Lv {player.Level}   HP {hpBar} {player.HP}/{player.MaxHP}   MP {mpBar} {player.MP}/{player.MaxMP}";
            BoxText(innerWidth, Truncate(stats, innerWidth), Theme.Label);

            BoxLineSep(innerWidth);
        }

        /// <summary>
        /// Top border for boxed sections.
        /// </summary>
        public static void BoxLineTop(int innerWidth)
        {
            Console.ForegroundColor = Theme.Border;
            Console.WriteLine("╔" + new string('═', innerWidth + 2) + "╗");
            Console.ResetColor();
        }

        /// <summary>
        /// Middle separator for boxed sections.
        /// </summary>
        public static void BoxLineSep(int innerWidth)
        {
            Console.ForegroundColor = Theme.Border;
            Console.WriteLine("╠" + new string('═', innerWidth + 2) + "╣");
            Console.ResetColor();
        }

        /// <summary>
        /// Bottom border for boxed sections.
        /// </summary>
        public static void BoxLineBottom(int innerWidth)
        {
            Console.ForegroundColor = Theme.Border;
            Console.WriteLine("╚" + new string('═', innerWidth + 2) + "╝");
            Console.ResetColor();
        }

        /// <summary>
        /// Writes an empty boxed line with the given width.
        /// </summary>
        public static void BoxBlank(int innerWidth)
        {
            Console.WriteLine("║ " + new string(' ', innerWidth) + " ║");
        }

        /// <summary>
        /// Writes a single line of text inside a box, truncating if needed.
        /// </summary>
        public static void BoxText(int innerWidth, string text, ConsoleColor color = Theme.Label)
        {
            text = text.Length > innerWidth ? Truncate(text, innerWidth) : text;
            Console.Write("║ ");
            Console.ForegroundColor = color;
            Console.Write(Pad(text, innerWidth));
            Console.ResetColor();
            Console.WriteLine(" ║");
        }

        /// <summary>
        /// Writes a key/value pair inside the box with optional colors.
        /// </summary>
        public static void BoxKeyValue(int innerWidth, string key, string value, ConsoleColor keyColor = Theme.Label, ConsoleColor valueColor = Theme.PromptColor)
        {
            Console.Write("║ ");
            Console.ForegroundColor = keyColor;
            Console.Write(key);
            Console.ResetColor();
            Console.Write(": ");
            Console.ForegroundColor = valueColor;
            var remaining = innerWidth - (key.Length + 2);
            Console.Write(Pad(Truncate(value, Math.Max(0, remaining)), Math.Max(0, remaining)));
            Console.ResetColor();
            Console.WriteLine(" ║");
        }

        /// <summary>
        /// Pads a string to the requested width.
        /// </summary>
        public static string Pad(string s, int width) => s.Length >= width ? s : s + new string(' ', width - s.Length);

        /// <summary>
        /// Truncates a string and appends an ellipsis if it exceeds the width.
        /// </summary>
        public static string Truncate(string s, int width)
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
        /// Centers and colors a title inside a boxed header line.
        /// </summary>
        private static void BoxTitle(int innerWidth, string title, ConsoleColor titleColor)
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
        /// Creates a simple progress bar string representing a current/max value.
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

