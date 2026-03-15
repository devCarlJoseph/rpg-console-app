using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    // Base class for all enemies in the game
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
        public Enemy(string name, int Level)
        {
            Name = name;
            Level = Level;
            MaxHP = 50 + (Level * 10);
            HP = MaxHP;
            Strength = 10 + (Level * 2);
            Defense = 5 + Level;
        }
        // --- Methods ---

        // Enemy attack damage
        public virtual int Attack()
        {
            return Strength;
        }

        // Damage calculation
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

    // Example of a stronger enemy Boss
    public class OrcWarlord : Enemy
    {
        public int SpecialAttackPower { get; private set; }

        public OrcWarlord(string name, int level) : base(name, level)
        {
            MaxHP += 500;
            HP = MaxHP;
            Strength += 80;
            Defense += 70;
            SpecialAttackPower = 90;
        }

        public override int Attack()
        {
            return Strength + SpecialAttackPower;
        }

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

    public class Demonlord : Enemy
    {
        public int SpecialAttackPower { get; private set; }

        public Demonlord(string name, int level) : base(name, level)
        {
            MaxHP += 700;
            HP = MaxHP;
            Strength += 110;
            Defense += 85;
            SpecialAttackPower = 95;
        }

        public override int Attack()
        {
            return Strength + SpecialAttackPower;
        }

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

    // Example of a weaker enemy
    public class Slime : Enemy
    {
        public Slime(string name, int level) : base(name, level)
        {
            MaxHP = 20;
            HP = MaxHP;
            Strength = 5;
            Defense = 2;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class Goblin : Enemy
    {
        public Goblin(string name, int level) : base(name, level)
        {
            MaxHP = 30;
            HP = MaxHP;
            Strength = 10;
            Defense = 5;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class Skeleton : Enemy
    {
        public Skeleton(string name, int level) : base(name, level)
        {
            MaxHP = 40;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class WildWolf : Enemy
    {
        public WildWolf(string name, int level) : base(name, level)
        {
            MaxHP = 60;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class CaveBat : Enemy
    {
        public CaveBat(string name, int level) : base(name, level)
        {
            MaxHP = 30;
            HP = MaxHP;
            Strength = 10;
            Defense = 5;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class Zombie : Enemy
    {
        public Zombie(string name, int level) : base(name, level)
        {
            MaxHP = 40;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    //Elite Enemy
    public class GoblinWarrior : Enemy
    {
        public GoblinWarrior(string name, int level) : base(name, level)
        {
            MaxHP = 70;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class GoblinMage : Enemy
    {
        public GoblinMage(string name, int level) : base(name, level)
        {
            MaxHP = 80;
            HP = MaxHP;
            Strength = 15;
            Defense = 10;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class DarkMage : Enemy
    {
        public DarkMage(string name, int level) : base(name, level)
        {
            MaxHP = 100;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class Dragon : Enemy
    {
        public Dragon(string name, int level) : base(name, level)
        {
            MaxHP = 100;
            HP = MaxHP;
            Strength = 20;
            Defense = 15;
        }

        public override int Attack()
        {
            return Strength;
        }

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

    public class UndeadKnight : Enemy
    {
        public UndeadKnight(string name, int level) : base(name, level)
        {
            MaxHP = 150;
            HP = MaxHP;
            Strength = 35;
            Defense = 25;
        }

        public override int Attack()
        {
            return Strength;
        }

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