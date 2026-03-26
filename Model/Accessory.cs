namespace ConsoleRPG.Model
{
    /// <summary>
    /// Accessory that grants both attack and defense bonuses.
    /// </summary>
    public class Accessory : Equipment
    {
        public int AttackBonus { get; private set; }
        public int DefenseBonus { get; private set; }

        /// <summary>
        /// Creates an accessory with dual bonuses.
        /// </summary>
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

        /// <summary>
        /// Applies attack and defense bonuses when equipped.
        /// </summary>
        public override void Equip(Player player)
        {
            player.IncreaseStrength(AttackBonus);
            player.IncreaseDefense(DefenseBonus);
        }

        /// <summary>
        /// Uses the accessory by equipping it.
        /// </summary>
        public override void Use(Player player)
        {
            Equip(player);
        }
    }
}

