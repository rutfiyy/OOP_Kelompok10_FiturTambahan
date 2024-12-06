using System;
using System.Collections.Generic;

namespace OOP_Kelompok2
{
    public class StatTradeTreasure
    {
        private List<IItem> _treasureForGet;

        public StatTradeTreasure()
        {
            _treasureForGet = new List<IItem>
            {
                new BuffPotion(),
                new HealthBuffPotion(),
                new EmotionChangerDecorator(new HappyCandy(), "Happy"),
                new EmotionChangerDecorator(new SadCandy(), "Sad"),
                new EmotionChangerDecorator(new AngryCandy(), "Angry")
            };
        }

        public string[] GetTreasure()
        {
            List<string> treasureDescriptions = new List<string>();
            for (int i = 0; i < _treasureForGet.Count; i++)
            {
                treasureDescriptions.Add($"{_treasureForGet[i].Name} - {_treasureForGet[i].Description}");
            }
            treasureDescriptions.Add("Exit");
            return treasureDescriptions.ToArray();
        }

        public void DisplayItems()
        {
            Console.WriteLine("\n=== Stat Trade Shop ===");
            for (int i = 0; i < _treasureForGet.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_treasureForGet[i].Name} - {_treasureForGet[i].Description}");
            }
        }

        public void BuyItem(int index, Player player, Inventory inventory)
        {
            if (index < 0 || index >= _treasureForGet.Count)
            {
                Console.WriteLine("Invalid item selection.");
                return;
            }

            IItem treasure = _treasureForGet[index];
            inventory.AddItem(treasure);
            _treasureForGet.Remove(treasure);
            Console.WriteLine($"{treasure.Name} bought and added to inventory.");
        }

        public int TreasuresCount => _treasureForGet.Count;
    }
}