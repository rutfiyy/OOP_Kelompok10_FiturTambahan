using System;
using System.Collections.Generic;

namespace OOP_Kelompok2
{
    public interface IItem
    {
        string Name { get; }
        string Description { get; }
        void Use(Player player);
    }

    public class Inventory
    {
        private List<IItem> _items = new List<IItem>();

        // Properti untuk mendapatkan jumlah item
        public int Count => _items.Count;

        public void AddItem(IItem item)
        {
            _items.Add(item);
            Console.WriteLine($"{item.Name} added to inventory.");
        }

        public void UseItem(int index, Player player)
        {
            if (index < 0 || index >= _items.Count)
            {
                Console.WriteLine("Invalid item selection.");
                return;
            }

            IItem item = _items[index];
            item.Use(player);
            _items.RemoveAt(index);
            Console.WriteLine($"{item.Name} removed from inventory after use.");
        }

        public string[] GetInventoryItems()
        {
            List<string> itemDescriptions = new List<string>();
            for (int i = 0; i < _items.Count; i++)
            {
                itemDescriptions.Add($"{_items[i].Name} - {_items[i].Description}");
            }
            itemDescriptions.Add("Exit");
            return itemDescriptions.ToArray();
        }

        public void DisplayInventory()
        {
            Console.WriteLine("\n=== Inventory ===");
            if (_items.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }

            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_items[i].Name} - {_items[i].Description}");
            }
        }
    }

    public class Potion : IItem
    {
        public string Name => "Potion";
        public string Description => "Restores 50 Heart.";

        public void Use(Player player)
        {
            player.Heart = Math.Min(player.Heart + 50, player.MaxHeart);
            Console.WriteLine($"{Name} used. {player.Name}'s Health restored by 50.");
        }
    }

    public class BuffPotion : IItem
    {
        public string Name => "Buff Potion";
        public string Description => "Increases Attack by 10 but reduces Max Health by 20.";

        public void Use(Player player)
        {
            player.Attack += 10;
            player.MaxHeart = Math.Max(player.MaxHeart - 20, 1);
            player.Heart = Math.Min(player.Heart, player.MaxHeart); // Ensure current health does not exceed max health
            Console.WriteLine($"{Name} used. {player.Name}'s Attack increased by 10 and Max Health reduced by 20.");
        }
    }

    public class HealthBuffPotion : IItem
    {
        public string Name => "Health Buff Potion";
        public string Description => "Increases Max Health by 30 but reduces Attack by 5.";

        public void Use(Player player)
        {
            player.MaxHeart += 30;
            player.Attack = Math.Max(player.Attack - 5, 0);
            Console.WriteLine($"{Name} used. {player.Name}'s Max Health increased by 30 and Attack reduced by 5.");
        }
    }

    public class HappyCandy : IItem
    {
        public string Name => "Happy Candy";
        public string Description => "Makes you happy. Increases Defense but reduces Accuracy.";

        public void Use(Player player)
        {
            player.Defense += 10;
            player.HitRate -= 5;
            Console.WriteLine($"{Name} used. {player.Name}'s Defense increased, but Accuracy decreased.");
        }
    }

    public class SadCandy : IItem
    {
        public string Name => "Sad Candy";
        public string Description => "Makes you sad. Reduces Defense but increases Attack.";

        public void Use(Player player)
        {
            player.Defense -= 5;
            player.Attack += 10;
            Console.WriteLine($"{Name} used. {player.Name}'s Defense decreased, but Attack increased.");
        }
    }

    public class AngryCandy : IItem
    {
        public string Name => "Angry Candy";
        public string Description => "Makes you angry. Increases Attack but reduces Defense.";

        public void Use(Player player)
        {
            player.Attack += 10;
            player.Defense -= 5;
            Console.WriteLine($"{Name} used. {player.Name}'s Attack increased, but Defense decreased.");
        }
    }

    public class ItemDecorator : IItem
    {
        protected IItem _baseItem;

        public ItemDecorator(IItem baseItem)
        {
            _baseItem = baseItem;
        }

        public virtual string Name => _baseItem.Name;
        public virtual string Description => _baseItem.Description;

        public virtual void Use(Player player)
        {
            _baseItem.Use(player);
        }
    }

    public class AttackBoostDecorator : ItemDecorator
    {
        public AttackBoostDecorator(IItem baseItem) : base(baseItem) { }

        public override string Description => _baseItem.Description + " Also boosts Attack temporarily.";

        public override void Use(Player player)
        {
            base.Use(player);
            player.Attack += 10;
            Console.WriteLine($"Attack boosted temporarily by 10.");
        }
    }

    public class EmotionChangerDecorator : ItemDecorator
    {
        private Emotion _emotion;

        public EmotionChangerDecorator(IItem baseItem, string emotion) : base(baseItem)
        {
            switch(emotion.ToLower())
            {
                case "angry":
                    _emotion = Emotion.Angry;
                    break;
                case "sad":
                    _emotion = Emotion.Sad;
                    break;
                case "happy":
                    _emotion = Emotion.Happy;
                    break;
                default:
                    _emotion = Emotion.Neutral;
                    break;
            }
            
        }

        public override string Description => _baseItem.Description + $" Changes player's emotion to {_emotion}.";

        public override void Use(Player player)
        {
            base.Use(player);
            player.EmotionType = _emotion;
            Console.WriteLine($"Player's emotion changed to {_emotion}.");
        }
    }
}
