using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    /// <summary>
    /// Represents the player character with stats, inventory, skills, and quests.
    /// </summary>
    public class Player : IEntity
    {
        // --- Basic Attributes ---
        public string Name { get; set; }
        public int Level { get; private set; } = 1;
        public int XP { get; private set; } = 0;
        public int Gold { get; private set; } = 0;
        public bool IsAlive => HP > 0;

        // --- Core Stats ---
        public int MaxHP { get; private set; } = 100;
        public int HP { get; set; }
        public int MaxMP { get; private set; } = 50;
        public int EnergyPoints { get; private set; } = 50;
        public int MP { get; set; }
        public int Strength { get; private set; } = 10;
        public int Agility { get; private set; } = 10;
        public int Intelligence { get; private set; } = 10; 
        public int Defense { get; private set; } = 5; 

        // --- Collections & Equipment ---
        public Inventory Inventory { get; private set; } = new Inventory();
        public List<Skill> ActiveSkills { get; private set; } = new List<Skill>();
        public List<Skill> PassiveSkills { get; private set; } = new List<Skill>();
        public List<Quest> ActiveQuests { get; private set; } = new List<Quest>();
        public Weapon? EquippedWeapon { get; private set; }
        public Armor? EquippedArmor { get; private set; }
        public Accessory? EquippedAccessory { get; private set; }
        public List<Quest> CompletedQuests { get; private set; } = new List<Quest>();
        public List<Shadow> Shadows { get; private set; } = new List<Shadow>();

        // --- Constructor
        /// <summary>
        /// Creates a new player with default starting stats and currency.
        /// </summary>
        public Player(string name)
        {
            Name = name;
            HP = MaxHP;
            MP = MaxMP;
            Gold = 50;
        }

        // --- Methods (Behaviors) ---
        /// <summary>
        /// Reduces HP after factoring in defense, never dropping below zero.
        /// </summary>
        public void TakeDamage(int damage)
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

        /// <summary>
        /// Heals the player by a specified amount, capped at MaxHP.
        /// </summary>
        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
        }

        /// <summary>
        /// Grants XP and triggers level-ups until excess XP is consumed.
        /// </summary>
        public void GainXP(int amount)
        {
            XP += amount;
            while (XP >= XPToLevelUp())
            {
                XP -= XPToLevelUp();
                LevelUp();
            }
        }

        public int XPToNextLevel => XPToLevelUp() - XP;

        /// <summary>
        /// Increases stats for a level gain and restores HP/MP.
        /// </summary>
        private void LevelUp()
        {
            Level++;
            MaxHP += 20;
            MaxMP += 10;
            Strength += 2;
            Agility += 2;
            Intelligence += 2;
            Defense += 1;
            HP = MaxHP;
            MP = MaxMP;
        }

        /// <summary>
        /// Calculates the XP threshold required to reach the next level.
        /// </summary>
        private int XPToLevelUp()
        {
            // Example: XP required increases per level
            return 100 + (Level - 1) * 50;
        }

        /// <summary>
        /// Adds positive gold amounts to the player's wallet.
        /// </summary>
        public void AddGold(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            Gold += amount;
        }

        /// <summary>
        /// Attempts to spend gold; returns true if the transaction succeeds.
        /// </summary>
        public bool TrySpendGold(int amount)
        {
            if (amount <= 0)
            {
                return true;
            }

            if (Gold < amount)
            {
                return false;
            }

            Gold -= amount;
            return true;
        }

        /// <summary>
        /// Equips a weapon, updating strength by removing old bonuses and applying new ones.
        /// </summary>
        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                Strength -= EquippedWeapon.AttackBonus;
            }

            EquippedWeapon = weapon;
            Strength += weapon.AttackBonus;
        }

        /// <summary>
        /// Consumes MP when casting abilities; floors at zero.
        /// </summary>
        public void ConsumeMana(int amount)
        {
            MP -= amount;
            if (MP < 0)
            {
                MP = 0;
            }
        }

        /// <summary>
        /// Restores MP by the given amount, capped at MaxMP.
        /// </summary>
        public void RestoreMana(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            MP += amount;
            if (MP > MaxMP)
            {
                MP = MaxMP;
            }
        }

        /// <summary>
        /// Permanently increases strength by the provided amount.
        /// </summary>
        public void IncreaseStrength(int amount)
        {
            Strength += amount;
        }

        /// <summary>
        /// Permanently increases defense by the provided amount.
        /// </summary>
        public void IncreaseDefense(int amount)
        {
            Defense += amount;
        }

        /// <summary>
        /// Performs a basic attack against an enemy, including weapon bonus.
        /// </summary>
        public void Attack(Enemy target)
        {
            int damage = Strength;
            if (EquippedWeapon != null)
            {
                damage += EquippedWeapon.AttackBonus;
            }

            Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            target.TakeDamage(damage);
        }
    }
}

