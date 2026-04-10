using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    
    // Base item with common fields such as name, value, rarity, and requirements.

    public abstract class Item : IItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Value { get; private set; }
        public int Weight { get; private set; }
        public string Rarity { get; private set; }
        public int LevelRequirement { get; private set; }

        
        // Initializes shared item metadata.
    
        protected Item(string name, string description, int value, int weight, string rarity, int levelRequirement)
        {
            Name = name;
            Description = description;
            Value = value;
            Weight = weight;
            Rarity = rarity;
            LevelRequirement = levelRequirement;
        }

        
        // Applies the item's effect to the player.
    
        public abstract void Use(Player player);
    }

    
    // Base class for equippable items such as weapons, armor, accessories.

    public abstract class Equipment : Item
    {

        
        // Passes common equipment metadata to the base item.
    
        protected Equipment(string name, string description, int value, int weight, string rarity, int levelRequirement)
        : base(name, description, value, weight, rarity, levelRequirement) { }
        
        // Equips the item on the player, applying relevant bonuses.
        public abstract void Equip(Player player);
    }

    
    // Offensive equipment that boosts player attack.

    public class Weapon : Equipment
    {
        public int AttackBonus { get; private set; }
        
        // Creates a weapon with the given stats and attack bonus.
    
        protected Weapon(string name, string description, int value, int weight, string rarity, int levelRequirement, int attackBonus)
        : base(name, description, value, weight, rarity, levelRequirement)
        {
            AttackBonus = attackBonus;
        }
        
        // Equips the weapon by delegating to the player's equip logic.
    
        public override void Equip(Player player)
        {
            player.EquipWeapon(this);
        }

        
        // Uses the weapon, which simply equips it.
    
        public override void Use(Player player)
        {
            Equip(player);
        }
    }

    
    // Single-use items that restore health or provide effects.

    public class Consumable : Item
    {
        public int HealAmount { get; private set; }
        
        // Creates a consumable with a heal value.
    
        protected Consumable(string name, string description, int value, int weight, string rarity, int levelRequirement, int healAmount)
        : base(name, description, value, weight, rarity, levelRequirement)
        {
            HealAmount = healAmount;
        }

        
        // Heals the player when consumed.
    
        public override void Use(Player player)
        {
            player.Heal(HealAmount);
        }
    }

    
    // Crafting material placeholders; not directly usable yet.

    public class Material : Item
    {
        public int StackSize { get; private set; }
        
        // Creates a material stack with capacity.
    
        protected Material(string name, string description, int value, int weight, string rarity, int levelRequirement, int stackSize)
        : base(name, description, value, weight, rarity, levelRequirement)
        {
            StackSize = stackSize;
        }

        
        // Materials currently have no direct use effect.
    
        public override void Use(Player player)
        {
            // Materials are not directly usable for now.
        }
    }
}

