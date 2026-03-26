namespace ConsoleRPG.Interfaces
{
    /// <summary>
    /// Basic combatant contract with health and damage handling.
    /// </summary>
    public interface IEntity
    {
        int HP { get; set; }
        int MaxHP { get; }
        /// <summary>
        /// Reduces HP by the specified amount after any implementor logic.
        /// </summary>
        void TakeDamage(int damage);
    }
}
