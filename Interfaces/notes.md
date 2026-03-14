# Interfaces Guide

Use interfaces to decouple your code and allow for abstraction and polymorphism.

## OOP Pillars Applied Here:
- **Abstraction**: Interfaces are pure abstraction. They define *what* classes can do (the "Contracts") without dictating *how* they do it. The rest of your application can depend on `IEntity` instead of `Player` or `Enemy`, reducing tight coupling.
- **Polymorphism**: By having multiple classes implement the same interface (like `Player` and `Enemy` both implementing `IEntity`, or `Sword` and `Potion` both implementing `IItem`), you can treat them interchangeably. A `List<IItem> Inventory` can hold anything that is an item and call `.Use()` on it, with each item behaving differently based on its own specific implementation.

## Essential Files (Base Contracts):

### `IEntity`
- `int HP { get; set; }`
- `int MaxHP { get; }`
- `void TakeDamage(int damage)`
- Both `Player` and `Enemy` can implement this.

### `IItem`
- `string Name { get; }`
- `int Value { get; }`
- `void Use(Player player)`
- Potions inside inventory can implement this. When the player uses an item, you just call `.Use()` on the generic interface.

### `ISkill`
- `string Name { get; }`
- `int ManaCost { get; }`
- `void Execute(IEntity target)`
- Both Player skills and Enemy skills can implement this structure.

### `IQuest`
- Defines the common structure for different types of quests.