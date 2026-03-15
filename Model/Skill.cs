namespace ConsoleRPG.Model
{
    //Base abstract class for all skills
    public abstract class Skill
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int RequiredLevel { get; private set; }

        protected Skill(string name, string description, int requiredLevel)
        {
            Name = name;
            Description = description;
            RequiredLevel = requiredLevel;
        }
    }

    //Active skills that the player manually uses in combat
    public abstract class ActiveSkill : Skill
    {
        public int ManaCost { get; private set; }
        public int CooldownTurns { get; private set; }

        protected ActiveSkill(string name, string description, int requiredLevel, int manaCost, int cooldown)
        : base(name, description, requiredLevel)
        {
            ManaCost = manaCost;
            CooldownTurns = cooldown;
        }

        //Each skill must define how it affects the target
        public abstract void Execute(Player caster, Enemy target);
    }

    // Example Damage Skill
    public class DamageSkill : ActiveSkill
    {
        public int BaseDamage { get; private set; }

        public DamageSkill(
            string name,
            string description,
            int requiredLevel,
            int manaCost,
            int cooldown,
            int baseDamage
        ) : base(name, description, requiredLevel, manaCost, cooldown)
        {
            BaseDamage = baseDamage;
        }

        public override void Execute(Player caster, Enemy target)
        {
            if (caster.MP < ManaCost)
            {
                Console.WriteLine("Not enough mana!");
                return;
            }

            // Consume mana
            caster.ConsumeMana(ManaCost);

            // Calculate damage
            int totalDamage = BaseDamage + caster.Intelligence;

            Console.WriteLine($"{caster.Name} used {Name}!");

            target.TakeDamage(totalDamage);

            Console.WriteLine($"{target.Name} took {totalDamage} damage!");
        }
    }

    //Example Healing Skill
    public class HealSkill : ActiveSkill
    {
        public int HealAmount { get; private set; }

        public HealSkill(string name, string description, int requiredLevel, int manaCost, int cooldown, int HealAmount)
        : base(name, description, requiredLevel, manaCost, cooldown)
        {
            this.HealAmount = HealAmount;
        }

        public override void Execute(Player caster, Enemy target)
        {
            if (caster.MP < ManaCost)
            {
                Console.WriteLine("Not Enough mana!");
                return;
            }

            caster.ConsumeMana(ManaCost);

            int totalHeal = HealAmount + caster.Intelligence;

            caster.Heal(totalHeal);
            Console.WriteLine($"{caster.Name} used {Name} and healed {totalHeal} HP!");
        }
    }

    // Passive skills give permanent bonuses
    public class PassiveSkill : Skill
    {
        public int BonusStrength { get; private set; }
        public int BonusDefense { get; private set; }

        public PassiveSkill(
            string name,
            string description,
            int requiredLevel,
            int bonusStrength,
            int bonusDefense
        ) : base(name, description, requiredLevel)
        {
            BonusStrength = bonusStrength;
            BonusDefense = bonusDefense;
        }

        // Apply passive bonuses
        public void Apply(Player player)
        {
            player.IncreaseStrength(BonusStrength);
            player.IncreaseDefense(BonusDefense);

            Console.WriteLine($"{player.Name} gained passive bonuses from {Name}!");
        }

    }
}
