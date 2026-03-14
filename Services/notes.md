# Services / Systems Guide

This folder contains the "Managers" or "Systems" that process the logic of the game. They connect the UI to the Models.

## OOP Pillars Applied Here:
- **Encapsulation**: Complex business logic like combat math is encapsulated in specific service classes (e.g., `CombatService.CalculateDamage`). The UI doesn't need to know the damage formula.
- **Abstraction**: A service simplifies complex internal mechanisms behind a clean API. When the core loop calls `SaveLoadService.SaveGame(player)`, it abstracts away the complex JSON serialization process into a single readable call.

## Essential Files (Systems to Implement):

### 1. Combat System (`CombatService`)
- **Turn-based Loop:** Player turn, Enemy turn.
- **Player Actions:** Attack, Use Skill, Use Item, Retreat.
- **Damage Formula:** `Damage = PlayerAttack - EnemyDefense ± RandomFactor`.
- **Mechanics:** 
  - Calculate Critical Hits (e.g., 2x damage on 10% chance).
  - Calculate Misses (based on Agility).
  - Apply Status Effects (Poison, Burn, Stun).
- **Resolution:** Handle XP/Gold/Loot drops when an enemy dies. Trigger Level Up if XP threshold is met.

### 2. Save/Load System (`SaveLoadService`)
- **Functions:** `SaveGame(Player data)`, `LoadGame()`.
- **Data storage:** Use JSON format (`System.Text.Json`) to serialize player progress, inventory, quest status, and current dungeon progression.
- **Auto-Save:** Call this service optionally before exiting or after completing a major dungeon.

### 3. Shop System (`ShopService`)
- **Logic:** Deduct gold from player, add item to inventory.
- **Economy:** Scale prices based on item rarity. Have different store inventories for different towns or "Black Markets".

### 4. Quest System (`QuestManager`)
- **Logic:** Track active objectives (e.g., "Defeat 5 Goblins").
- **Checks:** Hook into the Combat System to increment kill counts.
- **Rewards:** Grant XP, Gold, and Items upon quest completion.

### 5. Progression & Scaling
- Create algorithms to scale XP requirements for leveling up and scale Enemy stats based on the player's level or dungeon difficulty.