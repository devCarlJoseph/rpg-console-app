namespace ConsoleRPG.Services
{
    // Provides consistent colored system/hint/status messages for the console UI.
    public static class SystemMessageService
    {

        // Writes a cyan system-prefixed line.
        public static void System(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[SYSTEM] {message}");
            Console.ResetColor();
        }


        // Writes a magenta hint line.
        public static void Hint(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[Hint] {message}");
            Console.ResetColor();
        }


        // Writes a green success line.
        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        // Writes a yellow warning line.
        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        // Writes a red error line.
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

