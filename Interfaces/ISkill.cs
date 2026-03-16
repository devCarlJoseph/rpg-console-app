namespace ConsoleRPG.Interfaces
{
    public interface ISkill
    {
        string Name { get; }
        int ManaCost { get; }
        void Execute(IEntity target);
    }
}