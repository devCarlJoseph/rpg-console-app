namespace ConsoleRPG.Interfaces
{
    /// <summary>
    /// Represents an ability that can be executed against a target entity.
    /// </summary>
    public interface ISkill
    {
        string Name { get; }
        int ManaCost { get; }
        /// <summary>
        /// Performs the skill's effect on the target.
        /// </summary>
        void Execute(IEntity target);
    }
}
