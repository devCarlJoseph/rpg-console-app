using ConsoleRPG.Model;

namespace ConsoleRPG.Interfaces
{
    public interface IItem
    {
        string Name { get; }
        int Value { get; }
        void Use(Player player){}
    }
}