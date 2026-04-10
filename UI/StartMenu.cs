using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.UI
{
    /// <summary>
    /// Handles the title screen and initial start/load/exit choices before the main game loop.
    /// </summary>
    public static class StartMenu
    {
        /// <summary>
        /// Options shown to the player on the start menu.
        /// </summary>
        public enum StartMenuResult
        {
            StartNew,
            Load,
            Exit
        }

        /// <summary>
        /// Renders the ASCII title art and resets console colors.
        /// </summary>
        public static void ShowTitle()
        {
            ConsoleUi.SafeClear();

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
        /// Displays the start menu and loops until the player picks a valid option.
        /// </summary>
        public static StartMenuResult ShowStartMenu()
        {
            while (true)
            {
                Console.WriteLine();
                const int innerWidth = 46;
                ConsoleUi.BoxHeader("START MENU", innerWidth);
                ConsoleUi.BoxText(innerWidth, "[1] Start New Game");
                ConsoleUi.BoxText(innerWidth, "[2] Load Game");
                ConsoleUi.BoxText(innerWidth, "[3] Exit");
                ConsoleUi.BoxLineBottom(innerWidth);

                Console.ForegroundColor = ConsoleUi.Theme.PromptColor;
                Console.Write("> ");
                Console.ResetColor();

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        return StartMenuResult.StartNew;
                    case "2":
                        return StartMenuResult.Load;
                    case "3":
                        return StartMenuResult.Exit;
                    default:
                        ConsoleUi.ErrorMessage("Invalid choice.");
                        ConsoleUi.SafeClear();
                        ShowTitle();
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts for a hunter name and returns a new player with default starting stats.
        /// </summary>
        public static Player CreateNewPlayer()
        {
            ConsoleUi.SafeClear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter your hunter's name: ");
            Console.ResetColor();

            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Jerreh";
            }

            return new Player(name);
        }

        /// <summary>
        /// Loads a previously saved player from disk, or returns null if none exists.
        /// </summary>
        public static Player? LoadPlayer()
        {
            return SaveLoadService.Load();
        }
    }
}

