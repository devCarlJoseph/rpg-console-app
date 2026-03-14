using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    // GUIDE: The Player class represents the main character.
    // Apply Encapsulation: keep setters private where possible (e.g., Level, XP)
    // and only modify them through specific methods like GainXP() or LevelUp().
    public class Player : IEntity
    {
        // --- Basic Attributes ---
        //public string Name { get; set; } // Player's name
        //public int Level { get; private set; } = 1; // Player level starts at 1
        //public int XP { get; private set; } = 0; // Experience points
        
        // --- Core Stats ---
        //public int MaxHP { get; private set; } = 100; // Maximum HP
        //public int HP { get; set; } // Current HP (needs public setter for IEntity)
        //public int MaxMP { get; private set; } = 50; // Maximum Mana
        //public int MP { get; private set; } // Current Mana
        //public int Strength { get; private set; } = 10; // Physical attack power
        //public int Agility { get; private set; } = 10; // Speed / Evasion
        //public int Intelligence { get; private set; } = 10; // Magic power
        //public int Defense { get; private set; } = 5; // Damage reduction

        // --- Collections & Equipment ---
        //public List<Item> Inventory { get; private set; } = new List<Item>();
        //public List<Skill> ActiveSkills { get; private set; } = new List<Skill>();
        //public List<Skill> PassiveSkills { get; private set; } = new List<Skill>();
        //public List<Quest> ActiveQuests { get; private set; } = new List<Quest>();
        //public Weapon EquippedWeapon { get; private set; }
        //public Armor EquippedArmor { get; private set; }
        //public Accessory EquippedAccessory { get; private set; }
        //public List<Quest> CompletedQuests { get; private set; } = new List<Quest>();

        // --- Constructor
        //public Player(string name)
        //{
        //    Name = name;
        //    HP = MaxHP;
        //    MP = MaxMP;
        //}

        // --- Methods (Behaviors) ---
        // Reduce HP after considering Damage
        //public void TakeDamage(int damage)
        //{
        //    int damageTaken = damage - Defense;
        //    if (damageTaken < 0)
        //    {
        //        damageTaken = 0;
        //    }
        //    HP -= damageTaken;
            //if (HP < 0)
            //{
            //    HP = 0;
            //}
        //}

        // Heals the player by a specified amount, without exceeding MaxHP
        //public void Heal(int amount)
        //{
        //    HP += amount;
        //    if (HP > MaxHP)
        //    {
        //        HP = MaxHP;
        //    }
        //}

        //public void GainXP(int amount)
        //{
        //    XP += amount;
           // while (XP >= XPToLevelUp())
           // {
           //     XP -= XPToLevelUp();
           //     LevelUp();
           // }
        //}

        // Handles leveling up: increases stats
        //private void LevelUp()
        //{
        //    Level++;
        //    MaxHP += 20;
        //    MaxMP += 10;
        //    Strength += 2;
        //    Agility += 2;
        //    Intelligence += 2;
        //    Defense += 1;

            // Restore HP and MP on level up
        //    HP = MaxHP;
        //    MP = MaxMP;
        //}

        // Calculates required XP for next level
        //private int XPToLevelUp()
        //{
            // Example: XP required increases per level
            //return 100 + (Level - 1) * 50;
        //}

            // Equip methods (example for weapon)
        //public void EquipWeapon(Weapon weapon)
        //{
        //    EquippedWeapon = weapon;
        //    Strength += weapon.AttackBonus;
        //}
    }
}

