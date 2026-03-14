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
        Console.WriteLine("               ██╔══██ ██╔════╝██   ██║██╔═══██╗████╗  ██║██╔══██╗     ██║     ██║████╗ ████║██║   ██╔══╝ ██ ╚══╗  ");
        Console.WriteLine("               █████╔╝ █████╗  ╚██ ██╔╝██║   ██║██╔██╗ ██║██║  ██║     ██║     ██║██╔████╔██║██║   ██║    ██████║  ");
        Console.WriteLine("               ██╔══██ ██╔══╝   ║███╔╝ ██║   ██║██║╚██╗██║██║  ██║     ██║     ██║██║╚██╔╝██║██║   ██║        ██║  ");
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

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n        Enter your choice: ");
        Console.ResetColor();

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                StartGame();
                break;

            case "2":
                LoadGame();
                break;

            case "3":
                Console.WriteLine("Exiting the game...");
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Invalid choice. Press any key to try again...");
                Console.ReadKey();
                Main(args);
                break;
        }

        static void StartGame()
        {
            Console.WriteLine("Your Game is Starting");
        }

        static void LoadGame()
        {
            Console.WriteLine("Your Game is Loading");
        }

        Console.ResetColor();

        // Animated "Press Any Key"
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n        Press any key to begin your adventure...");
        Console.ResetColor();
        Console.ReadKey();
    }
}