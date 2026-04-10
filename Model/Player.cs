using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{

    // Represents the player character with stats, inventory, skills, and quests.
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

        // Creates a new player with default starting stats and currency.

        public Player(string name)
        {
            Name = name;
            HP = MaxHP;
            MP = MaxMP;
            Gold = 50;
        }

        // --- Methods (Behaviors) ---

        // Reduces HP after factoring in defense, never dropping below zero.

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


        // Heals the player by a specified amount, capped at MaxHP.

        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
        }


        // Grants XP and triggers level-ups until excess XP is consumed.

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


        // Increases stats for a level gain and restores HP/MP.

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


        // Calculates the XP threshold required to reach the next level.

        private int XPToLevelUp()
        {
            // Example: XP required increases per level
            return 100 + (Level - 1) * 50;
        }


        // Adds positive gold amounts to the player's wallet.

        public void AddGold(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            Gold += amount;
        }


        // Attempts to spend gold; returns true if the transaction succeeds.

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


        // Equips a weapon, updating strength by removing old bonuses and applying new ones.

        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                Strength -= EquippedWeapon.AttackBonus;
            }

            EquippedWeapon = weapon;
            Strength += weapon.AttackBonus;
        }


        // Consumes MP when casting abilities; floors at zero.

        public void ConsumeMana(int amount)
        {
            MP -= amount;
            if (MP < 0)
            {
                MP = 0;
            }
        }


        // Restores MP by the given amount, capped at MaxMP.

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


        // Permanently increases strength by the provided amount.

        public void IncreaseStrength(int amount)
        {
            Strength += amount;
        }


        // Permanently increases defense by the provided amount.

        public void IncreaseDefense(int amount)
        {
            Defense += amount;
        }


        // Performs a basic attack against an enemy, including weapon bonus.

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

