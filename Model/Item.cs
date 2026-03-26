using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    /// <summary>
    /// Base item with common fields such as name, value, rarity, and requirements.
    /// </summary>
    public abstract class Item : IItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Value { get; private set; }
        public int Weight { get; private set; }
        public string Rarity { get; private set; }
        public int LevelRequirement { get; private set; }

        /// <summary>
        /// Initializes shared item metadata.
        /// </summary>
        protected Item(string name, string description, int value, int weight, string rarity, int levelRequirement)
        {
            Name = name;
            Description = description;
            Value = value;
            Weight = weight;
            Rarity = rarity;
            LevelRequirement = levelRequirement;
        }

        /// <summary>
        /// Applies the item's effect to the player.
        /// </summary>
        public abstract void Use(Player player);
    }

    /// <summary>
    /// Base class for equippable items such as weapons, armor, accessories.
    /// </summary>
    public abstract class Equipment : Item
    {

        /// <summary>
        /// Passes common equipment metadata to the base item.
        /// </summary>
        protected Equipment(string name, string description, int value, int weight, string rarity, int levelRequirement)
        : base(name, description, value, weight, rarity, levelRequirement) { }
        /// <summary>
        /// Equips the item on the player, applying relevant bonuses.
        /// </summary>
        public abstract void Equip(Player player);
    }

    /// <summary>
    /// Offensive equipment that boosts player attack.
    /// </summary>
    public class Weapon : Equipment
    {
        public int AttackBonus { get; private set; }
        /// <summary>
        /// Creates a weapon with the given stats and attack bonus.
        /// </summary>
        protected Weapon(string name, string description, int value, int weight, string rarity, int levelRequirement, int attackBonus)
        : base(name, description, value, weight, rarity, levelRequirement)
        {
            AttackBonus = attackBonus;
        }
        /// <summary>
        /// Equips the weapon by delegating to the player's equip logic.
        /// </summary>
        public override void Equip(Player player)
        {
            player.EquipWeapon(this);
        }

        /// <summary>
        /// Uses the weapon, which simply equips it.
        /// </summary>
        public override void Use(Player player)
        {
            Equip(player);
        }
    }

    /// <summary>
    /// Single-use items that restore health or provide effects.
    /// </summary>
    public class Consumable : Item
    {
        public int HealAmount { get; private set; }
        /// <summary>
        /// Creates a consumable with a heal value.
        /// </summary>
        protected Consumable(string name, string description, int value, int weight, string rarity, int levelRequirement, int healAmount)
        : base(name, description, value, weight, rarity, levelRequirement)
        {
            HealAmount = healAmount;
        }

        /// <summary>
        /// Heals the player when consumed.
        /// </summary>
        public override void Use(Player player)
        {
            player.Heal(HealAmount);
        }
    }

    /// <summary>
    /// Crafting material placeholders; not directly usable yet.
    /// </summary>
    public class Material : Item
    {
        public int StackSize { get; private set; }
        /// <summary>
        /// Creates a material stack with capacity.
        /// </summary>
        protected Material(string name, string description, int value, int weight, string rarity, int levelRequirement, int stackSize)
        : base(name, description, value, weight, rarity, levelRequirement)
        {
            StackSize = stackSize;
        }

        /// <summary>
        /// Materials currently have no direct use effect.
        /// </summary>
        public override void Use(Player player)
        {
            // Materials are not directly usable for now.
        }
    }
}

