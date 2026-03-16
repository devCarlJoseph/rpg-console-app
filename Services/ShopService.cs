using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    public class ShopService
    {
        private readonly List<IItem> _stock = new()
        {
            new SmallHealthPotion(),
            new SmallHealthPotion(),
        };

        public IReadOnlyList<IItem> Stock => _stock;

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

        private sealed class SmallHealthPotion : IItem
        {
            public string Name => "Small Health Potion";
            public int Value => 15;

            public void Use(Player player)
            {
                player.Heal(25);
            }
        }
    }
}

