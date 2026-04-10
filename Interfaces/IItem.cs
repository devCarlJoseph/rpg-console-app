using ConsoleRPG.Model;

namespace ConsoleRPG.Interfaces
{
    // Represents an item that can be stored and used by a player.
    public interface IItem
    {
        string Name { get; }
        int Value { get; }
        // Applies the item's effect to the provided player.
        void Use(Player player);
    }
}
