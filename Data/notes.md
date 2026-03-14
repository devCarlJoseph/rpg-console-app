# Data / Saves Guide

This folder is intended to store the dynamic data of your RPG. 

## OOP Pillars Applied Here:
- **Encapsulation**: The precise state of the game is encapsulated within data formats (like JSON) abstracted away from the actual game logic.
- **Abstraction**: Reading and writing these files is abstracted behind a service (like `SaveLoadService`) so that the game objects themselves do not need to know where or how they are saved.

## Files to Store Here:

### 1. `save_game.json`
- Stores the serialized state of the player including stats, inventory, active quests, and progression.
- Handled by `SaveLoadService`.

### 2. Static Data (Optional)
- You can also store static JSON files here for initializing things like:
  - `items.json`
  - `quests.json`
  - `enemies.json`
- This makes game balancing much easier since you don't have to recompile code to change an item's stats!
