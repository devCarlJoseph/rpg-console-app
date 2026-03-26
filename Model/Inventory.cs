using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    /// <summary>
    /// Holds the player's item collection and basic item interactions.
    /// </summary>
    public class Inventory
    {
        private readonly List<IItem> _items = new();

        public IReadOnlyList<IItem> Items => _items;

        /// <summary>
        /// Adds an item instance to the inventory.
        /// </summary>
        public void Add(IItem item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Removes a specific item instance if present.
        /// </summary>
        public bool Remove(IItem item)
        {
            return _items.Remove(item);
        }

        /// <summary>
        /// Uses the item at the provided index and applies its effect to the player.
        /// </summary>
        public bool UseItem(int index, Player player)
        {
            if (index < 0 || index >= _items.Count)
            {
                return false;
            }

            var item = _items[index];
            item.Use(player);
            return true;
        }
    }
}

