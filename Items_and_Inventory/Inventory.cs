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
