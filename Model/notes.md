# Model / Entities Guide

This folder contains the core data structures (Classes) of your RPG.

## OOP Pillars Applied Here:
- **Encapsulation**: Using properties (e.g., `public int HP { get; private set; }`) ensures data validity. Methods like `TakeDamage()` or `LevelUp()` control state changes, preventing invalid data from being set directly.
- **Inheritance**: Base classes (like `Enemy`) define common attributes (HP, Damage). Derived classes (like `Boss : Enemy` or `Goblin : Enemy`) inherit these but can add their own unique fields (`SpecialLootDrop`).
- **Polymorphism**: The derived classes (like `Boss`) can override a virtual `Attack` method from `Enemy` to trigger a different attack pattern.
- **Abstraction**: Abstract classes (like `Item` or `Skill`) define a generalized concept so you don't instantiate raw "Items," but specific derived instances like `HealthPotion`.

## Essential Files (Classes to Implement):

### 1. Player
- **Stats:** Level, XP, HP, Mana, Strength, Agility, Intelligence, Defense, Energy Points.
- **Attributes:** Name.
- **Collections:** 
  - `List<Item> Inventory`
  - `List<Skill> ActiveSkills`
  - `List<Skill> PassiveSkills`
  - `List<Quest> ActiveQuests`
  - `Equipment` (Weapon, Armor, Accessory slots)
- **Functions:** `LevelUp()`, `TakeDamage(int amount)`, `Heal(int amount)`, `GainXP(int amount)`.

### 2. Enemy
- **Types:** Normal, Elite, Boss (Consider Inheritance: `class Boss : Enemy`).
- **Stats:** HP, Attack, Defense, Level.
- **Attributes:** Name, DroppedLoot, DroppedGold, DroppedXP.
- **Functions:** `Attack(Player target)`, `TakeDamage(int amount)`. Bosses might have special phase mechanics.

### 3. Item
- **Types:** Consumables (Potions), Equipment (Weapons/Armor), Quest Items, Materials.
- **Attributes:** Name, Description, Value (Price), Weight/Slot Size, Rarity, Level Requirement.
- **Equipment Specifics:** Stat bonuses (e.g., +10 Strength).

### 4. Quest
- **Types:** Main Story, Side Quest, Daily/Weekly.
- **Attributes:** Name, Level Requirement, Description, Objectives, Rewards (XP, Gold, Items).
- **State:** IsCompleted, IsActive.

### 5. Dungeon / Location
- **Attributes:** Name, Difficulty Level, List of Enemies, Boss, Environmental Traps, Clear Rewards.

### 6. Skill
- **Attributes:** Name, Description, ManaCost, DamageMultiplier, Cooldown, Type (Active/Passive).
