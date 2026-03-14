namespace ConsoleRPG
{
    public interface IItem
    {
        string Name { get; }
        int Value { get; }
        void Use(Player player){}
    }
}