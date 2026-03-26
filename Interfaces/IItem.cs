using ConsoleRPG.Model;

namespace ConsoleRPG.Interfaces
{
    /// <summary>
    /// Represents an item that can be stored and used by a player.
    /// </summary>
    public interface IItem
    {
        string Name { get; }
        int Value { get; }
        /// <summary>
        /// Applies the item's effect to the provided player.
        /// </summary>
        void Use(Player player);
    }
}
