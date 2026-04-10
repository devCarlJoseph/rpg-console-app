namespace ConsoleRPG.Model
{
    // Accessory that grants both attack and defense bonuses.
    public class Accessory : Equipment
    {
        public int AttackBonus { get; private set; }
        public int DefenseBonus { get; private set; }

        // Creates an accessory with dual bonuses.
        public Accessory(
            string name,
            string description,
            int value,
            int weight,
            string rarity,
            int levelRequirement,
            int attackBonus,
            int defenseBonus)
            : base(name, description, value, weight, rarity, levelRequirement)
        {
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
        }

        // Applies attack and defense bonuses when equipped.
        public override void Equip(Player player)
        {
            player.IncreaseStrength(AttackBonus);
            player.IncreaseDefense(DefenseBonus);
        }

        // Uses the accessory by equipping it.
        public override void Use(Player player)
        {
            Equip(player);
        }
    }
}

