namespace ConsoleRPG.Model
{

    public abstract class Item
    {
        public string Name {get; private set;}
        public string Description {get; private set;}
        public int Value {get; private set;}
        public int Weight {get; private set;}
        public string Rarity {get; private set;}
        public int LevelRequirement {get; private set;}

        protected Item(string name, string description, int value, int weight, string rarity, int levelRequirement)
        {
            Name = name;
            Description = description;
            Value = value;
            Weight = weight;
            Rarity = rarity;
            LevelRequirement = levelRequirement;
        }
    }

    public abstract class Equipment : Item
    {

        protected Equipment(string name, string description, int value, int weight, string rarity, int levelRequirement) 
        : base(name, description, value, weight, rarity, levelRequirement){}
        public abstract void (Player player);
    }

    public class Weapon : Equipment
    {
        public int AttackBonus { get; private set; }
        protected Weapon(string name, string description, int value, int weight, string rarity, int levelRequirement, int attackBonus) 
        : base(name, description, value, weight, rarity, levelRequirement){
            AttackBonus = attackBonus;
        }
        public override void Equip(Player player)
        {
            player.EquippedWeapon = this;
            player.Strength += AttackBonus;
        }
    }

    public class Consumable : Item
    {
        public int HealAmount { get; private set; }
        protected Consumable(string name, string description, int value, int weight, string rarity, int levelRequirement, int healAmount) 
        : base(name, description, value, weight, rarity, levelRequirement){
            HealAmount = healAmount;
        }
    }

    public class Material : Item
    {
        public int StackSize { get; private set; }
        protected Material(string name, string description, int value, int weight, string rarity, int levelRequirement, int stackSize) 
        : base(name, description, value, weight, rarity, levelRequirement){
            StackSize = stackSize;
        }
    }
}

