namespace ConsoleRPG.Interfaces
{
    // Represents an ability that can be executed against a target entity.
    public interface ISkill
    {
        string Name { get; }
        int ManaCost { get; }

        // Performs the skill's effect on the target.
        void Execute(IEntity target);
    }
}
