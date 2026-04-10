namespace ConsoleRPG.Model
{

    // Base abstract class for all skills.
    public abstract class Skill
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int RequiredLevel { get; private set; }

    
        // Initializes a skill with common metadata.

        protected Skill(string name, string description, int requiredLevel)
        {
            Name = name;
            Description = description;
            RequiredLevel = requiredLevel;
        }
    }


    // Active skills that the player manually uses in combat.
    public abstract class ActiveSkill : Skill
    {
        public int ManaCost { get; private set; }
        public int CooldownTurns { get; private set; }

    
        // Initializes an active skill with cost and cooldown.

        protected ActiveSkill(string name, string description, int requiredLevel, int manaCost, int cooldown)
        : base(name, description, requiredLevel)
        {
            ManaCost = manaCost;
            CooldownTurns = cooldown;
        }

    
        // Executes the skill effect on the target.

        public abstract void Execute(Player caster, Enemy target);
    }


    // Active skill that deals direct damage.
    public class DamageSkill : ActiveSkill
    {
        public int BaseDamage { get; private set; }

    
        // Creates a damage skill with base damage and mana cost.

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

    
        // Spends mana and applies intelligence-scaled damage to the target.

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


    // Active skill that restores HP to the caster.
    public class HealSkill : ActiveSkill
    {
        public int HealAmount { get; private set; }

    
        // Creates a healing skill with a fixed heal amount.

        public HealSkill(string name, string description, int requiredLevel, int manaCost, int cooldown, int HealAmount)
        : base(name, description, requiredLevel, manaCost, cooldown)
        {
            this.HealAmount = HealAmount;
        }

    
        // Spends mana and heals the caster.

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


    // Shadow "Arise" skill: extracted shadows become usable skills in combat.
    public class ShadowStrikeSkill : ActiveSkill
    {
        public string ShadowName { get; }
        public int ShadowLevel { get; }
        public int ShadowStrength { get; }

    
        // Creates a combat skill bound to a captured shadow's stats.

        public ShadowStrikeSkill(Shadow shadow)
            : base(
                name: $"Shadow: {shadow.Name}",
                description: "Command your shadow to strike the enemy.",
                requiredLevel: 1,
                manaCost: Math.Clamp(2 + shadow.Level / 3, 2, 10),
                cooldown: 0)
        {
            ShadowName = shadow.Name;
            ShadowLevel = shadow.Level;
            ShadowStrength = shadow.Strength;
        }

    
        // Uses shadow power to deal damage scaled by shadow and caster stats.

        public override void Execute(Player caster, Enemy target)
        {
            if (caster.MP < ManaCost)
            {
                return;
            }

            caster.ConsumeMana(ManaCost);

            // Scale with shadow STR and a little of player's STR/INT so it stays relevant.
            var damage = Math.Max(1, ShadowStrength + caster.Strength / 4 + caster.Intelligence / 4);
            target.TakeDamage(damage);
        }
    }


    // Passive skills give permanent bonuses.
    public class PassiveSkill : Skill
    {
        public int BonusStrength { get; private set; }
        public int BonusDefense { get; private set; }

    
        // Creates a passive skill that boosts stats.

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

    
        // Applies the passive bonuses to the player.

        public void Apply(Player player)
        {
            player.IncreaseStrength(BonusStrength);
            player.IncreaseDefense(BonusDefense);

            Console.WriteLine($"{player.Name} gained passive bonuses from {Name}!");
        }

    }
}
