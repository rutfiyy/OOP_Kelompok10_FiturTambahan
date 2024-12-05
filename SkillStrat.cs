using System;

namespace OOP_Kelompok2
{
    // Skill Strategy Interface
    public interface ISkillStrategy
    {
        void Description(); // Describes the skill
        void Execute(Player player, Enemy enemy); // Executes the skill
        void Message(Player player, Enemy enemy); // Displays skill message
        int GetCost(); // Method to get the cost of the skill
    }

    // DoubleSlash Skill
    public class DoubleSlash : ISkillStrategy
    {
        private const int Cost = 30;
        private const string Name = "Double Slash";

        public void Description()
        {
            Console.WriteLine($"{Name}: Attack the enemy twice.");
        }

        public void Execute(Player player, Enemy enemy)
        {
            if (player.Juice < Cost)
            {
                Console.WriteLine("Not enough juice to perform this skill!");
                return; // Skill execution is not performed if not enough juice
            }

            int damage = player.Attack * 2;
            player.Juice -= Cost;
            enemy.Heart = Math.Max(enemy.Heart - damage, 0);
        }

        public void Message(Player player, Enemy enemy)
        {
            Console.WriteLine($"{player.Name.ToUpper()} uses {Name}!");
            Console.WriteLine($"{enemy.Name.ToUpper()} takes {player.Attack * 2} damage!");
        }

        public int GetCost() => Cost; // Implement GetCost() to return the cost of the skill
    }

    // Heal Skill
    public class Heal : ISkillStrategy
    {
        private const int Cost = 20;
        private const string Name = "Heal";

        public void Description()
        {
            Console.WriteLine($"{Name}: Restore 30% of your max heart.");
        }

        public void Execute(Player player, Enemy enemy)
        {
            if (player.Juice < Cost)
            {
                Console.WriteLine("Not enough juice to perform this skill!");
                return; // Skill execution is not performed if not enough juice
            }

            int healAmount = (int)(player.MaxHeart * 0.30);
            player.Juice -= Cost;
            player.Heart = Math.Min(player.Heart + healAmount, player.MaxHeart);
        }

        public void Message(Player player, Enemy enemy)
        {
            Console.WriteLine($"{player.Name.ToUpper()} uses {Name}!");
            Console.WriteLine($"{player.Name.ToUpper()} restores 30% of their max heart!");
        }

        public int GetCost() => Cost; // Implement GetCost() to return the cost of the skill
    }

    // Annoy Skill
    public class Annoy : ISkillStrategy
    {
        private const int Cost = 30;
        private const string Name = "Annoy";

        public void Description()
        {
            Console.WriteLine($"{Name}: Turn the enemy's emotion to ANGRY.");
        }

        public void Execute(Player player, Enemy enemy)
        {
            if (player.Juice < Cost)
            {
                Console.WriteLine("Not enough juice to perform this skill!");
                return; // Skill execution is not performed if not enough juice
            }

            player.Juice -= Cost;
            enemy.EmotionType = Emotion.Angry;
            enemy.PerformEmotionEffect();
        }

        public void Message(Player player, Enemy enemy)
        {
            Console.WriteLine($"{player.Name.ToUpper()} uses {Name}!");
            Console.WriteLine($"{enemy.Name.ToUpper()} is now ANGRY!");
        }

        public int GetCost() => Cost; // Implement GetCost() to return the cost of the skill
    }

    // Calm Skill
    public class Calm : ISkillStrategy
    {
        private const int Cost = 15;
        private const string Name = "Calm";

        public void Description()
        {
            Console.WriteLine($"{Name}: Turn the player's emotion to NEUTRAL.");
        }

        public void Execute(Player player, Enemy enemy)
        {
            if (player.Juice < Cost)
            {
                Console.WriteLine("Not enough juice to perform this skill!");
                return; // Skill execution is not performed if not enough juice
            }

            player.Juice -= Cost;
            player.EmotionType = Emotion.Neutral;
        }

        public void Message(Player player, Enemy enemy)
        {
            Console.WriteLine($"{player.Name.ToUpper()} uses {Name}!");
            Console.WriteLine($"{player.Name.ToUpper()} calms down and is now NEUTRAL.");
        }

        public int GetCost() => Cost; // Implement GetCost() to return the cost of the skill
    }
}
