namespace ConsoleRPG.UI
{
    /// <summary>
    /// Displays the top-level navigation menu for the game loop.
    /// </summary>
    public static class MainMenuView
    {
        private const int InnerWidth = 46;

        /// <summary>
        /// Menu options the player can choose.
        /// </summary>
        public enum MainMenuChoice
        {
            Status,
            Inventory,
            Shop,
            Quests,
            EnterDungeon,
            Save,
            Exit,
            Invalid
        }

        /// <summary>
        /// Renders the main menu UI and returns the player's chosen action.
        /// </summary>
        public static MainMenuChoice Show(ConsoleRPG.Model.Player player)
        {
            ConsoleUi.SafeClear();
            ConsoleUi.Hud(player, "Main Menu");
            Console.WriteLine();
            ConsoleUi.BoxHeader("MAIN MENU", InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "[1] Character Status");
            ConsoleUi.BoxText(InnerWidth, "[2] Inventory");
            ConsoleUi.BoxText(InnerWidth, "[3] Shop");
            ConsoleUi.BoxText(InnerWidth, "[4] Quests");
            ConsoleUi.BoxText(InnerWidth, "[5] Enter Dungeon");
            ConsoleUi.BoxText(InnerWidth, "[6] Save Game");
            ConsoleUi.BoxText(InnerWidth, "[7] Exit Game");
            ConsoleUi.BoxBlank(InnerWidth);
            ConsoleUi.BoxText(InnerWidth, "Tip: type the number and press Enter", ConsoleUi.Theme.HintColor);
            ConsoleUi.BoxLineBottom(InnerWidth);

            var choice = ConsoleUi.Prompt("> ")?.Trim();
            return choice switch
            {
                "1" => MainMenuChoice.Status,
                "2" => MainMenuChoice.Inventory,
                "3" => MainMenuChoice.Shop,
                "4" => MainMenuChoice.Quests,
                "5" => MainMenuChoice.EnterDungeon,
                "6" => MainMenuChoice.Save,
                "7" => MainMenuChoice.Exit,
                _ => MainMenuChoice.Invalid
            };
        }
    }
}

