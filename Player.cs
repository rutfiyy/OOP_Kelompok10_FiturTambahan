namespace OOP_Kelompok2
{
    public class Player
    {
        public string? Name { get; set; }
        public int MaxHeart { get; set; }
        public int Heart { get; set; }
        public int Juice { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int Luck { get; set; }
        public int HitRate { get; set; }
        public int Level { get; set; } = 1;
        public int Exp { get; set; } = 0;
        public Emotion EmotionType { get; set; } = Emotion.Neutral;

        public void GainExp(int amount)
        {
            Exp += amount;
            Console.WriteLine($"{Name} gained {amount} EXP. Total EXP: {Exp}");
            if (Exp >= Level * 10) // Threshold level-up
            {
                Console.WriteLine("\n=== Level Up ===");
                LevelUp();
            }
        }

        private void LevelUp()
        {
            while (Exp >= Level * 10)
            {
                Exp -= Level * 10;
                Level++;
                Heart += 10;
                Juice += 5;
                Attack += 2;
                Defense += 2;
                
                Console.WriteLine($"{Name} leveled up! Now at Level {Level} with enhanced attributes.");
            }
            
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Health: {Heart}");
            Console.WriteLine($"Juice: {Juice}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Speed: {Speed}");
            Console.WriteLine($"Luck: {Luck}");
            Console.WriteLine($"Emotion: {EmotionType}");
        }
    }
}
