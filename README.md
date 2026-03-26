# RPG Console App

Small C# console RPG that uses simple menus, combat, quests, shops, and dungeons. Saves are stored under `Data/savegame.json`.

## Prerequisites
- .NET SDK 10.0 (or newer SDK compatible with `net10.0` target)

## Run the game
1. Open a terminal in the repository root.
2. Restore and run:  
   ```bash
   dotnet run --project rpg-game.csproj
   ```
3. Use the keyboard prompts to start a new game, load an existing save, or exit.

## Save / Load
- Saves are written to `Data/savegame.json` when you choose **Save Game** in the main menu.
- Loading is available from the title screen (**Load Game**). If no save exists, the game tells you.

## Testing
- There are no automated tests yet. To sanity-check changes:
  - Start a new game, buy an item, use it, and verify HP/MP change.
  - Enter a dungeon, defeat an enemy, and confirm XP/Gold and quest progress update.
  - Save, exit, restart, and load to verify persistence.
