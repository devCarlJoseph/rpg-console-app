# RPG Console App: Development Roadmap

This document outlines the step-by-step process for developing this Solo Leveling-inspired console RPG.

## Phase 1: Architecture & Foundation (Completed)
- [x] Set up the basic C# Console Application project (`rpg-game.csproj`).
- [x] Design the folder structure using the 4 Pillars of OOP (`Entities`, `Interfaces`, `Items`, `Core`).
- [x] Expand the folder structure to include Solo Leveling mechanics (`Quests`, `Systems`).
- [x] Configure global namespaces in `.csproj` to make files accessible everywhere.

## Phase 2: Core Mechanics Setup (To Do)
- [ ] **Player Initialization:** Update `Program.cs` to instantiate the `GameEngine` and start the game loop.
- [ ] **Inventory System:** Create a concrete `Inventory` class inside `Entities\Player.cs` (or as a separate class) that holds a `List<IItem>`. Expand it to have `Add`, `Remove`, and `UseItem` methods.
- [ ] **Stats and Combat Math:** Update `Character`, `Player`, and `Enemy` to have robust stats (Strength, Agility, Intelligence, Vitality). Implement damage calculation formulas in the `Attack` methods.
- [ ] **Leveling Up:** Implement experience thresholds in `Player.cs`. When XP reaches a threshold, trigger `LevelUp()`, increase stats, and fully restore Health.

## Phase 3: Solo Leveling Systems Integration (To Do)
- [ ] **The "System" (Quest System):** 
  - Flesh out `QuestManager` to assign the `DailyQuest` to the player every in-game day.
  - Implement a mechanism to check conditions (e.g., typing "Do Pushups", "Do Situps") and evaluate `CheckCompletion()`.
- [ ] **Secret Quests & Job Change:** 
  - Add logic in the combat loop where falling below 10% health might trigger the hidden `SecretQuest`.
  - Connect the completion of `SecretQuest` to the `JobSystem` to let the player become a "Necromancer" or "Shadow Monarch".
- [ ] **Shadow Extraction System:** 
  - Update the `Enemy` defeat logic so successful kills give an option to "Extract".
  - Build out the `ShadowArmy` class to turn defeated enemies into `Shadow` entities that fight alongside the player.

## Phase 4: Game Loop & Content (To Do)
- [ ] **Interactive Console Menu:** Expand `GameEngine.cs` to handle actual text parsing:
  - `> attack [enemy]`
  - `> stats`
  - `> inventory`
  - `> arise` (for extraction)
- [ ] **Dungeons & Gates:** Create a `GateSystem` that randomly generates enemies of different ranks (E-Rank to S-Rank).
- [ ] **Saving & Loading:** Implement a way to serialize the `Player` state (stats, inventory, shadows) to a JSON file so the game can be saved.

## Phase 5: Polish & Balancing (To Do)
- [ ] **Text Formatting:** Add colors to the console output (e.g., Red for enemy damage, Cyan for System messages, Purple for Shadows).
- [ ] **Balancing stats:** Ensure the Shadow Army doesn't make the game *too* easy (or maybe it should, it's Solo Leveling after all!).
- [ ] **Final Boss:** Implement the Monarch of Destruction as the final challenge.

---

## Suggested Development Roadmap: What to Code First

To safely build this application without getting overwhelmed, follow this specific coding order. This bridges the gap between the broad Phases and the Folder Structure by telling you exactly which files to create and when.

### Step 1: Establish the Contracts (`Interfaces` Folder)
*Why: You need to define the rules before you create the objects that follow them.*
1. **`IEntity.cs`**: Base stats (Health, Mana, Level) and essential methods (`TakeDamage()`).
2. **`IItem.cs`**: Base item properties (Name, Description) and `Use()` method.
3. **`ISkill.cs`**: Base skill properties (Name, ManaCost) and `Execute()` method.

### Step 2: Build the Core Entities (`Model` Folder)
*Why: Now that you have contracts, you can build the concrete objects that implement them.*
1. **`Player.cs`**: Implement `IEntity`. Add specific RPG stats (STR, AGI, INT, VIT) and Level up logic.
2. **`Enemy.cs`**: Implement `IEntity`. Add basic attack logic and loot drops.
3. **Items**: Create concrete classes like `HealthPotion.cs` that implement `IItem`.

### Step 3: Implement Basic Game Logic (`Services` Folder)
*Why: You have the objects, now you need to make them interact with each other.*
1. **`CombatService.cs`**: Write the math behind Player vs Enemy combat (e.g., `Player.Attack(Enemy)`).
2. **`SystemMessageService.cs`**: Handle colored console output for leveling up or taking damage.

### Step 4: Construct the Game Engine (`Core` Folder)
*Why: Tie everything together into a playable interactive loop.*
1. **`GameEngine.cs`**: Initialize the `Player`. Create the main `while(true)` game loop.
2. **Command Parser**: Implement terminal commands (`> status`, `> inventory`, `> hunt`, etc.).

### Step 5: Add Solo Leveling Specifics (Advanced Features)
*Why: Add the unique flavor on top of a solid working core game.*
1. **Shadows**: Create `Shadow.cs` (Model) and `ShadowExtractionService.cs` (Services).
2. **The System**: Implement `Quest.cs` and link it to daily quest requirements.

---

## Folder Structure & Implementation Guide (Solo Leveling Edition)

Based on your current `.csproj` (C# 10, nullable enabled) and your folder structure (`Interfaces`, `Model`, `Services`), plus the newly added `Core` folder, here is what you should implement in each folder:

### 1. `Interfaces` (Contracts & Abstractions)
*Define the rules of your world here.*
- **`IEntity.cs`**: The base contract for all living beings (Health, Mana, Level, TakeDamage()).
- **`IItem.cs`**: Contract for items (Elixirs, Weapons).
- **`ISkill.cs`**: Contract for active/passive skills (e.g., *Sprint*, *Vital Strike*).
- **`IQuest.cs`**: Contract for the System's quests (Daily Quest, Secret Quest).

### 2. `Model` (Data Entities & State)
*The physical objects and characters in the game.*
- **`Player.cs`**: Your Jinwoo. Holds stats (STR, AGI, INT, VIT), Level, current Job (e.g., *Necromancer*), and a list of active Shadows.
- **`Enemy.cs`**: Monsters found in Gates (Goblins, Magic Beasts, Bosses).
- **`Shadow.cs`**: The extracted shadows (Igris, Tank). They fight alongside the player.
- **`Gate.cs`**: Represents a dungeon instance (Rank E to S).
- **`Quest.cs`**: The concrete class holding quest requirements (e.g., 100 Push-ups).

### 3. `Services` (Game Logic & Mechanics)
*The rules executing the game mechanics behind the scenes.*
- **`CombatService.cs`**: Handles the math when `Player` attacks `Enemy` or when `Shadow` assists in battle.
- **`SystemMessageService.cs`**: Simulates the blue screen "System" messages. Prompts the user with Level Up notifications and Quest assignments.
- **`ShadowExtractionService.cs`**: Logic to determine if a defeated enemy can arise as a shadow (e.g., 3 chances to extract, success probability based on INT stat).
- **`GateGenerationService.cs`**: Randomly generates a dungeon environment and populates it with `Enemy` objects based on Rank.

### 4. `Core` (Game Engine & System)
*The heartbeat of the application.*
- **`GameEngine.cs`**: The main game loop (`while(true)`). Handles parsing terminal commands (`> arise`, `> status`), keeping the console active, and transitioning between the City and Gates.
- **`SaveLoadSystem.cs`**: For saving player state to a JSON file (using `System.Text.Json`).
