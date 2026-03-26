namespace ConsoleRPG.Model
{
    /// <summary>
    /// Base abstract class for all skills.
    /// </summary>
    public abstract class Skill
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int RequiredLevel { get; private set; }

        /// <summary>
        /// Initializes a skill with common metadata.
        /// </summary>
        protected Skill(string name, string description, int requiredLevel)
        {
            Name = name;
            Description = description;
            RequiredLevel = requiredLevel;
        }
    }

    /// <summary>
    /// Active skills that the player manually uses in combat.
    /// </summary>
    public abstract class ActiveSkill : Skill
    {
        public int ManaCost { get; private set; }
        public int CooldownTurns { get; private set; }

        /// <summary>
        /// Initializes an active skill with cost and cooldown.
        /// </summary>
        protected ActiveSkill(string name, string description, int requiredLevel, int manaCost, int cooldown)
        : base(name, description, requiredLevel)
        {
            ManaCost = manaCost;
            CooldownTurns = cooldown;
        }

        /// <summary>
        /// Executes the skill effect on the target.
        /// </summary>
        public abstract void Execute(Player caster, Enemy target);
    }

    /// <summary>
    /// Active skill that deals direct damage.
    /// </summary>
    public class DamageSkill : ActiveSkill
    {
        public int BaseDamage { get; private set; }

        /// <summary>
        /// Creates a damage skill with base damage and mana cost.
        /// </summary>
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

        /// <summary>
        /// Spends mana and applies intelligence-scaled damage to the target.
        /// </summary>
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

    /// <summary>
    /// Active skill that restores HP to the caster.
    /// </summary>
    public class HealSkill : ActiveSkill
    {
        public int HealAmount { get; private set; }

        /// <summary>
        /// Creates a healing skill with a fixed heal amount.
        /// </summary>
        public HealSkill(string name, string description, int requiredLevel, int manaCost, int cooldown, int HealAmount)
        : base(name, description, requiredLevel, manaCost, cooldown)
        {
            this.HealAmount = HealAmount;
        }

        /// <summary>
        /// Spends mana and heals the caster.
        /// </summary>
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

    /// <summary>
    /// Shadow "Arise" skill: extracted shadows become usable skills in combat.
    /// </summary>
    public class ShadowStrikeSkill : ActiveSkill
    {
        public string ShadowName { get; }
        public int ShadowLevel { get; }
        public int ShadowStrength { get; }

        /// <summary>
        /// Creates a combat skill bound to a captured shadow's stats.
        /// </summary>
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

        /// <summary>
        /// Uses shadow power to deal damage scaled by shadow and caster stats.
        /// </summary>
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

    /// <summary>
    /// Passive skills give permanent bonuses.
    /// </summary>
    public class PassiveSkill : Skill
    {
        public int BonusStrength { get; private set; }
        public int BonusDefense { get; private set; }

        /// <summary>
        /// Creates a passive skill that boosts stats.
        /// </summary>
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

        /// <summary>
        /// Applies the passive bonuses to the player.
        /// </summary>
        public void Apply(Player player)
        {
            player.IncreaseStrength(BonusStrength);
            player.IncreaseDefense(BonusDefense);

            Console.WriteLine($"{player.Name} gained passive bonuses from {Name}!");
        }

    }
}
