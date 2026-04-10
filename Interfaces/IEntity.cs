namespace ConsoleRPG.Interfaces
{
    // Basic combatant contract with health and damage handling.
    public interface IEntity
    {
        int HP { get; set; }
        int MaxHP { get; }
        // Reduces HP by the specified amount after any implementor logic.
        void TakeDamage(int damage);
    }
}
