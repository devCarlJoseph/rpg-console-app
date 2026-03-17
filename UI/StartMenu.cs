using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.UI
{
    public static class StartMenu
    {
        public enum StartMenuResult
        {
            StartNew,
            Load,
            Exit
        }

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
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey(true);
                        ConsoleUi.SafeClear();
                        ShowTitle();
                        break;
                }
            }
        }

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

        public static Player? LoadPlayer()
        {
            return SaveLoadService.Load();
        }
    }
}

