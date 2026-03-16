namespace ConsoleRPG.Model
{
    // Basic Accessory model used by Player. Expand with more fields later.
    public class Accessory : Equipment
    {
        public int AttackBonus { get; private set; }
        public int DefenseBonus { get; private set; }

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

        public override void Equip(Player player)
        {
            // Later you can modify player stats here when equipping accessories.
        }
    }
}

