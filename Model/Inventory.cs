using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    // Holds the player's item collection and basic item interactions.
    public class Inventory
    {
        private readonly List<IItem> _items = new();

        public IReadOnlyList<IItem> Items => _items;


        // Adds an item instance to the inventory.

        public void Add(IItem item)
        {
            _items.Add(item);
        }


        // Removes a specific item instance if present.

        public bool Remove(IItem item)
        {
            return _items.Remove(item);
        }


        // Uses the item at the provided index and applies its effect to the player.

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

