# System File Explainer: Comprehensive Guide for Reporting

This document provides a brief explanation of **every file** in the Solo Leveling RPG system. Use this as your "cheat sheet" when showing specific files to your instructor.

---

## 1. Project Root & Entry Point

- **`Program.cs`**: The starting point of the application. It initializes the title screen and handles the high-level transition between the title menu and the main game engine.
- **`rpg-game.csproj`**: The project configuration file. It defines the .NET version and project dependencies.

---

## 2. Core Folder (The Engine)

- **`GameEngine.cs`**: The "brain" of the game. It contains the main `while(true)` loop that routes the player between different menus (Shop, Dungeon, Inventory) and orchestrates the overall game state.

---

## 3. UI Folder (Presentation Layer)

- **`ConsoleUi.cs`**: A shared utility class. It defines the visual "Theme" (colors) and provides helpers for drawing professional-looking boxes, headers, and tables.
- **`StartMenu.cs`**: Handles the ASCII title art and the initial "New Game / Load Game" selection.
- **`MainMenuView.cs`**: Renders the central navigation hub where the player chooses their next action.
- **`InventoryView.cs`**: Renders the player's items in a table format and handles the "Use Item" command.
- **`ShopView.cs`**: Manages the multi-category shop interface, item filtering, and the table-based purchase display.
- **`CombatView.cs`**: Specialized UI for battles; shows HP/MP bars for both the player and the enemy and prompts for combat actions.
- **`DungeonSelectView.cs`**: Allows the player to browse available Gates by Rank (E-Rank to A-Rank).
- **`QuestBoardView.cs`**: Lists daily and dungeon quests, showing their status (Active, Completed, or Available).
- **`PlayerStatusView.cs`**: Shows a detailed breakdown of the player's RPG stats (STR, AGI, INT) and current level progress.
- **`CombatOutcomeView.cs`**: Displays the results of a battle, including XP/Gold gains and the "Shadow Extraction" prompt.
- **`ItemUseView.cs`**: A simple confirmation screen shown when a consumable or piece of equipment is used.

---

## 4. Model Folder (Entities & Data)

- **`Player.cs`**: The most complex model. It holds the player's stats, level-up logic, and methods for equipping gear.
- **`Enemy.cs`**: Represents the monsters and bosses found in dungeons.
- **`Item.cs`**: The base class for all items. Defines the abstract `Use()` method.
- **`Armor.cs` & `Accessory.cs`**: Specialized equipment types that provide defense and stat boosts.
- **`Dungeon.cs`**: Represents a specific instance of a Gate with a queue of enemies and a boss.
- **`Quest.cs`**: Holds quest data like titles, requirements, and rewards.
- **`Skill.cs`**: Defines active and passive abilities that players can use in combat.
- **`Shadow.cs`**: Data model for the "Arisen" shadows that join the player's army.
- **`CombatResult.cs`**: A "Data Transfer Object" (DTO) used to pass damage details between the combat logic and the UI.

---

## 5. Services Folder (Business Logic)

- **`CombatServices.cs`**: The heavy math behind battles. It calculates "Critical Hits", "Evades", and damage based on STR/DEF stats.
- **`ItemDataService.cs`**: Handles loading all item definitions from the `items.json` file.
- **`EnemyDataService.cs`**: Loads monster stats and types from `enemies.json`.
- **`QuestManager.cs`**: The logic for tracking quest progress and assigning daily requirements.
- **`SaveLoadService.cs`**: Uses JSON serialization to save the player's progress to disk and load it back later.
- **`ShadowExtractionService.cs`**: The "Arise" logic—calculates the success rate of turning a fallen monster into a shadow.
- **`ShopService.cs`**: Manages the transaction logic for buying items (checking gold and inventory space).
- **`SystemMessageService.cs`**: A helper for printing colored "[SYSTEM]" messages to simulate the Solo Leveling UI.
- **`GateGenerationService.cs`**: Randomly picks enemies and bosses to build a unique dungeon raid.
- **`DungeonDataService.cs`**: Loads the pre-defined dungeon templates from JSON.

---

## 6. Interfaces Folder (Abstraction)

- **`IEntity.cs`**: The contract for anything that can take damage (Players and Enemies).
- **`IItem.cs`**: The base contract for anything that can be put in an inventory.
- **`ISkill.cs`**: The contract for combat abilities.

---

## 7. Data Folder (JSON Storage)

- **`items.json`, `enemies.json`, `dungeons.json`, `quests.json`**: These files store the "Database" of the game. Our services read these files to populate the world with content without hardcoding every single item.
