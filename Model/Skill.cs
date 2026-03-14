namespace ConsoleRPG.Model
{
    // GUIDE: The Skill class models special abilities (Spells, Attacks, Passives).
    //
    // Apply Abstraction & Inheritance:
    // A base `Skill` class with derived classes for `ActiveSkill` and `PassiveSkill`.
    // Furthermore, ActiveSkills might be separated into `AttackSkill` and `HealSkill`.
    //
    // Example Structure:
    //
    // public abstract class Skill
    // {
    //     public string Name { get; private set; }
    //     public string Description { get; private set; }
    //     public int RequiredLevel { get; private set; }
    //
    //     protected Skill(string name, string description, int requiredLevel)
    //     {
    //         Name = name;
    //         Description = description;
    //         RequiredLevel = requiredLevel;
    //     }
    // }
    //
    // public abstract class ActiveSkill : Skill
    // {
    //     public int ManaCost { get; private set; }
    //     public int CooldownTurns { get; private set; }
    //
    //     protected ActiveSkill(string name, string description, int requiredLevel, int manaCost, int cooldown)
    //         : base(name, description, requiredLevel)
    //     {
    //         ManaCost = manaCost;
    //         CooldownTurns = cooldown;
    //     }
    //
    //     // Polymorphism: The `Execute` method takes the caster (Player) and target (Enemy).
    //     // public abstract void Execute(Player caster, Enemy target);
    // }
    //
    // public class DamageSkill : ActiveSkill
    // {
    //     public int BaseDamage { get; private set; }
    //
    //     // Constructor...
    //
    //     // public override void Execute(Player caster, Enemy target)
    //     // {
    //     //     if (caster.MP >= ManaCost)
    //     //     {
    //     //         caster.ConsumeMana(ManaCost);
    //     //         int totalDamage = BaseDamage + caster.Intelligence; 
    //     //         target.TakeDamage(totalDamage);
    //     //     }
    //     // }
    // }
}
