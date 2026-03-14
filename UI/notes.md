# UI & Menus Guide

This folder handles all the Console output and user input parsing. Separating UI from Logic makes your code much cleaner.

## OOP Pillars Applied Here:
- **Encapsulation**: The console commands, color formatting, and raw text output logic are completely isolated here. The game's Core Engine doesn't have to manage text colors directly.
- **Abstraction**: UI views act as a presentation abstraction layer. A UI class takes a complex object like `Player` and abstracts it into simple strings and ASCII bars that the player understands.

## Essential Files (Components to Implement):

### 1. Starting Menu
- **Display:** Welcome message, ASCII art title.
- **Options:** 
  1. Start New Game
  2. Load Game (Show last saved stats if possible)
  3. Exit
- **Input Handling:** Numeric selection (1, 2, or 3) for consistency.

### 2. Main Game Menu
- **Display Options:**
  - Character Status
  - Inventory
  - Shop
  - Quests
  - Enter Dungeon
  - Save Game
  - Exit Game
- **Logic:** Gray out or warn users if an option is unavailable (e.g., "No active quests" or "Inventory full").

### 3. Character Status Screen
- **Visuals:** Use ASCII bars for HP/Mana (e.g., `HP: [███████   ] 70/100`).
- **Information:** Display current stats, active/passive skills, and equipment effects (buffs/debuffs).
- **Interactions:** Prompt to spend available Skill Points or Energy Points to upgrade stats.

### 4. Inventory Display
- **Visuals:** List items in an organized format. Sortable by Rarity or Level Requirement.
- **Interactions:** Equip/Unequip, Use Item (highlight usable items during combat), Drop/Delete Item.

### 5. Shop Interface
- **Visuals:** Item list showing Name, Price, Stock, requirement.
- **Interactions:** Buy (input quantity), Sell items from inventory. Show dynamic stock based on player progression.

### 6. Quest Board UI
- **Visuals:** List of available/active quests with progress trackers.
- **Interactions:** Select quest -> view details -> confirm to start.

### Output Formatting Tips:
- Use `Console.ForegroundColor` for different elements (e.g., Red for enemy damage, Green for healing, Yellow for gold).
- Add `Console.Clear()` between menu transitions to keep the screen clean.
