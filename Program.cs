using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(" ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine(" ║                                                                                                                          ║");
        Console.WriteLine(" ║                                                                                                                          ║");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("          ██╗███████╗██████╗ ██████╗ ███████╗██╗  ██╗      ██╗     ███████╗██╗   ██╗███████╗██╗     ██╗███╗   ██╗ ██████╗");
        Console.WriteLine("          ██║██╔════╝██╔══██╗██╔══██╗██╔════╝██║  ██║  ██║ ██║     ██╔════╝██║   ██║██╔════╝██║     ██║████╗  ██║██╔════╝");
        Console.WriteLine("          ██║█████╗  ██████╔╝██████╔╝█████╗  ███████║      ██║     █████╗  ██║   ██║█████╗  ██║     ██║██╔██╗ ██║██║  ███╗");
        Console.WriteLine("     ██   ██║██╔══╝  ██╔══██╗██╔══██╗██╔══╝  ██╔══██║      ██║     ██╔══╝  ██║   ██║██╔══╝  ██║     ██║██║╚██╗██║██║   ██║");
        Console.WriteLine("     ╚█████╔╝███████╗██║  ██║██║  ██║███████╗██║  ██║  ██║ ███████╗███████╗╚██████╔╝███████╗███████╗██║██║ ╚████║╚██████╔╝");
        Console.WriteLine("      ╚════╝ ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝       ╚═════╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝╚═╝╚═╝  ╚═══╝ ╚═════╝");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("               █████╗  ███████╗██   ██╗ ██████╗ ███╗   ██╗█████╗       ██╗     ██╗███╗   ███╗██╗█████████ ██████║  ");
        Console.WriteLine("               ██╔══██╗██╔════╝██   ██║██╔═══██╗████╗  ██║██╔══██╗     ██║     ██║████╗ ████║██║   ██╔══╝ ██ ╚══╗  ");
        Console.WriteLine("               █████╔╝ █████╗  ╚██ ██╔╝██║   ██║██╔██╗ ██║██║  ██║     ██║     ██║██╔████╔██║██║   ██║    ██████║  ");
        Console.WriteLine("               ██╔══██╗██╔══╝   ║███╔╝ ██║   ██║██║╚██╗██║██║  ██║     ██║     ██║██║╚██╔╝██║██║   ██║        ██║  ");
        Console.WriteLine("               █████╔╝ ███████╗ ╚███║  ╚██████╔╝██║ ╚████║█████╔╝      ███████╗██║██║ ╚═╝ ██║██║   ██║    ██████║  ");
        Console.WriteLine("               ╚════╝  ╚══════╝  ╚══╝   ╚═════╝ ╚═╝  ╚═══╝╚════╝       ╚══════╝╚═╝╚═╝     ╚═╝╚═╝   ╚═╝    ╚═════╝  ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(" ║                                                                                                                          ║");
        Console.WriteLine(" ║                                                                                                                          ║");
        Console.WriteLine(" ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

        Console.WriteLine();

        // Menu
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("       1. Start Game");
        Console.WriteLine("       2. Load Game");
        Console.WriteLine("       3. Exit");

        Console.ResetColor();

        // Animated "Press Any Key"
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n        Press any key to begin your adventure...");
        Console.ResetColor();
        Console.ReadKey();
    }
}