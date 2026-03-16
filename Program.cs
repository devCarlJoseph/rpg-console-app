using ConsoleRPG.Core;
using ConsoleRPG.Model;

namespace ConsoleRPG
{
    /// <summary>
    /// Entry point for the RPG.
    /// 
    /// This class is intentionally kept small:
    /// - It only shows the title screen and main menu.
    /// - It delegates all real gameplay to <see cref="GameEngine" />.
    /// 
    /// If you are looking for the actual game loop and commands,
    /// open <c>Core/GameEngine.cs</c>.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            ShowTitle();
            ShowMainMenu();
        }

        private static void ShowTitle()
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
            Console.ResetColor();
        }

        /// <summary>
        /// Simple title-screen menu that routes into the game engine.
        /// </summary>
        private static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("       1. Start Game");
                Console.WriteLine("       2. Load Game (coming soon)");
                Console.WriteLine("       3. Exit");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\n        Enter your choice: ");
                Console.ResetColor();

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StartGame();
                        return;

                    case "2":
                        LoadGame();
                        break;

                    case "3":
                        Console.WriteLine("Exiting the game...");
                        Environment.Exit(0);
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey(true);
                        Console.Clear();
                        ShowTitle();
                        break;
                }
            }
        }

        /// <summary>
        /// Creates the Player, then hands control to the GameEngine loop.
        /// </summary>
        private static void StartGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter your hunter's name: ");
            Console.ResetColor();

            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Jinwoo";
            }

            var player = new Player(name);
            var engine = new GameEngine(player);
            engine.Run();

            // When Run() returns, we go back to the title menu.
            Console.Clear();
            ShowTitle();
            ShowMainMenu();
        }

        private static void LoadGame()
        {
            // Placeholder for the future Save/Load system.
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[TODO] Load/Save system not implemented yet.");
            Console.ResetColor();
        }
    }
}