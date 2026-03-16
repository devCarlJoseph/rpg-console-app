using ConsoleRPG.Core;
using ConsoleRPG.UI;

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
            StartMenu.ShowTitle();
            while (true)
            {
                var result = StartMenu.ShowStartMenu();
                switch (result)
                {
                    case StartMenu.StartMenuResult.StartNew:
                    {
                        var player = StartMenu.CreateNewPlayer();
                        var engine = new GameEngine(player);
                        engine.Run();
                        StartMenu.ShowTitle();
                        break;
                    }
                    case StartMenu.StartMenuResult.Load:
                    {
                        var player = StartMenu.LoadPlayer();
                        if (player is null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nNo save found at Data/savegame.json");
                            Console.ResetColor();
                            break;
                        }

                        var engine = new GameEngine(player);
                        engine.Run();
                        StartMenu.ShowTitle();
                        break;
                    }
                    case StartMenu.StartMenuResult.Exit:
                        Console.WriteLine("Exiting the game...");
                        return;
                }
            }
        }
    }
}