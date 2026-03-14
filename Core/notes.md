# Core Game Loop Guide

This folder contains the heartbeat of the game. It controls the overall flow and links the UI menus with the backend services.

## OOP Pillars Applied Here:
- **Encapsulation**: `GameEngine` encapsulates the game loop and state machine, hiding the complex internal transitions from the rest of the application. The main program just calls `.Start()` and trusts it to work.
- **Abstraction**: `GameEngine` acts as a high-level abstraction for the entire game session. The Entry Point (`Program.cs`) only needs to start the engine without knowing the complex inner workings of the different game states.

## Files to Implement:

### 1. Game Engine (`GameEngine.cs` or `Game.cs`)
- **Initialization:** Load settings, establish the starting state, load save files (delegated to `SaveLoadService`).
- **Game State Machine:** Manage the current state of the game:
  - `MainMenu`
  - `TownMenu` (Main Game Menu)
  - `CombatState`
  - `ExploringDungeon`
- **Main Loop:** A `while(isRunning)` loop that calls the appropriate UI displays and handles user input transitions between states.

### 2. Configuration / Constants (`GameConfig.cs`)
- Store game-wide constants here.
- Example: Max Level cap, starting Gold, basic progression multipliers, or file paths for save data.
