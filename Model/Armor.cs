namespace ConsoleRPG.Model
{
    // Basic Armor model used by Player. Expand with more fields later.
    public class Armor : Equipment
    {
        public int DefenseBonus { get; private set; }

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

        public override void Equip(Player player)
        {
            // Minimal: apply defense bonus when equipped.
            // (If you later add unequip logic, you can reverse this.)
            player.IncreaseDefense(DefenseBonus);
        }

        public override void Use(Player player)
        {
            Equip(player);
        }
    }
}

