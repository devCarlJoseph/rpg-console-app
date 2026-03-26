namespace ConsoleRPG.Services
{
    /// <summary>
    /// Provides consistent colored system/hint/status messages for the console UI.
    /// </summary>
    public static class SystemMessageService
    {
        /// <summary>
        /// Writes a cyan system-prefixed line.
        /// </summary>
        public static void System(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[SYSTEM] {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Writes a magenta hint line.
        /// </summary>
        public static void Hint(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[Hint] {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Writes a green success line.
        /// </summary>
        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes a yellow warning line.
        /// </summary>
        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes a red error line.
        /// </summary>
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

