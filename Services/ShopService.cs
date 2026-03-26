using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    /// <summary>
    /// Manages shop inventory and purchase transactions.
    /// </summary>
    public class ShopService
    {
        private readonly List<IItem> _stock;

        /// <summary>
        /// Loads all items from data files into initial shop stock.
        /// </summary>
        public ShopService()
        {
            _stock = ItemDataService.LoadAllItems();
        }

        public IReadOnlyList<IItem> Stock => _stock;

        /// <summary>
        /// Attempts to sell an item at the given stock index to the player.
        /// </summary>
        public bool TryBuy(Player player, int stockIndex)
        {
            if (stockIndex < 0 || stockIndex >= _stock.Count)
            {
                return false;
            }

            var item = _stock[stockIndex];
            if (!player.TrySpendGold(item.Value))
            {
                return false;
            }

            player.Inventory.Add(item);
            _stock.RemoveAt(stockIndex);
            return true;
        }
    }
}

