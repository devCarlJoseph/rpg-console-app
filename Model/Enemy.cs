using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    /// <summary>
    /// Base enemy type with common combat stats and behaviors.
    /// </summary>
    public class Enemy : IEntity
    {
        // --- Basic Information ---
        public string Name { get; protected set; }
        public int Level { get; protected set; }
        // --- Core Stats ---
        public int MaxHP { get; protected set; }
        public int HP { get; set; }
        public bool IsAlive => HP > 0;
        public int Strength { get; protected set; }
        public int Defense { get; protected set; }

        // --- Constructor ---
        /// <summary>
        /// Initializes an enemy with level-scaled stats.
        /// </summary>
        public Enemy(string name, int level)
        {
            Name = name;
            Level = level;
            MaxHP = 50 + (level * 10);
            HP = MaxHP;
            Strength = 10 + (level * 2);
            Defense = 5 + level;
        }
        // --- Methods ---

        /// <summary>
        /// Returns the base attack damage for this enemy.
        /// </summary>
        public virtual int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies incoming damage after defense reduction.
        /// </summary>
        public virtual void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Stronger boss-type enemy with higher stats and special power.
    /// </summary>
    public class OrcWarlord : Enemy
    {
        public int SpecialAttackPower { get; private set; }

        /// <summary>
        /// Boosts base stats to create an Orc Warlord.
        /// </summary>
        public OrcWarlord(string name, int level) : base(name, level)
        {
            MaxHP += 500;
            HP = MaxHP;
            Strength += 80;
            Defense += 70;
            SpecialAttackPower = 90;
        }

        /// <summary>
        /// Attacks with base strength plus special power.
        /// </summary>
        public override int Attack()
        {
            return Strength + SpecialAttackPower;
        }

        /// <summary>
        /// Applies damage with slightly increased mitigation.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int reducedDamage = damage - (Defense + 5);

            if (reducedDamage < 0)
            {
                reducedDamage = 0;
            }

            HP -= reducedDamage;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// High-tier boss with heavy health and power scaling.
    /// </summary>
    public class Demonlord : Enemy
    {
        public int SpecialAttackPower { get; private set; }

        /// <summary>
        /// Boosts base stats to create a Demonlord.
        /// </summary>
        public Demonlord(string name, int level) : base(name, level)
        {
            MaxHP += 700;
            HP = MaxHP;
            Strength += 110;
            Defense += 85;
            SpecialAttackPower = 95;
        }

        /// <summary>
        /// Attacks with base strength plus special power.
        /// </summary>
        public override int Attack()
        {
            return Strength + SpecialAttackPower;
        }

        /// <summary>
        /// Applies damage with added mitigation compared to the base enemy.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int reducedDamage = damage - (Defense + 5);

            if (reducedDamage < 0)
            {
                reducedDamage = 0;
            }

            HP -= reducedDamage;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Low-level enemy with minimal stats.
    /// </summary>
    public class Slime : Enemy
    {
        /// <summary>
        /// Sets lightweight stats for the slime.
        /// </summary>
        public Slime(string name, int level) : base(name, level)
        {
            MaxHP = 20;
            HP = MaxHP;
            Strength = 5;
            Defense = 2;
        }

        /// <summary>
        /// Uses base strength for its attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies incoming damage with slime-specific defense.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Standard early-game goblin foe.
    /// </summary>
    public class Goblin : Enemy
    {
        /// <summary>
        /// Sets baseline goblin stats.
        /// </summary>
        public Goblin(string name, int level) : base(name, level)
        {
            MaxHP = 30;
            HP = MaxHP;
            Strength = 10;
            Defense = 5;
        }

        /// <summary>
        /// Goblin basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage with goblin defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Mid-tier skeletal enemy with balanced stats.
    /// </summary>
    public class Skeleton : Enemy
    {
        /// <summary>
        /// Sets skeleton-specific stats.
        /// </summary>
        public Skeleton(string name, int level) : base(name, level)
        {
            MaxHP = 40;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

        /// <summary>
        /// Skeleton basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage with skeleton defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Fast wolf enemy with moderate strength.
    /// </summary>
    public class WildWolf : Enemy
    {
        /// <summary>
        /// Sets wolf stats.
        /// </summary>
        public WildWolf(string name, int level) : base(name, level)
        {
            MaxHP = 60;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

        /// <summary>
        /// Wolf basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage using wolf defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Agile bat enemy with low defenses.
    /// </summary>
    public class CaveBat : Enemy
    {
        /// <summary>
        /// Sets bat stats.
        /// </summary>
        public CaveBat(string name, int level) : base(name, level)
        {
            MaxHP = 30;
            HP = MaxHP;
            Strength = 10;
            Defense = 5;
        }

        /// <summary>
        /// Bat basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage using bat defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Slow zombie enemy with basic stats.
    /// </summary>
    public class Zombie : Enemy
    {
        /// <summary>
        /// Sets zombie stats.
        /// </summary>
        public Zombie(string name, int level) : base(name, level)
        {
            MaxHP = 40;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

        /// <summary>
        /// Zombie basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage using zombie defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Elite goblin variant with tougher stats.
    /// </summary>
    public class GoblinWarrior : Enemy
    {
        /// <summary>
        /// Sets elite goblin warrior stats.
        /// </summary>
        public GoblinWarrior(string name, int level) : base(name, level)
        {
            MaxHP = 70;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

        /// <summary>
        /// Goblin warrior basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage using warrior defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Magical goblin variant with balanced stats.
    /// </summary>
    public class GoblinMage : Enemy
    {
        /// <summary>
        /// Sets goblin mage stats.
        /// </summary>
        public GoblinMage(string name, int level) : base(name, level)
        {
            MaxHP = 80;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

        /// <summary>
        /// Goblin mage basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage using mage defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// High-level caster enemy.
    /// </summary>
    public class DarkMage : Enemy
    {
        /// <summary>
        /// Sets dark mage stats.
        /// </summary>
        public DarkMage(string name, int level) : base(name, level)
        {
            MaxHP = 100;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

        /// <summary>
        /// Dark mage basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage using mage defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Powerful dragon enemy archetype.
    /// </summary>
    public class Dragon : Enemy
    {
        /// <summary>
        /// Sets dragon stats.
        /// </summary>
        public Dragon(string name, int level) : base(name, level)
        {
            MaxHP = 100;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

        /// <summary>
        /// Dragon basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage using dragon defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }

    /// <summary>
    /// Late-game armored undead knight.
    /// </summary>
    public class UndeadKnight : Enemy
    {
        /// <summary>
        /// Sets undead knight stats.
        /// </summary>
        public UndeadKnight(string name, int level) : base(name, level)
        {
            MaxHP = 150;
            HP = MaxHP;
            Strength = 35;
            Defense = 25;
        }

        /// <summary>
        /// Knight basic attack.
        /// </summary>
        public override int Attack()
        {
            return Strength;
        }

        /// <summary>
        /// Applies damage using knight defenses.
        /// </summary>
        public override void TakeDamage(int damage)
        {
            int damageTaken = damage - Defense;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            HP -= damageTaken;

            if (HP < 0)
            {
                HP = 0;
            }
        }
    }
}
