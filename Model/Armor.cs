namespace ConsoleRPG.Model
{

    // Basic armor model used by Player; provides defense bonuses.
    public class Armor : Equipment
    {
        public int DefenseBonus { get; private set; }

        // Creates armor with a defense bonus.
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


        // Applies the defense bonus when equipped.
        public override void Equip(Player player)
        {
            // Minimal: apply defense bonus when equipped.
            // (If you later add unequip logic, you can reverse this.)
            player.IncreaseDefense(DefenseBonus);
        }


        // Uses the armor by equipping it.
        public override void Use(Player player)
        {
            Equip(player);
        }
    }
}

