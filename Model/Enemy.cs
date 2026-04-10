using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    // Base enemy type with common combat stats and behaviors.
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
        // Initializes an enemy with level-scaled stats.
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
        // Returns the base attack damage for this enemy.
        public virtual int Attack()
        {
            return Strength;
        }

        // Applies incoming damage after defense reduction.
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


    // Stronger boss-type enemy with higher stats and special power.
    public class OrcWarlord : Enemy
    {
        public int SpecialAttackPower { get; private set; }


        // Boosts base stats to create an Orc Warlord.
        
        public OrcWarlord(string name, int level) : base(name, level)
        {
            MaxHP += 500;
            HP = MaxHP;
            Strength += 80;
            Defense += 70;
            SpecialAttackPower = 90;
        }


        // Attacks with base strength plus special power.
        
        public override int Attack()
        {
            return Strength + SpecialAttackPower;
        }


        // Applies damage with slightly increased mitigation.
        
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


    // High-tier boss with heavy health and power scaling.
    
    public class Demonlord : Enemy
    {
        public int SpecialAttackPower { get; private set; }


        // Boosts base stats to create a Demonlord.
        
        public Demonlord(string name, int level) : base(name, level)
        {
            MaxHP += 700;
            HP = MaxHP;
            Strength += 110;
            Defense += 85;
            SpecialAttackPower = 95;
        }


        // Attacks with base strength plus special power.
        
        public override int Attack()
        {
            return Strength + SpecialAttackPower;
        }


        // Applies damage with added mitigation compared to the base enemy.
        
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


    // Low-level enemy with minimal stats.
    
    public class Slime : Enemy
    {

        // Sets lightweight stats for the slime.
        
        public Slime(string name, int level) : base(name, level)
        {
            MaxHP = 20;
            HP = MaxHP;
            Strength = 5;
            Defense = 2;
        }


        // Uses base strength for its attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies incoming damage with slime-specific defense.
        
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


    // Standard early-game goblin foe.
    
    public class Goblin : Enemy
    {
    
        // Sets baseline goblin stats.
        
        public Goblin(string name, int level) : base(name, level)
        {
            MaxHP = 30;
            HP = MaxHP;
            Strength = 10;
            Defense = 5;
        }

    
        // Goblin basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage with goblin defenses.
        
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


    // Mid-tier skeletal enemy with balanced stats.
    
    public class Skeleton : Enemy
    {
    
        // Sets skeleton-specific stats.
        
        public Skeleton(string name, int level) : base(name, level)
        {
            MaxHP = 40;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

    
        // Skeleton basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage with skeleton defenses.
        
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


    // Fast wolf enemy with moderate strength.
    
    public class WildWolf : Enemy
    {
    
        // Sets wolf stats.
        
        public WildWolf(string name, int level) : base(name, level)
        {
            MaxHP = 60;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

    
        // Wolf basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage using wolf defenses.
        
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


    // Agile bat enemy with low defenses.
    
    public class CaveBat : Enemy
    {
    
        // Sets bat stats.
        
        public CaveBat(string name, int level) : base(name, level)
        {
            MaxHP = 30;
            HP = MaxHP;
            Strength = 10;
            Defense = 5;
        }

    
        // Bat basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage using bat defenses.
        
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


    // Slow zombie enemy with basic stats.
    
    public class Zombie : Enemy
    {
    
        // Sets zombie stats.
        
        public Zombie(string name, int level) : base(name, level)
        {
            MaxHP = 40;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

    
        // Zombie basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage using zombie defenses.
        
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


    // Elite goblin variant with tougher stats.
    
    public class GoblinWarrior : Enemy
    {
    
        // Sets elite goblin warrior stats.
        
        public GoblinWarrior(string name, int level) : base(name, level)
        {
            MaxHP = 70;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

    
        // Goblin warrior basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage using warrior defenses.
        
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


    // Magical goblin variant with balanced stats.
    
    public class GoblinMage : Enemy
    {
    
        // Sets goblin mage stats.
        
        public GoblinMage(string name, int level) : base(name, level)
        {
            MaxHP = 80;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

    
        // Goblin mage basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage using mage defenses.
        
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


    // High-level caster enemy.
    
    public class DarkMage : Enemy
    {
    
        // Sets dark mage stats.
        
        public DarkMage(string name, int level) : base(name, level)
        {
            MaxHP = 100;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

    
        // Dark mage basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage using mage defenses.
        
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


    // Powerful dragon enemy archetype.
    
    public class Dragon : Enemy
    {
    
        // Sets dragon stats.
        
        public Dragon(string name, int level) : base(name, level)
        {
            MaxHP = 100;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

    
        // Dragon basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage using dragon defenses.
        
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


    // Late-game armored undead knight.
    
    public class UndeadKnight : Enemy
    {
    
        // Sets undead knight stats.
        
        public UndeadKnight(string name, int level) : base(name, level)
        {
            MaxHP = 150;
            HP = MaxHP;
            Strength = 35;
            Defense = 25;
        }

    
        // Knight basic attack.
        
        public override int Attack()
        {
            return Strength;
        }

    
        // Applies damage using knight defenses.
        
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
