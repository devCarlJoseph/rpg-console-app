using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    public class Inventory
    {
        private readonly List<IItem> _items = new();

        public IReadOnlyList<IItem> Items => _items;

        public void Add(IItem item)
        {
            _items.Add(item);
        }

        public bool Remove(IItem item)
        {
            return _items.Remove(item);
        }

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

