namespace ConsoleRPG
{
    public interface ISkill
    {
        string Name { get; }
        int ManaCost { get; }
        void Execute(IEntity target){}
    }
}