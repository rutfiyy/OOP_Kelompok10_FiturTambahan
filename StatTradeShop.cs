using System;
using System.Collections.Generic;

namespace OOP_Kelompok2
{
    public class StatTradeShop
    {
        private List<IItem> _itemsForSale;

        public StatTradeShop()
        {
            _itemsForSale = new List<IItem>
            {
                new BuffPotion(),
                new HealthBuffPotion()
            };
        }

        public void DisplayItems()
        {
            Console.WriteLine("\n=== Stat Trade Shop ===");
            for (int i = 0; i < _itemsForSale.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_itemsForSale[i].Name} - {_itemsForSale[i].Description}");
            }
        }

        public void BuyItem(int index, Player player, Inventory inventory)
        {
            if (index < 0 || index >= _itemsForSale.Count)
            {
                Console.WriteLine("Invalid item selection.");
                return;
            }

            IItem item = _itemsForSale[index];
            inventory.AddItem(item);
            Console.WriteLine($"{item.Name} bought and added to inventory.");
        }

        public int ItemsCount => _itemsForSale.Count;
    }
}