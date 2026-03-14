using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    // Base class for all enemies in the game
    public class Enemy : IEntity
    {
        // --- Basic Information ---
        //public string Name { get; protected set; }
        //public int Level { get; protected set; }

        // --- Core Stats ---
        //public int MaxHP { get; protected set; }
        //public int HP { get; set; }

        //public int Strength { get; protected set; }
        //public int Defense { get; protected set; }

        // --- Constructor ---
        //public Enemy(string name, int level)
        //{
        //    Name = name;
        //    Level = level;

        //    MaxHP = 50 + (level * 10);
        //    HP = MaxHP;

        //    Strength = 10 + (level * 2);
        //    Defense = 5 + level;
        //}

        // --- Methods ---

        // Enemy attack damage
        //public virtual int Attack()
        //{
        //    return Strength;
        //}

        // Damage calculation
        //public virtual void TakeDamage(int damage)
        //{
        //    int damageTaken = damage - Defense;

        //    if (damageTaken < 0)
        //        damageTaken = 0;

        //    HP -= damageTaken;

        //    if (HP < 0)
        //        HP = 0;
        //}

        //public bool IsDead()
        //{
        //    return HP <= 0;
        //}
    }

    // Example of a stronger enemy
    //public class Boss : Enemy
    //{
    //    public int SpecialAttackPower { get; private set; }

        // Call base constructor
    //    public Boss(string name, int level) : base(name, level)
    //    {
    //        MaxHP += 100;
    //        HP = MaxHP;

    //        Strength += 10;
    //        Defense += 5;

    //        SpecialAttackPower = 25;
    //    }

        // Boss deals stronger damage
    //    public override int Attack()
    //    {
    //        return Strength + SpecialAttackPower;
    //    }

        // Boss takes slightly reduced damage
    //    public override void TakeDamage(int damage)
    //    {
    //        int reducedDamage = damage - (Defense + 5);

    //        if (reducedDamage < 0)
    //            reducedDamage = 0;

    //        HP -= reducedDamage;

    //        if (HP < 0)
    //            HP = 0;
    //    }
    //}
}