using ConsoleRPG.Core;
using ConsoleRPG.View;

namespace ConsoleRPG
{
    // Entry point for the RPG.
    /// 
    // This class is intentionally kept small:
    // - It only shows the title screen and main menu.
    // - It delegates all real gameplay to <see cref="GameEngine" />.
    //
    internal class Program
    {
        // Displays the title screen, then loops through the start menu until the player exits the program.
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
                            Console.WriteLine("\nNo Save Games");
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
