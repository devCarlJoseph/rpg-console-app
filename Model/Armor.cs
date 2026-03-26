namespace ConsoleRPG.Model
{
    /// <summary>
    /// Basic armor model used by Player; provides defense bonuses.
    /// </summary>
    public class Armor : Equipment
    {
        public int DefenseBonus { get; private set; }

        /// <summary>
        /// Creates armor with a defense bonus.
        /// </summary>
        public Armor(
            string name,
            string description,
            int value,
            int weight,
            string rarity,
            int levelRequirement,
            int defenseBonus)
            : base(name, description, value, weight, rarity, levelRequirement)
        {
            DefenseBonus = defenseBonus;
        }

        /// <summary>
        /// Applies the defense bonus when equipped.
        /// </summary>
        public override void Equip(Player player)
        {
            // Minimal: apply defense bonus when equipped.
            // (If you later add unequip logic, you can reverse this.)
            player.IncreaseDefense(DefenseBonus);
        }

        /// <summary>
        /// Uses the armor by equipping it.
        /// </summary>
        public override void Use(Player player)
        {
            Equip(player);
        }
    }
}

