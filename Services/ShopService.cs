using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    // Manages shop inventory and purchase transactions.
    public class ShopService
    {
        private readonly List<IItem> _stock;


        // Loads all items from data files into initial shop stock.
        public ShopService()
        {
            _stock = ItemDataService.LoadAllItems();
        }

        public IReadOnlyList<IItem> Stock => _stock;


        // Attempts to sell an item at the given stock index to the player.
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

